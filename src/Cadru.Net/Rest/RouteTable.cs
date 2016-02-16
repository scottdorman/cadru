//------------------------------------------------------------------------------
// <copyright file="RouteTable.cs"
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
    using System.Collections.Concurrent;
    using System.Globalization;
    using System.Reflection;
    using Cadru.Contracts;
    using Cadru.Extensions;
    using Cadru.Portability;

    /// <summary>
    /// Represents a collection of routes that map between a method name the API endpoint.
    /// </summary>
    internal sealed class RouteTable
    {
        #region fields
        private ConcurrentDictionary<string, Route> map = new ConcurrentDictionary<string, Route>(StringComparer.OrdinalIgnoreCase);
        private Uri _baseAddress;
        #endregion

        #region events
        #endregion

        #region constructors
        /// <summary>
        /// Prevents a default instance of the <see cref="RouteTable"/> class from being created.
        /// </summary>
        private RouteTable()
        {
        }

        #endregion

        #region properties

        public Uri BaseAddress
        {
            get { return _baseAddress; }
            set
            {
                CheckBaseAddress(value, "value");
                _baseAddress = value;
            }
        }

        #region Item
        /// <summary>
        /// Gets the route associated with the specified name.
        /// </summary>
        /// <param name="name">The name of route to get.</param>
        /// <returns>The route at the specified name or <see langword="null"/>
        /// if the route doesn't exist or <paramref name="name"/> is
        /// <see langword="null"/> or an empty string.
        /// </returns>
        public Route this[string name]
        {
            get
            {
                Route route = null;
                if (!String.IsNullOrWhiteSpace(name))
                {
                    map.TryGetValue(name, out route);
                }

                return route;
            }
        }
        #endregion

        #endregion

        #region operators
        #endregion

        #region methods

        private static void CheckBaseAddress(Uri baseAddress, string parameterName)
        {
            if (baseAddress == null)
            {
                return; // It's OK to not have a base address specified.
            }

            if (!baseAddress.IsAbsoluteUri)
            {
                throw new ArgumentException(Resources.Strings.net_http_client_absolute_baseaddress_required, parameterName);
            }


            if (!HttpUtilities.IsHttpUri(baseAddress))
            {
                throw new ArgumentException(Resources.Strings.net_http_client_http_baseaddress_required, parameterName);
            }
        }


        #region Create
        /// <summary>
        /// Creates a new <see cref="RouteTable"/> instance from the specified type.
        /// </summary>
        /// <param name="type">The type used to create the route table.</param>
        /// <returns>A new <see cref="RouteTable"/> instance.</returns>
        public static RouteTable Create(Type type)
        {
            var routeTable = new RouteTable();

            foreach (var method in type.GetDeclaredMethods())
            {
                var routeAttribute = method.GetCustomAttribute<RouteAttribute>();
                if (routeAttribute != null)
                {
                    routeTable.Add(method.Name, new Route(routeAttribute));
                }
            }

            return routeTable;
        }
        #endregion

        #region Add
        /// <summary>
        /// Adds the named route to the route table.
        /// </summary>
        /// <param name="name">The name of the route to add.</param>
        /// <param name="item">The <see cref="Route"/> instance to add.</param>
        /// <exception cref="System.ArgumentException">The route table already
        /// contains a route for the given name.</exception>
        public void Add(string name, Route item)
        {
            Requires.NotNullOrWhiteSpace(name, nameof(name));
            Requires.NotNull(item, nameof(item));

            if (!this.map.TryAdd(name, item))
            {
                throw new ArgumentException(
                    String.Format(CultureInfo.CurrentUICulture, "SR.RouteCollection_DuplicateName", name), "name");
            }
        }
        #endregion

        #region Clear
        /// <summary>
        /// Removes all routes from the route table.
        /// </summary>
        public void Clear()
        {
            map.Clear();
        }
        #endregion

        #region Remove
        /// <summary>
        /// Removes the route with the specified key.
        /// </summary>
        /// <param name="name">The key of the route to remove.</param>
        /// <returns><see langword="true"/> if the route was removed;
        /// otherwise, <see langword="false"/>.</returns>
        public bool Remove(string name)
        {
            Requires.NotNullOrWhiteSpace(name, nameof(name));

            Route value;
            return map.TryRemove(name, out value);
        }
        #endregion

        #endregion
    }
}
