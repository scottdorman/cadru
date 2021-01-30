using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

namespace Cadru.AspNetCore.Http
{
    /// <summary>
    /// Provides methods for creating a
    /// <see cref="RequestResponseLoggingScope"/> from an
    /// <see cref="HttpRequestMessage"/> or <see cref="HttpRequest"/>.
    /// </summary>
    public class RequestResponseLogginScopeFactory : IRequestResponseLoggingScopeFactory
    {
        /// <inheritdoc/>
        public virtual async Task<RequestResponseLoggingScope> ToScopeObjectAsync(HttpRequestMessage requestMessage)
        {
            return await Task.FromResult(new RequestResponseLoggingScope
            {
                AdditionalItems =
                {
                    new KeyValuePair<string, string>("httpMethod", requestMessage?.Method?.Method ?? HttpMethod.Get.Method),
                    new KeyValuePair<string, string>("contentType", requestMessage?.Content?.Headers?.ContentType?.MediaType ?? String.Empty)
                },
            });
        }

        /// <inheritdoc/>
        public virtual async Task<RequestResponseLoggingScope> ToScopeObjectAsync(HttpRequest request)
        {
            return await Task.FromResult(new RequestResponseLoggingScope
            {
                AdditionalItems =
                {
                    new KeyValuePair<string, string>("httpMethod", request?.Method ?? HttpMethod.Get.Method),
                    new KeyValuePair<string, string>("contentType", request?.ContentType ?? String.Empty)
                },
            });
        }


        /// <inheritdoc/>
        public virtual async Task<RequestResponseLoggingScope> ToScopeObjectAsync(HttpContext httpContext)
        {
            return await this.ToScopeObjectAsync(httpContext.Request);
        }

    }
}
