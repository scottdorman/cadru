//------------------------------------------------------------------------------
// <copyright file="RetryDelegatingHandler.cs"
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

namespace Cadru.Net.Http
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    using Cadru.TransientFaultHandling;

    /// <summary>
    /// An HTTP handler that allows a request to be retried.
    /// </summary>
    public class RetryDelegatingHandler : DelegatingHandler
    {
        #region fields
        #endregion

        /// <summary>
        /// An instance of a callback delegate that will be invoked whenever a retry condition is encountered.
        /// </summary>
        private event EventHandler<RetryingEventArgs> Retrying;

        #region constructors

        #region RetryHandler()
        /// <summary>
        /// Initializes a new instance of the <see cref="RetryDelegatingHandler"/> class.
        /// </summary>
        public RetryDelegatingHandler()
            : this(new HttpRetryPolicy())
        {
        }
        #endregion

        #region RetryHandler(DelegatingHandler innerHandler)
        /// <summary>
        /// Initializes a new instance of the <see cref="RetryDelegatingHandler"/> class
        /// with a specific inner handler.
        /// </summary>
        /// <param name="innerHandler">Inner http handler.</param>
        public RetryDelegatingHandler(DelegatingHandler innerHandler)
            : base(innerHandler)
        {
            this.RetryPolicy = new HttpRetryPolicy();
        }
        #endregion

        #region RetryHandler(HttpRetryPolicy retryPolicy)
        /// <summary>
        /// Initializes a new instance of the <see cref="RetryDelegatingHandler"/> class.
        /// </summary>
        /// <param name="retryPolicy">The <see cref="HttpRetryPolicy"/> which is responsible for determining how the request will be retried.</param>
        public RetryDelegatingHandler(HttpRetryPolicy retryPolicy)
            : base()
        {
            Contracts.Requires.NotNull(retryPolicy, nameof(retryPolicy));

            this.RetryPolicy = retryPolicy;
        }
        #endregion

        #region RetryHandler(HttpRetryPolicy retryPolicy, DelegatingHandler innerHandler)
        /// <summary>
        /// Initializes a new instance of the <see cref="RetryDelegatingHandler"/> class
        /// with a specific inner handler.
        /// </summary>
        /// <param name="retryPolicy">The <see cref="HttpRetryPolicy"/> which is responsible for determining how the request will be retried.</param>
        /// <param name="innerHandler">Inner http handler.</param>
        public RetryDelegatingHandler(HttpRetryPolicy retryPolicy, DelegatingHandler innerHandler)
            : base(innerHandler)
        {
            Contracts.Requires.NotNull(retryPolicy, nameof(retryPolicy));

            this.RetryPolicy = retryPolicy;
        }
        #endregion


        #endregion

        #region properties
        /// <summary>
        /// Gets or sets retry policy.
        /// </summary>
        public RetryPolicy RetryPolicy { get; set; }
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
            this.RetryPolicy.Retrying += (sender, args) => Retrying?.Invoke(sender, args);

            HttpResponseMessage responseMessage = null;
            try
            {
                await this.RetryPolicy.ExecuteAsync(async () =>
                {
                    responseMessage = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
                    if (!responseMessage.IsSuccessStatusCode)
                    {
                        // dispose the message unless we have stopped retrying
                        this.Retrying += (sender, args) =>
                        {
                            if (responseMessage != null)
                            {
                                responseMessage.Dispose();
                                responseMessage = null;
                            }
                        };

                        throw new HttpRequestWithStatusException(responseMessage);
                    }

                    return responseMessage;
                }, cancellationToken).ConfigureAwait(false);
                return responseMessage;
            }
            catch
            {
                if (responseMessage != null)
                {
                    return responseMessage;
                }
                else
                {
                    throw;
                }
            }
            finally
            {
                if (Retrying != null)
                {
                    foreach (EventHandler<RetryingEventArgs> d in Retrying.GetInvocationList())
                    {
                        Retrying -= d;
                    }
                }
            }
        }
    }
    #endregion
}