//------------------------------------------------------------------------------
// <copyright file="IApiClientOptions.cs"
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

namespace Cadru.ApiClient.Configuration
{
    /// <summary>
    /// Represents common API configuration parameters.
    /// </summary>
    public interface IApiClientOptions
    {
        /// <summary>
        /// Gets the base URL for the service, ensuring that it has a trailing '/' character.
        /// </summary>
        /// <returns>The base URL for the service with a trailing '/' character.</returns>
        Uri GetBaseUrl();

        /// <summary>
        /// The base URL for the service.
        /// </summary>
        [Required]
        string? BaseUrl { get; set; }

        /// <summary>
        /// The headers which should be sent with each request.
        /// </summary>
        IDictionary<string, string> DefaultRequestHeaders { get; }
    }
}