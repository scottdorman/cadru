using System;
using System.Net.Http;

using Cadru.Extensions;

namespace Cadru.Net.Http
{
    /// <summary>
    /// Provides a fluent expression builder for creating a <see cref="HttpRequestMessage"/>.
    /// </summary>
    public sealed class HttpRequestMessageBuilder
    {
        private readonly HttpRequestMessage requestMessage;

        /// <summary>
        /// Gets the HTTP method used by the HTTP request message.
        /// </summary>
        /// <value>The HTTP method used by the request message.</value>
        public HttpMethod HttpMethod => requestMessage.Method;

        private HttpRequestMessageBuilder(HttpMethod method, Uri baseUri, string relativeUri, object? replacementObject)
        {
            Uri? requestUri = new(baseUri, new Uri(replacementObject != null ? relativeUri.FormatWith(replacementObject) : relativeUri, UriKind.Relative));
            requestMessage = new HttpRequestMessage(method, requestUri);
        }

        private HttpRequestMessageBuilder(HttpMethod method, Uri uri)
        {
            requestMessage = new HttpRequestMessage(method, uri);
        }

        internal HttpRequestMessage RequestMessage => requestMessage;

        /// <summary>
        /// Creates a <see cref="HttpRequestMessageBuilder"/>.
        /// </summary>
        /// <param name="method"></param>
        /// <param name="baseUri"></param>
        /// <param name="relativeUri"></param>
        /// <param name="replacementObject"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static HttpRequestMessageBuilder Create(HttpMethod method, Uri? baseUri, string relativeUri, object? replacementObject = null)
        {
#if NET6_0_OR_GREATER
            ArgumentNullException.ThrowIfNull(baseUri);
#else
            if (baseUri == null)
            {
                throw new ArgumentNullException(nameof(baseUri));
            }
#endif

            return new(method, baseUri, relativeUri, replacementObject);
        }

        /// <summary>
        /// Creates a <see cref="HttpRequestMessageBuilder"/>.
        /// </summary>
        /// <param name="method"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static HttpRequestMessageBuilder Create(HttpMethod method, Uri? uri)
        {
#if NET6_0_OR_GREATER
            ArgumentNullException.ThrowIfNull(uri);
#else
            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }
#endif
            
            return new(method, uri);
        }

        /// <summary>
        /// Builds a <see cref="HttpRequestMessage"/> from the current builder.
        /// </summary>
        /// <returns></returns>
        public HttpRequestMessage Build()
        {
            return requestMessage;
        }
    }
}
