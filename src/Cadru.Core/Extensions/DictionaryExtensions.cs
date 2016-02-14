//------------------------------------------------------------------------------
// <copyright file="ArrayExtensions.cs"
//  company="Scott Dorman"
//  library="Cadru">
//    Copyright (C) 2001-2014 Scott Dorman.
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

namespace Cadru.Extensions
{
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Provides basic routines for common dictionary manipulation.
    /// </summary>
    public static class DictionaryExtensions
    {
        #region fields
        #endregion

        #region events
        #endregion

        #region constructors
        #endregion

        #region properties
        #endregion

        #region methods

        #region GetValueOrDefault
        /// <summary>
        /// Gets the value associated with the specified key or the provided
        /// default value if the key is not found.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
        /// <param name="dictionary">The source dictionary.</param>
        /// <param name="key">The key of the value to get.</param>
        /// <param name="defaultValue">The default value to be returned if key is not found.</param>
        /// <returns>If <paramref name="key"/> is found, the value associated with the
        /// specified key; otherwise, <paramref name="defaultValue"/>.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="key"/> is <see langword="null"/>.</exception>
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
        {
            Contracts.Requires.NotNull(dictionary, "dictionary");

            TValue result;
            return dictionary.TryGetValue(key, out result) ? result : defaultValue;
        }

        /// <summary>
        /// Gets the value associated with the specified key or the provided
        /// default value if the key is not found.
        /// </summary>
        /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
        /// <param name="dictionary">The source dictionary.</param>
        /// <param name="key">The key of the value to get.</param>
        /// <param name="defaultValue">The default value to be returned if key is not found.</param>
        /// <returns>If <paramref name="key"/> is found, the value associated with the
        /// specified key; otherwise, <paramref name="defaultValue"/>.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="key"/> is <see langword="null"/>.</exception>
        public static TValue GetValueOrDefault<TValue>(this IDictionary dictionary, object key, TValue defaultValue)
        {
            Contracts.Requires.NotNull(dictionary, "dictionary");

            TValue result;
            var temp = dictionary as Dictionary<object, TValue>;
            if (temp != null)
            {
                result = temp.GetValueOrDefault<object, TValue>(key, defaultValue);
            }
            else
            {
                result = dictionary.Contains(key) ? (TValue)dictionary[key] : defaultValue;
            }

            return result;
        }
        #endregion

        #region TryGetValueOrDefault
        /// <summary>
        /// Gets the value associated with the specified key or the provided
        /// default value if the key is not found.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
        /// <param name="dictionary">The source dictionary.</param>
        /// <param name="key">The key of the value to get.</param>
        /// <param name="defaultValue">The default value to be returned if key is not found.</param>
        /// <param name="value">When this method returns, contains the value associated with the specified
        ///  key, if the <paramref name="key"/> is found; otherwise, <paramref name="defaultValue"/>. This parameter is passed uninitialized.</param>
        /// <returns><see langword="true"/> if the <see cref="System.Collections.Generic.Dictionary{TKey,TValue}"/> contains an
        ///  element with the specified key; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="key"/> is <see langword="null"/>.</exception>
        public static bool TryGetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue, out TValue value)
        {
            Contracts.Requires.NotNull(dictionary, "dictionary");

            bool found = true;
            if (!dictionary.TryGetValue(key, out value))
            {
                value = defaultValue;
                found = false;
            }

            return found;
        }

        /// <summary>
        /// Gets the value associated with the specified key or the provided
        /// default value if the key is not found.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
        /// <param name="dictionary">The source dictionary.</param>
        /// <param name="key">The key of the value to get.</param>
        /// <param name="defaultValue">The default value to be returned if key is not found.</param>
        /// <param name="value">When this method returns, contains the value associated with the specified
        ///  key, if the <paramref name="key"/> is found; otherwise, <paramref name="defaultValue"/>. This parameter is passed uninitialized.</param>
        /// <returns><see langword="true"/> if the <see cref="System.Collections.Generic.Dictionary{TKey,TValue}"/> contains an
        ///  element with the specified key; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="key"/> is <see langword="null"/>.</exception>
        public static bool TryGetValueOrDefault<TValue>(this IDictionary dictionary, object key, TValue defaultValue, out TValue value)
        {
            Contracts.Requires.NotNull(dictionary, "dictionary");

            bool found = true;
            var temp = dictionary as Dictionary<object, TValue>;
            if (temp != null)
            {
                found = temp.TryGetValueOrDefault<object, TValue>(key, defaultValue, out value);
            }
            else
            {
                found = dictionary.Contains(key);
                value = found ? (TValue)dictionary[key] : defaultValue;
            }

            return found;
        }
        #endregion

        #endregion
    }
}
