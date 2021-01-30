//------------------------------------------------------------------------------
// <copyright file="HttpExtensions.cs"
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
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

using Cadru.Net.Http.Collections;
using Cadru.Net.Http.Resources;

using Newtonsoft.Json.Linq;

namespace Cadru.Net.Http.Extensions
{
    /// <summary>
    /// Provides basic routines for common HTTPClient related manipulation.
    /// </summary>
    public static class HttpExtensions
    {
        /// <summary>
        /// Returns string representation of a HttpRequestMessage.
        /// </summary>
        /// <param name="httpRequest">Request object to format.</param>
        /// <returns>The string, formatted into curly braces.</returns>
        public static string AsFormattedString(this HttpRequestMessage httpRequest)
        {
            if (httpRequest == null)
            {
                throw new ArgumentNullException(nameof(httpRequest));
            }

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(httpRequest.ToString());
            if (httpRequest.Content != null)
            {
                stringBuilder.AppendLine();
                stringBuilder.AppendLine("Body:");
                stringBuilder.AppendLine("{");
                stringBuilder.AppendLine(httpRequest.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult());
                stringBuilder.AppendLine("}");
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Returns string representation of a HttpResponseMessage.
        /// </summary>
        /// <param name="httpResponse">Response object to format.</param>
        /// <returns>The string, formatted into curly braces.</returns>
        public static string AsFormattedString(this HttpResponseMessage httpResponse)
        {
            if (httpResponse == null)
            {
                throw new ArgumentNullException(nameof(httpResponse));
            }

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(httpResponse.ToString());
            if (httpResponse.Content != null)
            {
                stringBuilder.AppendLine();
                stringBuilder.AppendLine("Body:");
                stringBuilder.AppendLine("{");
                stringBuilder.AppendLine(httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult());
                stringBuilder.AppendLine("}");
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Converts given dictionary into a log string.
        /// </summary>
        /// <typeparam name="TKey">The dictionary key type.</typeparam>
        /// <typeparam name="TValue">The dictionary value type.</typeparam>
        /// <param name="dictionary">The dictionary object.</param>
        /// <returns>The string, formatted into curly braces.</returns>
        public static string AsFormattedString<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            if (dictionary == null)
            {
                return "{}";
            }

            return $"{{{ String.Join(",", dictionary.Select(kv => $"{kv.Key}={kv.Value?.ToString() ?? String.Empty}").ToArray())}}}";
        }

        /// <summary>
        /// Formats an HttpContent object as String.
        /// </summary>
        /// <param name="content">The HttpContent to format.</param>
        /// <returns>The formatted string.</returns>
        public static string AsString(this HttpContent content)
        {
            return content?.ReadAsStringAsync()
                    .ConfigureAwait(false).GetAwaiter().GetResult() ?? String.Empty;
        }

        /// <summary>
        /// Creates a valid <see cref="HttpRequestMessage"/> with the given properties.
        /// </summary>
        /// <param name="httpClient">
        /// The <see cref="HttpClient"/> instance used to create the message.
        /// </param>
        /// <param name="method">The HTTP method.</param>
        /// <param name="uri">The <see cref="Uri"/> to request.</param>
        /// <param name="queryStringParameters">The query string parameters.</param>
        /// <param name="headerCollection">
        /// Additional request headers to be included.
        /// </param>
        /// <returns>A valid <see cref="HttpRequestMessage"/>.</returns>
        public static HttpRequestMessage CreateRequestMessage(this HttpClient httpClient, HttpMethod method, Uri uri, QueryStringParametersDictionary? queryStringParameters = null, IDictionary<string, string>? headerCollection = null)
        {
            if (httpClient.BaseAddress == null && (uri == null || !uri.IsAbsoluteUri))
            {
                throw new InvalidOperationException(Strings.net_http_client_invalid_requesturi);
            }

            var requestUri = uri == null ? httpClient.BaseAddress! : new Uri(httpClient.BaseAddress!, uri);

            if (queryStringParameters != null)
            {
                var builder = new UriBuilder(requestUri)
                {
                    Query = queryStringParameters.ToQueryString()
                };

                requestUri = builder.Uri;
            }

            var requestMessge = new HttpRequestMessage(method, requestUri);

            if (headerCollection != null)
            {
                foreach (var header in headerCollection)
                {
                    requestMessge.Headers.Add(header.Key, header.Value);
                }
            }

            return requestMessge;
        }

        /// <summary>
        /// Get the content headers of an HtttRequestMessage.
        /// </summary>
        /// <param name="request">The request message.</param>
        /// <returns>The content headers.</returns>
        public static HttpContentHeaders? GetContentHeaders(this HttpRequestMessage request)
        {
            if (request != null && request.Content != null)
            {
                return request.Content.Headers;
            }

            return default;
        }

        /// <summary>
        /// Get the content headers of an HttpResponseMessage.
        /// </summary>
        /// <param name="response">The response message.</param>
        /// <returns>The content headers.</returns>
        public static HttpContentHeaders? GetContentHeaders(this HttpResponseMessage response)
        {
            if (response != null && response.Content != null)
            {
                return response.Content.Headers;
            }

            return default;
        }

        /// <summary>
        /// Serializes HttpResponseHeaders and HttpContentHeaders as Json dictionary.
        /// </summary>
        /// <param name="message">HttpResponseMessage</param>
        /// <returns>Json string</returns>
        public static JObject GetHeadersAsJson(this HttpResponseMessage message)
        {
            if (message == null)
            {
                return new JObject();
            }
            var jObject = new JObject();
            foreach (var httpResponseHeader in message.Headers)
            {
                if (httpResponseHeader.Value.Count() > 1)
                {
                    jObject[httpResponseHeader.Key] = new JArray(httpResponseHeader.Value);
                }
                else
                {
                    jObject[httpResponseHeader.Key] = httpResponseHeader.Value.FirstOrDefault();
                }
            }

            if (message.Content != null)
            {
                foreach (var httpResponseHeader in message.Content.Headers)
                {
                    if (httpResponseHeader.Value.Count() > 1)
                    {
                        jObject[httpResponseHeader.Key] = new JArray(httpResponseHeader.Value);
                    }
                    else
                    {
                        jObject[httpResponseHeader.Key] = httpResponseHeader.Value.FirstOrDefault();
                    }
                }
            }
            return jObject;
        }

        /// <summary>
        /// Serializes HttpHeaders as Json dictionary.
        /// </summary>
        /// <param name="headers">HttpHeaders</param>
        /// <returns>Json string</returns>
        public static JObject ToJson(this HttpHeaders headers)
        {
            if (headers == null || !headers.Any())
            {
                return new JObject();
            }
            else
            {
                return headers.ToDictionary(h => h.Key, h => h.Value).ToJson();
            }
        }

        /// <summary>
        /// Serializes header dictionary as Json dictionary.
        /// </summary>
        /// <param name="headers">Dictionary</param>
        /// <returns>Json string</returns>
        public static JObject ToJson(this IDictionary<string, IEnumerable<string>> headers)
        {
            if (headers == null || !headers.Any())
            {
                return new JObject();
            }
            else
            {
                var jObject = new JObject();
                foreach (var httpResponseHeader in headers)
                {
                    if (httpResponseHeader.Value.Count() > 1)
                    {
                        jObject[httpResponseHeader.Key] = new JArray(httpResponseHeader.Value);
                    }
                    else
                    {
                        jObject[httpResponseHeader.Key] = httpResponseHeader.Value.FirstOrDefault();
                    }
                }
                return jObject;
            }
        }

        /// <summary>
        /// Return if a specified header and specified values are stored in the
        /// System.Net.Http.Headers.HttpHeaders collection.
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <param name="name">The specified header.</param>
        /// <param name="value">The specified header value.</param>
        /// <returns>
        /// <see langword="true"/> if the specified header name and values are
        /// stored in the collection; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool TryGetHeaderValue(this HttpRequestMessage requestMessage, string name, out string? value)
        {
            var found = false;
            value = null;

            if (requestMessage.Headers.TryGetValues(name, out var values))
            {
                value = values.First();
                found = true;
            }

            return found;
        }
    }
}