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
using System.Net;
using System.Text.Json;

namespace Cadru.ApiClient.Models
{
    /// <summary>
    /// Represents an error from an endpoint call.
    /// </summary>
    public interface IApiError
    {
        /// <summary>
        /// The description of the error.
        /// </summary>
        string? Description { get; }

        /// <summary>
        /// A collection of <see cref="IErrorDetail"/> instances.
        /// </summary>
        IEnumerable<IErrorDetail> Details { get; }

        /// <summary>
        /// The error code of the error.
        /// </summary>
        string? ErrorCode { get; }

        /// <summary>
        /// The HTTP status code of the error.
        /// </summary>
        HttpStatusCode? HttpStatusCode { get; }

        /// <summary>
        /// The unique identifier for the error.
        /// </summary>
        Guid? Id { get; }

        /// <summary>
        /// The <see cref="JsonDocument"/> representing the HTTP response body.
        /// </summary>
        JsonDocument? ResponseContent { get; }
    }
}
