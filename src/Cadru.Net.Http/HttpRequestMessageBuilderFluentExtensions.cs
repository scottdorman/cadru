using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

using Cadru.Net.Http.Collections;

namespace Cadru.Net.Http
{
    /// <summary>
    /// Provides a fluent expression builder for creating a <see cref="HttpRequestMessage"/>.
    /// </summary>
    public static class HttpRequestMessageBuilderFluentExtensions
    {
        /// <summary>
        /// Adds the provided query string parameters to the request.
        /// </summary>
        /// <param name="httpRequestMessageBuilder"></param>
        /// <param name="queryStringParameters"></param>
        /// <returns></returns>
        public static HttpRequestMessageBuilder WithQueryParameters(this HttpRequestMessageBuilder httpRequestMessageBuilder, QueryStringParametersDictionary queryStringParameters)
        {
            Uri requestUri = httpRequestMessageBuilder.RequestMessage.RequestUri ?? throw new InvalidOperationException("The RequestUri property must have a value.");
            UrlBuilder urlBuilder = new(requestUri);
            urlBuilder.QueryParameters.AddRange(queryStringParameters);
            httpRequestMessageBuilder.RequestMessage.RequestUri = urlBuilder.Uri;
            return httpRequestMessageBuilder;
        }

        /// <summary>
        /// Adds the provided query string parameter to the request.
        /// </summary>
        /// <param name="httpRequestMessageBuilder"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static HttpRequestMessageBuilder WithQueryParameter(this HttpRequestMessageBuilder httpRequestMessageBuilder, string key, string value)
        {
            Uri requestUri = httpRequestMessageBuilder.RequestMessage.RequestUri ?? throw new InvalidOperationException("The RequestUri property must have a value.");
            UrlBuilder urlBuilder = new(requestUri);
            urlBuilder.QueryParameters.Add(key, value);
            httpRequestMessageBuilder.RequestMessage.RequestUri = urlBuilder.Uri;
            return httpRequestMessageBuilder;
        }

        /// <summary>
        /// Adds the provided query string parameter to the request.
        /// </summary>
        /// <param name="httpRequestMessageBuilder"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static HttpRequestMessageBuilder WithQueryParameter<T>(this HttpRequestMessageBuilder httpRequestMessageBuilder, string key, T value)
        {
            string parameterValue = value?.ToString() ?? throw new ArgumentNullException(nameof(value));
            Uri requestUri = httpRequestMessageBuilder.RequestMessage.RequestUri ?? throw new InvalidOperationException("The RequestUri property must have a value.");
            UrlBuilder urlBuilder = new(requestUri);

            urlBuilder.QueryParameters.Add(key, parameterValue);
            httpRequestMessageBuilder.RequestMessage.RequestUri = urlBuilder.Uri;
            return httpRequestMessageBuilder;
        }

        /// <summary>
        /// Adds the provided query string parameter to the request.
        /// </summary>
        /// <param name="httpRequestMessageBuilder"></param>
        /// <param name="keyValuePair"></param>
        /// <returns></returns>
        public static HttpRequestMessageBuilder WithQueryParameter(this HttpRequestMessageBuilder httpRequestMessageBuilder, KeyValuePair<string, string> keyValuePair)
        {
            Uri requestUri = httpRequestMessageBuilder.RequestMessage.RequestUri ?? throw new InvalidOperationException("The RequestUri property must have a value.");
            UrlBuilder urlBuilder = new(requestUri);
            urlBuilder.QueryParameters.Add(keyValuePair.Key, keyValuePair.Value);
            httpRequestMessageBuilder.RequestMessage.RequestUri = urlBuilder.Uri;
            return httpRequestMessageBuilder;
        }

        /// <summary>
        /// Sets the value of a specified HTTP request option.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="httpRequestMessageBuilder"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static HttpRequestMessageBuilder WithProperty<TValue>(this HttpRequestMessageBuilder httpRequestMessageBuilder, string key, TValue value)
        {
#if NET6_0_OR_GREATER
            httpRequestMessageBuilder.RequestMessage.Options.Set(new HttpRequestOptionsKey<TValue>(key), value);
#else
            httpRequestMessageBuilder.RequestMessage.Properties.Add(key, value);
#endif

            return httpRequestMessageBuilder;
        }

