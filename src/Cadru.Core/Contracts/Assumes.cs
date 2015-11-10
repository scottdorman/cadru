//------------------------------------------------------------------------------
// <copyright file="Assumes.cs" 
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

namespace Cadru.Contracts
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using Cadru.Extensions;
    using Cadru.Internal;
    using Cadru.Properties;

    /// <summary>
    /// Provides a set of methods to simplify debugging your code.
    /// </summary>
    public static class Assumes
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

        #region Fail
        /// <summary>
        /// Fails with the provided message as the reason.
        /// </summary>
        /// <param name="message">The message of the resulting <see cref="AssumptionException"/>.</param>
        /// <exception cref="AssumptionException">An assumption failed.</exception>
        [DebuggerStepThrough]
        [Conditional("DEBUG")]
        public static void Fail(string message)
        {
            Assumes.FailFast(message);
        }
        #endregion

        #region IsEnum

        #region IsEnum(Enum value, string parameterName)
        /// <summary>
        /// Checks that <paramref name="value"/> is an enumerated type.
        /// </summary>
        /// <param name="value">The parameter to test.</param>
        /// <param name="parameterName">The name of the parameter being tested.</param>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="value"/>  is not an enumerated type.</exception>
        [DebuggerStepThrough]
        [Conditional("DEBUG")]
        public static void IsEnum([ValidatedNotNull]Enum value, string parameterName)
        {
            Requires.NotNull(value, parameterName);
            Assumes.IsTrue(value.GetType().IsEnum);
        }
        #endregion

        #region IsEnum(object value, string parameterName)
        /// <summary>
        /// Checks that <paramref name="value"/> is an enumerated type.
        /// </summary>
        /// <param name="value">The parameter to test.</param>
        /// <param name="parameterName">The name of the parameter being tested.</param>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="value"/>  is not an enumerated type.</exception>
        [DebuggerStepThrough]
        [Conditional("DEBUG")]
        public static void IsEnum([ValidatedNotNull]object value, string parameterName)
        {
            Requires.NotNull(value, parameterName);
            Assumes.IsTrue(value.GetType().IsEnum);
        }
        #endregion

        #endregion

        #region IsFalse

        #region IsFalse(bool condition)
        /// <summary>
        /// Checks for a condition and displays a message and throws an exception if the condition is <see langword="true"/>.
        /// </summary>
        /// <param name="condition"><see langword="false"/> to prevent a message being displayed; otherwise, <see langword="true"/>.</param>
        /// <exception cref="AssumptionException">The condition is <see langword="false"/>.</exception>
        [DebuggerStepThrough]
        [Conditional("DEBUG")]
        public static void IsFalse(bool condition)
        {
            Assumes.IsFalse(condition, null);
        }
        #endregion

        #region IsFalse(bool condition, string message)
        /// <summary>
        /// Checks for a condition and displays a message and throws an exception if the condition is <see langword="true"/>.
        /// </summary>
        /// <param name="condition"><see langword="false"/> to prevent a message being displayed; otherwise, <see langword="true"/>.</param>
        /// <param name="message">A message to display and to be used in the resulting exception.</param>
        /// <exception cref="AssumptionException">The condition is <see langword="false"/>.</exception>
        [DebuggerStepThrough]
        [Conditional("DEBUG")]
        public static void IsFalse(bool condition, string message)
        {
            if (condition)
            {
                Assumes.Fail(message);
            }
        }
        #endregion

        #endregion

        #region IsTrue

        #region IsTrue(bool condition)
        /// <summary>
        /// Checks for a condition and displays a message and throws an exception if the condition is <see langword="false"/>.
        /// </summary>
        /// <param name="condition"><see langword="true"/> to prevent a message being displayed; otherwise, <see langword="false"/>.</param>
        /// <exception cref="AssumptionException">The condition is <see langword="false"/>.</exception>
        [DebuggerStepThrough]
        [Conditional("DEBUG")]
        public static void IsTrue(bool condition)
        {
            Assumes.IsTrue(condition, null);
        }
        #endregion

        #region IsTrue(bool condition, string message)
        /// <summary>
        /// Checks for a condition and displays a message and throws an exception if the condition is <see langword="false"/>.
        /// </summary>
        /// <param name="condition"><see langword="true"/> to prevent a message being displayed; otherwise, <see langword="false"/>.</param>
        /// <param name="message">A message to display and to be used in the resulting exception.</param>
        /// <exception cref="AssumptionException">The condition is <see langword="false"/>.</exception>
        [DebuggerStepThrough]
        [Conditional("DEBUG")]
        public static void IsTrue(bool condition, string message)
        {
            if (!condition)
            {
                Assumes.Fail(message);
            }
        }
        #endregion

        #endregion

        #region IsType

        #region IsType(object value, Type expectedType, string parameterName)
        /// <summary>
        /// Checks that <paramref name="value"/> is an enumerated type.
        /// </summary>
        /// <param name="value">The parameter to test.</param>
        /// <param name="expectedType">The type <paramref name="value"/> is expected to be.</param>
        /// <param name="parameterName">The name of the parameter being tested.</param>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="value"/>  is not an enumerated type.</exception>
        [DebuggerStepThrough]
        [Conditional("DEBUG")]
        public static void IsType([ValidatedNotNull]object value, Type expectedType, string parameterName)
        {
            Requires.NotNull(value, parameterName);
            Assumes.IsTrue(value.GetType() == expectedType);
        }
        #endregion

        #region IsType<T>(object value, string parameterName)
        /// <summary>
        /// Checks that <paramref name="value"/> is an enumerated type.
        /// </summary>
        /// <param name="value">The parameter to test.</param>
        /// <param name="parameterName">The name of the parameter being tested.</param>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="value"/>  is not an enumerated type.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "Reviewed.")]
        [DebuggerStepThrough]
        [Conditional("DEBUG")]
        public static void IsType<T>([ValidatedNotNull]object value, string parameterName)
        {
            Assumes.IsType(value, typeof(T), parameterName);
        }
        #endregion

        #endregion

        #region NotNull

        #region NotNull<T>(T value)
        /// <summary>
        /// Checks that <paramref name="value"/> is not <see langword="null"/>.
        /// </summary>
        /// <typeparam name="T">The type of the value to test.</typeparam>
        /// <param name="value">The value to test.</param>
        /// <exception cref="AssumptionException"><paramref name="value"/> is <see langword="null"/>.</exception>
        [DebuggerStepThrough]
        [Conditional("DEBUG")]
        public static void NotNull<T>(T value)
        {
            Assumes.IsTrue(value.IsNotNull());
        }
        #endregion

        #region NotNull<T1, T2>(T1 value1, T2 value2)
        /// <summary>
        /// Checks that <paramref name="value1"/> and <paramref name="value2"/> are not <see langword="null"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first value to test.</typeparam>
        /// <typeparam name="T2">The type of the second value to test.</typeparam>
        /// <param name="value1">The first value to test.</param>
        /// <param name="value2">The second value to test.</param>
        /// <exception cref="AssumptionException">
        /// <para><paramref name="value1"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para><paramref name="value2"/> is <see langword="null"/>.</para>
        /// </exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "1", Justification = "This is the clearest name.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "2", Justification = "This is the clearest name.")]
        [DebuggerStepThrough]
        [Conditional("DEBUG")]
        public static void NotNull<T1, T2>(T1 value1, T2 value2)
        {
            Assumes.NotNull(value1);
            Assumes.NotNull(value2);
        }
        #endregion

        #region NotNull<T1, T2, T3>(T1 value1, T2 value2, T3 value3)
        /// <summary>
        /// Checks that <paramref name="value1"/>, <paramref name="value2"/>, and <paramref name="value3"/> are not <see langword="null"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first value to test.</typeparam>
        /// <typeparam name="T2">The type of the second value to test.</typeparam>
        /// <typeparam name="T3">The type of the third value to test.</typeparam>
        /// <param name="value1">The first value to test.</param>
        /// <param name="value2">The second value to test.</param>
        /// <param name="value3">The third value to test.</param>
        /// <exception cref="AssumptionException">
        /// <para><paramref name="value1"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para><paramref name="value2"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para><paramref name="value3"/> is <see langword="null"/>.</para>
        /// </exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "1", Justification = "This is the clearest name.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "2", Justification = "This is the clearest name.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "3", Justification = "This is the clearest name.")]
        [DebuggerStepThrough]
        [Conditional("DEBUG")]
        public static void NotNull<T1, T2, T3>(T1 value1, T2 value2, T3 value3)
        {
            Assumes.NotNull(value1);
            Assumes.NotNull(value2);
            Assumes.NotNull(value3);
        }
        #endregion

        #region NotNull<T1, T2, T3, T4>(T1 value1, T2 value2, T3 value3, T4 value4)
        /// <summary>
        /// Checks that <paramref name="value1"/>, <paramref name="value2"/>, 
        /// <paramref name="value3"/>, and <paramref name="value4"/> are not <see langword="null"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first value to test.</typeparam>
        /// <typeparam name="T2">The type of the second value to test.</typeparam>
        /// <typeparam name="T3">The type of the third value to test.</typeparam>
        /// <typeparam name="T4">The type of the fourth value to test.</typeparam>
        /// <param name="value1">The first value to test.</param>
        /// <param name="value2">The second value to test.</param>
        /// <param name="value3">The third value to test.</param>
        /// <param name="value4">The fourth value to test.</param>
        /// <exception cref="AssumptionException">
        /// <para><paramref name="value1"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para><paramref name="value2"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para><paramref name="value3"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para><paramref name="value4"/> is <see langword="null"/>.</para>
        /// </exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "1", Justification = "This is the clearest name.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "2", Justification = "This is the clearest name.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "3", Justification = "This is the clearest name.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "4", Justification = "This is the clearest name.")]
        [DebuggerStepThrough]
        [Conditional("DEBUG")]
        public static void NotNull<T1, T2, T3, T4>(T1 value1, T2 value2, T3 value3, T4 value4)
        {
            Assumes.NotNull(value1);
            Assumes.NotNull(value2);
            Assumes.NotNull(value3);
            Assumes.NotNull(value4);
        }
        #endregion

        #endregion

        #region NotNullOrEmpty
        /// <summary>
        /// Checks that <paramref name="value"/> is not <see langword="null"/> or a zero-length string.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <exception cref="AssumptionException"><paramref name="value"/> is <see langword="null"/> or zero-length.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "Reviewed.")]
        [DebuggerStepThrough]
        [Conditional("DEBUG")]
        public static void NotNullOrEmpty([ValidatedNotNull]string value)
        {
            Assumes.NotNull(value);
            Assumes.IsTrue(value.Length > 0);
        }
        #endregion

        #region Null
        /// <summary>
        /// Checks that <paramref name="value"/> is <see langword="null"/>.
        /// </summary>
        /// <param name="value">The object to test.</param>
        /// <exception cref="AssumptionException"><paramref name="value"/> is not <see langword="null"/>.</exception>
        [DebuggerStepThrough]
        [Conditional("DEBUG")]
        public static void Null(object value)
        {
            Assumes.IsTrue(value.IsNull());
        }
        #endregion

        #region FailFast
        /// <summary>
        /// Throws a new <see cref="AssumptionException"/> and a <see cref="Debug.Assert(bool, string)"/> assertion failure.
        /// </summary>
        /// <param name="message">The message of the resulting AssumptionException.</param>
        /// <exception cref="AssumptionException">An assumption failed.</exception>
        private static void FailFast(string message)
        {
            if (String.IsNullOrEmpty(message))
            {
                message = Resources.AssumptionException_EmptyMessage;
            }
            else
            {
                message = String.Format(CultureInfo.CurrentUICulture, Resources.AssumptionException_Message, message);
            }

            // Need to use Debug.Assert instead of Debug.Fail because Silverlight doesn't contain Debug.Fail.
            Debug.Assert(false, message);
            throw new AssumptionException(message);
        }
        #endregion

        #endregion
    }
}
