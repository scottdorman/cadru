//------------------------------------------------------------------------------
// <copyright file="IRequestResponseLoggingSerializer.cs"
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

using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

namespace Cadru.AspNetCore.Http
{
    /// <summary>
    /// Provides methods for serializing an <see
    /// cref="HttpRequestMessage"/> or <see cref="HttpRequest"/>
    /// to a format suitable for logging.
    /// </summary>
    public interface IRequestResponseLoggingSerializer
    {
        /// <summary>
        /// Serialize a request to a string suitable for logging.
        /// </summary>
        /// <param name="request">The request to be serialized.</param>
        /// <returns>A string representing the serialized request.</returns>
        string SerializeRequest(HttpRequest request);

        /// <summary>
        /// Serialize a request to a string suitable for logging.
        /// </summary>
        /// <param name="request">The request to be serialized.</param>
        /// <returns>A string representing the serialized request.</returns>
        string SerializeRequest(HttpRequestMessage request);

        /// <summary>
        /// Serialize a response to a string suitable for logging.
        /// </summary>
        /// <param name="response">The response to be serialized.</param>
        /// <param name="responseStream">A <see cref="MemoryStream"/> used to serialize the response body.</param>
        /// <returns>A string representing the serialized response.</returns>
        Task<string> SerializeResponseAsync(HttpResponse? response, MemoryStream responseStream);

        /// <summary>
        /// Serialize a response to a string suitable for logging.
        /// </summary>
        /// <param name="response">The response to be serialized.</param>
        /// <returns>A string representing the serialized response.</returns>
        Task<string> SerializeResponseAsync(HttpResponseMessage? response);
    }
}