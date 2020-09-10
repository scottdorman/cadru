//------------------------------------------------------------------------------
// <copyright file="GroupingCharacter.cs"
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
    /// Indicates the grouping character used by an <see cref="IFilterGroup"></see>.
    /// </summary>
    public enum GroupingCharacter
    {
        /// <summary>
        /// No grouping characters will be used.
        /// </summary>
        None,

        /// <summary>
        /// Boolean expressions may be grouped using parentheses to change the
        /// standard order of operations.
        /// </summary>
        Parentheses,

        /// <summary>
        /// Service providers may support complex filters where expressions must
        /// be applied to the same value of a parent attribute. The expression
        /// with square brackets must be a valid filter expression based upon
        /// sub-attributes of the parent attribute. Nested expressions may be used.
        /// </summary>
        SquareBracket
    }
}