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

namespace Cadru.Collections
{
    public partial class MultiValueDictionary<TKey, TValue>
    {
        private sealed class Enumerator :
            IEnumerator<KeyValuePair<TKey, IReadOnlyCollection<TValue>>>
        {
            private readonly MultiValueDictionary<TKey, TValue> _multiValueDictionary;
            private readonly int _version;
            private Dictionary<TKey, InnerCollectionView>.Enumerator _enumerator;
            private EnumerationState _state;

            internal Enumerator(MultiValueDictionary<TKey, TValue> multiValueDictionary)
            {
                this._multiValueDictionary = multiValueDictionary;
                this._version = multiValueDictionary._version;
                this._enumerator = multiValueDictionary._dictionary.GetEnumerator();
                this._state = EnumerationState.BeforeFirst;
                this.Current = default;
            }

            private enum EnumerationState
            {
                BeforeFirst,
                During,
                AfterLast
            }

            public KeyValuePair<TKey, IReadOnlyCollection<TValue>> Current { get; private set; }

            object IEnumerator.Current => this._state switch
            {
                EnumerationState.BeforeFirst => throw new InvalidOperationException((Strings.InvalidOperation_EnumNotStarted)),
                EnumerationState.AfterLast => throw new InvalidOperationException((Strings.InvalidOperation_EnumEnded)),
                _ => this.Current,
            };

            public void Dispose() => this._enumerator.Dispose();

            public bool MoveNext()
            {
                if (this._version != this._multiValueDictionary._version)
                {
                    throw new InvalidOperationException(Strings.InvalidOperation_EnumFailedVersion);
                }

                if (this._enumerator.MoveNext())
                {
                    this.Current = new KeyValuePair<TKey, IReadOnlyCollection<TValue>>(this._enumerator.Current.Key, this._enumerator.Current.Value);
                    this._state = EnumerationState.During;
                    return true;
                }

                this.Current = default;
                this._state = EnumerationState.AfterLast;
                return false;
            }

            public void Reset()
            {
                if (this._version != this._multiValueDictionary._version)
                {
                    throw new InvalidOperationException(Strings.InvalidOperation_EnumFailedVersion);
                }

                this._enumerator.Dispose();
                this._enumerator = this._multiValueDictionary._dictionary.GetEnumerator();
                this.Current = default;
                this._state = EnumerationState.BeforeFirst;
            }
        }
    }
}