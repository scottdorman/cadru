//------------------------------------------------------------------------------
// <copyright file="DefaultResponseParser.cs"
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
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using Cadru.ApiClient.Extensions;
using Cadru.ApiClient.Models;
using Cadru.ApiClient.Resources;
using Cadru.ApiClient.Serialization;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

namespace Cadru.ApiClient.Services
{
    /// <summary>
    /// A utility class for parsing the <see cref="HttpResponseMessage"/>
    /// returned by an endpoint call.
    /// </summary>
    public sealed class DefaultResponseParser : ResponseParser
    {
        /// <summary>
        /// Creates a new instance of the <see
        /// cref="DefaultResponseParser"/> class.
        /// </summary>
        /// <param name="logger">The type used to perform logging.</param>
        public DefaultResponseParser(ILogger? logger = null, JsonSerializerOptions? serializerOptions = null)
            : base(logger, serializerOptions)
        {
        }

        /// <summary>
        /// Parses the <paramref name="response"/> into an appropriate <see cref="IApiResult{TData}"/> instance.
        /// </summary>
        /// <typeparam name="TData">The type of payload model.</typeparam>
        /// <param name="response">The <see cref="HttpResponseMessage"/>.</param>
        /// <returns>An <see cref="IApiResult{TData}"/> instance.</returns>
        protected override async Task<IApiResult<TData>> ParseCoreAsync<TData>(HttpResponseMessage response)
             where TData : class
        {
            return await ApiResult.FromAsync<TData>(response.Content, this.SerializerOptions);
        }

        /// <summary>
        /// Creates an <see cref="ApiError"/>.
        /// </summary>
        /// <param name="responseContent">The <see cref="JsonDocument"/> representing the HTTP response body.</param>
        /// <returns>An <see cref="ApiError"/> instance.</returns>
        protected override Task<IApiError> CreateApiError(JsonDocument? responseContent)
        {
            IApiError? apiError = null;

            try
            {
                if (responseContent != null)
                {
                    apiError = responseContent.Deserialize<ApiError>(this.SerializerOptions);
                }
            }
            catch (Exception e)
            {
                apiError = e.ToApiError();
            }

            return Task.FromResult(apiError ?? ApiError.Empty);
        }
    }
}