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
    public sealed class DefaultRequestResponseLoggingScopeFactory : RequestResponseLoggingScopeFactory
    {
    }
}
