//------------------------------------------------------------------------------
// <copyright file="RangeExtensions.cs"
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

    /// <summary>
    /// Provides basic methods for setting a default iterator on a <see cref="Range{T}"/>.
    /// </summary>
    public static class RangeExtensions
    {
        /// <summary>
        /// Sets a default iterator which increments the range to the next character.
        /// </summary>
        /// <param name="range">The range whose iterator is to be set.</param>
        public static Range<char> SetDefaultEnumerator(this Range<char> range)
        {
            range.SetEnumerator(new RangeIterator<char>(range, c => (char)(c + 1)));
            return range;
        }

        /// <summary>
        /// Sets a default iterator which increments the range by 1 byte.
        /// </summary>
        /// <param name="range">The range whose iterator is to be set.</param>
        public static Range<byte> SetDefaultEnumerator(this Range<byte> range)
        {
            range.SetEnumerator(new RangeIterator<byte>(range, x => (byte)(x + 1)));
            return range;
        }

        /// <summary>
        /// Sets a default iterator which increments the range by 1.
        /// </summary>
        /// <param name="range">The range whose iterator is to be set.</param>
        public static Range<short> SetDefaultEnumerator(this Range<short> range)
        {
            range.SetEnumerator(new RangeIterator<short>(range, x => (short)(x + 1)));
            return range;
        }

        /// <summary>
        /// Sets a default iterator which increments the range by 1.
        /// </summary>
        /// <param name="range">The range whose iterator is to be set.</param>
        public static Range<int> SetDefaultEnumerator(this Range<int> range)
        {
            range.SetEnumerator(new RangeIterator<int>(range, x => x + 1));
            return range;
        }

        /// <summary>
        /// Sets a default iterator which increments the range by 1.
        /// </summary>
        /// <param name="range">The range whose iterator is to be set.</param>
        public static Range<long> SetDefaultEnumerator(this Range<long> range)
        {
            range.SetEnumerator(new RangeIterator<long>(range, x => x + 1));
            return range;
        }

        /// <summary>
        /// Sets a default iterator which increments the range by 1.
        /// </summary>
        /// <param name="range">The range whose iterator is to be set.</param>
        public static Range<float> SetDefaultEnumerator(this Range<float> range)
        {
            range.SetEnumerator(new RangeIterator<float>(range, x => x + 1));
            return range;
        }

        /// <summary>
        /// Sets a default iterator which increments the range by 1.
        /// </summary>
        /// <param name="range">The range whose iterator is to be set.</param>
        public static Range<double> SetDefaultEnumerator(this Range<double> range)
        {
            range.SetEnumerator(new RangeIterator<double>(range, x => x + 1));
            return range;
        }

        /// <summary>
        /// Sets a default iterator which increments the range by 1.
        /// </summary>
        /// <param name="range">The range whose iterator is to be set.</param>
        public static Range<decimal> SetDefaultEnumerator(this Range<decimal> range)
        {
            range.SetEnumerator(new RangeIterator<decimal>(range, x => x + 1));
            return range;
        }

        /// <summary>
        /// Sets a default iterator which increments the range by 1.
        /// </summary>
        /// <param name="range">The range whose iterator is to be set.</param>
        public static Range<uint> SetDefaultEnumerator(this Range<uint> range)
        {
            range.SetEnumerator(new RangeIterator<uint>(range, x => x + 1));
            return range;
        }

        /// <summary>
        /// Sets a default iterator which increments the range by 1.
        /// </summary>
        /// <param name="range">The range whose iterator is to be set.</param>
        public static Range<ulong> SetDefaultEnumerator(this Range<ulong> range)
        {
            range.SetEnumerator(new RangeIterator<ulong>(range, x => x + 1));
            return range;
        }

        /// <summary>
        /// Sets a default iterator which increments the range by 1 day.
        /// </summary>
        /// <param name="range">The range whose iterator is to be set.</param>
        public static Range<DateTime> SetDefaultEnumerator(this Range<DateTime> range)
        {
            range.SetEnumerator(new RangeIterator<DateTime>(range, x => x.AddDays(1)));
            return range;
        }

        /// <summary>
        /// Sets a default iterator which increments the range by 1 day.
        /// </summary>
        /// <param name="range">The range whose iterator is to be set.</param>
        public static Range<DateTimeOffset> SetDefaultEnumerator(this Range<DateTimeOffset> range)
        {
            range.SetEnumerator(new RangeIterator<DateTimeOffset>(range, x => x.AddDays(1)));
            return range;
        }
    }
}