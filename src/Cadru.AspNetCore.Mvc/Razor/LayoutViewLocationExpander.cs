//------------------------------------------------------------------------------
// <copyright file="LayoutViewLocationExpander.cs"
//  company="Scott Dorman"
//  library="Cadru">
//    Copyright (C) 2001-2017 Scott Dorman.
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

using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc.Razor;

namespace Cadru.AspNetCore.Mvc.Razor
{
    /// <summary>
    /// An <see cref="IViewLocationExpander"/> that adds a Layout folder as a prefix to view names.
    /// </summary>
    public class LayoutViewLocationExpander : IViewLocationExpander
    {
        private const string ValueKey = "Layout";

        /// <inheritdoc/>
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (viewLocations == null)
            {
                throw new ArgumentNullException(nameof(viewLocations));
            }

            context.Values.TryGetValue(ValueKey, out var value);

            if (!String.IsNullOrEmpty(value))
            {
                return viewLocations;
            }

            return this.ExpandViewLocationsCore(viewLocations, value);
        }

        /// <inheritdoc/>
        public void PopulateValues(ViewLocationExpanderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.Values[ValueKey] = context.ActionContext.RouteData.Values[ValueKey]?.ToString();
        }

        private IEnumerable<string> ExpandViewLocationsCore(IEnumerable<string> viewLocations, string value)
        {
            foreach (var location in viewLocations)
            {
                yield return location.Replace("{0}", value + "/{0}");
                yield return location;
            }
        }
    }
}