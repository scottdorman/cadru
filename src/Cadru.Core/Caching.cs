using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web.Configuration;

using Cadru.Extensions;

using NLog;

using SABOL.Core.Models;

namespace SABOL.Core
{
    public static class Caching
    {
        private static HashSet<CacheKey> keys = new HashSet<CacheKey>();
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public class CacheKey
        {
            public int Hash { get; }

            public string Key { get; }

            public CacheKey()
            {
            }

            internal CacheKey(string prefix, int fiscalYear, IList<string> sectors) : this(prefix, new object[] { fiscalYear, sectors })
            {
            }

            internal CacheKey(string prefix) : this(prefix, null)
            {
            }

            internal CacheKey(string prefix, IEnumerable<object> data)
            {
                var keyBuilder = new List<string>
                {
                    prefix
                };

                if (data != null)
                {
                    foreach (var d in data)
                    {
                        if (d != null)
                        {
                            if (d is string s)
                            {
                                keyBuilder.Add(s);
                            }
                            else if (d is IEnumerable e)
                            {
                                var tempBuilder = new List<string>();

                                foreach (var element in e)
                                {
                                    tempBuilder.Add(element.ToString());
                                }

                                keyBuilder.Add(String.Join("-", tempBuilder.ToArray()));
                            }
                            else
                            {
                                keyBuilder.Add(d.ToString());
                            }
                        }
                    }
                }

                var cacheKey = keyBuilder.Join("-");
                this.Key = cacheKey;
                this.Hash = cacheKey.GetHashCode();
                logger.Debug("CacheKey created: ({0}, {1})", this.Key, this.Hash);

            }

            public override bool Equals(object obj) => (obj is CacheKey key) ? this.Key == key.Key : base.Equals(obj);

            public override int GetHashCode() => this.Hash;

            public override string ToString() => this.Key;
        }

        public static IEnumerable<CacheKey> Keys => keys;

        public static long GetCount()
        {
            return MemoryCache.Default.GetCount();
        }

        public static CacheKey CreateKey(string prefix, SettingsCookie settingsCookie)
        {
            var key = new CacheKey(prefix, settingsCookie.FiscalYear, settingsCookie.Sector);
            keys.Add(key);
            return key;
        }

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

        /// <summary>
        /// A generic method for getting and setting objects to the memory cache.
        /// </summary>
        /// <typeparam name="T">The type of the object to be returned.</typeparam>
        /// <param name="cacheItemName">The name to be used when storing this object in the cache.</param>
        /// <param name="cacheTimeInMinutes">How long to cache this object for.</param>
        /// <param name="data">The data to be cached.</param>
        /// <returns>An object of the type you asked for</returns>
        public static T Get<T>(CacheKey key)
        {
            var cache = MemoryCache.Default;
            var cachedObject = (T)cache[key.Key];

            logger.Debug("Cache {0} for {1}", cachedObject != null ? "hit" : "miss", key);
            return cachedObject;
        }

        public static IEnumerable<T> GetOrAddItem<T>(CacheKey key, string cacheProfileName, Func<IEnumerable<T>> action)
        {
            var cacheItem = Get<IEnumerable<T>>(key);

            IEnumerable<T> GetData()
            {
                IEnumerable<T> data;
                try
                {
                    data = action();
                    logger.Debug("Data retrieved for {0}", key);
                }
                catch (Exception e)
                {
                    data = Enumerable.Empty<T>();
                    logger.Error(e, "Error retrieving data for {0}", key);
                }

                Set(key, cacheProfileName, data);
                return data;
            }

            return cacheItem ?? GetData();
        }

        public static CacheItem GetCacheItem(CacheKey key)
        {
            return MemoryCache.Default.GetCacheItem(key.Key);
        }

        public static IEnumerable<CacheItem> GetCacheItems()
        {
            var cache = MemoryCache.Default;
            foreach (var key in keys)
            {
                yield return cache.GetCacheItem(key.Key);
            }
        }

        public static T Set<T>(CacheKey key, string cacheProfileName, T data)
        {
            var cache = MemoryCache.Default;
            cache.Set(key.Key, data, GetCacheItemPolicy(cacheProfileName, cache));
            keys.Add(key);

            logger.Debug("Data set for {0}", key);

            return (T)cache[key.Key];
        }

        public static void InvalidateAll()
        {
            var keysCopy = keys.ToList();
            foreach (var key in keysCopy)
            {
                Invalidate(key);
            }
        }

        public static void InvalidateAll(string cacheItemPrefix)
        {
            var keysCopy = keys.ToList();
            foreach (var key in keysCopy.Where(k => k.Key.StartsWith(cacheItemPrefix)))
            {
                Invalidate(key);
            }
        }

        public static void Invalidate(CacheKey key)
        {
            var cache = MemoryCache.Default;
            cache.Remove(key.Key);
            keys.Remove(key);
            logger.Debug("Cache invalidated for {0}", key.Key);
        }

        public static void Invalidate(string key)
        {
            var cacheKey = keys.SingleOrDefault(k => k.Key == key);
            Invalidate(cacheKey);
        }

        private static CacheItemPolicy GetCacheItemPolicy(string cacheProfileName, MemoryCache cache)
        {
            CacheItemPolicy policy = null;
            try
            {
                var configuration = WebConfigurationManager.OpenWebConfiguration("~");
                var outputCacheSettings = configuration.GetSection("system.web/caching/outputCacheSettings") as OutputCacheSettingsSection;
                var cacheProfile = outputCacheSettings.OutputCacheProfiles.Get(cacheProfileName);
                policy = new CacheItemPolicy
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(cacheProfile?.Duration ?? Constants.Cache.DefaultCacheTimeout)
                };
            }
            catch
            {
                policy = new CacheItemPolicy
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(Constants.Cache.DefaultCacheTimeout)
                };
            }

            //policy.ChangeMonitors.Add(cache.CreateCacheEntryChangeMonitor(new string[] { "mycachebreakerkey" })))
            return policy;
        }
    }
}
