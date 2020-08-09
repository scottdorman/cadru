using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Cadru.Net.Http.Extensions
{
    /// <summary>
    /// Extension methods that aid in making formatted requests using <see
    /// cref="HttpClient"></see>.
    /// </summary>
    public static class HttpClientExtensions
    {
        /// <summary>
        /// Send a POST request to the specified Uri as an asynchronous
        /// operation.
        /// </summary>
        /// <param name="client">The client used to make the request.</param>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <returns>A task object representing the asynchronous operation.</returns>
        public static Task<HttpResponseMessage> PostAsync(this HttpClient client, string requestUri)
        {
            return client.PostAsync(requestUri, new StringContent(String.Empty, Encoding.UTF8));
        }

        /// <summary>
        /// Send a POST request to the specified Uri as an asynchronous
        /// operation.
        /// </summary>
        /// <param name="client">The client used to make the request.</param>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <returns>A task object representing the asynchronous operation.</returns>
        public static Task<HttpResponseMessage> PostAsync(this HttpClient client, Uri requestUri)
        {
            return client.PostAsync(requestUri, new StringContent(String.Empty, Encoding.UTF8));
        }

        /// <summary>
        /// Sends a POST request as an asynchronous operation to the specified Uri with the
        /// given value serialized as JSON.
        /// </summary>
        /// <typeparam name="T">The type of value.</typeparam>
        /// <param name="client">The client used to make the request.</param>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="value">The value that will be placed in the request's entity body.</param>
        /// <param name="mediaType">The Content-Type header used to send the request.</param>
        /// <returns>A task object representing the asynchronous operation.</returns>
        /// <remarks>This method uses a default instance of <see cref="System.Net.Http.Formatting.JsonMediaTypeFormatter"></see>.</remarks>
        public static Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, string requestUri, T value, string mediaType)
        {
            return client.PostAsync(requestUri, value, new JsonMediaTypeFormatter(), mediaType);
        }

        /// <summary>
        /// Sends a POST request as an asynchronous operation to the specified Uri with the
        /// given value serialized as JSON.
        /// </summary>
        /// <typeparam name="T">The type of value.</typeparam>
        /// <param name="client">The client used to make the request.</param>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="value">The value that will be placed in the request's entity body.</param>
        /// <param name="mediaType">The Content-Type header used to send the request.</param>
        /// <returns>A task object representing the asynchronous operation.</returns>
        /// <remarks>This method uses a default instance of <see cref="System.Net.Http.Formatting.JsonMediaTypeFormatter"></see>.</remarks>
        public static Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, Uri requestUri, T value, string mediaType)
        {
            return client.PostAsync(requestUri, value, new JsonMediaTypeFormatter(), mediaType);
        }

        /// <summary>
        /// Sends a POST request as an asynchronous operation to the specified Uri.
        /// </summary>
        /// <param name="client">The client used to make the request.</param>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="mediaType">The Content-Type header used to send the request.</param>
        /// <returns>A task object representing the asynchronous operation.</returns>
        public static Task<HttpResponseMessage> PostAsync(this HttpClient client, string requestUri, string mediaType)
        {
            return client.PostAsync(requestUri, new StringContent(String.Empty, Encoding.UTF8, mediaType));
        }

        /// <summary>
        /// Sends a POST request as an asynchronous operation to the specified Uri.
        /// </summary>
        /// <param name="client">The client used to make the request.</param>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="mediaType">The Content-Type header used to send the request.</param>
        /// <returns>A task object representing the asynchronous operation.</returns>
        public static Task<HttpResponseMessage> PostAsync(this HttpClient client, Uri requestUri, string mediaType)
        {
            return client.PostAsync(requestUri, new StringContent(String.Empty, Encoding.UTF8, mediaType));
        }

        /// <summary>
        /// Sends a PUT request as an asynchronous operation to the specified
        /// Uri with the given value serialized as JSON.
        /// </summary>
        /// <typeparam name="T">The type of value.</typeparam>
        /// <param name="client">The client used to make the request.</param>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="value">The value that will be placed in the request's
        /// entity body.</param>
        /// <param name="mediaType">The Content-Type header used to send the
        /// request.</param>
        /// <returns>A task object representing the asynchronous
        /// operation.</returns>
        /// <remarks>This method uses a default instance of <see cref="System.Net.Http.Formatting.JsonMediaTypeFormatter"></see>.</remarks>
        public static Task<HttpResponseMessage> PutAsync<T>(this HttpClient client, string requestUri, T value, string mediaType)
        {
            return client.PutAsync(requestUri, value, new JsonMediaTypeFormatter(), mediaType);
        }

        /// <summary>
        /// Sends a PUT request as an asynchronous operation to the specified
        /// Uri with the given value serialized as JSON.
        /// </summary>
        /// <typeparam name="T">The type of value.</typeparam>
        /// <param name="client">The client used to make the request.</param>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="value">The value that will be placed in the request's entity body.</param>
        /// <param name="mediaType">The Content-Type header used to send the request.</param>
        /// <returns>A task object representing the asynchronous operation.</returns>
        /// <remarks>This method uses a default instance of <see cref="System.Net.Http.Formatting.JsonMediaTypeFormatter"></see>.</remarks>
        public static Task<HttpResponseMessage> PutAsync<T>(this HttpClient client, Uri requestUri, T value, string mediaType)
        {
            return client.PutAsync(requestUri, value, new JsonMediaTypeFormatter(), mediaType);
        }

        /// <summary>
        /// Sends a PUT request as an asynchronous operation to the specified
        /// Uri.
        /// </summary>
        /// <param name="client">The client used to make the request.</param>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="mediaType">The Content-Type header used to send the request.</param>
        /// <returns>A task object representing the asynchronous operation.</returns>
        public static Task<HttpResponseMessage> PutAsync(this HttpClient client, string requestUri, string mediaType)
        {
            return client.PutAsync(requestUri, new StringContent(String.Empty, Encoding.UTF8, mediaType));
        }

        /// <summary>
        /// Sends a PUT request as an asynchronous operation to the specified
        /// Uri.
        /// </summary>
        /// <param name="client">The client used to make the request.</param>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="mediaType">The Content-Type header used to send the request.</param>
        /// <returns>A task object representing the asynchronous operation.</returns>
        public static Task<HttpResponseMessage> PutAsync(this HttpClient client, Uri requestUri, string mediaType)
        {
            return client.PutAsync(requestUri, new StringContent(String.Empty, Encoding.UTF8, mediaType));
        }

        internal static MediaTypeHeaderValue BuildHeaderValue(string mediaType)
        {
            return mediaType != null ? new MediaTypeHeaderValue(mediaType) : null;
        }
    }
}