using System.Net.Http;
using System.Threading.Tasks;

using Cadru.ApiClient.Models;

namespace Cadru.ApiClient.Services
{
    /// <summary>
    /// Represents an API response parser
    /// </summary>
    public interface IResponseParser
    {
        /// <summary>
        /// Parses the <paramref name="response"/> into an appropriate <see cref="IApiResult{TData}"/> instance.
        /// </summary>
        /// <typeparam name="TData">The type of payload model.</typeparam>
        /// <param name="response">The <see cref="HttpResponseMessage"/>.</param>
        /// <returns>An <see cref="IApiResult{TData}"/> instance.</returns>
        Task<IApiResult<TData>> ParseAsync<TData>(HttpResponseMessage response) where TData : class;
    }
}