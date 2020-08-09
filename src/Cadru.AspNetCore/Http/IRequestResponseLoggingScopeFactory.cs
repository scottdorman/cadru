using System.Net.Http;
using System.Threading.Tasks;

namespace Cadru.AspNetCore.Http
{
    /// <summary>
    /// Represents a type used to create a <see
    /// cref="RequestResponseLoggingScope"/> instance from a <see
    /// cref="HttpRequestMessage"/>.
    /// </summary>
    public interface IRequestResponseLoggingScopeFactory
    {
        Task<RequestResponseLoggingScope> ToScopeObjectAsync(HttpRequestMessage requestMessage);
    }

}
