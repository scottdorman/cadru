//------------------------------------------------------------------------------
// <copyright file="ValidationHostedService.cs"
//  company="Scott Dorman"
//  library="Cadru">
//    Copyright (C) 2001-2021 Scott Dorman.
// </copyright>
//
// <license>
//    Licensed under the Microsoft Public License (Ms-PL) (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//    http://opensource.org/licenses/Ms-PL.html
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
// </license>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Cadru.ApiClient.Configuration.DependencyInjection
{
#if !NET6_0
    internal class ValidatorOptions
    {
        // Maps each options type to a method that forces its evaluation, e.g. IOptionsMonitor<TOptions>.Get(name)
        public IDictionary<Type, Action> Validators { get; } = new Dictionary<Type, Action>();
    }

    internal class ValidationHostedService : IHostedService
    {
        private readonly IDictionary<Type, Action> _validators;

        public ValidationHostedService(IOptions<ValidatorOptions> validatorOptions)
        {
            this._validators = validatorOptions?.Value?.Validators ?? throw new ArgumentNullException(nameof(validatorOptions));
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var exceptions = new List<Exception>();

            foreach (var validate in this._validators.Values)
            {
                try
                {
                    // Execute the validation method and catch the validation error
                    validate();
                }
                catch (OptionsValidationException ex)
                {
                    exceptions.Add(ex);
                }
            }

            if (exceptions.Count == 1)
            {
                // Rethrow if it's a single error
                ExceptionDispatchInfo.Capture(exceptions[0]).Throw();
            }

            if (exceptions.Count > 1)
            {
                // Aggregate if we have many errors
                throw new AggregateException(exceptions);
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
#endif
}
