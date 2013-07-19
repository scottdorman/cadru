//------------------------------------------------------------------------------
// <copyright file="ConditionAssert.cs" 
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
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Contains assertion types that are not provided with the standard MSTest assertions.
    /// </summary>
    public static class ConditionAssert
    {
        #region Greater

        #region Greater<T1>(T arg1, T arg2)
        /// <summary>
        /// Verifies that the first value is greater than the second value.
        /// </summary>
        /// <typeparam name="T">The type of the values to compare.</typeparam>
        /// <param name="arg1">The first value, expected to be greater.</param>
        /// <param name="arg2">The second value, expected to be less.</param>
        public static void Greater<T>(T arg1, T arg2) where T : IComparable
        {
            Greater(arg1, arg2, "{0} is less than or equal to {1}.", arg2, arg1);
        }
        #endregion

        #region Greater<T1>(T arg1, T arg2, string message)
        /// <summary>
        /// Verifies that the first value is greater than the second value.
        /// </summary>
        /// <typeparam name="T">The type of the values to compare.</typeparam>
        /// <param name="arg1">The first value, expected to be greater.</param>
        /// <param name="arg2">The second value, expected to be less.</param>
        /// <param name="message">A message to display. This message can be seen in the unit test results.</param>
        public static void Greater<T>(T arg1, T arg2, string message) where T : IComparable
        {
            if (((IComparable)arg1).CompareTo(arg2) <= 0)
            {
                Assert.Fail(message);
            }
        }
        #endregion

        #region Greater<T>(T arg1, T arg2, string message, params object[] parameters)
        /// <summary>
        /// Verifies that the first value is greater than the second value.
        /// </summary>
        /// <typeparam name="T">The type of the values to compare.</typeparam>
        /// <param name="arg1">The first value, expected to be greater.</param>
        /// <param name="arg2">The second value, expected to be less.</param>
        /// <param name="message">A message to display. This message can be seen in the unit test results.</param>
        /// <param name="parameters">An array of parameters to use when formatting <paramref name="message"/>.</param>
        public static void Greater<T>(T arg1, T arg2, string message, params object[] parameters) where T : IComparable
        {
            if (((IComparable)arg1).CompareTo(arg2) <= 0)
            {
                Assert.Fail(message, parameters);
            }
        }
        #endregion

        #endregion

        #region GreaterOrEqual

        #region GreaterOrEqual<T>(T arg1, T arg2)
        /// <summary>
        /// Verifies that the first value is greater than or equal to the second value.
        /// </summary>
        /// <typeparam name="T">The type of the values to compare.</typeparam>
        /// <param name="arg1">The first value, expected to be greater.</param>
        /// <param name="arg2">The second value, expected to be less.</param>
        public static void GreaterOrEqual<T>(T arg1, T arg2) where T : IComparable
        {
            GreaterOrEqual(arg1, arg2, "{0} is less than {1}.", arg2, arg1);
        }
        #endregion

        #region GreaterOrEqual<T>(T arg1, T arg2, string message)
        /// <summary>
        /// Verifies that the first value is greater than or equal to the second value.
        /// </summary>
        /// <typeparam name="T">The type of the values to compare.</typeparam>
        /// <param name="arg1">The first value, expected to be greater.</param>
        /// <param name="arg2">The second value, expected to be less.</param>
        /// <param name="message">A message to display. This message can be seen in the unit test results.</param>
        public static void GreaterOrEqual<T>(T arg1, T arg2, string message) where T : IComparable
        {
            if (((IComparable)arg1).CompareTo(arg2) < 0)
            {
                Assert.Fail(message);
            }
        }
        #endregion

        #region GreaterOrEqual<T>(T arg1, T arg2, string message, params object[] parameters)
        /// <summary>
        /// Verifies that the first value is greater than or equal to the second value.
        /// </summary>
        /// <typeparam name="T">The type of the values to compare.</typeparam>
        /// <param name="arg1">The first value, expected to be greater.</param>
        /// <param name="arg2">The second value, expected to be less.</param>
        /// <param name="message">A message to display. This message can be seen in the unit test results.</param>
        /// <param name="parameters">An array of parameters to use when formatting <paramref name="message"/>.</param>
        public static void GreaterOrEqual<T>(T arg1, T arg2, string message, params object[] parameters) where T : IComparable
        {
            if (((IComparable)arg1).CompareTo(arg2) < 0)
            {
                Assert.Fail(message, parameters);
            }
        }
        #endregion

        #endregion

        #region Less

        #region Less<T>(T arg1, T arg2)
        /// <summary>
        /// Verifies that the first value is less than the second value.
        /// </summary>
        /// <typeparam name="T">The type of the values to compare.</typeparam>
        /// <param name="arg1">The first value, expected to be less.</param>
        /// <param name="arg2">The second value, expected to be greater.</param>
        public static void Less<T>(T arg1, T arg2) where T : IComparable
        {
            Less(arg1, arg2, "{0} is greater than or equal to {1}.", arg2, arg1);
        }
        #endregion

        #region Less<T1>(T arg1, T arg2, string message)
        /// <summary>
        /// Verifies that the first value is less than the second value.
        /// </summary>
        /// <typeparam name="T">The type of the values to compare.</typeparam>
        /// <param name="arg1">The first value, expected to be less.</param>
        /// <param name="arg2">The second value, expected to be greater.</param>
        /// <param name="message">A message to display. This message can be seen in the unit test results.</param>
        public static void Less<T>(T arg1, T arg2, string message) where T : IComparable
        {
            if (((IComparable)arg1).CompareTo(arg2) >= 0)
            {
                Assert.Fail(message);
            }
        }
        #endregion

        #region Less<T>(T arg1, T arg2, string message, params object[] parameters)
        /// <summary>
        /// Verifies that the first value is less than the second value.
        /// </summary>
        /// <typeparam name="T">The type of the values to compare.</typeparam>
        /// <param name="arg1">The first value, expected to be less.</param>
        /// <param name="arg2">The second value, expected to be greater.</param>
        /// <param name="message">A message to display. This message can be seen in the unit test results.</param>
        /// <param name="parameters">An array of parameters to use when formatting <paramref name="message"/>.</param>
        public static void Less<T>(T arg1, T arg2, string message, params object[] parameters) where T : IComparable
        {
            if (((IComparable)arg1).CompareTo(arg2) >= 0)
            {
                Assert.Fail(message, parameters);
            }
        }
        #endregion

        #endregion

        #region LessOrEqual

        #region LessOrEqual<T>(T arg1, T arg2)
        /// <summary>
        /// Verifies that the first value is less than or equal to the second value.
        /// </summary>
        /// <typeparam name="T">The type of the values to compare.</typeparam>
        /// <param name="arg1">The first value, expected to be less.</param>
        /// <param name="arg2">The second value, expected to be greater.</param>
        public static void LessOrEqual<T>(T arg1, T arg2) where T : IComparable
        {
            LessOrEqual(arg1, arg2, "{0} is greater than {1}.", arg2, arg1);
        }
        #endregion

        #region LessOrEqual<T>(T arg1, T arg2, string message)
        /// <summary>
        /// Verifies that the first value is less than or equal to the second value.
        /// </summary>
        /// <typeparam name="T">The type of the values to compare.</typeparam>
        /// <param name="arg1">The first value, expected to be less.</param>
        /// <param name="arg2">The second value, expected to be greater.</param>
        /// <param name="message">A message to display. This message can be seen in the unit test results.</param>
        public static void LessOrEqual<T>(T arg1, T arg2, string message) where T : IComparable
        {
            if (((IComparable)arg1).CompareTo(arg2) > 0)
            {
                Assert.Fail(message);
            }
        }
        #endregion

        #region LessOrEqual<T>(T arg1, T arg2, string message, params object[] parameters)
        /// <summary>
        /// Verifies that the first value is less than or equal to the second value.
        /// </summary>
        /// <typeparam name="T">The type of the values to compare.</typeparam>
        /// <param name="arg1">The first value, expected to be less.</param>
        /// <param name="arg2">The second value, expected to be greater.</param>
        /// <param name="message">A message to display. This message can be seen in the unit test results.</param>
        /// <param name="parameters">An array of parameters to use when formatting <paramref name="message"/>.</param>
        public static void LessOrEqual<T>(T arg1, T arg2, string message, params object[] parameters) where T : IComparable
        {
            if (((IComparable)arg1).CompareTo(arg2) > 0)
            {
                Assert.Fail(message, parameters);
            }
        }
        #endregion

        #endregion

        #region IsNaN

        #region IsNaN(double value)
        /// <summary>
        /// Verifies that the value is <see cref="Double.NaN"/>.
        /// </summary>
        /// <param name="value">The value to test.</param>
        public static void IsNaN(double value)
        {
            IsNaN(value, "Expected {0} but was {1}", Double.NaN, value);
        }
        #endregion

        #region IsNaN(double value, string message)
        /// <summary>
        /// Verifies that the value is <see cref="Double.NaN"/>.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <param name="message">A message to display. This message can be seen in the unit test results.</param>
        public static void IsNaN(double value, string message)
        {
            IsNaN(value, message, null);
        }
        #endregion

        #region IsNaN(double value, string message, params object[] parameters)
        /// <summary>
        /// Verifies that the value is <see cref="Double.NaN"/>.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <param name="message">A message to display. This message can be seen in the unit test results.</param>
        /// <param name="parameters">An array of parameters to use when formatting <paramref name="message"/>.</param>
        public static void IsNaN(double value, string message, params object[] parameters)
        {
            if (!Double.IsNaN(value))
            {
                Assert.Fail(message, parameters);
            }
        }
        #endregion

        #endregion
    }
}
