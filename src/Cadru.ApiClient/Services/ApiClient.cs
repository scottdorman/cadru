//------------------------------------------------------------------------------
// <copyright file="ApiClient.cs"
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
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;

using Cadru.ApiClient.Models;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Cadru.ApiClient.Extensions;
using System.Net.Http.Headers;
using System.Reflection;

namespace Cadru.ApiClient.Services
{
    /// <summary>
    /// Represents a strongly typed <see cref="HttpClient"/>.
    /// </summary>
    public abstract partial class ApiClient : IApiClient
    {
        /// <summary>
        /// The <see cref="HttpClient"/> used for sending HTTP
        /// requests and receiving HTTP responses from a resource identified
        /// by a URI.
        /// </summary>
        protected readonly HttpClient _httpClient;

        /// <summary>
        /// The type used to perform logging.
        /// </summary>
        protected readonly ILogger _logger;

        /// <summary>
        /// The type used to parse HTTP responses into strongly typed objects.
        /// </summary>
        protected readonly IResponseParser _responseParser;

        /// <inheritdoc/>
        AuthenticationHeaderValue? IApiClient.AuthenticationHeaderValue { get; set; }

        /// <inheritdoc/>
        CookieContainer? IApiClient.Cookies { get; } = new CookieContainer();

        /// <summary>
        /// Initializes a new instance of an <see cref="ApiClient"/>.
        /// </summary>
        /// <param name="httpClient">The <see cref="HttpClient"/> used for sending HTTP
        /// requests and receiving HTTP responses from a resource identified
        /// by a URI.</param>
        /// <param name="responseParser">The type used to parse HTTP responses into strongly typed objects.</param>
        /// <param name="logger">The type used to perform logging.</param>
        public ApiClient(HttpClient httpClient, IResponseParser responseParser, ILogger? logger = null)
        {
            this._httpClient = httpClient;
            this._logger = logger ?? NullLogger.Instance;
            this._responseParser = responseParser;
        }
    }
}
