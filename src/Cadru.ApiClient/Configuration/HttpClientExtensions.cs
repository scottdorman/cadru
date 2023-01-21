//------------------------------------------------------------------------------
// <copyright file="HttpClientExtensions.cs"
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

using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Cadru.ApiClient.Configuration
{
    /// <summary>
    /// Extension methods for configuring an <see cref="HttpClient"/> from
    /// provided options.
    /// </summary>
    public static class HttpClientExtensions
    {
        /// <summary>
        /// Adds the specified headers and their values from <see cref="IApiClientOptions.DefaultRequestHeaders"/>
        /// into the <see cref="HttpHeaders"/> collection.
        /// </summary>
        /// <param name="httpClient">The <see cref="HttpClient"/> whose default request headers will be configured.</param>
        /// <param name="options">An <see cref="IApiClientOptions"/> instance</param>
        public static void ConfigureDefaultRequestHeaders(this HttpClient httpClient, IApiClientOptions options)
        {
            if (options.DefaultRequestHeaders.Any())
            {
                foreach(var header in options.DefaultRequestHeaders)
                {
                    httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }
        }
    }
}
