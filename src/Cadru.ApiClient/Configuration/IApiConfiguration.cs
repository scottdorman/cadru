using System;

namespace Cadru.ApiClient.Configuration
{
    /// <summary>
    /// Represents common API configuration parameters.
    /// </summary>
    public interface IApiConfiguration
    {
        /// <summary>
        /// Gets the base URL for the service, ensuring that it has a trailing '/' character.
        /// </summary>
        /// <returns>The base URL for the service with a trailing '/' character.</returns>
        Uri GetBaseUrl();

        /// <summary>
        /// The base URL for the service.
        /// </summary>
        string? BaseUrl { get; set; }
    }
}