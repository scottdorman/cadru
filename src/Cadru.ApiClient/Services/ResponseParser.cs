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

namespace Cadru.ApiClient.Services
{
    public abstract class ResponseParser : IResponseParser
    {
        /// <summary>
        /// The type used to perform logging.
        /// </summary>
        protected readonly ILogger _logger;

        private readonly JsonSerializerOptions _serializerOptions;

        /// <summary>
        /// Creates a new instance of the <see
        /// cref="ResponseParser"/> class.
        /// </summary>
        /// <param name="logger">The type used to perform logging.</param>
        public ResponseParser(ILogger? logger = null, JsonSerializerOptions? serializerOptions = null)
        {
            this._logger = logger ?? NullLogger.Instance;
            this._serializerOptions = serializerOptions ?? DefaultJsonSerializerOptions.Create();
        }

        /// <summary>
        /// Provides options to be used with JSON serialization.
        /// </summary>
        /// <remarks>
        /// <para>
        /// When overriding in a derived class, don't create a new
        /// <see cref="JsonSerializerOptions"/> instance each time
        /// this property is accessed by declaring it as
        /// <code>
        /// public override JsonSerializerOptions SerializerOptions => new JsonSerializerOptions();
        /// </code>
        /// </para>
        /// <para>
        /// Instead, reuse the same instance by creating it as a static private variable and
        /// returning that instance as the property value
        /// <code>
        /// private static readonly JsonSerializerOptions _serializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        /// public override JsonSerializerOptions SerializerOptions => _serializerOptions;
        /// </code>
        /// </para>
        /// <para>
        /// It's safe to use the same instance across multiple threads. The
        /// metadata caches on the options instance are thread-safe, and the
        /// instance is immutable after the first serialization or
        /// deserialization.
        /// </para>
        /// </remarks>
        public virtual JsonSerializerOptions SerializerOptions => _serializerOptions;

        public async Task<IApiResult<TData>> ParseAsync<TData>(HttpResponseMessage response)
            where TData : class
            => await this.ParseAsync<TData>(response, r => r.IsSuccessStatusCode);

        /// <inheritdoc/>
        public async Task<IApiResult<TData>> ParseAsync<TData>(HttpResponseMessage response, Func<HttpResponseMessage, bool>? success) 
            where TData : class
        {
            IApiResult<TData> result;

            try
            {
                if (success != null && success(response))
                {
                    result = await this.ParseCoreAsync<TData>(response);
                }
                else
                {
                    result = await this.ParseAsApiErrorAsync<TData>(response);
                }
            }
            catch (Exception e)
            {
                this._logger.LogError(e, Strings.Error_UnexpectedApiError);
                result = ApiResult.ErrorResult<TData>(e, description: "Unable to parse the JSON response.");
            }

            if (response.Headers.TryGetValues("Set-Cookie", out var responseCookies))
            {
                var requestUri = response.RequestMessage!.RequestUri!;

                if (result.Cookies == null)
                {
                    result.Cookies = new CookieCollection();
                }

                var container = new CookieContainer();
                foreach (var cookie in responseCookies)
                {
                    container.SetCookies(requestUri, cookie);
                }

                result.Cookies = container.GetCookies(requestUri);
            }

            return result;
        }

        /// <summary>
        /// Parses the <paramref name="response"/> into an appropriate <see cref="IApiResult{TData}"/> instance.
        /// </summary>
        /// <typeparam name="TData">The type of payload model.</typeparam>
        /// <param name="response">The <see cref="HttpResponseMessage"/>.</param>
        /// <returns>An <see cref="IApiResult{TData}"/> instance.</returns>
        protected abstract Task<IApiResult<TData>> ParseCoreAsync<TData>(HttpResponseMessage response)
            where TData : class;

        /// <summary>
        /// Creates an <see cref="ApiError"/>.
        /// </summary>
        /// <param name="responseContent">The <see cref="JsonDocument"/> representing the HTTP response body.</param>
        /// <returns>An <see cref="ApiError"/> instance.</returns>
        protected abstract Task<IApiError> CreateApiError(JsonDocument? responseContent);

        /// <summary>
        /// Parses the <paramref name="response"/> into an <see cref="IApiResult{TData}"/>
        /// instance representing an error condition.
        /// </summary>
        /// <typeparam name="TData">The type of payload model.</typeparam>
        /// <param name="response">The <see cref="HttpResponseMessage"/>.</param>
        /// <returns>An <see cref="IApiResult{TData}"/> instance.</returns>
        protected async Task<IApiResult<TData>> ParseAsApiErrorAsync<TData>(HttpResponseMessage response) where TData : class
        {
            IApiError apiError;

            try
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var data = responseContent != null ? JsonDocument.Parse(responseContent) : null;
                apiError = new ApiError(response.StatusCode, data, await this.CreateApiError(data));
            }
            catch (Exception e)
            {
                this._logger.LogError(e, Strings.Error_UnexpectedApiError);
                apiError = e.ToApiError();
            }

            return ApiResult.ErrorResult<TData>(apiError);
        }
    }
}