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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using Cadru.Resources;

using Validation;

namespace Cadru.Extensions
{
    /// <summary>
    /// Provides basic routines for common sequence and collection manipulation.
    /// </summary>
    public static class EnumerableExtensions
    {
        ///<summary>Finds the index of the first item matching an expression in an enumerable.</summary>
        ///<param name="items">The enumerable to search.</param>
        ///<param name="predicate">The expression to test the items against.</param>
        ///<returns>The index of the first matching item, or -1 if no items match.</returns>
        public static int FindIndex(this IEnumerable items, Func<object, bool> predicate)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            var retVal = 0;
            foreach (var item in items)
            {
                if (predicate(item)) return retVal;
                retVal++;
            }

            return -1;
        }

        ///<summary>Finds the index of the first item matching an expression in an enumerable.</summary>
        ///<param name="items">The enumerable to search.</param>
        ///<param name="predicate">The expression to test the items against.</param>
        ///<returns>The index of the first matching item, or -1 if no items match.</returns>
        public static int FindIndex<T>(this IEnumerable<T> items, Func<T, bool> predicate)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            var retVal = 0;
            foreach (var item in items)
            {
                if (predicate(item)) return retVal;
                retVal++;
            }

            return -1;
        }

        ///<summary>Finds the index of the first matching string in an enumerable.</summary>
        ///<param name="items">The enumerable to search.</param>
        ///<param name="value">The string to search for.</param>
        ///<returns>The index of the first matching item, or -1 if no items match.</returns>
        public static int FindIndex(this IEnumerable<string> items, string value) => FindIndex(items, h => String.CompareOrdinal(h, value) == 0);

        /// <summary>
        /// Performs the specified action on each element of the <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the values in the collection.</typeparam>
        /// <param name="source">The source <see cref="IEnumerable{T}"/>.</param>
        /// <param name="action">
        /// The <see cref="Action{T1, T2}"/> delegate to perform on each element
        /// of the <see cref="IEnumerable{T}"/>.
        /// </param>
        /// <remarks>
        /// The <see cref="Action{T1, T2}"/> delegate receives the item and the
        /// index of the item.
        /// </remarks>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T, int> action)
        {
            var index = 0;
            foreach (var item in source)
            {
                action(item, index++);
            }
        }

        /// <summary>
        /// Performs the specified action on each element of the <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the values in the collection.</typeparam>
        /// <param name="source">The source <see cref="IEnumerable{T}"/>.</param>
        /// <param name="action">
        /// The <see cref="Action{T}"/> delegate to perform on each element of
        /// the <see cref="IEnumerable{T}"/>.
        /// </param>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
            }
        }

        /// <summary>
        /// Searches for the specified object and returns the zero-based index
        /// of the first occurrence within the range of elements in the <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <param name="source">The source <see cref="IEnumerable{T}"/>.</param>
        /// <param name="item">
        /// The object to locate. The value can be <see langword="null"/> for
        /// reference types.
        /// </param>
        /// <returns>
        /// The zero-based index of the first occurrence of item within the
        /// range of elements in the <see cref="IEnumerable{T}"/>, if found;
        /// otherwise, -1.
        /// </returns>
        public static int IndexOf(this IEnumerable source, object item) => FindIndex(source, i => Equals(i, item));

        /// <summary>
        /// Searches for the specified object and returns the zero-based index
        /// of the first occurrence within the range of elements in the <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <param name="source">The source <see cref="IEnumerable{T}"/>.</param>
        ///<param name="predicate">The expression to test the items against.</param>
        /// <returns>The zero-based index of the first occurrence of item within the range of elements in the <see cref="IEnumerable{T}"/>, if found; otherwise, -1.</returns>
        public static int IndexOf(this IEnumerable source, Func<object, bool> predicate) => FindIndex(source, predicate);

        /// <summary>
        /// Searches for the specified object and returns the zero-based index
        /// of the first occurrence within the range of elements in the <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <param name="source">The source <see cref="IEnumerable{T}"/>.</param>
        /// <param name="item">
        /// The object to locate. The value can be <see langword="null"/> for
        /// reference types.
        /// </param>
        /// <returns>
        /// The zero-based index of the first occurrence of item within the
        /// range of elements in the <see cref="IEnumerable{T}"/>, if found;
        /// otherwise, -1.
        /// </returns>
        public static int IndexOf<T>(this IEnumerable<T> source, T item) => FindIndex(source, i => Equals(i, item));

        /// <summary>
        /// Searches for the specified object and returns the zero-based index
        /// of the first occurrence within the range of elements in the <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <param name="source">The source <see cref="IEnumerable{T}"/>.</param>
        ///<param name="predicate">The expression to test the items against.</param>
        /// <returns>The zero-based index of the first occurrence of item within the range of elements in the <see cref="IEnumerable{T}"/>, if found; otherwise, -1.</returns>
        public static int IndexOf<T>(this IEnumerable<T> source, Func<T, bool> predicate) => FindIndex(source, predicate);

        /// <summary>
        /// Searches for the specified string and returns the zero-based index
        /// of the first occurrence within the range of elements in the <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <param name="source">The source <see cref="IEnumerable{T}"/>.</param>
        /// <param name="item">The object to locate. The value can be <see langword="null"/>.</param>
        /// <returns>
        /// The zero-based index of the first occurrence of item within the
        /// range of elements in the <see cref="IEnumerable{T}"/>, if found;
        /// otherwise, -1.
        /// </returns>
        public static int IndexOf(this IEnumerable<string> source, string item) => source.FindIndex(item);

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

        /// <summary>
        /// Concatenates the members of a collection, using the specified
        /// separator between each member.
        /// </summary>
        /// <param name="values">
        /// A collection that contains the strings to concatenate.
        /// </param>
        /// <param name="separator">
        /// The string to use as a separator. <paramref name="separator"/> is
        /// included in the returned string only if <paramref name="values"/>
        /// has more than one element.
        /// </param>
        /// <param name="startIndex">
        /// The first element in <paramref name="values"/> to use.
        /// </param>
        /// <param name="count">
        /// The number of elements of <paramref name="values"/> to use.
        /// </param>
        /// <returns>
        /// <para>
        /// A string that consists of the members of values delimited by the
        /// separator string.
        /// </para>
        /// <para>-or-</para>
        /// <para>
        /// <see cref="System.String.Empty"/> if <paramref name="count"/> is
        /// zero, <paramref name="values"/> has no elements, or
        /// <paramref name="separator"/> and all of the elements of
        /// <paramref name="values"/> are <see cref="System.String.Empty"/>.
        /// </para>
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="values"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// <para>
        /// <paramref name="startIndex"/> or <paramref name="count"/> is less
        /// than 0.
        /// </para>
        /// <para>-or-</para>
        /// <para>
        /// <paramref name="startIndex"/> plus <paramref name="count"/> is
        /// greater than the number of elements in <paramref name="values"/>.
        /// </para>
        /// </exception>
        /// <exception cref="System.OutOfMemoryException">Out of memory.</exception>
        public static string Join(this IList<string> values, string separator, int startIndex, int count)
        {
            return String.Join(separator, values.ToArray(), startIndex, count);
        }

        /// <summary>
        /// Concatenates the members of a collection, using a comma (,) between
        /// each member.
        /// </summary>
        /// <param name="values">
        /// A collection that contains the strings to concatenate.
        /// </param>
        /// <param name="startIndex">
        /// The first element in <paramref name="values"/> to use.
        /// </param>
        /// <param name="count">
        /// The number of elements of <paramref name="values"/> to use.
        /// </param>
        /// <returns>
        /// <para>
        /// A string that consists of the members of values delimited by a comma (,).
        /// </para>
        /// <para>-or-</para>
        /// <para>
        /// <see cref="System.String.Empty"/> if <paramref name="count"/> is
        /// zero, <paramref name="values"/> has no elements, or all of the
        /// elements of <paramref name="values"/> are <see cref="System.String.Empty"/>.
        /// </para>
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="values"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// <para>
        /// <paramref name="startIndex"/> or <paramref name="count"/> is less
        /// than 0.
        /// </para>
        /// <para>-or-</para>
        /// <para>
        /// <paramref name="startIndex"/> plus <paramref name="count"/> is
        /// greater than the number of elements in <paramref name="values"/>.
        /// </para>
        /// </exception>
        /// <exception cref="System.OutOfMemoryException">Out of memory.</exception>
        public static string Join(this IList<string> values, int startIndex, int count)
        {
            return String.Join(",", values.ToArray(), startIndex, count);
        }

        /// <summary>
        /// Concatenates the members of a collection, using a comma (,)
        /// separator between each member.
        /// </summary>
        /// <param name="values">
        /// A collection that contains the strings to concatenate.
        /// </param>
        /// <returns>
        /// A string that consists of the members of values delimited by a comma
        /// (,). If <paramref name="values"/> has no members, the method returns <see cref="System.String.Empty"/>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="values"/> is <see langword="null"/>.
        /// </exception>
        public static string Join(this IEnumerable<string> values)
        {
            return String.Join(",", values);
        }

        /// <summary>
        /// Concatenates the members of a collection, using the specified
        /// separator between each member.
        /// </summary>
        /// <param name="values">
        /// A collection that contains the strings to concatenate.
        /// </param>
        /// <param name="separator">
        /// The string to use as a separator. <paramref name="separator"/> is
        /// included in the returned string only if <paramref name="values"/>
        /// has more than one element.
        /// </param>
        /// <returns>
        /// A string that consists of the members of values delimited by the
        /// separator string. If <paramref name="values"/> has no members, the
        /// method returns <see cref="System.String.Empty"/>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="values"/> is <see langword="null"/>.
        /// </exception>
        public static string Join(this IEnumerable<string> values, string separator)
        {
            return String.Join(separator, values);
        }

        /// <summary>
        /// Concatenates the members of a collection, using a comma (,)
        /// separator between each member.
        /// </summary>
        /// <typeparam name="T">The type of the members of <paramref name="values"/>.</typeparam>
        /// <param name="values">
        /// A collection that contains the objects to concatenate.
        /// </param>
        /// <returns>
        /// A string that consists of the members of values delimited by a comma
        /// (,). If <paramref name="values"/> has no members, the method returns <see cref="System.String.Empty"/>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="values"/> is <see langword="null"/>.
        /// </exception>
        public static string Join<T>(this IEnumerable<T> values)
        {
            return String.Join(",", values);
        }

        /// <summary>
        /// Concatenates the members of a collection, using the specified
        /// separator between each member.
        /// </summary>
        /// <typeparam name="T">The type of the members of <paramref name="values"/>.</typeparam>
        /// <param name="values">
        /// A collection that contains the objects to concatenate.
        /// </param>
        /// <param name="separator">
        /// The string to use as a separator. <paramref name="separator"/> is
        /// included in the returned string only if <paramref name="values"/>
        /// has more than one element.
        /// </param>
        /// <returns>
        /// A string that consists of the members of values delimited by the
        /// separator string. If <paramref name="values"/> has no members, the
        /// method returns <see cref="System.String.Empty"/>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="values"/> is <see langword="null"/>.
        /// </exception>
        public static string Join<T>(this IEnumerable<T> values, string separator)
        {
            return String.Join(separator, values);
        }

        /// <summary>
        /// Partitions the specified collection into a collection of smaller collections.
        /// </summary>
        /// <typeparam name="T">The type of the members of <paramref name="source"/>.</typeparam>
        /// <param name="source">
        /// A collection that contains the objects to partition.
        /// </param>
        /// <param name="size">The size of each partition.</param>
        /// <returns>
        /// A new <see cref="IEnumerable{T}"/> containing one or more
        /// <see cref="IEnumerable{T}"/> collections.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Reviewed.")]
        public static IEnumerable<IEnumerable<T>> Partition<T>(this IEnumerable<T> source, int size)
        {
            Requires.NotNull(source, nameof(source));
            Requires.Range(size > 0, nameof(size), Strings.ArgumentOutOfRange_IndexLessThanZero);

            T[]? array = null;
            var count = 0;
            foreach (var item in source)
            {
                if (array == null)
                {
                    array = new T[size];
                }

                array[count++] = item;
                if (count == size)
                {
                    yield return new ReadOnlyCollection<T>(array);
                    array = null;
                    count = 0;
                }
            }

            if (array != null)
            {
                Array.Resize(ref array, count);
                yield return new ReadOnlyCollection<T>(array);
            }
        }

        /// <summary>
        /// Returns a pseudo-random element from the specified collection.
        /// </summary>
        /// <typeparam name="T">The type of the members of <paramref name="source"/>.</typeparam>
        /// <param name="source">
        /// A collection that contains the objects to segment.
        /// </param>
        /// <returns>A pseudo-random element from the specified collection.</returns>
        public static T RandomElement<T>(this IEnumerable<T> source)
        {
            return source.RandomElement<T>(new Random());
        }

        /// <summary>
        /// Returns a pseudo-random element from the specified collection.
        /// </summary>
        /// <typeparam name="T">The type of the members of <paramref name="source"/>.</typeparam>
        /// <param name="source">
        /// A collection that contains the objects to segment.
        /// </param>
        /// <param name="random">A pseudo-random number generator.</param>
        /// <returns>A pseudo-random element from the specified collection.</returns>
        public static T RandomElement<T>(this IEnumerable<T> source, Random random)
        {
            var current = default(T);
            var count = 0;
            foreach (var element in source)
            {
                count++;
                if (random.Next(count) == 0)
                {
                    current = element;
                }
            }

            return current!;
        }

