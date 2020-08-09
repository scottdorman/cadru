using System;
using System.Collections.Generic;

namespace Cadru.AspNetCore.Http
{
    /// <summary>
    /// An object that can be used to provide additional logging data through a
    /// logging scope.
    /// </summary>
    public class RequestResponseLoggingScope
    {
        /// <summary>
        /// Additional key-value pairs to be added to the logging scope.
        /// </summary>
        public IDictionary<string, string> AdditionalItems { get; } = new Dictionary<string, string>();

        /// <summary>
        /// The value of the Content-Type content header on an HTTP response.
        /// </summary>
        public string? ContentType { get; set; }

        /// <summary>
        /// The HTTP method used by the request message. The default is the GET method.
        /// </summary>
        public string HttpMethod { get; set; } = System.Net.Http.HttpMethod.Get.Method;

        /// <summary>
        /// Returns a collection of key-value pairs to be used in the logging scope.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<KeyValuePair<string, string>> ToLoggingScope()
        {
            yield return new KeyValuePair<string, string>("httpMethod", this.HttpMethod?.ToString() ?? System.Net.Http.HttpMethod.Get.Method);
            yield return new KeyValuePair<string, string>("contentType", this.ContentType ?? String.Empty);

            foreach (var additionalItem in this.AdditionalItems)
            {
                yield return additionalItem;
            }
        }
    }
}
