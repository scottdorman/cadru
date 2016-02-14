//------------------------------------------------------------------------------
// <copyright file="routeattribute.cs"
//  company="Scott Dorman"
//  library="Cadru">
//    Copyright (C) 2001-2016 Scott Dorman.
// </copyright>
//
// <license>
//    Licensed under the Microsoft Public License (Ms-PL) (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//    http://opensource.org/licenses/Ms-PL.html
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
// </license>
//------------------------------------------------------------------------------

namespace Cadru.Net.Rest
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;

    /// <summary>
    /// Class RouteAttribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class RouteAttribute : Attribute
    {
        #region fields
        #endregion

        #region events
        #endregion

        #region constructors

        #region RouteAttribute(string path, string host, int port)
        /// <summary>
        /// Initializes a new instance of the <see cref="RouteAttribute"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="host">The host.</param>
        /// <param name="port">The port.</param>
        public RouteAttribute(string path, string host, int port)
        {
            this.Host = host;
            this.Path = path;
            this.Port = port;
        }
        #endregion

        #region RouteAttribute(string path, string host) : this(route, host, 80)
        /// <summary>
        /// Initializes a new instance of the <see cref="RouteAttribute"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="host">The host.</param>
        public RouteAttribute(string path, string host) : this(path, host, 80)
        {
        }
        #endregion

        #region RouteAttribute(string path)
        /// <summary>
        /// Initializes a new instance of the <see cref="RouteAttribute"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public RouteAttribute(string path)
        {
            this.Path = path;
            this.Port = 80;
        }
        #endregion

        #endregion

        #region properties
        /// <summary>
        /// Gets the host.
        /// </summary>
        /// <value>The host.</value>
        public string Host { get; }
        /// <summary>
        /// Gets the path.
        /// </summary>
        /// <value>The path.</value>
        public string Path { get; }
        public string QueryString { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [use HTTPS].
        /// </summary>
        /// <value><c>true</c> if [use HTTPS]; otherwise, <c>false</c>.</value>
        public bool UseHttps { get; set; }
        /// <summary>
        /// Gets the port.
        /// </summary>
        /// <value>The port.</value>
        public int Port { get; }
        /// <summary>
        /// Gets or sets the method.
        /// </summary>
        /// <value>The method.</value>
        public RestMethod Method { get; set; }
        /// <summary>
        /// Gets or sets the accept headers.
        /// </summary>
        /// <value>The accept headers.</value>
        public string AcceptHeaders { get; set; }
        #endregion

        #region operators
        #endregion

        #region methods
        internal HttpMethod GetHttpMethod()
        {
            HttpMethod httpMethod = null;

            switch (this.Method)
            {
                case RestMethod.Post:
                    httpMethod = HttpMethod.Post;
                    break;

                case RestMethod.Put:
                    httpMethod = HttpMethod.Put;
                    break;

                case RestMethod.Delete:
                    httpMethod = HttpMethod.Delete;
                    break;

                default:
                    httpMethod = HttpMethod.Get;
                    break;
            }

            return httpMethod;
        }

        /// <summary>
        /// Gets the accept headers.
        /// </summary>
        /// <returns>IEnumerable&lt;MediaTypeWithQualityHeaderValue&gt;.</returns>
        internal IEnumerable<MediaTypeWithQualityHeaderValue> GetAcceptHeaders()
        {
            if (!String.IsNullOrWhiteSpace(this.AcceptHeaders))
            {
                foreach (var header in this.AcceptHeaders.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    MediaTypeWithQualityHeaderValue mediaType = null;
                    if (MediaTypeWithQualityHeaderValue.TryParse(header, out mediaType))
                    {
                        yield return mediaType;
                    }
                }
            }
        }
        #endregion
    }
}
