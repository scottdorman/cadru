//------------------------------------------------------------------------------
// <copyright file="RequestResponseLoggingMiddleware.cs"
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
using System.Threading.Tasks;

using Cadru.AspNetCore.Http.Internal;
using Cadru.AspNetCore.Resources;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IO;

namespace Cadru.AspNetCore.Http
{
    /// <summary>
    /// Provides support for logging requests, optionally including logging
    /// scope information.
    /// </summary>
    public class RequestResponseLoggingMiddleware
    {
        private readonly ILogger _logger;
        private readonly IRequestResponseLoggingScopeFactory _loggingScopeFactory;
        private readonly RequestDelegate _next;
        private readonly RequestResponseLoggingOptions _options;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;
        private readonly IRequestResponseLoggingSerializer _requestResponseLoggingSerializer;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="RequestResponseLoggingMiddleware"/> class.
        /// </summary>
        /// <param name="next"></param>
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
        public RequestResponseLoggingMiddleware(RequestDelegate next, IOptions<RequestResponseLoggingOptions> optionsAccessor, IRequestResponseLoggingSerializer requestResponseLoggingSerializer, IRequestResponseLoggingScopeFactory loggingScopeFactory, ILoggerFactory loggerFactory)
        {
            this._next = next;
            this._options = optionsAccessor.Value;
            this._loggingScopeFactory = loggingScopeFactory;
            this._logger = loggerFactory.CreateLogger<RequestResponseLoggingMiddleware>();
            this._requestResponseLoggingSerializer = requestResponseLoggingSerializer;
            this._recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }

        /// <summary>
        /// Optionally logs the request and response.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/> of the request.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            var loggingScope = Enumerable.Empty<KeyValuePair<string, string>>();
            if (this._options.CaptureScopes)
            {
                var scopeObject = await this._loggingScopeFactory.ToScopeObjectAsync(context.Request);
                loggingScope = scopeObject.AsEnumerable();
            }

            using (this._options.CaptureScopes ? this._logger.BeginScope(loggingScope) : EmptyDisposable.Instance)
            {
                try
                {
                    await this._next(context);
                }
                finally
                {
                    var logLevel = context.Response.IsSuccessStatusCode() ? this._options.LogLevel : LogLevel.Error;
                    if (this._logger.IsEnabled(logLevel))
                    {
                        using var responseStream = this._recyclableMemoryStreamManager.GetStream();
                        var requestJson = this._requestResponseLoggingSerializer.SerializeRequest(context.Request);
                        var responseJson = await this._requestResponseLoggingSerializer.SerializeResponseAsync(context.Response, responseStream);
                        this._logger.Log(this._options.LogLevel, Strings.Debugging_HttpMessages, requestJson, responseJson);
                    }
                }
            }
        }
    }
}