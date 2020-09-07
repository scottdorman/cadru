//------------------------------------------------------------------------------
// <copyright file="SqlStrategyConfiguration.cs"
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

using Cadru.Contracts;

namespace Cadru.Polly.Sql
{
    /// <summary>
    /// Represents a collection of key/value pairs used for configuring an <see
    /// cref="ISqlStrategy"/>.
    /// </summary>
    public sealed partial class SqlStrategyConfiguration : ISqlStrategyConfiguration, IDictionary<string, object>, IDictionary
    {
        private readonly Dictionary<string, object> wrappedDictionary;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlStrategyConfiguration"/> class.
        /// </summary>
        public SqlStrategyConfiguration() : this(new Dictionary<string, object>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see
        /// cref="SqlStrategyConfiguration"/> class.
        /// </summary>
        /// <param name="contextData">The <see cref="IDictionary{TKey,
        /// TValue}"/> whose elements are copied into the <see
        /// cref="SqlStrategyConfiguration"/>.</param>
        public SqlStrategyConfiguration(IDictionary<string, object> contextData)
        {
            Requires.NotNull(contextData, nameof(contextData));

            this.wrappedDictionary = new Dictionary<string, object>(contextData);
        }

        /// <summary>
        /// Initializes a new instance of the <see
        /// cref="SqlStrategyConfiguration"/> class.
        /// </summary>
        /// <param name="contextData">The <see cref="IDictionary{TKey,
        /// TValue}"/> whose elements are copied into the <see
        /// cref="SqlStrategyConfiguration"/>.</param>
        public SqlStrategyConfiguration(IEnumerable<KeyValuePair<string, object>> contextData)
        {
            Requires.NotNull(contextData, nameof(contextData));

            this.wrappedDictionary = new Dictionary<string, object>(contextData);
        }

        #region IDictionary<string,object> implementation
        /// <inheritdoc cref="IDictionary{TKey,Value}.Keys"/>
        public ICollection<string> Keys => this.wrappedDictionary.Keys;

        /// <inheritdoc cref="IDictionary{TKey,Value}.Values"/>
        public ICollection<object> Values => this.wrappedDictionary.Values;

        /// <inheritdoc cref="Dictionary{TKey,Value}.Count"/>
        public int Count => this.wrappedDictionary.Count;

        /// <inheritdoc cref="ICollection{T}.IsReadOnly"/>
        bool ICollection<KeyValuePair<string, object>>.IsReadOnly => ((IDictionary<string, object>)this.wrappedDictionary).IsReadOnly;

        /// <inheritdoc cref="P:IDictionary{TKey,Value}.Item(String)"/>
        public object this[string key]
        {
            get => this.wrappedDictionary[key];
            set => this.wrappedDictionary[key] = value;
        }

        /// <inheritdoc cref="IDictionary{TKey,Value}.Add(TKey, Value)"/>
        public void Add(string key, object value)
        {
            this.wrappedDictionary.Add(key, value);
        }

        /// <inheritdoc cref="IDictionary{TKey,Value}.ContainsKey(TKey)"/>
        public bool ContainsKey(string key) => this.wrappedDictionary.ContainsKey(key);

        /// <inheritdoc cref="IDictionary{TKey,Value}.Remove(TKey)"/>
        public bool Remove(string key) => this.wrappedDictionary.Remove(key);

        /// <inheritdoc cref="IDictionary{TKey,Value}.TryGetValue(TKey, out Value)"/>
        public bool TryGetValue(string key, out object value) => this.wrappedDictionary.TryGetValue(key, out value);

        /// <inheritdoc cref="ICollection{T}.Add(T)"/>
        void ICollection<KeyValuePair<string, object>>.Add(KeyValuePair<string, object> item) => ((IDictionary<string, object>)this.wrappedDictionary).Add(item);

        /// <inheritdoc cref="Dictionary{TKey,Value}.Clear"/>
        public void Clear() => this.wrappedDictionary.Clear();

        /// <inheritdoc cref="ICollection{T}.Contains(T)"/>
        bool ICollection<KeyValuePair<string, object>>.Contains(KeyValuePair<string, object> item) => ((IDictionary<string, object>)this.wrappedDictionary).Contains(item);

        /// <inheritdoc cref="ICollection{T}.CopyTo(T[], Int32)"/>
        void ICollection<KeyValuePair<string, object>>.CopyTo(KeyValuePair<string, object>[] array, int arrayIndex) => ((IDictionary<string, object>)this.wrappedDictionary).CopyTo(array, arrayIndex);

        /// <inheritdoc cref="ICollection{T}.Remove(T)"/>
        bool ICollection<KeyValuePair<string, object>>.Remove(KeyValuePair<string, object> item) => ((IDictionary<string, object>)this.wrappedDictionary).Remove(item);

        /// <inheritdoc cref="Dictionary{TKey, TValue}.GetEnumerator"/>
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator() => this.wrappedDictionary.GetEnumerator();

        /// <inheritdoc cref="IEnumerable.GetEnumerator"/>
        IEnumerator IEnumerable.GetEnumerator() => this.wrappedDictionary.GetEnumerator();

        /// <inheritdoc cref="IDictionary.Add(Object, Object)"/>
        public void Add(object key, object value) => ((IDictionary)this.wrappedDictionary).Add(key, value);

        /// <inheritdoc cref="IDictionary.Contains"/>
        public bool Contains(object key) => ((IDictionary)this.wrappedDictionary).Contains(key);

        /// <inheritdoc cref="IDictionary.GetEnumerator"/>
        IDictionaryEnumerator IDictionary.GetEnumerator() => ((IDictionary)this.wrappedDictionary).GetEnumerator();

        /// <inheritdoc cref="IDictionary.Remove(Object)"/>
        public void Remove(object key) => ((IDictionary)this.wrappedDictionary).Remove(key);

        /// <inheritdoc cref="ICollection.CopyTo"/>
        public void CopyTo(Array array, int index) => ((IDictionary)this.wrappedDictionary).CopyTo(array, index);
        #endregion

        #region IReadOnlyDictionary<string, object> implementation

        /// <inheritdoc cref="IReadOnlyDictionary{TKey, TValue}.Keys"/>
        IEnumerable<string> IReadOnlyDictionary<string, object>.Keys => ((IReadOnlyDictionary<string, object>)this.wrappedDictionary).Keys;

        /// <inheritdoc cref="IReadOnlyDictionary{TKey, TValue}.Values"/>
        IEnumerable<object> IReadOnlyDictionary<string, object>.Values => ((IReadOnlyDictionary<string, object>)this.wrappedDictionary).Values;
        #endregion

        #region IDictionary implementation

        /// <inheritdoc cref="IDictionary.IsFixedSize"/>
        bool IDictionary.IsFixedSize => ((IDictionary)this.wrappedDictionary).IsFixedSize;

        /// <inheritdoc cref="IDictionary.IsReadOnly"/>
        bool IDictionary.IsReadOnly => ((IDictionary)this.wrappedDictionary).IsReadOnly;

        /// <inheritdoc cref="IDictionary.Keys"/>
        ICollection IDictionary.Keys => ((IDictionary)this.wrappedDictionary).Keys;

        /// <inheritdoc cref="IDictionary.Values"/>
        ICollection IDictionary.Values => ((IDictionary)this.wrappedDictionary).Values;

        /// <inheritdoc cref="ICollection.IsSynchronized"/>
        bool ICollection.IsSynchronized => ((IDictionary)this.wrappedDictionary).IsSynchronized;

        /// <inheritdoc cref="ICollection.SyncRoot"/>
        object ICollection.SyncRoot => ((IDictionary)this.wrappedDictionary).SyncRoot;

        /// <inheritdoc cref="P:IDictionary.Item(Object)"/>
        object IDictionary.this[object key] { get => ((IDictionary)this.wrappedDictionary)[key]; set => ((IDictionary)this.wrappedDictionary)[key] = value; }
        #endregion
    }
}