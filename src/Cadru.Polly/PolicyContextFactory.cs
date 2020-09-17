//------------------------------------------------------------------------------
// <copyright file="PolicyContextFactory.cs"
//  company="Scott Dorman"
//  library="Cadru">
//    Copyright (C) 2001-2020 Scott Dorman.
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
using System.Threading.Tasks;

using Polly;

namespace Cadru.Polly
{
    /// <summary>
    /// Represents a set of methods for creating instances of a
    /// <see cref="Context"/> which has an <see cref="IServiceProvider"/> item
    /// automatically added.
    /// </summary>
    public class PolicyContextFactory : IPolicyContextFactory
    {
        private readonly IServiceProvider serviceProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="PolicyContextFactory"/> class.
        /// </summary>
        public PolicyContextFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        /// <inheritdoc/>
        public Context Create()
        {
            return new Context
            {
                { PolicyContextItems.Services, this.serviceProvider }
            };
        }

        /// <inheritdoc/>
        public virtual Task<Context> CreateAsync()
        {
            return Task.FromResult(this.Create());
        }
    }
}