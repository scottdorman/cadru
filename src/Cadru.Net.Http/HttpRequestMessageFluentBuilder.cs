using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

using Cadru.Extensions;
using Cadru.Net.Http.Collections;
using Cadru.Net.Http.Resources;

namespace Cadru.Net.Http
{
    public static class HttpRequestMessageFluentBuilder
    {
        /// <summary>
        /// Creates a valid <see cref="HttpRequestMessage"/> with the given properties.
        /// </summary>
        /// <param name="httpClient">
        /// The <see cref="HttpClient"/> instance used to create the message.
        /// </param>
        /// <param name="method">The HTTP method.</param>
        /// <param name="uri">The <see cref="Uri"/> to request.</param>
        /// <returns>A valid <see cref="HttpRequestMessage"/>.</returns>
        public static HttpRequestMessage CreateRequestMessage(this HttpClient httpClient, HttpMethod method, Uri uri)
        {
            if (httpClient.BaseAddress == null && (uri == null || !uri.IsAbsoluteUri))
            {
                throw new InvalidOperationException(Strings.net_http_client_invalid_requesturi);
            }

            var requestUri = uri == null ? httpClient.BaseAddress! : new Uri(httpClient.BaseAddress!, uri);
            var requestMessge = new HttpRequestMessage(method, requestUri);
            return requestMessge;
        }
    }
}
