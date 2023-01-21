//------------------------------------------------------------------------------
// <copyright file="ApiClient.cs"
//  company="Scott Dorman"
//  library="Cadru">
//    Copyright (C) 2001-2021 Scott Dorman.
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
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;

using Cadru.ApiClient.Models;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Cadru.ApiClient.Extensions;

namespace Cadru.ApiClient.Services
{
    /// <summary>
    /// Represents a strongly typed <see cref="HttpClient"/>.
    /// </summary>
    public abstract partial class ApiClient
    {
        /// <summary>
        /// The default <see cref="HttpCompletionOption"/> indicating
        /// that the operation should complete after reading the entire
        /// response including the content.
        /// </summary>
        protected const HttpCompletionOption DefaultCompletionOption = HttpCompletionOption.ResponseContentRead;

        /// <summary>
        /// Send an HTTP request as an asynchronous operation.
        /// </summary>
        /// <typeparam name="TData">The type of payload model.</typeparam>
        /// <param name="request">The HTTP request message to send.</param>
        /// <returns>The task object representing the asynchronous 
        /// operation.</returns>
        public async Task<IApiResult<TData>> SendAsync<TData>(HttpRequestMessage request)
            where TData : class =>
            await this.SendAsync<TData>(request, DefaultCompletionOption, CancellationToken.None);

        /// <summary>
        /// Send an HTTP request as an asynchronous operation.
        /// </summary>
        /// <typeparam name="TData">The type of payload model.</typeparam>
        /// <param name="request">The HTTP request message to send.</param>
        /// <param name="cancellationToken">The cancellation token to cancel
        /// operation.</param>
        /// <returns>The task object representing the asynchronous 
        /// operation.</returns>
        public async Task<IApiResult<TData>> SendAsync<TData>(HttpRequestMessage request, CancellationToken cancellationToken)
            where TData : class =>
            await this.SendAsync<TData>(request, DefaultCompletionOption, cancellationToken);

        /// <summary>
        /// Send an HTTP request as an asynchronous operation.
        /// </summary>
        /// <typeparam name="TData">The type of payload model.</typeparam>
        /// <param name="request">The HTTP request message to send.</param>
        /// <param name="completionOption">When the operation should complete 
        /// (as soon as a response is available or after reading the whole 
        /// response content).</param>
        /// <returns>The task object representing the asynchronous 
        /// operation.</returns>
        public async Task<IApiResult<TData>> SendAsync<TData>(HttpRequestMessage request, HttpCompletionOption completionOption)
            where TData : class =>
            await this.SendAsync<TData>(request, completionOption, CancellationToken.None);

        /// <summary>
        /// Send an HTTP request as an asynchronous operation.
        /// </summary>
        /// <typeparam name="TData">The type of payload model.</typeparam>
        /// <param name="request">The HTTP request message to send.</param>
        /// <param name="completionOption">When the operation should complete 
        /// (as soon as a response is available or after reading the whole 
        /// response content).</param>
        /// <param name="cancellationToken">The cancellation token to cancel
        /// operation.</param>
        /// <returns>The task object representing the asynchronous 
        /// operation.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// The request is <see langword="null"/>.
        /// </exception>
        /// <exception cref="System.InvalidOperationException">
        /// The request message was already sent by the 
        /// <see cref="System.Net.Http.HttpClient"/> instance.
        /// </exception>
        /// <exception cref="System.Net.Http.HttpRequestException">
        /// The request failed due to an underlying issue such as network 
        /// connectivity, DNS failure, server certificate validation or
        /// timeout.
        /// </exception>
        public async Task<IApiResult<TData>> SendAsync<TData>(HttpRequestMessage request, HttpCompletionOption completionOption, CancellationToken cancellationToken)
            where TData : class
        {
            IApiResult<TData>? result;

            try
            {
                var response = await this._httpClient.SendAsync(request, completionOption, cancellationToken);
                result = await this._responseParser.ParseAsync<TData>(response);
            }
            catch (Exception e)
            {
                this._logger.LogError(e, "An unexpected API error occurred.");
                result = ApiResult.ErrorResult<TData>(e.ToApiError());
            }

            return result;
        }
    }
}
