//------------------------------------------------------------------------------
// <copyright file="ReverseComparisonComparer(T).cs"
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

namespace Cadru.Collections
{
    using System;
    using System.Collections.Generic;
    using Contracts;

    /// <summary>
    /// Represents a <see cref="Comparer{T}"/> which uses a
    /// <see cref="Comparison{T}"/> as the basis for the comparison,
    /// but that reverses the comparison operation.
    /// </summary>
    /// <typeparam name="T">The type of the objects to compare.</typeparam>
    public class ReverseComparisonComparer<T> : Comparer<T>
    {
        private readonly Comparison<T> comparison;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReverseComparisonComparer{T}"/>
        /// class.
        /// </summary>
        /// <param name="comparison">The comparison to use.</param>
        protected ReverseComparisonComparer(Comparison<T> comparison)
        {
            this.comparison = comparison;
        }

        /// <summary>
        /// Creates a comparer by using the specified comparison.
        /// </summary>
        /// <param name="comparison">The comparison to use.</param>
        /// <returns>The new comparer.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="comparison"/> is <see langword="null"/>.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "The type must be generic but the Create method shouldn't be.")]
        public new static Comparer<T> Create(Comparison<T> comparison)
        {
            Requires.NotNull(comparison, nameof(comparison));

            return new ReverseComparisonComparer<T>(comparison);
        }

        /// <summary>
        /// Performs a comparison of two objects of the same type and returns
        /// a value indicating whether one object is less than, equal to, or
        /// greater than the other.
        /// </summary>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        /// <returns>
        /// A signed integer that indicates the relative values of x and y, as
        /// shown in the following table.
        /// <list type="table">
        /// <listheader>
        /// <term>Value</term>
        /// <term>Condition</term>
        /// </listheader>
        /// <item>
        /// <term>Less than zero</term>
        /// <description>
        /// <paramref name="y"/> is less than <paramref name="x"/>.
        /// </description>
        /// </item>
        /// <item>
        /// <term>Zero</term>
        /// <description>
        /// <paramref name="y"/> equals <paramref name="x"/>.
        /// </description>
        /// </item>
        /// <item>
        /// <term>Greater than zero</term>
        /// <description>
        /// <paramref name="y"/> is greater than <paramref name="x"/>.
        /// </description>
        /// </item>
        /// </list>
        /// </returns>
        public override int Compare(T x, T y)
        {
            return this.comparison(y, x);
        }
    }
}
