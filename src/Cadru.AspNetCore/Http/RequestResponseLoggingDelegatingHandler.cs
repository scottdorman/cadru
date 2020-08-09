using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Cadru.AspNetCore.Resources;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Cadru.AspNetCore.Http
{
    /// <summary>
    /// A delegated handler which logs API endpoint requests, optionally
    /// including logging scope information.
    /// </summary>
    public class RequestResponseLoggingDelegatingHandler : DelegatingHandler
    {
        private readonly ILogger<RequestResponseLoggingDelegatingHandler> _logger;
        private readonly IRequestResponseLoggingScopeFactory _loggingScopeFactory;
        private readonly RequestResponseLoggingMiddlewareOptions _options;

        /// <summary>
        /// Creates a new instance of the <see
        /// cref="RequestResponseLoggingDelegatingHandler"></see> class.
        /// </summary>
        /// <param name="loggingScope"></param>
        /// <param name="optionsAccessor"></param>
        /// <param name="loggerFactory"></param>
        public RequestResponseLoggingDelegatingHandler(IOptions<RequestResponseLoggingMiddlewareOptions> optionsAccessor, IRequestResponseLoggingScopeFactory loggingScopeFactory, ILoggerFactory loggerFactory)
        {
            this._logger = loggerFactory.CreateLogger<RequestResponseLoggingDelegatingHandler>();
            this._options = optionsAccessor.Value;
            this._loggingScopeFactory = loggingScopeFactory;
        }

        /// <inheritdoc/>
        protected override async Task<HttpResponseMessage?> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            RequestResponseLoggingScope? scopeObject = null;
            if (this._options.IncludeScopes)
            {
                scopeObject = await this._loggingScopeFactory.ToScopeObjectAsync(request);
            }

            using (this._options.IncludeScopes ? this._logger.BeginScope(scopeObject?.ToLoggingScope()) : EmptyDisposable.Instance)
            {
                HttpResponseMessage? response = null;
                try
                {
                    response = await base.SendAsync(request, cancellationToken);
                }
                finally
                {
                    var logLevel = (response?.IsSuccessStatusCode ?? false) ? this._options.LogLevel : LogLevel.Error;
                    if (this._logger.IsEnabled(logLevel))
                    {
                        var requestJson = SerializeHttpRequestMessage(request);
                        var responseJson = await SerializeHttpResponseMessageAsync(response);
                        this._logger.Log(logLevel, Strings.Debugging_HttpMessages, requestJson, responseJson);
                    }
                }

                return response;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Pending>")]
        private static string SerializeHttpRequestMessage(HttpRequestMessage request)
        {
            string result;
            try
            {
                var data = new StringBuilder($"{request.Method} {request.RequestUri} ");
                if (request.Content?.Headers.ContentType != null)
                {
                    data.AppendFormat(Strings.Debugging_HttpMessages_ContentType, request.Content?.Headers.ContentType);
                }

                result = data.ToString();
            }
            catch (Exception e)
            {
                result = e.ToString();
            }

            return result;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Pending>")]
        private static async Task<string> SerializeHttpResponseMessageAsync(HttpResponseMessage? response)
        {
            string? result = null;
            if (response != null)
            {
                try
                {
                    var data = new StringBuilder(String.Format(Strings.Debugging_HttpMessage_Response, response.StatusCode, (int)response.StatusCode));
                    if (response.Headers.Location != null)
                    {
                        data.AppendFormat(Strings.Debugging_HttpMessages_Location, response.Headers.Location);
                    }

                    if (response.Content != null)
                    {
                        var responseBody = await response.Content.ReadAsStringAsync();
                        if (!String.IsNullOrWhiteSpace(responseBody))
                        {
                            data.AppendFormat(Strings.Debugging_HttpMessages_Body, responseBody);
                        }
                    }

                    result = data.ToString();
                }
                catch (Exception e)
                {
                    result = e.ToString();
                }
            }

            return result ?? Strings.Debugging_HttpMessages_EmptyResponse;
        }
    }

}
