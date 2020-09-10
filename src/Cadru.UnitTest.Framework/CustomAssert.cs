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

using Cadru.UnitTest.Framework.Resources;

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
        public static void AreEqualIgnoringCase(string expected, string actual)
        {
            AreEqualIgnoringCase(expected, actual, Strings.Assertion_GenericFailure, expected, actual);
        }

        /// <summary>
        /// Asserts that two strings are equal, without regard to case.
        /// </summary>
        /// <param name="expected">The expected string.</param>
        /// <param name="actual">The actual string.</param>
        /// <param name="message">A message to display. This message can be seen in the unit test results.</param>
        public static void AreEqualIgnoringCase(string expected, string actual, string message)
        {
            AreEqualIgnoringCase(expected, actual, message, null);
        }

        /// <summary>
        /// Asserts that two strings are equal, without regard to case.
        /// </summary>
        /// <param name="expected">The expected string.</param>
        /// <param name="actual">The actual string.</param>
        /// <param name="message">A message to display. This message can be seen in the unit test results.</param>
        /// <param name="parameters">An array of parameters to use when formatting <paramref name="message" />.</param>
        public static void AreEqualIgnoringCase(string expected, string actual, string message, params object[] parameters)
        {
            Assert.IsTrue(String.Compare(expected, actual, StringComparison.CurrentCultureIgnoreCase) == 0, message, parameters);
        }

        /// <summary>
        /// Assert that an array, list or other collection is empty.
        /// </summary>
        /// <param name="collection">The value to be tested.</param>
        public static void IsEmpty(ICollection collection)
        {
            IsEmpty(collection, Strings.Assertion_CollectionFailure, 0, collection.Count);
        }

        /// <summary>
        /// Assert that an array, list or other collection is empty.
        /// </summary>
        /// <param name="collection">The value to be tested.</param>
        /// <param name="message">A message to display. This message can be seen in the unit test results.</param>
        public static void IsEmpty(ICollection collection, string message)
        {
            IsEmpty(collection, message, null);
        }

        /// <summary>
        /// Assert that an array, list or other collection is empty.
        /// </summary>
        /// <param name="collection">The value to be tested.</param>
        /// <param name="message">A message to display. This message can be seen in the unit test results.</param>
        /// <param name="parameters">An array of parameters to use when formatting <paramref name="message" />.</param>
        public static void IsEmpty(ICollection collection, string message, params object[] parameters)
        {
            Assert.IsTrue(collection.Count == 0, message, parameters);
        }

        /// <summary>
        /// Asserts that a string is empty.
        /// </summary>
        /// <param name="value">The value to be tested.</param>
        public static void IsEmpty(string value)
        {
            IsEmpty(value, Strings.Assertion_GenericFailure, String.Empty, value);
        }

        /// <summary>
        /// Asserts that a string is empty.
        /// </summary>
        /// <param name="value">The value to be tested.</param>
        /// <param name="message">A message to display. This message can be seen in the unit test results.</param>
        public static void IsEmpty(string value, string message)
        {
            IsEmpty(value, message, null);
        }

        /// <summary>
        /// Asserts that a string is empty.
        /// </summary>
        /// <param name="value">The value to be tested.</param>
        /// <param name="message">A message to display. This message can be seen in the unit test results.</param>
        /// <param name="parameters">An array of parameters to use when formatting <paramref name="message" />.</param>
        public static void IsEmpty(string value, string message, params object[] parameters)
        {
            Assert.IsTrue(value.Length == 0, message, parameters);
        }

        /// <summary>
        /// Assert that an array, list or other collection is not empty.
        /// </summary>
        /// <param name="collection">The value to be tested.</param>
        public static void IsNotEmpty(ICollection collection)
        {
            IsNotEmpty(collection, Strings.Assertion_CollectionFailure, collection.Count, 0);
        }

        /// <summary>
        /// Assert that an array, list or other collection is not empty.
        /// </summary>
        /// <param name="collection">The value to be tested.</param>
        /// <param name="message">A message to display. This message can be seen in the unit test results.</param>
        public static void IsNotEmpty(ICollection collection, string message)
        {
            IsNotEmpty(collection, message, null);
        }

        /// <summary>
        /// Assert that an array, list or other collection is not empty.
        /// </summary>
        /// <param name="collection">The value to be tested.</param>
        /// <param name="message">A message to display. This message can be seen in the unit test results.</param>
        /// <param name="parameters">An array of parameters to use when formatting <paramref name="message" />.</param>
        public static void IsNotEmpty(ICollection collection, string message, params object[] parameters)
        {
            Assert.IsFalse(collection.Count == 0, message, parameters);
        }

        /// <summary>
        /// Asserts that a string is not empty.
        /// </summary>
        /// <param name="value">The value to be tested.</param>
        public static void IsNotEmpty(string value)
        {
            IsNotEmpty(value, Strings.Assertion_GenericFailure, value, String.Empty);
        }

        /// <summary>
        /// Asserts that a string is not empty.
        /// </summary>
        /// <param name="value">The value to be tested.</param>
        /// <param name="message">A message to display. This message can be seen in the unit test results.</param>
        public static void IsNotEmpty(string value, string message)
        {
            IsNotEmpty(value, message, null);
        }

        /// <summary>
        /// Asserts that a string is not empty.
        /// </summary>
        /// <param name="value">The value to be tested.</param>
        /// <param name="message">A message to display. This message can be seen in the unit test results.</param>
        /// <param name="parameters">An array of parameters to use when formatting <paramref name="message" />.</param>
        public static void IsNotEmpty(string value, string message, params object[] parameters)
        {
            Assert.IsFalse(value.Length == 0, message, parameters);
        }
    }
}