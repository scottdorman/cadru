//------------------------------------------------------------------------------
// <copyright file="RetryDelegatingHandler.cs"
//  company="Scott Dorman"
//  library="Cadru">
//    Copyright (C) 2001-2015 Scott Dorman.
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

namespace Cadru.Net.Http
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Cadru.Net.Extensions;

    /// <summary>
    /// An HTTP handler that allows a request to be retried.
    /// </summary>
    [CLSCompliant(false)]
    public class RetryHandler : DelegatingHandler
    {
        #region fields
        private HttpRetryPolicy retryPolicy;
        #endregion

        #region constructors

        #region RetryHandler()
        /// <summary>
        /// Initializes a new instance of the <see cref="RetryHandler"/> class
        /// with a specific inner handler.
        /// </summary>
        public RetryHandler()
            : this(new HttpRetryPolicy())
        {
        }
        #endregion

        #region RetryHandler(HttpRetryPolicy retryPolicy)
        /// <summary>
        /// Initializes a new instance of the <see cref="RetryHandler"/> class
        /// with a specific inner handler.
        /// </summary>
        /// <param name="retryPolicy">The <see cref="HttpRetryPolicy"/> which is responsible for determining how the request will be retried.</param>
        public RetryHandler(HttpRetryPolicy retryPolicy)
            : base()
        {
            Contracts.Requires.NotNull(retryPolicy, nameof(retryPolicy));

            this.retryPolicy = retryPolicy;
        }
        #endregion

        #endregion

        #region properties
        #endregion

        #region methods
        /// <summary>
        /// Sends an HTTP request to the inner handler to send to the server as an asynchronous operation.
        /// </summary>
        /// <param name="request">The HTTP request message to send to the server.</param>
        /// <param name="cancellationToken">A cancellation token to cancel operation.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = null;
            await this.retryPolicy.ExecuteAsync(async () =>
            {
                response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
                response = response.EnsureSuccessStatusCodeWithData();

                return response;

            }, cancellationToken).ConfigureAwait(false);

            return response;
        }
        #endregion
    }
}