//------------------------------------------------------------------------------
// <copyright file="RequestResponseLoggingSerializer.cs"
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

using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Cadru.AspNetCore.Resources;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

namespace Cadru.AspNetCore.Http
{
    /// <summary>
    /// Provides methods for serializing an <see
    /// cref="HttpRequestMessage" /> or <see cref="HttpRequest" />
    /// to a format suitable for logging.
    /// </summary>
    public class RequestResponseLoggingSerializer : IRequestResponseLoggingSerializer
    {
        /// <summary>
        /// The Location header name.
        /// </summary>
        protected const string Location = "Location";

        /// <inheritdoc/>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Pending>")]
        public virtual string SerializeRequest(HttpRequest request)
        {
            string result;
            try
            {
                var data = new StringBuilder($"{request.Method} {GetRawTarget(request)} ");
                data.AppendFormat(Strings.Debugging_HttpMessages_ContentType, request.ContentType);
                result = data.ToString();
            }
            catch (Exception e)
            {
                result = e.ToString();
            }

            return result;
        }

        /// <inheritdoc/>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Pending>")]
        public virtual string SerializeRequest(HttpRequestMessage request)
        {
            string result;
            try
            {
                var data = new StringBuilder($"{request.Method} {request.RequestUri} ");
                if (request.Content?.Headers.ContentType != null)
                {
                    data.AppendFormat(Strings.Debugging_HttpMessages_ContentType, request.Content?.Headers.ContentType);
                }

                result = data.ToString();
            }
            catch (Exception e)
            {
                result = e.ToString();
            }

            return result;
        }

        /// <inheritdoc/>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Pending>")]
        public virtual async Task<string> SerializeResponseAsync(HttpResponse? response, MemoryStream responseStream)
        {
            string? result = null;

            if (response != null)
            {
                try
                {
                    var data = new StringBuilder(String.Format(Strings.Debugging_HttpMessage_Response, response.StatusCode));
                    if (response.Headers.TryGetValue(Location, out var location))
                    {
                        data.AppendFormat(Strings.Debugging_HttpMessages_Location, location.ToString());
                    }

                    var originalBody = response.Body;
                    if (originalBody != null)
                    {
                        response.Body = responseStream;
                        await responseStream.CopyToAsync(originalBody);
                        data.Append(await this.ReadToEndAsync(responseStream));
                        response.Body = originalBody;
                    }

                    result = data.ToString();
                }
                catch (Exception e)
                {
                    result = e.ToString();
                }
            }

            return result ?? Strings.Debugging_HttpMessages_EmptyResponse;
        }

        /// <inheritdoc/>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Pending>")]
        public virtual async Task<string> SerializeResponseAsync(HttpResponseMessage? response)
        {
            string? result = null;

            if (response != null)
            {
                try
                {
                    var data = new StringBuilder(String.Format(Strings.Debugging_HttpMessage_Response, response.StatusCode, (int)response.StatusCode));
                    if (response.Headers.Location != null)
                    {
                        data.AppendFormat(Strings.Debugging_HttpMessages_Location, response.Headers.Location);
                    }

                    if (response.Content != null)
                    {
                        var responseBody = await response.Content.ReadAsStringAsync();
                        if (!String.IsNullOrWhiteSpace(responseBody))
                        {
                            data.AppendFormat(Strings.Debugging_HttpMessages_Body, responseBody);
                        }
                    }

                    result = data.ToString();
                }
                catch (Exception e)
                {
                    result = e.ToString();
                }
            }

            return result ?? Strings.Debugging_HttpMessages_EmptyResponse;
        }

        /// <summary>
        /// The request target as it was sent in the HTTP request. This property contains
        /// the raw path and full query, as well as other request targets such as * for OPTIONS
        /// requests (https://tools.ietf.org/html/rfc7230#section-5.3).
        /// </summary>
        /// <param name="request">The HTTP request.</param>
        /// <returns>A string representing the raw target URI.</returns>
        /// <remarks>
        /// It has not been UrlDecoded and care should be taken in its use.
        /// </remarks>
        protected static string GetRawTarget(HttpRequest request)
        {
            var httpContext = request.HttpContext;
            var requestFeature = httpContext.Features.Get<IHttpRequestFeature>();
            return requestFeature.RawTarget;
        }

        /// <summary>
        /// Reads all characters from the current position to the end of the stream asynchronously
        /// and returns them as one string.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns>A task that represents the asynchronous read operation. The value contains
        /// a string with the characters from the current position to the end of the stream.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The number of characters is larger than <see cref="System.Int32.MaxValue" />.</exception>
        /// <exception cref="ObjectDisposedException">The <paramref name="stream" /> has been disposed.</exception>
        /// <exception cref="InvalidOperationException">The reader is currently in use by a previous read operation..</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>")]
        protected async Task<string> ReadToEndAsync(MemoryStream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);

            string result;
            using (var reader = new StreamReader(stream))
            {
                result = await reader.ReadToEndAsync();
            }

            return result;
        }
    }
}