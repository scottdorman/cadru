//------------------------------------------------------------------------------
// <copyright file="ApiResult.cs"
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

using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text.Json.Serialization;

using Cadru.ApiClient.Serialization;
using Cadru.ApiClient.Services;

namespace Cadru.ApiClient.Models
{
    /// <summary>
    /// Represents the response of an endpoint call.
    /// </summary>
    /// <typeparam name="TData">The type of payload model.</typeparam>
    [JsonConverter(typeof(ApiResultConverterFactory))]
    public class ApiResult<TData> : IApiResult<TData> where TData : class
    {
        /// <inheritdoc/>
        [MemberNotNullWhen(false, nameof(Data))]
        [MemberNotNullWhen(true, nameof(Error))]
        public virtual bool IsError => this.Error != null;

        /// <inheritdoc/>
        [JsonPropertyName(JsonSerializationPropertyNames.Error)]
        [JsonInclude]
        public IApiError? Error { get; internal set; } = null;

        /// <inheritdoc/>
        [JsonPropertyName(JsonSerializationPropertyNames.Data)]
        [JsonInclude]
        public TData? Data { get; internal set; }

        /// <inheritdoc/>
        CookieCollection? IApiResult<TData>.Cookies { get; set; }
    }
}
