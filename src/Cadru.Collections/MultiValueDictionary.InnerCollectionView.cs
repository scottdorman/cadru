//------------------------------------------------------------------------------
// <copyright file="MultiValueDictionary.cs"
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

using Cadru.Collections.Resources;

using Validation;

namespace Cadru.Collections
{
    public partial class MultiValueDictionary<TKey, TValue>
    {
        private sealed class InnerCollectionView :
            ICollection<TValue>,
            IReadOnlyCollection<TValue>
        {
            private readonly ICollection<TValue> _collection;

            public InnerCollectionView(TKey key, ICollection<TValue> collection)
            {
                this.Key = key;
                this._collection = collection;
            }

            public int Count => this._collection.Count;

            public bool IsReadOnly => true;

            public TKey Key { get; }

            void ICollection<TValue>.Add(TValue item) => throw new NotSupportedException(Strings.ReadOnly_Modification);

            public void AddValue(TValue item) => this._collection.Add(item);

            void ICollection<TValue>.Clear() => throw new NotSupportedException(Strings.ReadOnly_Modification);

            public bool Contains(TValue item) => this._collection.Contains(item);

            public void CopyTo(TValue[] array, int arrayIndex)
            {
                Requires.NotNull(array, nameof(array));
                Requires.Argument(arrayIndex >= 0, nameof(arrayIndex), Strings.ArgumentOutOfRange_NeedNonNegNum);
                Requires.Argument(arrayIndex <= array.Length, nameof(arrayIndex), Strings.ArgumentOutOfRange_Index);
                Requires.Argument(array.Length - arrayIndex >= this._collection.Count, nameof(arrayIndex), Strings.CopyTo_ArgumentsTooSmall);

                this._collection.CopyTo(array, arrayIndex);
            }

            public IEnumerator<TValue> GetEnumerator() => this._collection.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

            bool ICollection<TValue>.Remove(TValue item) => throw new NotSupportedException(Strings.ReadOnly_Modification);

            public bool RemoveValue(TValue item) => this._collection.Remove(item);
        }
    }
}