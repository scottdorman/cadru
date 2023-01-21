//------------------------------------------------------------------------------
// <copyright file="ApiResult.static.cs"
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
using Cadru.ApiClient.Resources;
using Cadru.ApiClient.Serialization;

namespace Cadru.ApiClient.Models
{
    /// <summary>
    /// Represents the response of an endpoint call.
    /// </summary>
    public abstract partial class ApiResult
    {
        /// <summary>
        /// Creates an <see cref="ApiResult"/> with the
        /// specified <see cref="ApiError"/> information.
        /// </summary>
        /// <typeparam name="TData">The type of payload model.</typeparam>
        /// <param name="apiError">An <see cref="ApiError"/> instance which
        /// represents the error.</param>
        /// <returns>A new <see cref="IApiResult{TData}"/> representing the response.</returns>
        public static ApiResult<TData> ErrorResult<TData>(IApiError apiError) where TData : class
        {
            return new ApiResult<TData>
            {
                Error = apiError
            };
        }

        /// <summary>
        /// Creates an <see cref="ApiResult"/> with the
        /// <see cref="ApiError"/> representing the given exception.
        /// </summary>
        /// <typeparam name="TData">The type of payload model.</typeparam>
        /// <param name="e">The exception to convert.</param>
        /// <param name="errorId">The unique identifier for the error.</param>
        /// <param name="httpStatusCode">The HTTP status code of the error.</param>
        /// <param name="errorCode">The error code of the error.</param>
        /// <param name="description">The description of the error.</param>
        /// <param name="responseContent">The JSON document r</param>
        /// <returns>A new <see cref="IApiResult{TData}"/> representing the response.</returns>
        public static ApiResult<TData> ErrorResult<TData>(Exception e, Guid? errorId = null, string? errorCode = null, HttpStatusCode? httpStatusCode = null, string? description = null, JsonDocument? responseContent = null) where TData : class
        {
            return new ApiResult<TData>
            {
                Error = e.ToApiError(errorId, errorCode, httpStatusCode, description, responseContent)
            };
        }

        /// <summary>
        ///  Parses the text representing a single JSON value into an instance of the type
        ///  specified by <typeparamref name="TData"/>.
        /// </summary>
        /// <typeparam name="TData">The type of payload model.</typeparam>
        /// <param name="json">The JSON text to parse.</param>
        /// <returns>A <see cref="IApiResult{TData}"/> representation of the JSON value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="json"/> is <see langword="null"/>.</exception>
        /// <exception cref="JsonException">
        /// <para>The JSON is invalid.</para>
        /// <para>-or-</para>
        /// <para><typeparamref name="TData"/> is not compatible with the JSON.</para>
        /// <para>-or-</para>
        /// <para>There is remaining data in the string beyond a single JSON value.</para>
        /// </exception>
        /// <exception cref="NotSupportedException">There is no compatible
        /// <see cref="System.Text.Json.Serialization.JsonConverter"/> for
        /// <typeparamref name="TData"/> or its serializable members.</exception>
        public static IApiResult<TData> From<TData>(string json) where TData : class
            => From<TData>(json, DefaultJsonSerializerOptions.Create());

        /// <summary>
        ///  Parses the text representing a single JSON value into an instance of the type
        ///  specified by <typeparamref name="TData"/>.
        /// </summary>
        /// <typeparam name="TData">The type of payload model.</typeparam>
        /// <param name="json">The JSON text to parse.</param>
        /// <param name="options">Options to control the behavior during parsing.</param>
        /// <returns>A <see cref="IApiResult{TData}"/> representation of the JSON value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="json"/> is <see langword="null"/>.</exception>
        /// <exception cref="JsonException">
        /// <para>The JSON is invalid.</para>
        /// <para>-or-</para>
        /// <para><typeparamref name="TData"/> is not compatible with the JSON.</para>
        /// <para>-or-</para>
        /// <para>There is remaining data in the string beyond a single JSON value.</para>
        /// </exception>
        /// <exception cref="NotSupportedException">There is no compatible
        /// <see cref="System.Text.Json.Serialization.JsonConverter"/> for
        /// <typeparamref name="TData"/> or its serializable members.</exception>
        public static IApiResult<TData> From<TData>(string json, JsonSerializerOptions options) where TData : class
            => FromObject(JsonSerializer.Deserialize<TData>(json, options)) ?? ApiResult.ErrorResult<TData>(ApiError.Empty);

        /// <summary>
        /// Creates a new <see cref="IApiResult{TData}"/> from the given HTTP content.
        /// </summary>
        /// <typeparam name="TData">The type of payload model.</typeparam>
        /// <param name="content">The content to parse.</param>
        /// <returns>A new <see cref="IApiResult{TData}"/> representing the response.</returns>
        public static async Task<IApiResult<TData>> FromAsync<TData>(HttpContent content)
            where TData : class
            => From<TData>(await content.ReadAsStringAsync());

        /// <summary>
        /// Creates a new <see cref="IApiResult{TData}"/> from the given HTTP content.
        /// </summary>
        /// <typeparam name="TData">The type of payload model.</typeparam>
        /// <param name="content">The content to parse.</param>
        /// <param name="options">Options to control the behavior during parsing.</param>
        /// <returns>A new <see cref="IApiResult{TData}"/> representing the response.</returns>
        public static async Task<IApiResult<TData>> FromAsync<TData>(HttpContent content, JsonSerializerOptions options)
            where TData : class
            => From<TData>(await content.ReadAsStringAsync(), options);

        /// <summary>
        /// Creates a new <see cref="IApiResult{TData}"/> from the given object instance.
        /// </summary>
        /// <typeparam name="TData">The type of payload model.</typeparam>
        /// <param name="value">The object instance.</param>
        /// <returns>A new <see cref="IApiResult{TData}"/> representing the response.</returns>
        public static IApiResult<TData> FromObject<TData>(TData? value) where TData : class
        {
            return new ApiResult<TData>
            {
                Data = value
            };
        }
    }
}
