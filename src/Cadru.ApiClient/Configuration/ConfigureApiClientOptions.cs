//------------------------------------------------------------------------------
// <copyright file="ConfigureApiClientOptions.cs"
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

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Cadru.ApiClient.Configuration
{
    /// <summary>
    /// An implementation of <see cref="IConfigureNamedOptions{TOptions}"/> that
    /// configures an <see cref="IApiClientOptions"/>.
    /// </summary>
    /// <typeparam name="TOptions">The options type to configure.</typeparam>
    /// <remarks>This implementation is useful when implementing multiple <see cref="ApiClient.Services.ApiClient"/>
    /// that need different configurations.</remarks>
    public class ConfigureApiClientOptions<TOptions> : IConfigureNamedOptions<TOptions>
        where TOptions : class, IApiClientOptions
    {
        private readonly IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of an <see cref="ConfigureApiClientOptions{TOptions}"/>.
        /// </summary>
        /// <param name="configuration"></param>
        public ConfigureApiClientOptions(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <inheritdoc/>
        public virtual void Configure(string? name, TOptions options)
        {
            var apiConfiguration = this.configuration.GetSection(name ?? Options.DefaultName);
            apiConfiguration.Bind(options);
        }

        /// <inheritdoc/>
        public void Configure(TOptions options) => this.Configure(Options.DefaultName, options);
    }
}
