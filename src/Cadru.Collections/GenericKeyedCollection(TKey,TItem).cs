//------------------------------------------------------------------------------
// <copyright file="GenericKeyedCollection(TKey,TItem).cs"
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
using System.Collections.ObjectModel;

namespace Cadru.Collections
{
    /// <summary>
    /// Provides a collection whose keys are embedded in the values and can use
    /// a function for extracting the key.
    /// </summary>
    /// <typeparam name="TKey">The type of keys in the collection.</typeparam>
    /// <typeparam name="TItem">The type of items in the collection.</typeparam>
    public class GenericKeyedCollection<TKey, TItem> : KeyedCollection<TKey, TItem>
    {
        private readonly Func<TItem, TKey> getKeyFunc;

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyedCollection{TKey,TItem}"/>
        /// class that uses the default equality comparer.
        /// </summary>
        /// <param name="getKeyFunc">The function used to extract a key from the item.</param>
        public GenericKeyedCollection(Func<TItem, TKey> getKeyFunc) : base()
        {
            this.getKeyFunc = getKeyFunc;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyedCollection{TKey,TItem}"/>
        /// class that uses the that uses the specified equality comparer.
        /// </summary>
        /// <param name="getKeyFunc">The function used to extract a key from the item.</param>
        /// <param name="comparer">The implementation of the
        /// <see cref="IEqualityComparer{T}"/> generic interface to use when
        /// comparing keys, or <see langword="null" /> to use the default
        /// equality comparer for the type of the key, obtained from
        /// <see cref="EqualityComparer{T}.Default">Default</see>.</param>
        public GenericKeyedCollection(Func<TItem, TKey> getKeyFunc, IEqualityComparer<TKey> comparer) : base(comparer)
        {
            this.getKeyFunc = getKeyFunc;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyedCollection{TKey,TItem}"/>
        /// class that uses the that uses the specified equality comparer
        /// and creates a lookup dictionary when the specified threshold is exceeded..
        /// </summary>
        /// <param name="getKeyFunc">The function used to extract a key from the item.</param>
        /// <param name="comparer">The implementation of the
        /// <see cref="IEqualityComparer{T}"/> generic interface to use when
        /// comparing keys, or <see langword="null" /> to use the default
        /// equality comparer for the type of the key, obtained from
        /// <see cref="EqualityComparer{T}.Default">Default</see>.</param>
        /// <param name="dictionaryCreationThreshold">The number of elements
        /// the collection can hold without creating a lookup dictionary
        /// (0 creates the lookup dictionary when the first item is added),
        /// or –1 to specify that a lookup dictionary is never created.</param>
        public GenericKeyedCollection(Func<TItem, TKey> getKeyFunc, IEqualityComparer<TKey> comparer, int dictionaryCreationThreshold) : base(comparer, dictionaryCreationThreshold)
        {
            this.getKeyFunc = getKeyFunc;
        }

        /// <summary>
        /// Extracts the key for the given item.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected override TKey GetKeyForItem(TItem item)
        {
            return this.getKeyFunc(item);
        }
    }
}