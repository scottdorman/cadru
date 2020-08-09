using System;
using System.Collections.Generic;

namespace Cadru.Caching
{
    public static partial class CacheKeys
    {
        private static readonly HashSet<CacheKey> keys = new HashSet<CacheKey>();

        public static int Count => keys.Count;

        public static IEnumerable<CacheKey> Keys => keys;

        public static CacheKey Add(string prefix, params object[] data)
        {
            return Add(new CacheKey(prefix, data));
        }

        public static CacheKey Add(string prefix)
        {
            return Add(new CacheKey(prefix));
        }

        public static CacheKey Add(CacheKey item)
        {
            keys.Add(item);
            return item;
        }

        public static void Clear()
        {
            keys.Clear();
        }

        public static bool Contains(CacheKey item)
        {
            return keys.Contains(item);
        }

        public static bool Remove(CacheKey item)
        {
            return keys.Remove(item);
        }

        public static int RemoveWhere(Predicate<CacheKey> match)
        {
            return keys.RemoveWhere(match);
        }

        public static void TrimExcess()
        {
            keys.TrimExcess();
        }
    }
}
