using System.Threading.Tasks;

using Cadru.AspNetCore.Http.Internal;
using Cadru.AspNetCore.Resources;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IO;

namespace Cadru.AspNetCore.Http
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;
        private readonly RequestResponseLoggingMiddlewareOptions _options;

        public RequestResponseLoggingMiddleware(RequestDelegate next, IOptions<RequestResponseLoggingMiddlewareOptions> optionsAccessor, ILoggerFactory loggerFactory)
        {
            this._next = next;
            this._options = optionsAccessor.Value;
            this._logger = loggerFactory.CreateLogger<RequestResponseLoggingMiddleware>();
            this._recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }

        public async Task Invoke(HttpContext context)
        {
            RequestResponseLoggingScope? scopeObject = null;
            if (this._options.IncludeScopes)
            {
                scopeObject = new RequestResponseLoggingScope
                {
                    HttpMethod = context.Request.Method,
                    ContentType = context.Request.ContentType
                };
            }

            using (this._options.IncludeScopes ? this._logger.BeginScope(scopeObject!.ToLoggingScope()) : EmptyDisposable.Instance)
            {
                try
                {
                    await this._next(context);
                }
                finally
                {
                    if (this._logger.IsEnabled(this._options.LogLevel))
                    {
                        using var responseStream = this._recyclableMemoryStreamManager.GetStream();
                        var requestJson = context.Request.SerializeHttpRequest();
                        var responseJson = await context.Response.SerializeHttpResponseAsync(responseStream);
                        this._logger.Log(this._options.LogLevel, Strings.Debugging_HttpMessages, requestJson, responseJson);
                    }
                }
            }
        }
    }
}
