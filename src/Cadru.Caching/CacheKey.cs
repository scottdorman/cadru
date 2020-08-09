using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

using Cadru.Contracts;

namespace Cadru.Caching
{
    public class CacheKey
    {
        private CacheKey()
        {
            this.CancellationToken = new CancellationTokenSource();
        }

        public CacheKey(string prefix) : this(prefix, null)
        {
            Requires.NotNullOrWhiteSpace(prefix, nameof(prefix));
        }

        public CacheKey(string prefix, IEnumerable<object> data) : this()
        {
            Requires.NotNullOrWhiteSpace(prefix, nameof(prefix));

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

                            keyBuilder.Add(String.Join("-", tempBuilder));
                        }
                        else
                        {
                            keyBuilder.Add(d.ToString());
                        }
                    }
                }
            }

            var cacheKey = String.Join("-", keyBuilder);
            this.Key = cacheKey;
            this.Hash = cacheKey.GetHashCode();
        }

        private int Hash { get; }

        public string Key { get; }

        public CancellationTokenSource CancellationToken { get; set; }

        public override bool Equals(object obj) => (obj is CacheKey key) ? this.Key == key.Key : base.Equals(obj);

        public override int GetHashCode() => this.Hash;

        public override string ToString() => this.Key;
    }
}
