//------------------------------------------------------------------------------
// <copyright file="JsonRouteAttribute.cs"
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
    /// <summary>
    /// Class JsonRouteAttribute.
    /// </summary>
    public class JsonRouteAttribute : RouteAttribute
    {
        #region fields
        #endregion

        #region events
        #endregion

        #region constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonRouteAttribute"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="host">The host.</param>
        /// <param name="port">The port.</param>
        public JsonRouteAttribute(string path, string host, int port) : base(path, host, port)
        {
            this.AcceptHeaders = "application/json";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonRouteAttribute"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="host">The host.</param>
        public JsonRouteAttribute(string path, string host) : base(path, host, 80)
        {
            this.AcceptHeaders = "application/json";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonRouteAttribute"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public JsonRouteAttribute(string path) : base(path)
        {
            this.AcceptHeaders = "application/json";
        }
        #endregion

        #region properties
        #endregion

        #region operators
        #endregion

        #region methods
        #endregion
    }
}