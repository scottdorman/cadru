//------------------------------------------------------------------------------
// <copyright file="ApiClientOptions.cs"
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

using Cadru.Extensions;

namespace Cadru.ApiClient.Configuration
{
    /// <summary>
    /// Represents common API configuration parameters.
    /// </summary>
    public abstract class ApiClientOptions : IApiClientOptions
    {
        /// <inheritdoc/>
        public Uri GetBaseUrl()
        {
            if (!String.IsNullOrWhiteSpace(this.BaseUrl))
            {
                return new Uri(this.BaseUrl!.EnsureTrailingCharacter('/'));
            }

            throw new InvalidOperationException();
        }

        /// <inheritdoc/>
        [Required]
        public string? BaseUrl { get; set; }

        /// <inheritdoc/>
        public IDictionary<string, string> DefaultRequestHeaders { get; } = new Dictionary<string, string>();
    }
}
