//------------------------------------------------------------------------------
// <copyright file="CustomAssert.cs"
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

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cadru.UnitTest.Framework
{
    /// <summary>
    /// Contains assertion types that are not provided with the standard MSTest assertions.
    /// </summary>
    public static class CustomAssert
    {
        /// <summary>
        /// Asserts that two strings are equal, without regard to case.
        /// </summary>
        /// <param name="expected">The expected string.</param>
        /// <param name="actual">The actual string.</param>
        [Obsolete("Use Assert.That.AreEqualIgnoringCase(string, string).")]
        public static void AreEqualIgnoringCase(string expected, string actual)
        {
            Assert.That.AreEqualIgnoringCase(expected, actual);
        }

        /// <summary>
        /// Asserts that two strings are equal, without regard to case.
        /// </summary>
        /// <param name="expected">The expected string.</param>
        /// <param name="actual">The actual string.</param>
        /// <param name="message">
        /// A message to display. This message can be seen in the unit test results.
        /// </param>
        [Obsolete("Use Assert.That.AreEqualIgnoringCase(string, string, string).")]
        public static void AreEqualIgnoringCase(string expected, string actual, string message)
        {
            Assert.That.AreEqualIgnoringCase(expected, actual, message);
        }

        /// <summary>
        /// Asserts that two strings are equal, without regard to case.
        /// </summary>
        /// <param name="expected">The expected string.</param>
        /// <param name="actual">The actual string.</param>
        /// <param name="message">
        /// A message to display. This message can be seen in the unit test results.
        /// </param>
        /// <param name="parameters">
        /// An array of parameters to use when formatting <paramref name="message"/>.
        /// </param>
        [Obsolete("Use Assert.That.AreEqualIgnoringCase(string, string, string, object[]).")]
        public static void AreEqualIgnoringCase(string expected, string actual, string message, params object[] parameters)
        {
            Assert.That.AreEqualIgnoringCase(expected, actual, message, parameters);
        }

        /// <summary>
        /// Assert that an array, list or other collection is empty.
        /// </summary>
        /// <param name="collection">The value to be tested.</param>
        [Obsolete("Use CollectionAssert.That.IsEmpty(ICollection).")]
        public static void IsEmpty(ICollection collection)
        {
            CollectionAssert.That.IsEmpty(collection);
        }

        /// <summary>
        /// Assert that an array, list or other collection is empty.
        /// </summary>
        /// <param name="collection">The value to be tested.</param>
        /// <param name="message">
        /// A message to display. This message can be seen in the unit test results.
        /// </param>
        [Obsolete("Use CollectionAssert.That.IsEmpty(ICollection, string).")]
        public static void IsEmpty(ICollection collection, string message)
        {
            CollectionAssert.That.IsEmpty(collection, message);
        }

        /// <summary>
        /// Assert that an array, list or other collection is empty.
        /// </summary>
        /// <param name="collection">The value to be tested.</param>
        /// <param name="message">
        /// A message to display. This message can be seen in the unit test results.
        /// </param>
        /// <param name="parameters">
        /// An array of parameters to use when formatting <paramref name="message"/>.
        /// </param>
        [Obsolete("Use CollectionAssert.That.IsEmpty(ICollection, string, object[]).")]
        public static void IsEmpty(ICollection collection, string message, params object[] parameters)
        {
            CollectionAssert.That.IsEmpty(collection, message, parameters);
        }

        /// <summary>
        /// Asserts that a string is empty.
        /// </summary>
        /// <param name="value">The value to be tested.</param>
        [Obsolete("Use Assert.That.IsEmpty(string).")]
        public static void IsEmpty(string value)
        {
            Assert.That.IsEmpty(value);
        }

        /// <summary>
        /// Asserts that a string is empty.
        /// </summary>
        /// <param name="value">The value to be tested.</param>
        /// <param name="message">
        /// A message to display. This message can be seen in the unit test results.
        /// </param>
        [Obsolete("Use Assert.That.IsEmpty(string, string).")]
        public static void IsEmpty(string value, string message)
        {
            Assert.That.IsEmpty(value, message);
        }

        /// <summary>
        /// Asserts that a string is empty.
        /// </summary>
        /// <param name="value">The value to be tested.</param>
        /// <param name="message">
        /// A message to display. This message can be seen in the unit test results.
        /// </param>
        /// <param name="parameters">
        /// An array of parameters to use when formatting <paramref name="message"/>.
        /// </param>
        [Obsolete("Use Assert.That.IsEmpty(string, string, object[]).")]
        public static void IsEmpty(string value, string message, params object[] parameters)
        {
            Assert.That.IsEmpty(value, message, parameters);
        }

        /// <summary>
        /// Assert that an array, list or other collection is not empty.
        /// </summary>
        /// <param name="collection">The value to be tested.</param>
        [Obsolete("Use CollectionAssert.That.IsNotEmpty(ICollection).")]
        public static void IsNotEmpty(ICollection collection)
        {
            CollectionAssert.That.IsNotEmpty(collection);
        }

        /// <summary>
        /// Assert that an array, list or other collection is not empty.
        /// </summary>
        /// <param name="collection">The value to be tested.</param>
        /// <param name="message">
        /// A message to display. This message can be seen in the unit test results.
        /// </param>
        [Obsolete("Use CollectionAssert.That.IsNotEmpty(ICollection, string).")]
        public static void IsNotEmpty(ICollection collection, string message)
        {
            CollectionAssert.That.IsNotEmpty(collection, message);
        }

        /// <summary>
        /// Assert that an array, list or other collection is not empty.
        /// </summary>
        /// <param name="collection">The value to be tested.</param>
        /// <param name="message">
        /// A message to display. This message can be seen in the unit test results.
        /// </param>
        /// <param name="parameters">
        /// An array of parameters to use when formatting <paramref name="message"/>.
        /// </param>
        [Obsolete("Use CollectionAssert.That.IsNotEmpty(ICollection, string, object[]).")]
        public static void IsNotEmpty(ICollection collection, string message, params object[] parameters)
        {
            CollectionAssert.That.IsNotEmpty(collection, message, parameters);
        }

        /// <summary>
        /// Asserts that a string is not empty.
        /// </summary>
        /// <param name="value">The value to be tested.</param>
        [Obsolete("Use Assert.That.IsNotEmpty(string).")]
        public static void IsNotEmpty(string value)
        {
            Assert.That.IsNotEmpty(value);
        }

        /// <summary>
        /// Asserts that a string is not empty.
        /// </summary>
        /// <param name="value">The value to be tested.</param>
        /// <param name="message">
        /// A message to display. This message can be seen in the unit test results.
        /// </param>
        [Obsolete("Use Assert.That.IsNotEmpty(string, string).")]
        public static void IsNotEmpty(string value, string message)
        {
            Assert.That.IsNotEmpty(value, message);
        }

        /// <summary>
        /// Asserts that a string is not empty.
        /// </summary>
        /// <param name="value">The value to be tested.</param>
        /// <param name="message">
        /// A message to display. This message can be seen in the unit test results.
        /// </param>
        /// <param name="parameters">
        /// An array of parameters to use when formatting <paramref name="message"/>.
        /// </param>
        [Obsolete("Use Assert.That.IsNotEmpty(string, string, object[]).")]
        public static void IsNotEmpty(string value, string message, params object[] parameters)
        {
            Assert.That.IsNotEmpty(value, message, parameters);
        }
    }
}