#if NETSTANDARD2_0
        public static IEnumerable<T> SkipLast<T>(this IEnumerable<T> source, int n)
        {
            var it = source.GetEnumerator();
            bool hasRemainingItems = false;
            var cache = new Queue<T>(n + 1);

            do
            {
                if (hasRemainingItems = it.MoveNext())
                {
                    cache.Enqueue(it.Current);
                    if (cache.Count > n)
                        yield return cache.Dequeue();
                }
            } while (hasRemainingItems);
        }
#endif

        /// <summary>
        /// Returns a segment of the specified collection.
        /// </summary>
        /// <typeparam name="T">The type of the members of <paramref name="source"/>.</typeparam>
        /// <param name="source">
        /// A collection that contains the objects to segment.
        /// </param>
        /// <param name="startIndex">The starting index of the collection.</param>
        /// <param name="endIndex">The ending index of the collection.</param>
        /// <returns>
        /// A new <see cref="IEnumerable{T}"/> containing the segment of the
        /// specified collection.
        /// </returns>
        /// <exception cref="System.ArgumentException">
        /// <paramref name="startIndex"/> must be less than or equal to <paramref name="endIndex"/>.
        /// </exception>
        public static IEnumerable<T> Slice<T>(this IEnumerable<T> source, int startIndex, int endIndex)
        {
            Requires.NotNull(source, nameof(source));
            Requires.Range(startIndex <= endIndex, nameof(startIndex), Strings.Argument_StartIndexGreaterThanEndIndex);
            Requires.Range(startIndex >= 0, nameof(startIndex), Strings.ArgumentOutOfRange_IndexLessThanZero);
            Requires.Range(endIndex >= 0, nameof(endIndex), Strings.ArgumentOutOfRange_IndexLessThanZero);

            var index = 0;
            foreach (var item in source)
            {
                if (index >= startIndex && index <= endIndex)
                {
                    yield return item;
                }

                ++index;
            }
        }

        /// <summary>
        /// Filters a sequence of values based on a predicate. Each element's
        /// index is used in the logic of the predicate function.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to filter.</param>
        /// <param name="condition">
        /// The condition used to determine if the sequence should be filtered.
        /// </param>
        /// <param name="predicate">
        /// A function to test each source element for a condition; the second
        /// parameter of the function represents the index of the source element.
        /// </param>
        /// <returns>
        /// If <paramref name="condition"/> is <see langword="true"/>, an
        /// <see cref="IEnumerable{T}"/> that contains elements from the input
        /// sequence that satisfy the condition; otherwise, the original input sequence.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="source"/> or <paramref name="predicate"/> is <see langword="null"/>.
        /// </exception>
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, bool condition, Func<T, bool> predicate)
        {
            return condition ? source.Where(predicate) : source;
        }

        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to filter.</param>
        /// <param name="condition">
        /// The condition used to determine if the sequence should be filtered.
        /// </param>
        /// <param name="predicate">
        /// A function to test each source element for a condition.
        /// </param>
        /// <returns>
        /// If <paramref name="condition"/> is <see langword="true"/>, an
        /// <see cref="IEnumerable{T}"/> that contains elements from the input
        /// sequence that satisfy the condition; otherwise, the original input sequence.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="source"/> or <paramref name="predicate"/> is <see langword="null"/>.
        /// </exception>
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, bool condition, Func<T, int, bool> predicate)
        {
            return condition ? source.Where(predicate) : source;
        }

        /// <summary>
        /// Returns the element at a specified index in a sequence.
        /// </summary>
        /// <param name="source">
        /// An <see cref="IEnumerable"/> to return an element from.
        /// </param>
        /// <param name="index">The zero-based index of the element to retrieve.</param>
        /// <returns>
        /// The element at the specified position in the source sequence.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="index"/> is less than 0 or greater than or equal to
        /// the number of elements in <paramref name="source"/>.
        /// </exception>
        internal static object ElementAt(this IEnumerable source, int index)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            if (source is IList list && list.Count > 0)
            {
                return list[index];
            }

            foreach (var item in source)
            {
                if (index == 0)
                {
                    return item;
                }

                index--;
            }

            throw new ArgumentOutOfRangeException(nameof(index));
        }

        /// <summary>
        /// Returns the element at a specified index in a sequence or
        /// <see langword="null"/> if the index is out of range.
        /// </summary>
        /// <param name="source">
        /// An <see cref="IEnumerable"/> to return an element from.
        /// </param>
        /// <param name="index">The zero-based index of the element to retrieve.</param>
        /// <returns>
        /// <see langword="null"/> if the index is outside the bounds of the
        /// source sequence; otherwise, the element at the specified position in
        /// the source sequence.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="index"/> is less than 0 or greater than or equal to
        /// the number of elements in <paramref name="source"/>.
        /// </exception>
        internal static object? ElementAtOrDefault(this IEnumerable source, int index)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            if (source is IList list && list.Count > 0)
            {
                return list[index];
            }

            foreach (var item in source)
            {
                if (index == 0)
                {
                    return item;
                }

                index--;
            }

            return null;
        }
    }
}