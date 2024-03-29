﻿//------------------------------------------------------------------------------
// <copyright file="IApiResult.cs"
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

namespace Cadru.ApiClient.Models
{
    /// <summary>
    /// Represents the response of an endpoint call.
    /// </summary>
    /// <typeparam name="TData">The type of payload model.</typeparam>
    public interface IApiResult<out TData> where TData : class
    {
        /// <summary>
        /// The returned response object.
        /// </summary>
        TData? Data { get; }

        /// <summary>
        /// A boolean value that indicates if the response is an error.
        /// </summary>
#if !NETSTANDARD2_1
        [MemberNotNullWhen(false, nameof(Data))]
        [MemberNotNullWhen(true, nameof(Error))]
#endif
        bool IsError { get; }

        /// <summary>
        /// An <see cref="ApiError"/> instance representing the error from the response.
        /// </summary>
        IApiError? Error { get; }

        CookieCollection? Cookies { get; internal set; }
    }
}
