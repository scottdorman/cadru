//------------------------------------------------------------------------------
// <copyright file="RangeIterator(T).cs"
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

namespace Cadru.Collections
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using Contracts;

    /// <summary>
    /// Supports iteration over a <see cref="Range{T}"/>.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "Reviewed. Ending in Collection does not make semantic sense in this case.")]
    public class RangeIterator<T> : IEnumerable<T> where T : notnull
    {
        private readonly Range<T> range;
        private readonly Func<T, T> step;

        /// <summary>
        /// Initializes a new instance of the <see cref="RangeIterator{T}"/>
        /// class with the specified range with the step function.
        /// </summary>
        public RangeIterator(Range<T> range, Func<T, T> step)
        {
            Requires.NotNull(range, nameof(range));
            Requires.NotNull(step, nameof(step));

            this.range = range;
            this.step = step;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="Range{T}"/>.
        /// </summary>
        /// <returns>
        /// An <see cref="IEnumerator{T}"/> object that can be used to iterate
        /// through the <see cref="Range{T}"/>.
        /// </returns>
        public IEnumerator<T> GetEnumerator()
        {
            var current = this.range.IncludesLowerBound ? this.range.LowerBound : this.step(this.range.LowerBound);

            while (this.range.Contains(current))
            {
                yield return current;
                var next = this.step(current);

                // Handle a stepping function which wraps around from a value
                // near the end to one near the start; or a stepping function
                // which does nothing.
                if (this.range.Comparer.Compare(next, current) <= 0)
                {
                    yield break;
                }

                current = next;
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="Range{T}"/>.
        /// </summary>
        /// <returns>
        /// An <see cref="IEnumerator"/> object that can be used to iterate
        /// through the <see cref="Range{T}"/>.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}