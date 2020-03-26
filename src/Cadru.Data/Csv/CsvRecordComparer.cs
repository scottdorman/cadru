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
                throw new ArgumentOutOfRangeException("field", field, String.Format(CultureInfo.InvariantCulture, Resources.Strings.FieldIndexOutOfRange, field));
            }

            Field = field;
            Direction = direction;
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
            Debug.Assert(x != null && y != null && x.Length == y.Length && Field < x.Length);

            var result = String.Compare(x[Field], y[Field], StringComparison.CurrentCulture);

            return (Direction == ListSortDirection.Ascending ? result : -result);
        }
    }
#endif
}