//------------------------------------------------------------------------------
// <copyright file="CaseInsensitiveStringDictionary.cs"
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

namespace Cadru.Collections
{
    /// <summary>
    /// Represents a collection of string keys and values that are compared
    /// in a case insensitive manner.
    /// </summary>
    public class CaseInsensitiveStringDictionary : Dictionary<string, string>
    {
        /// <summary>
        /// Initializes a new instance of the <see
        /// cref="CaseInsensitiveStringDictionary" /> class that is empty, has
        /// the default initial capacity, and uses <see
        /// cref="StringComparer.OrdinalIgnoreCase" /> for the key type.
        /// </summary>
        public CaseInsensitiveStringDictionary() : base(StringComparer.OrdinalIgnoreCase)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see
        /// cref="CaseInsensitiveStringDictionary" /> class that contains
        /// elements copied from the specified <see cref="IDictionary{TKey,
        /// TValue}"/>, and uses <see cref="StringComparer.OrdinalIgnoreCase" />
        /// for the key type.
        /// </summary>
        /// <param name="dictionary">The <see cref="IDictionary{TKey, TValue}"/>
        /// whose elements are copied to the new <see
        /// cref="CaseInsensitiveStringDictionary" /></param>
        /// <exception cref="System.ArgumentException"><paramref
        /// name="dictionary" /> is <see langword="null" />.</exception>
        /// <exception cref="System.ArgumentException"><paramref
        /// name="dictionary" /> contains one or more duplicate keys.</exception>
        public CaseInsensitiveStringDictionary(IDictionary<string, string> dictionary) : base(dictionary, StringComparer.OrdinalIgnoreCase)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see
        /// cref="CaseInsensitiveStringDictionary" /> class that is empty, has
        /// the specified initial capacity, and uses <see
        /// cref="StringComparer.OrdinalIgnoreCase" /> for the key type.
        /// </summary>
        /// <param name="capacity">The initial number of elements that the <see
        /// cref="CaseInsensitiveStringDictionary" /> can contain.</param>
        /// <exception cref="System.ArgumentOutOfRangeException"><paramref name="capacity" /> is less than 0.</exception>
        public CaseInsensitiveStringDictionary(int capacity) : base(capacity, StringComparer.OrdinalIgnoreCase)
        {
        }
    }
}