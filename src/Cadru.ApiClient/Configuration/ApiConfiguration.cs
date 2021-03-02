using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Cadru.Extensions;

namespace Cadru.ApiClient.Configuration
{
    /// <summary>
    /// Represents common API configuration parameters.
    /// </summary>
    public abstract class ApiConfiguration : IApiConfiguration
    {
        /// <inheritdoc/>
        public Uri GetBaseUrl()
        {
            if (!String.IsNullOrWhiteSpace(this.BaseUrl))
            {
                return new Uri(this.BaseUrl!.EnsureTrailingCharacter('/'));
            }

            throw new InvalidOperationException();
        }

        /// <inheritdoc/>
        public string? BaseUrl { get; set; }
    }
}
