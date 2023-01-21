using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Cadru.Diagnostics
{
    public sealed class DictionaryDebugView<K, V> where K : notnull
    {
        private readonly IDictionary<K, V> _dict;

        public DictionaryDebugView(IDictionary<K, V> dictionary)
        {
            _dict = dictionary ?? throw new ArgumentNullException(nameof(dictionary));
        }

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public KeyValuePair<K, V>[] Items
        {
            get
            {
                KeyValuePair<K, V>[] items = new KeyValuePair<K, V>[_dict.Count];
                _dict.CopyTo(items, 0);
                return items;
            }
        }
    }
}
