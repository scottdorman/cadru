﻿//------------------------------------------------------------------------------
// <copyright file="FilterGroup.cs"
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

using System;
using System.Collections.Generic;

namespace Cadru.Scim.Filters
{
    /// <summary>
    /// Represents a group of <see cref="IFilter"></see> instances.
    /// </summary>
    public class FilterGroup : IFilterGroup
    {
        /// <inheritdoc/>
        public IList<IFilter> Filters { get; } = new List<IFilter>();

        /// <inheritdoc/>
        public GroupingCharacter GroupingCharacter { get; set; } = GroupingCharacter.Parentheses;

        /// <inheritdoc/>
        public FilterLogicalOperator LogicalOperator { get; set; } = FilterLogicalOperator.And;

        /// <inheritdoc/>
        public string ToFilterExpression(bool prependQuerySeprator = true)
        {
            return this.ToFilterExpression(new FilterExpressionFormatOptions { IncludeQuerySeparator = prependQuerySeprator });
        }

        /// <inheritdoc/>
        public string ToFilterExpression(FilterExpressionFormatOptions options)
        {
            return $@"{(options.IncludeQuerySeparator ? "?" : "")}{(options.IncludeFilterParameterName ? "filter=" : "")}{ this }";
        }

        /// <inheritdoc/>
        public override string? ToString()
        {
            var brackets = this.GroupingCharacter switch
            {
                GroupingCharacter.Parentheses => new string[] { "(", ")" },
                GroupingCharacter.SquareBracket => new string[] { "[", "]" },
                _ => new string[] { String.Empty, String.Empty }
            };

            return $"{ brackets[0] }{ String.Join($" { this.LogicalOperator } ", this.Filters) }{ brackets[1] }";
        }
    }
}