//------------------------------------------------------------------------------
// <copyright file="ApiError.cs"
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
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

using Cadru.ApiClient.Serialization;

namespace Cadru.ApiClient.Models
{
    /// <summary>
    /// Represents an error from an endpoint call.
    /// </summary>
    public sealed partial class ApiError : IApiError
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiError"/> class.
        /// </summary>
        private ApiError()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiError"/> class
        /// with the given information.
        /// </summary>
        /// <param name="id">The unique identifier for the error.</param>
        /// <param name="errorCode">The error code of the error.</param>
        /// <param name="description">The description of the error.</param>
        /// <param name="details">A collection of <see cref="IErrorDetail"/> instances.</param>
        [JsonConstructor]
        public ApiError(Guid? id, string? errorCode, string? description, IEnumerable<IErrorDetail>? details = null) 
            : this()
        {
            this.Id = id ?? Guid.Empty;
            this.ErrorCode = errorCode;
            this.Description = description;
            this.Details = details ?? Enumerable.Empty<IErrorDetail>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiError"/> class
        /// with the given information.
        /// </summary>
        /// <param name="id">The unique identifier for the error.</param>
        /// <param name="httpStatusCode">The HTTP status code of the error.</param>
        /// <param name="errorCode">The error code of the error.</param>
        /// <param name="description">The description of the error.</param>
        /// <param name="responseContent">The <see cref="JsonDocument"/> representing the HTTP response body.</param>
        /// <param name="details">A collection of <see cref="IErrorDetail"/> instances.</param>
        internal ApiError(Guid? id, HttpStatusCode? httpStatusCode, string? errorCode, string? description, JsonDocument? responseContent = null, Exception? exception = null, IEnumerable<IErrorDetail>? details = null)
            : this(id, errorCode, description, details)
        {
            this.HttpStatusCode = httpStatusCode;
            this.ResponseContent = responseContent;
            this.Exception = exception;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiError"/> class
        /// with the given information.
        /// </summary>
        /// <param name="httpStatusCode">The HTTP status code of the error.</param>
        /// <param name="responseContent">The <see cref="JsonDocument"/> representing the HTTP response body.</param>
        /// <param name="source">An <see cref="ApiError"/> instance from which properties will be copied.</param>
        internal ApiError(HttpStatusCode httpStatusCode, JsonDocument? responseContent, IApiError source)
        {
            this.Id = source.Id;
            this.ErrorCode = source.ErrorCode;
            this.HttpStatusCode = httpStatusCode;
            this.Description = source.Description;
            this.Details = source.Details;
            this.ResponseContent = responseContent;
        }

        /// <inheritdoc/>
        [JsonPropertyName(JsonSerializationPropertyNames.Description)]
        [JsonInclude]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? Description { get; }

        /// <inheritdoc/>
        [JsonPropertyName(JsonSerializationPropertyNames.ErrorDetails)]
        [JsonInclude]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public IEnumerable<IErrorDetail> Details { get; } = Enumerable.Empty<IErrorDetail>();

        /// <inheritdoc/>
        [JsonPropertyName(JsonSerializationPropertyNames.ErrorCode)]
        [JsonInclude]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? ErrorCode { get; set; }

        /// <inheritdoc/>
        [JsonPropertyName(JsonSerializationPropertyNames.StatusCode)]
        [JsonInclude]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public HttpStatusCode? HttpStatusCode { get; }

        /// <inheritdoc/>
        [JsonPropertyName(JsonSerializationPropertyNames.Id)]
        [JsonInclude]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [DefaultValue("{00000000-0000-0000-0000-000000000000}")]
        public Guid? Id { get;} = Guid.Empty;

        /// <inheritdoc/>
        [JsonIgnore]
        public JsonDocument? ResponseContent { get; }

        /// <inheritdoc/>
        [JsonIgnore]
        public Exception? Exception { get; }
    }
}
