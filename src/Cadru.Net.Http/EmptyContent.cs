using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Cadru.Net.Http
{
    /// <summary>
    ///  Provides HTTP content based on an empty string.
    /// </summary>
    public static class EmptyContent
    {
        /// <summary>
        /// Creates a new instance of the <see cref="EmptyContent"/> class.
        /// </summary>
        /// <returns>A <see cref="HttpContent"/> instance.</returns>
        public static HttpContent Create() => new StringContent(String.Empty, Encoding.UTF8);

        /// <summary>
        /// Creates a new instance of the <see cref="EmptyContent"/> class.
        /// </summary>
        /// <param name="mediaType">The media type to use for the content.</param>
        /// <returns>A <see cref="HttpContent"/> instance.</returns>
        public static HttpContent Create(string? mediaType) => new StringContent(String.Empty, Encoding.UTF8, mediaType);
    }
}
