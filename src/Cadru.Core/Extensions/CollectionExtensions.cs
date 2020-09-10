//------------------------------------------------------------------------------
// <copyright file="CollectionExtensions.cs"
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

using System.Collections.Generic;

namespace Cadru.Extensions
{
    /// <summary>
    /// Provides extensions for working with collections.
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Adds the elements of the specified collection to the <see cref="ICollection{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// <param name="instance">The collection to update.</param>
        /// <param name="collection">
        /// The collection whose elements should be added to the end of the
        /// <see cref="ICollection{T}"/>. The collection itself cannot be
        /// <see langword="null"/>, but it can contain elements that are
        /// <see langword="null"/>, if type <typeparamref name="T"/> is a
        /// reference type.
        /// </param>
        public static void AddRange<T>(this ICollection<T> instance, IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                instance.Add(item);
            }
        }
    }
}