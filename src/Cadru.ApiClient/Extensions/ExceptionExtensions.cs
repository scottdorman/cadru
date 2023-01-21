//------------------------------------------------------------------------------
// <copyright file="ExceptionExtensions.cs"
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
using System.Linq;
using System.Net;
using System.Text.Json;

using Cadru.ApiClient.Models;
using Cadru.ApiClient.Resources;
using Cadru.Diagnostics;

namespace Cadru.ApiClient.Extensions
{
    /// <summary>
    /// Extension methods for converting an <see cref="Exception"/> into a collection
    /// of <see cref="IErrorDetail"/> instances.
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Converts the given <see cref="Exception"/> into a collection
        /// of <see cref="IErrorDetail"/> instances.
        /// </summary>
        /// <param name="e">The exception to convert.</param>
        /// <returns>A collection of <see cref="IErrorDetail"/> instances representing
        /// all of the exceptions in the exception hierarchy.</returns>
        public static IEnumerable<IErrorDetail> GetErrorDetails(this Exception e)
        {
            var details = new List<IErrorDetail>();
            details.AddRange(e.GetAllMessages().Select(m => new ExceptionErrorDetail(m)));
            return details;
        }

        /// <summary>
        /// Converts the given <see cref="Exception"/> into an <see cref="ApiError"/> instance.
        /// </summary>
        /// <param name="e">The exception to convert.</param>
        /// <param name="errorId">The unique identifier for the error.</param>
        /// <param name="httpStatusCode">The HTTP status code of the error.</param>
        /// <param name="errorCode">The error code of the error.</param>
        /// <param name="description">The description of the error.</param>
        /// <param name="responseContent">The <see cref="JsonDocument"/> representing the HTTP response body.</param>
        /// <returns>An <see cref="ApiError"/> instance representing the exception.</returns>
        public static IApiError ToApiError(this Exception e, Guid? errorId = null, string? errorCode = null, HttpStatusCode? httpStatusCode = null, string? description = null, JsonDocument? responseContent = null)
            => new ApiError(errorId ?? Guid.Empty, httpStatusCode ?? HttpStatusCode.InternalServerError, errorCode, description ?? Strings.Error_UnexpectedApiError, responseContent, e, e.GetErrorDetails());
    }
}
