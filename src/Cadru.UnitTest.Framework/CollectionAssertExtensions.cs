using System;
using System.Collections;

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
        public static void IsEmpty(this CollectionAssert assert, IEnumerable collection, string message, params object[] parameters)
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
        public static void IsNotEmpty(this CollectionAssert assert, IEnumerable collection, string message, params object[] parameters)
        {
            if (collection != null && !collection.GetEnumerator().MoveNext())
            {
                Helpers.HandleFail("CollectionAssert.IsNotEmpty", message, parameters);
            }
        }
    }
}
