//------------------------------------------------------------------------------
// <copyright file="HttpRequestWithStatusException.cs"
//  company="Scott Dorman"
//  library="Cadru">
//    Copyright (C) 2001-2017 Scott Dorman.
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
using System.Globalization;
using System.Net;
using System.Net.Http;

using Cadru.Net.Http.Resources;

namespace Cadru.Net.Http
{
    /// <summary>
    /// Inherits HttpRequestException adding HttpStatusCode to the exception.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Justification = "HttpRequestException hides the constructor needed for serialization.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2229:Implement serialization constructors", Justification = "HttpRequestException hides the constructor needed for serialization.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S3925:\"ISerializable\" should be implemented correctly", Justification = "HttpRequestException hides the constructor needed for serialization.")]
    public class HttpRequestWithStatusException : HttpRequestException
    {
        private static string GetFormattedMessage(HttpResponseMessage responseMessage)
        {
            return String.Format(CultureInfo.InvariantCulture, Strings.ResponseStatusCodeError, (int)responseMessage.StatusCode, responseMessage.StatusCode);
        }

        public HttpRequestWithStatusException(HttpResponseMessage responseMessage) :
            base(GetFormattedMessage(responseMessage))
        {
            this.StatusCode = responseMessage.StatusCode;
            this.ReasonPhrase = responseMessage.ReasonPhrase;
        }

        public HttpRequestWithStatusException(HttpResponseMessage responseMessage, Exception inner) :
            base(GetFormattedMessage(responseMessage), inner)
        {
            this.StatusCode = responseMessage.StatusCode;
            this.ReasonPhrase = responseMessage.ReasonPhrase;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpRequestWithStatusException"/> class.
        /// </summary>
        public HttpRequestWithStatusException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpRequestWithStatusException"/> class
        /// with a specific message that describes the current exception.
        /// </summary>
        /// <param name="message">A message that describes the current exception.</param>
        public HttpRequestWithStatusException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpRequestWithStatusException"/> class
        /// with a specific message that describes the current exception and an inner
        /// exception.
        /// </summary>
        /// <param name="message">A message that describes the current exception.</param>
        /// <param name="inner">The inner exception.</param>
        public HttpRequestWithStatusException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Gets the status code of the HTTP response.
        /// </summary>
        /// <value>The status code of the HTTP response.</value>
        public HttpStatusCode StatusCode { get; }

        /// <summary>
        /// Gets the reason phrase which typically is sent by servers together with
        /// the status code.
        /// </summary>
        /// <value>The reason phrase sent by the server.</value>
        public string ReasonPhrase { get; }
    }
}