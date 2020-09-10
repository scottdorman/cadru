//------------------------------------------------------------------------------
// <copyright file="EnumerableExtensions.cs"
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

using System.Collections;

namespace Cadru.Contracts.Internal
{
    /// <summary>
    /// Provides basic routines for common sequence and collection manipulation.
    /// </summary>
    internal static class EnumerableExtensions
    {
        /// <summary>
        /// Determines if the collection contains values.
        /// </summary>
        /// <param name="source">The collection to test.</param>
        /// <returns>
        /// <see langword="true"/> if the collection does not contain values;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsEmpty(this IEnumerable source)
        {
            Requires.NotNull(source, nameof(source));

            var empty = false;

            if (source is ICollection collection)
            {
                empty = collection.Count == 0;
            }
            else
            {
                var enumerator = source.GetEnumerator();
                if (!enumerator.MoveNext())
                {
                    empty = true;
                }
            }

            return empty;
        }

        /// <summary>
        /// Determines if the collection is <see langword="null"/>.
        /// </summary>
        /// <param name="source">The collection to test.</param>
        /// <returns>
        /// <see langword="true"/> if the collection is <see langword="null"/>;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsNull([ValidatedNotNull] this IEnumerable source)
        {
            return source == null;
        }

        /// <summary>
        /// Determines if the collection is <see langword="null"/> or contains values.
        /// </summary>
        /// <param name="source">The collection to test.</param>
        /// <returns>
        /// <see langword="true"/> if the collection is <see langword="null"/>
        /// or does not contain values; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsNullOrEmpty([ValidatedNotNull] this IEnumerable source)
        {
            return source == null || source.IsEmpty();
        }
    }
}