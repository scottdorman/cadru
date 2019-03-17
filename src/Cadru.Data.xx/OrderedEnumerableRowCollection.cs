using System.Collections.Generic;

namespace Cadru.Data
{
    /// <summary>
    /// This class provides a wrapper for DataTables representing an ordered sequence.
    /// </summary>
    public sealed class OrderedEnumerableRowCollection<TRow> : EnumerableRowCollection<TRow>
    {
        /// <summary>
        /// Copy Constructor that sets enumerableRows to the one given in the input
        /// </summary>
        internal OrderedEnumerableRowCollection(EnumerableRowCollection<TRow> enumerableTable, IEnumerable<TRow> enumerableRows)
            : base(enumerableTable, enumerableRows, null)
        {

        }
    }
}
