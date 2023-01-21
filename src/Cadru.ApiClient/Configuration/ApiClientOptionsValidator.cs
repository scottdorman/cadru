//------------------------------------------------------------------------------
// <copyright file="ApiClientOptionsValidator.cs"
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
using System.ComponentModel.DataAnnotations;

using Microsoft.Extensions.Options;

namespace Cadru.ApiClient.Configuration
{

    /// <summary>
    /// An implementation of <see cref="IValidateOptions{TOptions}"/> that
    /// validates an <see cref="IApiClientOptions"/> using
    /// <see cref="Validator">System.ComponentModel.DataAnnotations.Validator</see>.
    /// </summary>
    /// <typeparam name="TOptions">The options type to validate.</typeparam>
    public class ApiClientOptionsValidator<TOptions> : IValidateOptions<TOptions>
        where TOptions: class, IApiClientOptions
    {
        /// <summary>
        /// Initializes a new instance of an <see cref="ApiClientOptionsValidator{TOptions}"/>.
        /// </summary>
        /// <param name="name">The name of the option.</param>
        public ApiClientOptionsValidator(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// The name of the options instance being validated.
        /// </summary>
        public string Name { get; }

        /// <inheritdoc/>
        public virtual ValidateOptionsResult Validate(string? name, TOptions options)
        {
            // Null name is used to configure all named options.
            if (this.Name != null && this.Name != name)
            {
                // Ignored if not validating this instance.
                return ValidateOptionsResult.Skip;
            }

            // Ensure options are provided to validate against
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            var validationResults = new List<ValidationResult>();
            if (Validator.TryValidateObject(options, new ValidationContext(options), validationResults, validateAllProperties: true))
            {
                return ValidateOptionsResult.Success;
            }

            var typeName = options.GetType().Name;
            var errors = new List<string>();
            foreach (var result in validationResults)
            {
                errors.Add($"Validation failed for '{typeName}' members '{String.Join(",", result.MemberNames)}' with the error '{result.ErrorMessage}'.");
            }

            return ValidateOptionsResult.Fail(errors);
        }
    }
}
