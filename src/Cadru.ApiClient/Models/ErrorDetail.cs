
using Newtonsoft.Json;

namespace Cadru.ApiClient.Models
{
    /// <summary>
    /// Represents a display-friendly error message
    /// </summary>
    public class ErrorDetail
    {
        /// <summary>
        /// The error message header.
        /// </summary>
        [JsonProperty("messageheader", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string? Header { get; set; }

        /// <summary>
        /// The error message content.
        /// </summary>
        [JsonProperty("message", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string? Message { get; set; }
    }
}
