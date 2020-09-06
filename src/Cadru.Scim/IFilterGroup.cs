//------------------------------------------------------------------------------
// <copyright file="IFilterGroup.cs"
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

using System.Collections.Generic;

namespace Cadru.Scim.Filters
{
    /// <summary>
    /// Represents a group of <see cref="IFilter"></see> instances.
    /// </summary>
    public interface IFilterGroup : IFilter
    {
        /// <summary>
        /// The collection of <see cref="IFilter"></see> instances to be
        /// grouped.
        /// </summary>
        /// <remarks>
        /// An <see cref="IFilterGroup"></see> may contain either <see
        /// cref="IFilterExpression"></see> instances or <see
        /// cref="IFilterGroup"></see> instances.
        /// </remarks>
        IList<IFilter> Filters { get; }

        /// <summary>
        /// The grouping character used.
        /// </summary>
        GroupingCharacter GroupingCharacter { get; set; }

        /// <summary>
        /// The logical grouping operator.
        /// </summary>
        FilterLogicalOperator LogicalOperator { get; set; }
    }
}