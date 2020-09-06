//------------------------------------------------------------------------------
// <copyright file="IFilter.cs"
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
    /// Represents common features of an SCIM filter expression or filter group.
    /// </summary>
    public interface IFilter
    {
        /// <summary>
        /// Returns a string that represents the current <see
        /// cref="FilterExpression"></see> as a valid query
        /// </summary>
        /// <param name="prependQuerySeprator">To prepend the "?" query string
        /// separator, <see langword="true"></see>; otherwise, <see
        /// langword="false"></see>.</param>
        /// <returns>A string that represents the current <see
        /// cref="FilterExpression"></see>.</returns>
        string ToFilterExpression(bool prependQuerySeprator = true);
    }
}