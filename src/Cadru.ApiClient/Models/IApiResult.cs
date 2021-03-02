using System.Collections.Generic;

namespace Cadru.ApiClient.Models
{
    /// <summary>
    /// Represents the response of an endpoint call.
    /// </summary>
    public interface IApiResult
    {
        /// <summary>
        /// A boolean value that indicates if the response is an error.
        /// </summary>
        bool IsError { get; }

        /// <summary>
        /// An <see cref="ApiError"/> instance representing the error from the response.
        /// </summary>
        ApiError? Error { get; }
    }


    /// <summary>
    /// Represents the response of an endpoint call.
    /// </summary>
    /// <typeparam name="TData">The type of payload model.</typeparam>
    public interface IApiResult<out TData> : IApiResult where TData : class
    {
        /// <summary>
        /// The returned response object.
        /// </summary>
        TData? Data { get; }
    }
}
