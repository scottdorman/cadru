//------------------------------------------------------------------------------
// <copyright file="CustomAssert.cs" 
//  company="Scott Dorman" 
//  library="Cadru">
//    Copyright (C) 2001-2013 Scott Dorman.
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

namespace Cadru.UnitTest.Framework
{
    using System;
    using System.Collections;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Contains assertion types that are not provided with the standard MSTest assertions.
    /// </summary>
    public static class CustomAssert
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

        #region AreEqualIgnoringCase

        #region AreEqualIgnoringCase(string expected, string actual)
        /// <summary>
        /// Asserts that two strings are equal, without regard to case. 
        /// </summary>
        /// <param name="expected">The expected string.</param>
        /// <param name="actual">The actual string.</param>
        public static void AreEqualIgnoringCase(string expected, string actual)
        {
            CustomAssert.AreEqualIgnoringCase(expected, actual, Properties.Resources.Assertion_GenericFailure, expected, actual);
        }
        #endregion

        #region AreEqualIgnoringCase(string expected, string actual, string message)
        /// <summary>
        /// Asserts that two strings are equal, without regard to case. 
        /// </summary>
        /// <param name="expected">The expected string.</param>
        /// <param name="actual">The actual string.</param>
        /// <param name="message">A message to display. This message can be seen in the unit test results.</param>
        public static void AreEqualIgnoringCase(string expected, string actual, string message)
        {
            CustomAssert.AreEqualIgnoringCase(expected, actual, message, null);
        }
        #endregion

        #region AreEqualIgnoringCase(string expected, string actual, string message, params object[] parameters)
        /// <summary>
        /// Asserts that two strings are equal, without regard to case. 
        /// </summary>
        /// <param name="expected">The expected string.</param>
        /// <param name="actual">The actual string.</param>
        /// <param name="message">A message to display. This message can be seen in the unit test results.</param>
        /// <param name="parameters">An array of parameters to use when formatting <paramref name="message"/>.</param>
        public static void AreEqualIgnoringCase(string expected, string actual, string message, params object[] parameters)
        {
            Assert.IsTrue(String.Compare(expected, actual, StringComparison.CurrentCultureIgnoreCase) == 0, message, parameters);
        }
        #endregion

        #endregion

        #region IsEmpty

        #region IsEmpty(ICollection collection)
        /// <summary>
        /// Assert that an array, list or other collection is empty.
        /// </summary>
        /// <param name="collection">The value to be tested.</param>
        public static void IsEmpty(ICollection collection)
        {
            CustomAssert.IsEmpty(collection, Properties.Resources.Assertion_CollectionFailure, 0, collection.Count);
        }
        #endregion

        #region IsEmpty(ICollection collection, string message)
        /// <summary>
        /// Assert that an array, list or other collection is empty.
        /// </summary>
        /// <param name="collection">The value to be tested.</param>
        /// <param name="message">A message to display. This message can be seen in the unit test results.</param>
        public static void IsEmpty(ICollection collection, string message)
        {
            CustomAssert.IsEmpty(collection, message, null);
        }
        #endregion

        #region IsEmpty(ICollection collection, string message, params object[] parameters)
        /// <summary>
        /// Assert that an array, list or other collection is empty.
        /// </summary>
        /// <param name="collection">The value to be tested.</param>
        /// <param name="message">A message to display. This message can be seen in the unit test results.</param>
        /// <param name="parameters">An array of parameters to use when formatting <paramref name="message"/>.</param>
        public static void IsEmpty(ICollection collection, string message, params object[] parameters)
        {
            Assert.IsTrue(collection.Count == 0, message, parameters);
        }
        #endregion

        #region IsEmpty(string value)
        /// <summary>
        /// Asserts that a string is empty.
        /// </summary>
        /// <param name="value">The value to be tested.</param>
        public static void IsEmpty(string value)
        {
            CustomAssert.IsEmpty(value, Properties.Resources.Assertion_GenericFailure, String.Empty, value);
        }
        #endregion

        #region IsEmpty(string value, string message)
        /// <summary>
        /// Asserts that a string is empty.
        /// </summary>
        /// <param name="value">The value to be tested.</param>
        /// <param name="message">A message to display. This message can be seen in the unit test results.</param>
        public static void IsEmpty(string value, string message)
        {
            CustomAssert.IsEmpty(value, message, null);
        }
        #endregion

        #region IsEmpty(string value, string message, params object[] parameters)
        /// <summary>
        /// Asserts that a string is empty.
        /// </summary>
        /// <param name="value">The value to be tested.</param>
        /// <param name="message">A message to display. This message can be seen in the unit test results.</param>
        /// <param name="parameters">An array of parameters to use when formatting <paramref name="message"/>.</param>
        public static void IsEmpty(string value, string message, params object[] parameters)
        {
            Assert.IsTrue(value.Length == 0, message, parameters);
        }
        #endregion

        #endregion

        #region IsNotEmpty

        #region IsNotEmpty(ICollection collection)
        /// <summary>
        /// Assert that an array, list or other collection is not empty.
        /// </summary>
        /// <param name="collection">The value to be tested.</param>
        public static void IsNotEmpty(ICollection collection)
        {
            CustomAssert.IsNotEmpty(collection, Properties.Resources.Assertion_CollectionFailure, collection.Count, 0);
        }
        #endregion

        #region IsNotEmpty(ICollection collection, string message)
        /// <summary>
        /// Assert that an array, list or other collection is not empty.
        /// </summary>
        /// <param name="collection">The value to be tested.</param>
        /// <param name="message">A message to display. This message can be seen in the unit test results.</param>
        public static void IsNotEmpty(ICollection collection, string message)
        {
            CustomAssert.IsNotEmpty(collection, message, null);
        }
        #endregion

        #region IsNotEmpty(ICollection collection, string message, params object[] parameters)
        /// <summary>
        /// Assert that an array, list or other collection is not empty.
        /// </summary>
        /// <param name="collection">The value to be tested.</param>
        /// <param name="message">A message to display. This message can be seen in the unit test results.</param>
        /// <param name="parameters">An array of parameters to use when formatting <paramref name="message"/>.</param>
        public static void IsNotEmpty(ICollection collection, string message, params object[] parameters)
        {
            Assert.IsFalse(collection.Count == 0, message, parameters);
        }
        #endregion

        #region IsNotEmpty(string value)
        /// <summary>
        /// Asserts that a string is not empty.
        /// </summary>
        /// <param name="value">The value to be tested.</param>
        public static void IsNotEmpty(string value)
        {
            CustomAssert.IsNotEmpty(value, Properties.Resources.Assertion_GenericFailure, value, String.Empty);
        }
        #endregion

        #region IsNotEmpty(string value, string message)
        /// <summary>
        /// Asserts that a string is not empty.
        /// </summary>
        /// <param name="value">The value to be tested.</param>
        /// <param name="message">A message to display. This message can be seen in the unit test results.</param>
        public static void IsNotEmpty(string value, string message)
        {
            CustomAssert.IsNotEmpty(value, message, null);
        }
        #endregion

        #region IsNotEmpty(string value, string message, params object[] parameters)
        /// <summary>
        /// Asserts that a string is not empty.
        /// </summary>
        /// <param name="value">The value to be tested.</param>
        /// <param name="message">A message to display. This message can be seen in the unit test results.</param>
        /// <param name="parameters">An array of parameters to use when formatting <paramref name="message"/>.</param>
        public static void IsNotEmpty(string value, string message, params object[] parameters)
        {
            Assert.IsFalse(value.Length == 0, message, parameters);
        }
        #endregion

        #endregion

        #endregion
    }
}
