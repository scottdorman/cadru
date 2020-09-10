//------------------------------------------------------------------------------
// <copyright file="RequestResponseLoggingDelegatingHandler.cs"
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

using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Cadru.AspNetCore.Http.Internal;
using Cadru.AspNetCore.Resources;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Cadru.AspNetCore.Http
{
    /// <summary>
    /// A delegating handler which logs requests, optionally including logging
    /// scope information.
    /// </summary>
    public class RequestResponseLoggingDelegatingHandler : DelegatingHandler
    {
        private readonly ILogger<RequestResponseLoggingDelegatingHandler> _logger;
        private readonly IRequestResponseLoggingScopeFactory _loggingScopeFactory;
        private readonly RequestResponseLoggingOptions _options;
        private readonly IRequestResponseLoggingSerializer _requestResponseLoggingSerializer;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="RequestResponseLoggingDelegatingHandler"></see> class.
        /// </summary>
        /// <param name="optionsAccessor">The logging configuration options.</param>
        /// <param name="requestResponseLoggingSerializer">
        /// The serializer used for creating the log message content.
        /// </param>
        /// <param name="loggingScopeFactory">
        /// A factory instance for creating the scope object.
        /// </param>
        /// <param name="loggerFactory">
        /// An <see cref="ILoggerFactory"/> instance used to create a logger.
        /// </param>
        public RequestResponseLoggingDelegatingHandler(IOptions<RequestResponseLoggingOptions> optionsAccessor, IRequestResponseLoggingSerializer requestResponseLoggingSerializer, IRequestResponseLoggingScopeFactory loggingScopeFactory, ILoggerFactory loggerFactory)
        {
            this._options = optionsAccessor.Value;
            this._requestResponseLoggingSerializer = requestResponseLoggingSerializer;
            this._loggingScopeFactory = loggingScopeFactory;
            this._logger = loggerFactory.CreateLogger<RequestResponseLoggingDelegatingHandler>();
        }

        /// <inheritdoc/>
        protected override async Task<HttpResponseMessage?> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var loggingScope = Enumerable.Empty<KeyValuePair<string, string>>();

            if (this._options.CaptureScopes)
            {
                var scopeObject = await this._loggingScopeFactory.ToScopeObjectAsync(request);
                loggingScope = scopeObject.ToLoggingScope();
            }

            using (this._options.CaptureScopes ? this._logger.BeginScope(loggingScope) : EmptyDisposable.Instance)
            {
                HttpResponseMessage? response = null;
                try
                {
                    response = await base.SendAsync(request, cancellationToken);
                }
                finally
                {
                    var logLevel = response.IsSuccessStatusCode() ? this._options.LogLevel : LogLevel.Error;
                    if (this._logger.IsEnabled(logLevel))
                    {
                        var requestJson = this._requestResponseLoggingSerializer.SerializeRequest(request);
                        var responseJson = await this._requestResponseLoggingSerializer.SerializeResponseAsync(response);
                        this._logger.Log(logLevel, Strings.Debugging_HttpMessages, requestJson, responseJson);
                    }
                }

                return response;
            }
        }
    }
}