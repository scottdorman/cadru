using Microsoft.Extensions.Logging;

namespace Cadru.AspNetCore.Http
{
    public class RequestResponseLoggingMiddlewareOptions
    {
        public bool IncludeScopes { get; set; } = true;
        public LogLevel LogLevel { get; set; } = LogLevel.Trace;
    }
}
