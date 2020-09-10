//------------------------------------------------------------------------------
// <copyright file="FilterGroupExtensions.cs"
//  company="Scott Dorman"
//  library="Cadru">
//    Copyright (C) 2001-2020 Scott Dorman.
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

namespace Cadru.Scim.Filters
{
    /// <summary>
    /// Extension methods for working with <see cref="IFilterGroup"></see> instances.
    /// </summary>
    public static class FilterGroupExtensions
    {
        /// <summary>
        /// Add a new <see cref="IFilterExpression"></see> to the <see
        /// cref="IFilterGroup"></see>.
        /// </summary>
        /// <param name="filterGroup">A <see cref="IFilterGroup"></see> to
        /// modify.</param>
        /// <param name="filter">An <see cref="IFilterExpression"></see> to
        /// add.</param>
        /// <returns>A reference to the <paramref name="filterGroup" /> after the
        /// operation has completed. </returns>
        public static IFilterGroup AddExpression(this IFilterGroup filterGroup, IFilterExpression filter)
        {
            filterGroup.Filters.Add(filter);
            return filterGroup;
        }

        /// <summary>
        /// Add a new <see cref="IFilterGroup"></see> to the <see cref="IFilterGroup"></see>.
        /// </summary>
        /// <param name="filterGroup">A <see cref="IFilterGroup"></see> to modify.</param>
        /// <param name="group">An <see cref="IFilterGroup"></see> to
        /// add.</param>
        /// <returns>A reference to the <paramref name="filterGroup" /> after the
        /// operation has completed. </returns>
        public static IFilterGroup AddGroup(this IFilterGroup filterGroup, IFilterGroup group)
        {
            filterGroup.Filters.Add(group);
            return filterGroup;
        }
    }
}