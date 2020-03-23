//------------------------------------------------------------------------------
// <copyright file="HttpExtensions.cs"
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

namespace Cadru.Net.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;

    using Cadru.Collections;

    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Provides basic routines for common HTTPClient related manipulation.
    /// </summary>
    public static class HttpExtensions
    {
        #region AsFormattedString
        /// <summary>
        /// Returns string representation of a HttpRequestMessage.
        /// </summary>
        /// <param name="httpRequest">Request object to format.</param>
        /// <returns>The string, formatted into curly braces.</returns>
        public static string AsFormattedString(this HttpRequestMessage httpRequest)
        {
            if (httpRequest == null)
            {
                throw new ArgumentNullException("httpRequest");
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
                throw new ArgumentNullException("httpResponse");
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

            return "{" + String.Join(",",
                dictionary.Select(kv => kv.Key.ToString() +
                                        "=" +
                                        (kv.Value == null ? String.Empty : kv.Value.ToString()))
                    .ToArray()) + "}";
        }
        #endregion

        #region AsString
        /// <summary>
        /// Formats an HttpContent object as String.
        /// </summary>
        /// <param name="content">The HttpContent to format.</param>
        /// <returns>The formatted string.</returns>
        public static string AsString(this HttpContent content)
        {
            if (content != null)
            {
                // Await for the content.
                return
                    content
                        .ReadAsStringAsync()
                        .ConfigureAwait(false)
                        .GetAwaiter()
                        .GetResult();
            }
            return null;
        }
        #endregion

        public static HttpRequestMessage CreateRequestMessage(this HttpClient httpClient, HttpMethod method, Uri uri, QueryStringParametersDictionary queryStringParameters = null, IDictionary<string, string> headerCollection = null)
        {
            Uri requestUri = null;
            if ((uri == null) && (httpClient.BaseAddress == null))
            {
                throw new InvalidOperationException("SR.net_http_client_invalid_requesturi");
            }
            if (uri == null)
            {
                requestUri = httpClient.BaseAddress;
            }
            else
            {
                // If the request Uri is an absolute Uri, just use it. Otherwise try to combine it with the base Uri.
                if (!uri.IsAbsoluteUri)
                {
                    if (httpClient.BaseAddress == null)
                    {
                        throw new InvalidOperationException("SR.net_http_client_invalid_requesturi");
                    }
                    else
                    {
                        requestUri = new Uri(httpClient.BaseAddress, uri);
                    }
                }
            }

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

        #region GetContentHeaders
        /// <summary>
        /// Get the content headers of an HtttRequestMessage.
        /// </summary>
        /// <param name="request">The request message.</param>
        /// <returns>The content headers.</returns>
        public static HttpHeaders GetContentHeaders(this HttpRequestMessage request)
        {
            if (request != null && request.Content != null)
            {
                return request.Content.Headers;
            }
            return null;
        }

        /// <summary>
        /// Get the content headers of an HttpResponseMessage.
        /// </summary>
        /// <param name="response">The response message.</param>
        /// <returns>The content headers.</returns>
        public static HttpHeaders GetContentHeaders(this HttpResponseMessage response)
        {
            if (response != null && response.Content != null)
            {
                return response.Content.Headers;
            }
            return null;
        }
        #endregion

        #region GetHeadersAsJson
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
        #endregion

        #region ToJson
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
        #endregion
    }
}