        /// <summary>
        /// Sets the value of a specified HTTP request header.
        /// </summary>
        /// <param name="httpRequestMessageBuilder"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static HttpRequestMessageBuilder WithRequestHeader(this HttpRequestMessageBuilder httpRequestMessageBuilder, string name, string? value)
        {
            httpRequestMessageBuilder.RequestMessage.Headers.Add(name, value);
            return httpRequestMessageBuilder;
        }

        /// <summary>
        /// Sets the value of a specified HTTP request header.
        /// </summary>
        /// <param name="httpRequestMessageBuilder"></param>
        /// <param name="name"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static HttpRequestMessageBuilder WithRequestHeader(this HttpRequestMessageBuilder httpRequestMessageBuilder, string name, IEnumerable<string?> values)
        {
            httpRequestMessageBuilder.RequestMessage.Headers.Add(name, values);
            return httpRequestMessageBuilder;
        }

        /// <summary>
        /// Sets the <see cref="HttpRequestMessage"/> Authorization header value.
        /// </summary>
        /// <param name="httpRequestMessageBuilder"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static HttpRequestMessageBuilder WithBearerToken(this HttpRequestMessageBuilder httpRequestMessageBuilder, string accessToken)
        {
            httpRequestMessageBuilder.RequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            return httpRequestMessageBuilder;
        }

        //private async Task<AccessTokenItem> getApiToken(string api_name, string api_scope, string secret)
        //{
        //    try
        //    {
        //        var disco = await HttpClientDiscoveryExtensions.GetDiscoveryDocumentAsync(
        //            _httpClient,
        //            _authConfigurations.Value.StsServer);

        //        if (disco.IsError)
        //        {
        //            _logger.LogError($"disco error Status code: {disco.IsError}, Error: {disco.Error}");
        //            throw new ApplicationException($"Status code: {disco.IsError}, Error: {disco.Error}");
        //        }

        //        var tokenResponse = await HttpClientTokenRequestExtensions.RequestClientCredentialsTokenAsync(_httpClient, new ClientCredentialsTokenRequest
        //        {
        //            Scope = api_scope,
        //            ClientSecret = secret,
        //            Address = disco.TokenEndpoint,
        //            ClientId = api_name
        //        });

        //        if (tokenResponse.IsError)
        //        {
        //            _logger.LogError($"tokenResponse.IsError Status code: {tokenResponse.IsError}, Error: {tokenResponse.Error}");
        //            throw new ApplicationException($"Status code: {tokenResponse.IsError}, Error: {tokenResponse.Error}");
        //        }

        //        return new AccessTokenItem
        //        {
        //            ExpiresIn = DateTime.UtcNow.AddSeconds(tokenResponse.ExpiresIn),
        //            AccessToken = tokenResponse.AccessToken
        //        };

        //    }
        //    catch (Exception e)
        //    {
        //        _logger.LogError($"Exception {e}");
        //        throw new ApplicationException($"Exception {e}");
        //    }
        //}

        /// <summary>
        /// Sets the <see cref="HttpRequestMessage"/> Authorization header value.
        /// </summary>
        /// <param name="httpRequestMessageBuilder"></param>
        /// <param name="authorization"></param>
        /// <returns></returns>
        public static HttpRequestMessageBuilder WithAuthorization(this HttpRequestMessageBuilder httpRequestMessageBuilder, AuthenticationHeaderValue? authorization)
        {
            if (authorization != null)
            {
                httpRequestMessageBuilder.RequestMessage.Headers.Authorization = authorization;
            }

            return httpRequestMessageBuilder;
        }

        /// <summary>
        /// Sets the <see cref="HttpRequestMessage"/> Content.
        /// </summary>
        /// <param name="httpRequestMessageBuilder"></param>
        /// <param name="content"></param>
        /// <param name="contentType"></param>
        /// <param name="contentDisposition"></param>
        /// <returns></returns>
        public static HttpRequestMessageBuilder WithContent(this HttpRequestMessageBuilder httpRequestMessageBuilder, HttpContent? content, MediaTypeHeaderValue? contentType = null, ContentDispositionHeaderValue? contentDisposition = null)
        {
            if (content != null)
            {
                httpRequestMessageBuilder.RequestMessage.Content = content;
                if (contentType != null)
                {
                    httpRequestMessageBuilder.RequestMessage.Content.Headers.ContentType = contentType;
                }

                if (contentDisposition != null)
                {
                    httpRequestMessageBuilder.RequestMessage.Content.Headers.ContentDisposition = contentDisposition;
                }
            }

            return httpRequestMessageBuilder;
        }
    }
}
