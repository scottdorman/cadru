using System.Collections.Generic;

namespace Cadru.Caching
{
    public static partial class CacheKeys
    {
        private static readonly HashSet<CacheKey> keys = new HashSet<CacheKey>();

        public static IEnumerable<CacheKey> Keys => keys;

        public static CacheKey CreateKey(string prefix, params object[] data)
        {
            var key = new CacheKey(prefix, data);
            keys.Add(key);
            return key;
        }

        public static CacheKey CreateKey(string prefix)
        {
            var key = new CacheKey(prefix);
            keys.Add(key);
            return key;
        }
    }
}
