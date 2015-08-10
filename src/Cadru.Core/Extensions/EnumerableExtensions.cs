//------------------------------------------------------------------------------
// <copyright file="EnumerableExtensions.cs"
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
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Cadru.Internal;
    using Cadru.Properties;


    /// <summary>
    /// Provides basic routines for common sequence and collection manipulation.
    /// </summary>
    public static class EnumerableExtensions
    {
        #region fields
        #endregion

        #region constructors
        #endregion

        #region events
        #endregion

        #region properties
        #endregion

        #region methods

        #region IsEmpty
        /// <summary>
        /// Determines if the collection contains values.
        /// </summary>
        /// <param name="source">The collection to test.</param>
        /// <returns><see langword="true"/> if the collection does not contain values;
        /// otherwise, <see langword="false"/>.</returns>
        public static bool IsEmpty(this IEnumerable source)
        {
            Contracts.Requires.NotNull(source, "source");

            bool empty = false;

            ICollection collection = source as ICollection;
            if (collection.IsNotNull())
            {
                empty = collection.Count == 0;
            }
            else
            {
                IEnumerator enumerator = source.GetEnumerator();
                if (!enumerator.MoveNext())
                {
                    empty = true;
                }
            }

            return empty;
        }
        #endregion

        #region IsNull
        /// <summary>
        /// Determines if the collection is <see langword="null"/>.
        /// </summary>
        /// <param name="source">The collection to test.</param>
        /// <returns><see langword="true"/> if the collection is <see langword="null"/>;
        /// otherwise, <see langword="false"/>.</returns>
        public static bool IsNull([ValidatedNotNull]this IEnumerable source)
        {
            return source == null;
        }
        #endregion

        #region IsNullOrEmpty
        /// <summary>
        /// Determines if the collection is <see langword="null"/> or contains values.
        /// </summary>
        /// <param name="source">The collection to test.</param>
        /// <returns><see langword="true"/> if the collection is <see langword="null"/>
        /// or does not contain values; otherwise, <see langword="false"/>.</returns>
        public static bool IsNullOrEmpty([ValidatedNotNull]this IEnumerable source)
        {
            return source == null || source.IsEmpty();
        }
        #endregion

        #region Join

        #region Join(this IList<string> values, string separator, int startIndex, int count)
        /// <summary>
        /// Concatenates the members of a collection, using the specified separator between each member.
        /// </summary>
        /// <param name="values">A collection that contains the strings to concatenate.</param>
        /// <param name="separator">The string to use as a separator. <paramref name="separator"/>
        /// is included in the returned string only if <paramref name="values"/> has more than one element.</param>
        /// <param name="startIndex">The first element in <paramref name="values"/> to use.</param>
        /// <param name="count">The number of elements of <paramref name="values"/> to use.</param>
        /// <returns><para>A string that consists of the members of values delimited by the separator string.</para>
        /// <para>-or-</para>
        /// <para><see cref="System.String.Empty"/> if <paramref name="count"/> is zero,
        /// <paramref name="values"/> has no elements, or <paramref name="separator"/> and
        /// all of the elements of <paramref name="values"/> are <see cref="System.String.Empty"/>.</para>
        /// </returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="values"/> is <see langword="null"/>.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// <para><paramref name="startIndex"/> or <paramref name="count"/> is less than 0.</para>
        /// <para>-or-</para>
        /// <para><paramref name="startIndex"/> plus <paramref name="count"/> is greater than the
        /// number of elements in <paramref name="values"/>.</para>
        /// </exception>
        /// <exception cref="System.OutOfMemoryException">Out of memory.</exception>
        public static string Join(this IList<string> values, string separator, int startIndex, int count)
        {
            return String.Join(separator, values.ToArray(), startIndex, count);
        }
        #endregion

        #region Join(this IList<string> values, int startIndex, int count)
        /// <summary>
        /// Concatenates the members of a collection, using a comma (,) between each member.
        /// </summary>
        /// <param name="values">A collection that contains the strings to concatenate.</param>
        /// <param name="startIndex">The first element in <paramref name="values"/> to use.</param>
        /// <param name="count">The number of elements of <paramref name="values"/> to use.</param>
        /// <returns><para>A string that consists of the members of values delimited by a comma (,).</para>
        /// <para>-or-</para>
        /// <para><see cref="System.String.Empty"/> if <paramref name="count"/> is zero,
        /// <paramref name="values"/> has no elements, or all of the elements of
        /// <paramref name="values"/> are <see cref="System.String.Empty"/>.</para>
        /// </returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="values"/> is <see langword="null"/>.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// <para><paramref name="startIndex"/> or <paramref name="count"/> is less than 0.</para>
        /// <para>-or-</para>
        /// <para><paramref name="startIndex"/> plus <paramref name="count"/> is greater than the
        /// number of elements in <paramref name="values"/>.</para>
        /// </exception>
        /// <exception cref="System.OutOfMemoryException">Out of memory.</exception>
        public static string Join(this IList<string> values, int startIndex, int count)
        {
            return String.Join(",", values.ToArray(), startIndex, count);
        }
        #endregion

        #region Join(this IEnumerable<string> values)
        /// <summary>
        /// Concatenates the members of a collection, using a comma (,) separator between each member.
        /// </summary>
        /// <param name="values">A collection that contains the strings to concatenate.</param>
        /// <returns>A string that consists of the members of values delimited by a comma (,).
        /// If <paramref name="values"/> has no members, the method returns <see cref="System.String.Empty"/>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="values"/> is <see langword="null"/>.</exception>
        public static string Join(this IEnumerable<string> values)
        {
            return String.Join(",", values);
        }
        #endregion

        #region Join(this IEnumerable<string> values, string separator)
        /// <summary>
        /// Concatenates the members of a collection, using the specified separator between each member.
        /// </summary>
        /// <param name="values">A collection that contains the strings to concatenate.</param>
        /// <param name="separator">The string to use as a separator. <paramref name="separator"/>
        /// is included in the returned string only if <paramref name="values"/> has more than one element.</param>
        /// <returns>A string that consists of the members of values delimited by the separator string.
        /// If <paramref name="values"/> has no members, the method returns <see cref="System.String.Empty"/>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="values"/> is <see langword="null"/>.</exception>
        public static string Join(this IEnumerable<string> values, string separator)
        {
            return String.Join(separator, values);
        }
        #endregion

        #region Join<T>(this IEnumerable<T> values)
        /// <summary>
        /// Concatenates the members of a collection, using a comma (,) separator between each member.
        /// </summary>
        /// <typeparam name="T">The type of the members of <paramref name="values"/>.</typeparam>
        /// <param name="values">A collection that contains the objects to concatenate.</param>
        /// <returns>A string that consists of the members of values delimited by a comma (,).
        /// If <paramref name="values"/> has no members, the method returns <see cref="System.String.Empty"/>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="values"/> is <see langword="null"/>.</exception>
        public static string Join<T>(this IEnumerable<T> values)
        {
            return String.Join(",", values);
        }
        #endregion

        #region Join<T>(this IEnumerable<T> values, string separator)
        /// <summary>
        /// Concatenates the members of a collection, using the specified separator between each member.
        /// </summary>
        /// <typeparam name="T">The type of the members of <paramref name="values"/>.</typeparam>
        /// <param name="values">A collection that contains the objects to concatenate.</param>
        /// <param name="separator">The string to use as a separator. <paramref name="separator"/>
        /// is included in the returned string only if <paramref name="values"/> has more than one element.</param>
        /// <returns>A string that consists of the members of values delimited by the separator string.
        /// If <paramref name="values"/> has no members, the method returns <see cref="System.String.Empty"/>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="values"/> is <see langword="null"/>.</exception>
        public static string Join<T>(this IEnumerable<T> values, string separator)
        {
            return String.Join(separator, values);
        }
        #endregion

        #endregion

        #region Slice
        /// <summary>Returns a segment of the specified collection.</summary>
        /// <typeparam name="T">The type of the members of <paramref name="values"/>.</typeparam>
        /// <param name="values">A collection that contains the objects to segment.</param>
        /// <param name="startIndex">The starting index of the collection.</param>
        /// <param name="endIndex">The ending index of the collection.</param>
        /// <returns>A new <see cref="IEnumerable{T}"/> containing the segment
        /// of the specified collection.</returns>
        /// <exception cref="System.ArgumentException">
        /// <paramref name="startIndex"/> must be less than or equal to
        /// <paramref name="endIndex"/>.</exception>
        public static IEnumerable<T> Slice<T>(this IEnumerable<T> values, int startIndex, int endIndex)
        {
            Contracts.Requires.NotNull(values, "values");
            Contracts.Requires.ValidRange(startIndex > endIndex, "startIndex", Resources.Argument_StartIndexGreaterThanEndIndex);
            Contracts.Requires.ValidRange(startIndex < 0, "startIndex", Resources.ArgumentOutOfRange_IndexLessThanZero);
            Contracts.Requires.ValidRange(endIndex < 0, "endIndex", Resources.ArgumentOutOfRange_IndexLessThanZero);

            var index = 0;
            foreach (var item in values)
            {
                if (index >= startIndex && index <= endIndex)
                {
                    yield return item;
                }

                ++index;
            }
        }
        #endregion

        #region WhereIf

        #region WhereIf<T>(this IEnumerable<TSource> source, bool condition, Func<T, bool> predicate)
        /// <summary>
        /// Filters a sequence of values based on a predicate. Each element's
        /// index is used in the logic of the predicate function.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to filter.</param>
        /// <param name="condition">The condition used to determine if the
        /// sequence should be filtered.</param>
        /// <param name="predicate">A function to test each source element for
        /// a condition; the second parameter of the function represents the
        /// index of the source element.</param>
        /// <returns>If <paramref name="condition"/> is <see langword="true"/>,
        /// an <see cref="IEnumerable{T}"/> that contains elements from the input
        /// sequence that satisfy the condition; otherwise, the original input
        /// sequence.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="source"/> or <paramref name="predicate"/> is
        /// <see langword="null"/>.</exception>
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, bool condition, Func<T, bool> predicate)
        {
            return condition ? source.Where(predicate) : source;
        }
        #endregion

        #region WhereIf<T>(this IEnumerable<TSource> source, bool condition, Func<T, int, bool> predicate)
        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to filter.</param>
        /// <param name="condition">The condition used to determine if the
        /// sequence should be filtered.</param>
        /// <param name="predicate">A function to test each source element for
        /// a condition.</param>
        /// <returns>If <paramref name="condition"/> is <see langword="true"/>,
        /// an <see cref="IEnumerable{T}"/> that contains elements from the input
        /// sequence that satisfy the condition; otherwise, the original input
        /// sequence.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="source"/> or <paramref name="predicate"/> is
        /// <see langword="null"/>.</exception>
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, bool condition, Func<T, int, bool> predicate)
        {
            return condition ? source.Where(predicate) : source;
        }
        #endregion

        #endregion

        #endregion
    }
}
