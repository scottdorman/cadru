//------------------------------------------------------------------------------
// <copyright file="restclient.cs"
//  company="Scott Dorman"
//  library="Cadru">
//    Copyright (C) 2001-2016 Scott Dorman.
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

using Cadru.Contracts;
using Cadru.Extensions;
using Cadru.Net.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cadru.Net.Rest
{
    [CLSCompliant(false)]
    public class RestClient : IDisposable
    {
        #region fields
#if NET40
        private static readonly TimeSpan InfiniteTimeout = new TimeSpan(0, 0, 0, 0, System.Threading.Timeout.Infinite);
#else
        private static readonly TimeSpan InfiniteTimeout = System.Threading.Timeout.InfiniteTimeSpan;
#endif

        private static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(100);
        private const long MaxBufferSize = Int32.MaxValue; // Ideally this should be HttpContent.MaxBufferSize but that's an internal member.
        private static readonly TimeSpan MaxTimeout = TimeSpan.FromMilliseconds(int.MaxValue);

        private volatile HttpClient client;
        private volatile bool disposed;
        private bool disposeHandler;
        private CancellationTokenSource pendingRequestsCts;
        private RouteTable routeTable;
        private TimeSpan timeout;
        #endregion

        #region events
        #endregion

        #region constructors

        #region RestClient()
        public RestClient()
            : this(new HttpClientHandler())
        {
        }
        #endregion

        #region RestClient(HttpMessageHandler handler)
        public RestClient(HttpMessageHandler handler)
            : this(handler, true)
        {
        }
        #endregion

        #region RestClient(HttpMessageHandler handler, bool disposeHandler)
        public RestClient(HttpMessageHandler handler, bool disposeHandler)
        {
            this.disposeHandler = disposeHandler;
            this.timeout = RestClient.DefaultTimeout;
            this.pendingRequestsCts = new CancellationTokenSource();
            this.routeTable = RouteTable.Create(this.GetType());
            this.client = new HttpClient(handler, this.disposeHandler);
        }
        #endregion

        #endregion

        #region properties

        public HttpRequestHeaders DefaultRequestHeaders
        {
            get
            {
                return this.client.DefaultRequestHeaders;
            }
        }

        #region Timeout
        /// <summary>
        /// Gets or sets the number of milliseconds to wait before the request times out.
        /// </summary>
        /// <value>The number of milliseconds to wait before the request times out.</value>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// The timeout specified is less than or equal to zero and is not <see cref="System.Threading.Timeout.Infinite"/></exception>
        public TimeSpan Timeout
        {
            get
            {
                return this.client.Timeout;
            }
            set
            {
                this.client.Timeout = value;
            }
        }
        #endregion

        #endregion

        #region operators
        #endregion

        #region methods

        private static RouteValueDictionary AnonymousObjectToRouteValueDictionary(object routeValues)
        {
            var routeValueDictionary = routeValues as RouteValueDictionary;
            if (routeValueDictionary != null)
            {
                return routeValueDictionary;
            }

            var dictionary = routeValues as IDictionary<string, object>;
            if (dictionary != null)
            {
                return new RouteValueDictionary(dictionary);
            }

            return new RouteValueDictionary(routeValues);
        }

        #region CreateHttpRequestMessageForRoute

        private static HttpRequestMessage CreateHttpRequestMessageForRoute(Route route, string host, RouteValueDictionary routeValues, object data)
        {
            HttpRequestMessage request = null;
            if (route != null)
            {
                var url = route.GetFormattedRoute(host.IsNullOrWhiteSpace() ? route.Host : host, routeValues);
                request = new HttpRequestMessage(route.HttpMethod, url);
                foreach (var header in route.GetAcceptHeaders())
                {
                    request.Headers.Accept.Add(header);
                }

                if (data != null)
                {
                    var postData = JsonConvert.SerializeObject(data);
                    request.Content = new StringContent(postData, Encoding.UTF8, "application/json");
                }
            }

            return request;
        }

        protected HttpRequestMessage CreateHttpRequestMessageForRoute(object routeValues, [CallerMemberName]string routeName = null)
        {
            return CreateHttpRequestMessageForRoute(routeValues, null, routeName);
        }

        protected HttpRequestMessage CreateHttpRequestMessageForRoute(object routeValues, object data, [CallerMemberName]string routeName = null)
        {
            var routeData = AnonymousObjectToRouteValueDictionary(routeValues);
            return CreateHttpRequestMessageForRoute(this.routeTable[routeName], null, routeData, data);
        }

        protected HttpRequestMessage CreateHttpRequestMessageForRoute(RouteValueDictionary routeValues, [CallerMemberName]string routeName = null)
        {
            return CreateHttpRequestMessageForRoute(routeValues, null, routeName);
        }

        protected HttpRequestMessage CreateHttpRequestMessageForRoute(RouteValueDictionary routeValues, object data, [CallerMemberName]string routeName = null)
        {
            return CreateHttpRequestMessageForRoute(this.routeTable[routeName], null, routeValues, data);
        }

        protected HttpRequestMessage CreateHttpRequestMessageForRoute(string host, [CallerMemberName]string routeName = null)
        {
            return CreateHttpRequestMessageForRoute(host, null, null, routeName);
        }

        protected HttpRequestMessage CreateHttpRequestMessageForRoute(string host, object routeValues, [CallerMemberName]string routeName = null)
        {
            return CreateHttpRequestMessageForRoute(host, routeValues, null, routeName);
        }

        protected HttpRequestMessage CreateHttpRequestMessageForRoute(string host, object routeValues, object data, [CallerMemberName]string routeName = null)
        {
            var routeData = AnonymousObjectToRouteValueDictionary(routeValues);
            return CreateHttpRequestMessageForRoute(this.routeTable[routeName], host, routeData, data);
        }

        protected HttpRequestMessage CreateHttpRequestMessageForRoute(string host, RouteValueDictionary routeValues, object data, [CallerMemberName]string routeName = null)
        {
            return CreateHttpRequestMessageForRoute(this.routeTable[routeName], host, routeValues, data);
        }

        protected HttpRequestMessage CreateHttpRequestMessageForRoute(string host, RouteValueDictionary routeValues, [CallerMemberName]string routeName = null)
        {
            return CreateHttpRequestMessageForRoute(host, routeValues, null, routeName);
        }
        #endregion

        public void CancelPendingRequests()
        {
            CheckDisposed();

            // With every request we link this cancellation token source.
            CancellationTokenSource currentCts = Interlocked.Exchange(ref this.pendingRequestsCts, new CancellationTokenSource());

            currentCts.Cancel();
            currentCts.Dispose();
        }

        private void CheckDisposed()
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(GetType().ToString());
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !this.disposed)
            {
                // Cancel all pending requests (if any). Note that we don't call CancelPendingRequests() but cancel
                // the CTS directly. The reason is that CancelPendingRequests() would cancel the current CTS and create
                // a new CTS. We don't want a new CTS in this case.
                this.pendingRequestsCts.Cancel();
                this.pendingRequestsCts.Dispose();

                this.client.Dispose();
                this.disposed = true;
            }
        }

        protected async Task<TData> DeserializeResponseContentAsync<TData>(HttpResponseMessage response, CancellationToken cancellationToken)
        {
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return await await Task.Factory.StartNew(async () => JsonConvert.DeserializeObject<TData>(await response.Content.ReadAsStringAsync()), cancellationToken, TaskCreationOptions.None, TaskScheduler.Default);
        }

        #region SendAsync

        protected async Task<T> SendAsync<T>(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Requires.NotNull(request, nameof(request));

            CheckDisposed();
            var linkedCancellationToken = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, pendingRequestsCts.Token);
            var taskCompletionSource = new TaskCompletionSource<HttpResponseMessage>();

            try
            {
                SetTimeout(linkedCancellationToken);
                var response = await this.client.SendAsync(request, HttpCompletionOption.ResponseContentRead, linkedCancellationToken.Token);
                var result = await DeserializeResponseContentAsync<T>(response, linkedCancellationToken.Token);
                SetTaskCompleted(request, linkedCancellationToken, taskCompletionSource, response);
                return result;
            }
            catch (TaskCanceledException)
            {
                SetTaskCanceled(request, linkedCancellationToken, taskCompletionSource);
                var webException = new WebException(Resources.Strings.net_cancelled, WebExceptionStatus.RequestCanceled);
                throw new HttpRequestException(Resources.Strings.net_cancelled, webException);
            }
            catch (Exception e)
            {
                SetTaskFaulted(request, linkedCancellationToken, taskCompletionSource, e);
                var webException = new WebException(Resources.Strings.net_http_message_not_success_statuscode, WebExceptionStatus.SendFailure);
                throw new HttpRequestException(Resources.Strings.net_http_message_not_success_statuscode, webException);
            }
        }

        protected async Task<T> SendAsync<T>(object routeValues, CancellationToken cancellationToken, [CallerMemberName]string routeName = null)
        {
            return await SendAsync<T>(routeValues, null, cancellationToken, routeName);
        }

        protected async Task<T> SendAsync<T>(object routeValues, object data, CancellationToken cancellationToken, [CallerMemberName]string routeName = null)
        {
            T response;

            var route = this.routeTable[routeName];
            if (route != null)
            {
                response = await SendAsync<T>(route.Host, routeValues, data, cancellationToken, routeName);
            }
            else
            {
                response = default(T);
            }

            return response;
        }

        protected async Task<T> SendAsync<T>(object routeValues, [CallerMemberName]string routeName = null)
        {
            return await SendAsync<T>(routeValues, null, CancellationToken.None, routeName);
        }

        protected async Task<T> SendAsync<T>(string host, object routeValues, object data, CancellationToken cancellationToken, [CallerMemberName]string routeName = null)
        {
            T response;
            using (var request = CreateHttpRequestMessageForRoute(host, routeValues, data, routeName))
            {
                if (request != null)
                {
                    response = await SendAsync<T>(request, cancellationToken);
                }
                else
                {
                    response = default(T);
                }
            }

            return response;
        }

        protected async Task<T> SendAsync<T>(string host, object routeValues, object data, [CallerMemberName]string routeName = null)
        {
            return await SendAsync<T>(host, routeValues, data, CancellationToken.None, routeName);
        }

        protected async Task<T> SendAsync<T>(string host, object routeValues, [CallerMemberName]string routeName = null)
        {
            return await SendAsync<T>(host, routeValues, null, CancellationToken.None, routeName);
        }

        protected async Task<T> SendAsync<T>(string host, [CallerMemberName]string routeName = null)
        {
            return await SendAsync<T>(host, null, null, CancellationToken.None, routeName);
        }
        #endregion

        private void SetTaskCanceled(HttpRequestMessage request, CancellationTokenSource cancellationTokenSource, TaskCompletionSource<HttpResponseMessage> tcs)
        {
            tcs.TrySetCanceled();
            cancellationTokenSource.Dispose();
        }

        private void SetTaskCompleted(HttpRequestMessage request, CancellationTokenSource cancellationTokenSource, TaskCompletionSource<HttpResponseMessage> tcs, HttpResponseMessage response)
        {
            tcs.TrySetResult(response);
            cancellationTokenSource.Dispose();
        }

        private void SetTaskFaulted(HttpRequestMessage request, CancellationTokenSource cancellationTokenSource, TaskCompletionSource<HttpResponseMessage> tcs, Exception e)
        {
            tcs.TrySetException(e);
            cancellationTokenSource.Dispose();
        }

        private void SetTimeout(CancellationTokenSource cancellationTokenSource)
        {
            Requires.NotNull(cancellationTokenSource, nameof(cancellationTokenSource));

            if (this.timeout != InfiniteTimeout)
            {
                cancellationTokenSource.CancelAfter(this.timeout);
            }
        }

        #endregion
    }
}
