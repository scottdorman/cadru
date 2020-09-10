//------------------------------------------------------------------------------
// <copyright file="CsvRecordComparer.cs"
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
using System.ComponentModel;
using System.Globalization;

using Debug = System.Diagnostics.Debug;

namespace Cadru.Data.Csv
{
#if !NETSTANDARD1_3

    /// <summary>
    /// Represents a CSV record comparer.
    /// </summary>
    public class CsvRecordComparer : IComparer<string[]>
    {
        /// <summary>
        /// Initializes a new instance of the CsvRecordComparer class.
        /// </summary>
        /// <param name="field">The field index of the values to compare.</param>
        /// <param name="direction">The sort direction.</param>
        public CsvRecordComparer(int field, ListSortDirection direction)
        {
            if (field < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(field), field, String.Format(CultureInfo.InvariantCulture, Resources.Strings.FieldIndexOutOfRange, field));
            }

            this.Field = field;
            this.Direction = direction;
        }

        /// <summary>
        /// Contains the field index of the values to compare.
        /// </summary>
        public int Field { get; }

        /// <summary>
        /// Contains the sort direction.
        /// </summary>
        public ListSortDirection Direction { get; }

        public int Compare(string[] x, string[] y)
        {
            Debug.Assert(x != null && y != null && x.Length == y.Length && this.Field < x.Length);

            var result = String.Compare(x[this.Field], y[this.Field], StringComparison.CurrentCulture);

            return (this.Direction == ListSortDirection.Ascending ? result : -result);
        }
    }

#endif
}