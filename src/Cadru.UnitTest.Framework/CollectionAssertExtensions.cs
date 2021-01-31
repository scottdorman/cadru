using System;
using System.Collections;
using System.Linq;

using Cadru.UnitTest.Framework.Resources;

using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Cadru.UnitTest.Framework
{
    /// <summary>
    /// A collection of helpers to test various conditions within unit tests.
    /// If the condition being tested is not met, an exception is thrown.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]

    public static class CollectionAssertExtensions
    {
        private static readonly object?[] Empty = Array.Empty<object>();

        /// <summary>
        /// Assert that an array, list or other collection is empty.
        /// </summary>
        /// <param name="assert">The <see cref="CollectionAssert"/> instance to extend.</param>
        /// <param name="collection">The value to be tested.</param>
        public static void IsEmpty(this CollectionAssert assert, IEnumerable collection)
        {
            assert.IsEmpty(collection, Strings.IsCollectionEmptyFailMsg);
        }

        /// <summary>
        /// Assert that an array, list or other collection is empty.
        /// </summary>
        /// <param name="assert">The <see cref="CollectionAssert"/> instance to extend.</param>
        /// <param name="collection">The value to be tested.</param>
        /// <param name="message">
        /// A message to display. This message can be seen in the unit test results.
        /// </param>
        public static void IsEmpty(this CollectionAssert assert, IEnumerable collection, string message)
        {
            assert.IsEmpty(collection, message, 0);
        }

        /// <summary>
        /// Assert that an array, list or other collection is empty.
        /// </summary>
        /// <param name="assert">The <see cref="CollectionAssert"/> instance to extend.</param>
        /// <param name="collection">The value to be tested.</param>
        /// <param name="message">
        /// A message to display. This message can be seen in the unit test results.
        /// </param>
        /// <param name="parameters">
        /// An array of parameters to use when formatting <paramref name="message"/>.
        /// </param>
        public static void IsEmpty(this CollectionAssert assert, IEnumerable collection, string message, params object?[] parameters)
        {
            if (collection != null && collection.GetEnumerator().MoveNext())
            {
                Helpers.HandleFail("CollectionAssert.IsEmpty", message, parameters);
            }
        }

        /// <summary>
        /// Assert that an array, list or other collection is not empty.
        /// </summary>
        /// <param name="assert">The <see cref="CollectionAssert"/> instance to extend.</param>
        /// <param name="collection">The value to be tested.</param>
        public static void IsNotEmpty(this CollectionAssert assert, IEnumerable collection)
        {
            assert.IsNotEmpty(collection, Strings.IsCollectionNotEmptyFailMsg);
        }

        /// <summary>
        /// Assert that an array, list or other collection is not empty.
        /// </summary>
        /// <param name="assert">The <see cref="CollectionAssert"/> instance to extend.</param>
        /// <param name="collection">The value to be tested.</param>
        /// <param name="message">
        /// A message to display. This message can be seen in the unit test results.
        /// </param>
        public static void IsNotEmpty(this CollectionAssert assert, IEnumerable collection, string message)
        {
            assert.IsNotEmpty(collection, message, Array.Empty<object>());
        }

        /// <summary>
        /// Assert that an array, list or other collection is not empty.
        /// </summary>
        /// <param name="assert">The <see cref="CollectionAssert"/> instance to extend.</param>
        /// <param name="collection">The value to be tested.</param>
        /// <param name="message">
        /// A message to display. This message can be seen in the unit test results.
        /// </param>
        /// <param name="parameters">
        /// An array of parameters to use when formatting <paramref name="message"/>.
        /// </param>
        public static void IsNotEmpty(this CollectionAssert assert, IEnumerable collection, string message, params object?[] parameters)
        {
            if (collection != null && !collection.GetEnumerator().MoveNext())
            {
                Helpers.HandleFail("CollectionAssert.IsNotEmpty", message, parameters);
            }
        }

        #region IsOrdered
        /// <summary>
        /// Assert that an array, list or other collection is ordered
        /// </summary>
        /// <param name="assert">The <see cref="CollectionAssert"/> instance to extend.</param>
        /// <param name="collection">An array, list or other collection implementing IEnumerable</param>
        /// <param name="message">The message to be displayed on failure</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        public static void IsOrdered(this CollectionAssert assert, IEnumerable collection, string message, params object?[] args)
        {
            assert.IsOrdered(collection, Comparer.Default, message, args);
        }

        /// <summary>
        /// Assert that an array, list or other collection is ordered
        /// </summary>
        /// <param name="assert">The <see cref="CollectionAssert"/> instance to extend.</param>
        /// <param name="collection">An array, list or other collection implementing IEnumerable</param>
        public static void IsOrdered(this CollectionAssert assert, IEnumerable collection)
        {
            assert.IsOrdered(collection, Comparer.Default, String.Empty, Empty);
        }

        /// <summary>
        /// Assert that an array, list or other collection is ordered
        /// </summary>
        /// <param name="assert">The <see cref="CollectionAssert"/> instance to extend.</param>
        /// <param name="collection">An array, list or other collection implementing IEnumerable</param>
        /// <param name="comparer">A custom comparer to perform the comparisons</param>
        /// <param name="message">The message to be displayed on failure</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        public static void IsOrdered(this CollectionAssert assert, IEnumerable collection, IComparer comparer, string message, params object?[] args)
        {
            Helpers.CheckParameterNotNull(collection, "CollectionAssert.IsOrdered", "collection", String.Empty);
            Helpers.CheckParameterNotNull(comparer, "CollectionAssert.IsOrdered", "comparer", String.Empty);

            var enumerator = collection.GetEnumerator();
            using (enumerator as IDisposable)
            {
                do
                {
                    var x = enumerator.Current;
                    if (enumerator.MoveNext())
                    {
                        if (comparer.Compare(x, enumerator.Current) > 0)
                        {
                            Helpers.HandleFail("CollectionAssert.IsOrdered", Strings.IsOrderedFail);
                            break;
                        }
                    }
                } while (enumerator.MoveNext());
            }
        }

        /// <summary>
        /// Assert that an array, list or other collection is ordered
        /// </summary>
        /// <param name="assert">The <see cref="CollectionAssert"/> instance to extend.</param>
        /// <param name="collection">An array, list or other collection implementing IEnumerable</param>
        /// <param name="comparer">A custom comparer to perform the comparisons</param>
        public static void IsOrdered(this CollectionAssert assert, IEnumerable collection, IComparer comparer)
        {
            assert.IsOrdered(collection, comparer, String.Empty, Empty);
        }
        #endregion
    }
}
