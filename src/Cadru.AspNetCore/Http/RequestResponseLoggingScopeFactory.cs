using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using Cadru.Core.Logging;

using Microsoft.AspNetCore.Http;

namespace Cadru.AspNetCore.Http
{
    /// <summary>
    /// Provides methods for creating a
    /// <see cref="LoggingScope"/> from an
    /// <see cref="HttpRequestMessage"/> or <see cref="HttpRequest"/>.
    /// </summary>
    public abstract class RequestResponseLoggingScopeFactory : IRequestResponseLoggingScopeFactory
    {
        /// <inheritdoc/>
        public virtual async Task<LoggingScope> ToScopeObjectAsync(HttpRequestMessage requestMessage)
        {
            return await Task.FromResult(new LoggingScope
            {
                AdditionalItems =
                {
                    new KeyValuePair<string, string>("httpMethod", requestMessage?.Method?.Method ?? HttpMethod.Get.Method),
                    new KeyValuePair<string, string>("contentType", requestMessage?.Content?.Headers?.ContentType?.MediaType ?? String.Empty)
                },
            });
        }

        /// <inheritdoc/>
        public virtual async Task<LoggingScope> ToScopeObjectAsync(HttpRequest request)
        {
            return await Task.FromResult(new LoggingScope
            {
                AdditionalItems =
                {
                    new KeyValuePair<string, string>("httpMethod", request?.Method ?? HttpMethod.Get.Method),
                    new KeyValuePair<string, string>("contentType", request?.ContentType ?? String.Empty)
                },
            });
        }

        /// <inheritdoc/>
        public virtual async Task<LoggingScope> ToScopeObjectAsync(HttpContext httpContext)
        {
            return await this.ToScopeObjectAsync(httpContext.Request);
        }
    }
}
