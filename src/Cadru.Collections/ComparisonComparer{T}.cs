//------------------------------------------------------------------------------
// <copyright file="ComparisonComparer{T}.cs"
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
using System.Diagnostics.CodeAnalysis;

using Cadru.Contracts;

namespace Cadru.Collections
{
    /// <summary>
    /// Represents a <see cref="Comparer{T}"/> which uses a
    /// <see cref="Comparison{T}"/> as the basis for the comparison.
    /// </summary>
    /// <typeparam name="T">The type of the objects to compare.</typeparam>
    public class ComparisonComparer<T> : Comparer<T>
    {
        private readonly Comparison<T> comparison;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="ComparisonComparer{T}"/> class.
        /// </summary>
        /// <param name="comparison">The comparison to use.</param>
        protected ComparisonComparer(Comparison<T> comparison)
        {
            this.comparison = comparison;
        }

        /// <summary>
        /// Creates a comparer by using the specified comparison.
        /// </summary>
        /// <param name="comparison">The comparison to use.</param>
        /// <returns>The new comparer.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="comparison"/> is <see langword="null"/>.
        /// </exception>
        [SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "The type must be generic but the Create method shouldn't be.")]
        public new static Comparer<T> Create(Comparison<T> comparison)
        {
            Requires.NotNull(comparison, nameof(comparison));

            return new ComparisonComparer<T>(comparison);
        }

        /// <inheritdoc/>
        public override int Compare(T x, T y)
        {
            return this.comparison(x, y);
        }
    }
}