//------------------------------------------------------------------------------
// <copyright file="routebase.cs"
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

using Cadru.Net.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Cadru.Net.Rest
{
    /// <summary>
    /// Class Route.
    /// </summary>
    internal sealed class Route
    {
        #region fields
        private RouteAttribute attribute;
        #endregion

        #region events
        #endregion

        #region constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Route" /> class.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        public Route(RouteAttribute attribute)
        {
            this.attribute = attribute;
            this.HttpMethod = attribute.GetHttpMethod();
        }
        #endregion

        #region properties
        /// <summary>
        /// Gets the host.
        /// </summary>
        /// <value>The host.</value>
        public string Host
        {
            get
            {
                return this.attribute.Host;
            }
        }

        /// <summary>
        /// Gets the path.
        /// </summary>
        /// <value>The path.</value>
        public string Path
        {
            get
            {
                return this.attribute.Path;
            }
        }

        /// <summary>
        /// Gets the query.
        /// </summary>
        /// <value>The query.</value>
        public string Query
        {
            get
            {
                return this.attribute.QueryString;
            }
        }

        /// <summary>
        /// Gets the HTTP method.
        /// </summary>
        /// <value>The HTTP method.</value>
        public HttpMethod HttpMethod { get; }
        #endregion

        #region operators
        #endregion

        #region methods
        /// <summary>
        /// Replaces the tokens.
        /// </summary>
        /// <param name="tokenizedString">The tokenized string.</param>
        /// <param name="routeData">The route data.</param>
        /// <returns>System.String.</returns>
        private string ReplaceTokens(string tokenizedString, RouteValueDictionary routeData)
        {
            var result = String.Empty;
            if (!String.IsNullOrWhiteSpace(tokenizedString))
            {
                var builder = new StringBuilder(tokenizedString);
                if (routeData != null)
                {
                    foreach (var token in routeData)
                    {
                        builder.Replace($"{{{token.Key}}}", token.Value.ToString());
                    }
                }

                result = builder.ToString();
            }

            return result;
        }

        /// <summary>
        /// Gets any Accept headers for the route.
        /// </summary>
        /// <returns>A collection of <see cref="MediaTypeWithQualityHeaderValue" />
        /// instances containing the Accept headers.</returns>
        internal IEnumerable<MediaTypeWithQualityHeaderValue> GetAcceptHeaders()
        {
            return attribute.GetAcceptHeaders();
        }

        /// <summary>
        /// Gets the route after the tokens have been replaced.
        /// </summary>
        /// <param name="url">The base URL for the route.</param>
        /// <param name="routeData">The route data.</param>
        /// <returns>System.String.</returns>
        internal string GetFormattedRoute(string url, RouteValueDictionary routeData)
        {
            var urlBuilder = new UrlBuilder(url)
            {
                Scheme = attribute.UseHttps ? UriScheme.Https : UriScheme.Http,
                Path = ReplaceTokens(this.Path, routeData),
                Query = ReplaceTokens(this.Query, routeData),
                Port = attribute.Port
            };

            return urlBuilder.ToString();
        }
        #endregion
    }
}