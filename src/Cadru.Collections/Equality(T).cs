//------------------------------------------------------------------------------
// <copyright file="Equality(T).cs"
//  company="Scott Dorman"
//  library="Cadru">
//    Copyright (C) 2001-2017 Scott Dorman.
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

namespace Cadru.Collections
{
    using Cadru.Contracts;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents an <see cref="IEqualityComparer{T}"/> which uses a
    /// function to extract a key from an element and, optionally, using
    /// the specified comparer.
    /// </summary>
    /// <typeparam name="TSource">The type of the objects to compare.</typeparam>
    public static class Equality<TSource>
    {
        /// <summary>
        /// Creates an instance of an <see cref="IEqualityComparer{T}"/> using
        /// the provided function to extract a key from an element and the
        /// default <see cref="EqualityComparer{T}"/> for <typeparamref name="TKey"/>.
        /// </summary>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/>.</typeparam>
        /// <param name="keySelector">A function to extract a key from an element.</param>
        /// <returns>An instance of an <see cref="EqualityComparer{T}"/>.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "Reviewed.")]
        public static IEqualityComparer<TSource> CreateComparer<TKey>(Func<TSource, TKey> keySelector)
        {
            return CreateComparer(keySelector, null);
        }

        /// <summary>
        /// Creates an instance of an <see cref="IEqualityComparer{T}"/> using
        /// the provided function to extract a key from an element and the
        /// specified <see cref="EqualityComparer{T}"/>.
        /// </summary>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/>.</typeparam>
        /// <param name="keySelector">A function to extract a key from an element.</param>
        /// <param name="comparer">An <see cref="IComparer{T}"/> to compare keys.</param>
        /// <returns>An instance of an <see cref="EqualityComparer{T}"/>.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "Reviewed.")]
        public static IEqualityComparer<TSource> CreateComparer<TKey>(Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
        {
            return new KeyEqualityComparer<TKey>(keySelector, comparer);
        }

        class KeyEqualityComparer<V> : IEqualityComparer<TSource>
        {
            readonly Func<TSource, V> keySelector;
            readonly IEqualityComparer<V> comparer;

            public KeyEqualityComparer(Func<TSource, V> keySelector, IEqualityComparer<V> comparer)
            {
                Requires.NotNull(keySelector, nameof(keySelector));

                this.keySelector = keySelector;
                this.comparer = this.comparer ?? EqualityComparer<V>.Default;
            }

            public bool Equals(TSource x, TSource y)
            {
                return this.comparer.Equals(this.keySelector(x), this.keySelector(y));
            }

            public int GetHashCode(TSource obj)
            {
                return this.comparer.GetHashCode(this.keySelector(obj));
            }
        }
    }
}
