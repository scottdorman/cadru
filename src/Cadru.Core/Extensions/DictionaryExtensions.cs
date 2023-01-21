//------------------------------------------------------------------------------
// <copyright file="DictionaryExtensions.cs"
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
using System.Diagnostics.CodeAnalysis;

using Validation;

namespace Cadru.Extensions
{
    /// <summary>
    /// Provides basic routines for common dictionary manipulation.
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Gets or sets the value associated with the specified key if the
        /// condition is <see langword="true"/>.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
        /// <param name="source">The source dictionary.</param>
        /// <param name="key">The key of the value to get or set.</param>
        /// <param name="value">The value associated with the specified key.</param>
        /// <param name="condition">
        /// <see langword="true"/> to add or update the value; otherwise, <see langword="false"/>.
        /// </param>
        /// <returns>
        /// The updated <see cref="IDictionary{TKey, TValue}"/> instance.
        /// </returns>
        public static IDictionary<TKey, TValue> Add<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key, TValue value, Func<bool> condition)
        {
            if (condition())
            {
                source[key] = value!;
            }

            return source;
        }

        /// <summary>
        /// Gets the value associated with the specified key or the provided
        /// default value if the key is not found.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
        /// <param name="source">The source dictionary.</param>
        /// <param name="key">The key of the value to get.</param>
        /// <param name="defaultValue">
        /// The default value to be returned if key is not found.
        /// </param>
        /// <returns>
        /// If <paramref name="key"/> is found, the value associated with the
        /// specified key; otherwise, <paramref name="defaultValue"/>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="key"/> is <see langword="null"/>.
        /// </exception>
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key, TValue defaultValue)
        {
            Requires.NotNull(source, nameof(source));

            return source.TryGetValue(key, out var result) ? result : defaultValue;
        }

        /// <summary>
        /// Gets the value associated with the specified key or the provided
        /// default value if the key is not found.
        /// </summary>
        /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
        /// <param name="source">The source dictionary.</param>
        /// <param name="key">The key of the value to get.</param>
        /// <param name="defaultValue">
        /// The default value to be returned if key is not found.
        /// </param>
        /// <returns>
        /// If <paramref name="key"/> is found, the value associated with the
        /// specified key; otherwise, <paramref name="defaultValue"/>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="key"/> is <see langword="null"/>.
        /// </exception>
        public static TValue? GetValueOrDefault<TValue>(this IDictionary source, object key, TValue defaultValue)
        {
            Requires.NotNull(source, nameof(source));

            TValue? result;
            if (source is Dictionary<object, TValue> temp)
            {
                result = temp.GetValueOrDefault<object, TValue>(key, defaultValue);
            }
            else
            {
                result = !source.Contains(key) ? defaultValue : (TValue?)source[key];
            }

            return result;
        }

        /// <summary>
        /// Merges the specified key/value pair into the source dictionary.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
        /// <param name="source">The source <see cref="IDictionary{TKey, TValue}"/>.</param>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value">
        /// The value of the element to add. It can be <see langword="null"/>.
        /// </param>
        /// <param name="replaceExisting">
        /// <see langword="true"/> to replace the existing value, if found;
        /// otherwise, <see langword="false"/>.
        /// </param>
        public static void Merge<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key, TValue value, bool replaceExisting)
        {
            if (replaceExisting || !source.ContainsKey(key))
            {
                source[key] = value;
            }
        }

        /// <summary>
        /// Merges the specified instance into the source dictionary.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
        /// <param name="first">The source <see cref="IDictionary{TKey, TValue}"/>.</param>
        /// <param name="second">
        /// An <see cref="IDictionary{TKey, TValue}"/> whose elements will be merged.
        /// </param>
        /// <param name="replaceExisting">
        /// <see langword="true"/> to replace the existing value, if found;
        /// otherwise, <see langword="false"/>.
        /// </param>
        public static void Merge<TKey, TValue>(this IDictionary<TKey, TValue> first, IDictionary<TKey, TValue> second, bool replaceExisting)
        {
            foreach (var pair in second)
            {
                if (!replaceExisting && first.ContainsKey(pair.Key))
                {
                    continue; // Try the next
                }

                first[pair.Key] = pair.Value;
            }
        }

        /// <summary>
        /// Merges the specified instance into the source dictionary.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
        /// <param name="first">The source <see cref="IDictionary{TKey, TValue}"/>.</param>
        /// <param name="second">
        /// An <see cref="IDictionary{TKey, TValue}"/> whose elements will be merged.
        /// </param>
        public static void Merge<TKey, TValue>(this IDictionary<TKey, TValue> first, IDictionary<TKey, TValue> second)
        {
            Merge(first, second, true);
        }

        /// <summary>
        /// Attempts to add the specified key and value to the dictionary.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
        /// <param name="dictionary"></param>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value">
        /// The value of the element to add. It can be <see langword="null"/>.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the key/value pair was added to the
        /// dictionary successfully; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool TryAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, value);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Attempts to add the specified key and value to the dictionary.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
        /// <param name="dictionary"></param>
        /// <param name="item">
        /// The <see cref="KeyValuePair{TKey, TValue}"/> to add.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the key/value pair was added to the
        /// dictionary successfully; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool TryAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, KeyValuePair<TKey, TValue> item)
        {
            if (!dictionary.ContainsKey(item.Key))
            {
                dictionary.Add(item);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets the value associated with the specified key or the provided
        /// default value if the key is not found.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
        /// <param name="dictionary">The source dictionary.</param>
        /// <param name="key">The key of the value to get.</param>
        /// <param name="defaultValue">
        /// The default value to be returned if key is not found.
        /// </param>
        /// <param name="value">
        /// When this method returns, contains the value associated with the
        /// specified key, if the <paramref name="key"/> is found; otherwise,
        /// <paramref name="defaultValue"/>. This parameter is passed uninitialized.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the
        /// <see cref="System.Collections.Generic.Dictionary{TKey,TValue}"/>
        /// contains an element with the specified key; otherwise, false.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="key"/> is <see langword="null"/>.
        /// </exception>
        public static bool TryGetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue, [MaybeNullWhen(false)] out TValue value)
        {
            Requires.NotNull(dictionary, nameof(dictionary));

            var found = true;
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
        /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
        /// <param name="dictionary">The source dictionary.</param>
        /// <param name="key">The key of the value to get.</param>
        /// <param name="defaultValue">
        /// The default value to be returned if key is not found.
        /// </param>
        /// <param name="value">
        /// When this method returns, contains the value associated with the
        /// specified key, if the <paramref name="key"/> is found; otherwise,
        /// <paramref name="defaultValue"/>. This parameter is passed uninitialized.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the
        /// <see cref="System.Collections.Generic.Dictionary{TKey,TValue}"/>
        /// contains an element with the specified key; otherwise, false.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="key"/> is <see langword="null"/>.
        /// </exception>
        public static bool TryGetValueOrDefault<TValue>(this IDictionary dictionary, object key, TValue defaultValue, [MaybeNullWhen(false)] out TValue value)
        {
            Requires.NotNull(dictionary, nameof(dictionary));

            bool found;
            if (dictionary is Dictionary<object, TValue> temp)
            {
                found = temp.TryGetValueOrDefault<object, TValue>(key, defaultValue, out value);
            }
            else
            {
                found = dictionary.Contains(key);
                value = !found ? defaultValue : (TValue?)dictionary[key];
            }

            return found;
        }
    }
}