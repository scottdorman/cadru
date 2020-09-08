//------------------------------------------------------------------------------
// <copyright file="CacheKey.cs"
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
using System.Collections;
using System.Collections.Generic;
using System.Threading;

using Cadru.Contracts;

namespace Cadru.Caching
{
    /// <summary>
    /// An object which can be used as a key for caching.
    /// </summary>
    public class CacheKey
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CacheKey"/> class using
        /// the specified <paramref name="prefix"/> value.
        /// </summary>
        /// <param name="prefix">The value used as the cache key prefix.</param>
        /// <exception cref="ArgumentNullException"><paramref name="prefix"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="prefix"/> is a zero-length string.</exception>
        public CacheKey(string prefix) : this(prefix, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheKey"/> class using
        /// the specified <paramref name="prefix"/> value and the values in
        /// <paramref name="data"/> to form the key.
        /// </summary>
        /// <param name="prefix">The value used as the cache key prefix.</param>
        /// <param name="data">Additional values used to form the key.</param>
        /// <exception cref="ArgumentNullException"><paramref name="prefix"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="prefix"/> is a zero-length string.</exception>
        public CacheKey(string prefix, IEnumerable<object>? data)
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

        /// <summary>
        /// Gets or sets a <see cref="CancellationTokenSource"/> that can be
        /// used to invalidate the key.
        /// </summary>
        public CancellationTokenSource CancellationToken { get; set; } = new CancellationTokenSource();

        /// <summary>
        /// Gets the value of the cache key.
        /// </summary>
        public string Key { get; }

        private int Hash { get; }

        /// <inheritdoc/>
        public override bool Equals(object obj) => (obj is CacheKey key) ? this.Key == key.Key : this == obj;

        /// <inheritdoc/>
        public override int GetHashCode() => this.Hash;

        /// <inheritdoc/>
        public override string ToString() => this.Key;
    }
}