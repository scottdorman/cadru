//------------------------------------------------------------------------------
// <copyright file="CacheKeys.cs"
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

namespace Cadru.Caching
{
    /// <summary>
    /// Represents a set of <see cref="CacheKey"/see> instances.
    /// </summary>
    public static partial class CacheKeys
    {
        private static readonly HashSet<CacheKey> keys = new HashSet<CacheKey>();

        /// <summary>
        /// Gets the number of cache key elements in the set.
        /// </summary>
        /// <remarks>The number of elements that are contained in the set.</remarks>
        public static int Count => keys.Count;

        /// <summary>
        /// The set of cache key elements.
        /// </summary>
        public static IEnumerable<CacheKey> Keys => keys;


        /// <summary>
        /// Adds a new <see cref="CacheKey"/> instance to the set.
        /// </summary>
        /// <param name="prefix">The value used as the cache key prefix.</param>
        /// <param name="data">Additional values used to form the key.</param>
        /// <returns>The <see cref="CacheKey"/> instance if it was added to the
        /// set; otherwise <see langword="null"/>.</returns>
        public static CacheKey? Add(string prefix, params object[] data)
        {
            return Add(new CacheKey(prefix, data));
        }

        /// <summary>
        /// Adds the specified cache key to the set.
        /// </summary>
        /// <param name="prefix">The value used as the cache key prefix.</param>
        /// <returns>The <see cref="CacheKey"/> instance if it was added to the
        /// set; otherwise <see langword="null"/>.</returns>
        public static CacheKey? Add(string prefix)
        {
            return Add(new CacheKey(prefix));
        }

        /// <summary>
        /// Adds the specified cache key to the set.
        /// </summary>
        /// <param name="item">The <see cref="CacheKey"/> to add to the
        /// set.</param>
        /// <returns>The <see cref="CacheKey"/> instance if it was added to the
        /// set; otherwise <see langword="null"/>.</returns>
        public static CacheKey? Add(CacheKey item)
        {
            return keys.Add(item) ? item : null;
        }

        /// <summary>
        /// Removes all cache key elements from the set.
        /// </summary>
        public static void Clear()
        {
            keys.Clear();
        }

        /// <summary>
        /// Determines whether the set contains the specified cache key.
        /// </summary>
        /// <param name="item">The <see cref="CacheKey"/> to locate.</param>
        /// <returns><see langword="true"/> if the set contains the specified
        /// key; otherwise <see langword="false"/>.</returns>
        public static bool Contains(CacheKey item)
        {
            return keys.Contains(item);
        }

        /// <summary>
        /// Removes the specified cache key.
        /// </summary>
        /// <param name="item">>The <see cref="CacheKey"/> to remove.</param>
        /// <returns><see langword="true"/> if the cache key is successfully
        /// found and removed; otherwise <see langword="false"/>. This method
        /// returns <see langword="false"/> if <paramref name="item"/> is not
        /// found.</returns>
        public static bool Remove(CacheKey item)
        {
            return keys.Remove(item);
        }

        /// <summary>
        ///  Removes all cache keys that match the conditions defined by the
        ///  specified predicate.
        /// </summary>
        /// <param name="match">The <see cref="Predicate{T}"/> delegate that
        /// defines the conditions of the cache keys to remove.</param>
        /// <returns>The number of cache keys that were removed.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="match"/> is
        /// <see langword="null"/>.</exception>
        public static int RemoveWhere(Predicate<CacheKey> match)
        {
            return keys.RemoveWhere(match);
        }

        /// <summary>
        /// Sets the capacity to the actual number of elements contained,
        /// rounded up to a nearby value.
        /// </summary>
        public static void TrimExcess()
        {
            keys.TrimExcess();
        }
    }
}
