//------------------------------------------------------------------------------
// <copyright file="Extensions.cs"
//  company="Scott Dorman"
//  library="Cadru">
//    Copyright (C) 2001-2020 Scott Dorman.
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

using System.Net.Http;

using Microsoft.AspNetCore.Http;

namespace Cadru.AspNetCore.Http.Internal
{
    internal static class Extensions
    {
        /// <summary>
        /// Gets a value that indicates if the HTTP response was successful.
        /// </summary>
        /// <param name="responseMessage"></param>
        /// <returns><see langword="true"/> if <see
        /// cref="HttpResponseMessage.StatusCode"/> was in the range 200-299;
        /// otherwise, <see langword="false"/>.</returns>
        /// <remarks>As <see cref="HttpResponse"/> does not have a built-in way
        /// to check for a successful response, this is a wrapper over <see
        /// cref="HttpResponseMessage.IsSuccessStatusCode"/> to standardize
        /// checking between <see cref="HttpResponse"/> and <see
        /// cref="HttpResponseMessage"/>.</remarks>
        public static bool IsSuccessStatusCode(this HttpResponseMessage? responseMessage)
        {
            return responseMessage?.IsSuccessStatusCode ?? false;
        }

        /// <summary>
        /// Gets a value that indicates if the HTTP response was successful.
        /// </summary>
        /// <param name="response"></param>
        /// <returns><see langword="true"/> if <see cref="HttpResponse.StatusCode"/>
        /// was in the range 200-299; otherwise, <see langword="false"/>.</returns>
        /// <remarks>As <see cref="HttpResponse"/> does not have a built-in way
        /// to check for a successful response, this is a wrapper over <see
        /// cref="HttpResponseMessage.IsSuccessStatusCode"/> to standardize
        /// checking between <see cref="HttpResponse"/> and <see
        /// cref="HttpResponseMessage"/>.</remarks>
        public static bool IsSuccessStatusCode(this HttpResponse? response)
        {
            return response != null && (response.StatusCode >= 200) && (response.StatusCode <= 299);
        }
    }
}