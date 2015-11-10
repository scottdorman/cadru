//------------------------------------------------------------------------------
// <copyright file="Range(T).cs"
//  company="Scott Dorman"
//  library="Cadru">
//    Copyright (C) 2001-2015 Scott Dorman.
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
    using System.Globalization;
    using System.Text;
    using System.Collections;
    using Contracts;
    using Extensions;

    /// <summary>
    /// Represents a range, or interval, of values.
    /// </summary>
    /// <typeparam name="T">The type of the objects in the range.</typeparam>
    public sealed class Range<T> : IEnumerable<T>
    {
        #region fields
        private RangeEndpointOption option;
        private RangeIterator<T> enumerator;
        #endregion

        #region constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Range{T}"/> class.
        /// </summary>
        /// <param name="lowerBound">The start of the range.</param>
        /// <param name="upperBound">The end of the range.</param>
        public Range(T lowerBound, T upperBound) 
            : this(lowerBound, upperBound, Comparer<T>.Default)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Range{T}"/> class.
        /// </summary>
        /// <param name="lowerBound">The start of the range.</param>
        /// <param name="upperBound">The end of the range.</param>
        /// <param name="comparer">The <see cref="IComparer{T}"/> used to perform range comparisons.</param>
        public Range(T lowerBound, T upperBound, IComparer<T> comparer)
            : this(lowerBound, upperBound, comparer, RangeEndpointOption.Open)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Range{T}"/> class.
        /// </summary>
        /// <param name="lowerBound">The start of the range.</param>
        /// <param name="upperBound">The end of the range.</param>
        /// <param name="comparer">The <see cref="IComparer{T}"/> used to perform range comparisons.</param>
        /// <param name="option"></param>
        public Range(T lowerBound, T upperBound, IComparer<T> comparer, RangeEndpointOption option)
        {
            Requires.NotNull(comparer, nameof(comparer));
            Requires.IsTrue(comparer.Compare(lowerBound, upperBound) <= 0, Properties.Resources.ArgumentOutOfRange_UpperBound);

            this.LowerBound = lowerBound;
            this.UpperBound = upperBound;
            this.Comparer = comparer;
            this.option = option;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Range{T}"/> class.
        /// </summary>
        /// <param name="lowerBound">The start of the range.</param>
        /// <param name="upperBound">The end of the range.</param>
        /// <param name="option"></param>
        public Range(T lowerBound, T upperBound, RangeEndpointOption option)
            : this(lowerBound, upperBound, Comparer<T>.Default, option)
        {
        }
        #endregion

        #region events
        #endregion

        #region properties

        #region Comparer
        /// <summary>
        /// Gets the <see cref="IComparer{T}"/> used to compare the values in the range.
        /// </summary>
        /// <value>An <see cref="IComparer{T}"/> instance.</value>
        public IComparer<T> Comparer { get; private set; }
        #endregion

        internal bool IncludesLowerBound
        {
           get
            {
                return this.option == RangeEndpointOption.Open || this.option == RangeEndpointOption.LeftHalfOpen;
            }
        }

        internal bool IncludesUpperBound
        {
            get
            {
                return this.option == RangeEndpointOption.Open || this.option == RangeEndpointOption.RightHalfOpen;
            }
        }

        #region LowerBound
        /// <summary>
        /// Gets the start of the range.
        /// </summary>
        /// <value>The start of the range.</value>
        public T LowerBound
        {
            get;
            private set;
        }
        #endregion

        #region UpperBound
        /// <summary>
        /// Gets the end of the range.
        /// </summary>
        /// <value>The end of the range.</value>
        public T UpperBound
        {
            get;
            private set;
        }
        #endregion

        #endregion

        #region methods

        #region ComputeRangeOption
        private static RangeEndpointOption ComputeRangeOption(Range<T> left, Range<T> right)
        {
            var computed = RangeEndpointOption.Open;

            // Computed options follow these rules:
            //
            // (a,b) (c,d) => (e,f)
            // (a,b] (c,d) => (e,f)
            // [a,b) (c,d) => [e,f)
            // [a,b] (c,d) => [e,f)
            //
            // (a,b) (c,d) => (e,f)
            // (a,b) (c,d] => (e,f]
            // (a,b) [c,d) => (e,f)
            // (a,b) [c,d] => (e,f]

            switch (left.option)
            {
                default:
                case RangeEndpointOption.Open:
                    switch (right.option)
                    {
                        default:
                        case RangeEndpointOption.Open:
                        case RangeEndpointOption.RightHalfOpen:
                            computed = RangeEndpointOption.Open;
                            break;
                        case RangeEndpointOption.LeftHalfOpen:
                        case RangeEndpointOption.Closed:
                            computed = RangeEndpointOption.LeftHalfOpen;
                            break;
                    }
                    break;
                case RangeEndpointOption.LeftHalfOpen:
                    switch (right.option)
                    {
                        case RangeEndpointOption.Closed:
                        case RangeEndpointOption.LeftHalfOpen:
                            computed = RangeEndpointOption.LeftHalfOpen;
                            break;
                        default:
                        case RangeEndpointOption.Open:
                        case RangeEndpointOption.RightHalfOpen:
                            computed = RangeEndpointOption.Open;
                            break;
                    }
                    break;
                case RangeEndpointOption.RightHalfOpen:
                    switch (right.option)
                    {
                        case RangeEndpointOption.Closed:
                        case RangeEndpointOption.LeftHalfOpen:
                            computed = RangeEndpointOption.Closed;
                            break;
                        default:
                        case RangeEndpointOption.Open:
                        case RangeEndpointOption.RightHalfOpen:
                            computed = RangeEndpointOption.RightHalfOpen;
                            break;
                    }
                    break;
                case RangeEndpointOption.Closed:
                    switch (right.option)
                    {
                        case RangeEndpointOption.Closed:
                        case RangeEndpointOption.LeftHalfOpen:
                            computed = RangeEndpointOption.Closed;
                            break;
                        default:
                        case RangeEndpointOption.Open:
                        case RangeEndpointOption.RightHalfOpen:
                            computed = RangeEndpointOption.RightHalfOpen;
                            break;
                    }
                    break;
            }

            return computed;
        }
        #endregion

        #region Contains

        #region Contains(T value)
        /// <summary>
        /// Determines whether the <see cref="Range{T}"/> contains the specified value.
        /// </summary>
        /// <param name="value">The value to locate in the <see cref="Range{T}"/>.</param>
        /// <returns><see langword="true"/> if the <see cref="Range{T}"/> 
        /// contains the specified value; otherwise, <see langword="false"/>.</returns>
        public bool Contains(T value)
        {
            bool result = true;

            switch (this.option)
            {
                default:
                case RangeEndpointOption.Open:
                    result = (Comparer.Compare(this.LowerBound, value) <= 0) && (Comparer.Compare(this.UpperBound, value) >= 0);
                    break;
                case RangeEndpointOption.LeftHalfOpen:
                    result = (Comparer.Compare(this.LowerBound, value) <= 0) && (Comparer.Compare(this.UpperBound, value) > 0);
                    break;
                case RangeEndpointOption.RightHalfOpen:
                    result = (Comparer.Compare(this.LowerBound, value) < 0) && (Comparer.Compare(this.UpperBound, value) >= 0);
                    break;
                case RangeEndpointOption.Closed:
                    result = (Comparer.Compare(this.LowerBound, value) < 0) && (Comparer.Compare(this.UpperBound, value) > 0);
                    break;
            }

            return result;
        }
        #endregion

        #region Contains(Range<T> range)
        /// <summary>
        /// Determines whether the <see cref="Range{T}"/> contains the specified value.
        /// </summary>
        /// <param name="range">The value to locate in the <see cref="Range{T}"/>.</param>
        /// <returns><see langword="true"/> if the <see cref="Range{T}"/> 
        /// contains the specified value; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="range"/> is <see langword="null"/>.</exception>
        public bool Contains(Range<T> range)
        {
            Requires.NotNull(range, nameof(range));

            return this.Contains(range.LowerBound) && this.Contains(range.UpperBound);
        }
        #endregion

        #endregion

        #region GetEnumerator

        #region GetEnumerator()
        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="Range{T}"/>.
        /// </summary>
        /// <returns>An <see cref="IEnumerator{T}"/> object that can be used
        ///  to iterate through the <see cref="Range{T}"/>.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            Requires.IsTrue(this.enumerator.IsNotNull());
            return this.enumerator.GetEnumerator();
        }
        #endregion

        #region IEnumerable.GetEnumerator()
        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="Range{T}"/>.
        /// </summary>
        /// <returns>An <see cref="IEnumerator"/> object that can be used
        ///  to iterate through the <see cref="Range{T}"/>.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        #endregion

        #endregion

        #region Intersect
        /// <summary>
        /// Produces the intersection of two ranges.
        /// </summary>
        /// <param name="other">A <see cref="Range{T}"/> whose distinct elements
        /// that also appear in this instance will be returned.</param>
        /// <returns>A <see cref="Range{T}"/> that contains the elements that form the interval intersection
        /// of the two ranges, or <see langword="null"/> if there is no intersection.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="other"/> is <see langword="null"/>.</exception>
        public Range<T> Intersect(Range<T> other)
        {
            Requires.NotNull(other, nameof(other));
            Range<T> result = null;

            if (this.Overlaps(other))
            {
                T start = Comparer.Compare(this.LowerBound, other.LowerBound) > 0 ? this.LowerBound : other.LowerBound;
                T end = Comparer.Compare(other.UpperBound, this.UpperBound) < 0 ? other.UpperBound : this.UpperBound;
                result = new Range<T>(start, end, ComputeRangeOption(this, other));
            }

            return result;
        }
        #endregion

        #region IsContainedBy
        /// <summary>
        /// Determines whether the <see cref="Range{T}"/> is contained within the
        /// specified range.
        /// </summary>
        /// <param name="range">The range used to test for containment.</param>
        /// <returns><see langword="true"/> if the <see cref="Range{T}"/> 
        /// is contained within the specified range; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="range"/> is <see langword="null"/>.</exception>
        public bool IsContainedBy(Range<T> range)
        {
            Requires.NotNull(range, nameof(range));
            return range.Contains(this);
        }
        #endregion

        #region IsContiguousWith
        /// <summary>
        /// Determines whether the <see cref="Range{T}"/> is contiguous with the
        /// specified range.
        /// </summary>
        /// <param name="range">The range to check.</param>
        /// <returns><see langword="true"/> if the ranges are contiguous;
        /// otherwise, <see langword="false"/>.</returns>
        /// <remarks>Contiguous can mean containing, overlapping, or being next to.</remarks>
        /// <exception cref="ArgumentNullException"><paramref name="range"/> is <see langword="null"/>.</exception>
        public bool IsContiguousWith(Range<T> range)
        {
            if (Overlaps(range) || range.Overlaps(this) || range.Contains(this) || Contains(range))
            {
                return true;
            }

            // Once we remove overlapping and containing, only touching if available
            return UpperBound.Equals(range.LowerBound) || LowerBound.Equals(range.UpperBound);
        }
        #endregion

        #region Overlaps
        /// <summary>
        /// Determines whether the <see cref="Range{T}"/> overlaps the
        /// specified range.
        /// </summary>
        /// <param name="range">A range to test.</param>
        /// <returns><see langword="true"/> if the ranges overlap;
        /// otherwise, <see langword="false"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="range"/> is <see langword="null"/>.</exception>
        public bool Overlaps(Range<T> range)
        {
            Requires.NotNull(range, nameof(range));

            return (Contains(range.LowerBound) || Contains(range.UpperBound) || range.Contains(LowerBound) || range.Contains(UpperBound));
        }
        #endregion

        #region SetEnumerator
        /// <summary>
        /// Sets an enumerator that iterates through the <see cref="Range{T}"/>.
        /// </summary>
        /// <param name="enumerator">An <see cref="IEnumerator"/> object that
        /// can be used to iterate through the <see cref="Range{T}"/>.</param>
        public void SetEnumerator(RangeIterator<T> enumerator)
        {
            this.enumerator = enumerator;
        }
        #endregion

        #region ToString
        /// <summary>
        /// Returns a string representation of the <see cref="Range{T}"/>.
        /// </summary>
        /// <returns>The value of this <see cref="Range{T}"/>, formatted
        ///  using standard interval notation.
        /// </returns>
        public override string ToString()
        {
            return $"{(this.IncludesLowerBound ? "[" : "(")}{this.LowerBound}, {this.UpperBound}{(this.IncludesUpperBound ? "]" : ")")}";
        }
        #endregion

        #region Union
        /// <summary>
        /// Produces the union of two ranges.
        /// </summary>
        /// <param name="other">A <see cref="Range{T}"/> whose distinct elements
        /// form the second interval for the union.</param>
        /// <returns>A <see cref="Range{T}"/> that contains the elements that from both ranges,
        /// or <see langword="null"/> if there is no union.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="other"/> is <see langword="null"/>.</exception>
        public Range<T> Union(Range<T> other)
        {
            Requires.NotNull(other, nameof(other));
            Range<T> result = null;

            if (this.IsContiguousWith(other))
            {
                if (this.Contains(other))
                {
                    result = this;
                }
                else if (other.Contains(this))
                {
                    result = other;
                }
                else
                {
                    T start = Comparer.Compare(this.LowerBound, other.LowerBound) < 0 ? this.LowerBound : other.LowerBound;
                    T end = Comparer.Compare(other.UpperBound, this.UpperBound) > 0 ? other.UpperBound : this.UpperBound;
                    result = new Range<T>(start, end, ComputeRangeOption(this, other));
                }
            }

            return result;
        }
        #endregion

        #endregion
    }
}

