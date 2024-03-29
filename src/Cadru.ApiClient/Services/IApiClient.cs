﻿//------------------------------------------------------------------------------
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

using System.Net;
using System.Net.Http.Headers;

namespace Cadru.ApiClient.Services
{
    public interface IApiClient
    {
        /// <summary>
        /// Gets or sets the value of the Authorization header for an HTTP request.
        /// </summary>
        public AuthenticationHeaderValue? AuthenticationHeaderValue { get; set; }

        public CookieContainer? Cookies { get; }
    }
}
