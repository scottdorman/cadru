//------------------------------------------------------------------------------
// <copyright file="Requires.cs" 
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

namespace Cadru.Contracts
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using Cadru.Internal;
    using Cadru.UnitTest.Framework.Properties;

    /// <summary>
    /// Provides a set of methods to simplify code contract requirements.
    /// </summary>
    public static class Requires
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

        #region IsFalse

        #region IsFalse(bool condition)
        /// <summary>
        /// Checks for a condition and throws an exception if the condition is <see langword="true"/>.
        /// </summary>
        /// <param name="condition"><see langword="false"/> to prevent a message being displayed; otherwise, <see langword="true"/>.</param>
        /// <exception cref="InvalidOperationException">The condition is <see langword="true"/>.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Reviewed.")]
        [DebuggerStepThrough]
        public static void IsFalse(bool condition)
        {
            Requires.IsFalse(condition, null);
        }
        #endregion

        #region IsFalse(bool condition, string message)
        /// <summary>
        /// Checks for a condition and throws an exception if the condition is <see langword="true"/>.
        /// </summary>
        /// <param name="condition"><see langword="false"/> to prevent a message being displayed; otherwise, <see langword="true"/>.</param>
        /// <param name="message">A message to be used in the resulting exception.</param>
        /// <exception cref="InvalidOperationException">The condition is <see langword="true"/>.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Reviewed.")]
        [DebuggerStepThrough]
        public static void IsFalse(bool condition, string message)
        {
            if (condition)
            {
                throw ExceptionBuilder.CreateInvalidOperation(message);
            }
        }
        #endregion

        #region IsFalse(bool condition, string parameterName, string message)
        /// <summary>
        /// Checks for a condition and throws an exception if the condition is <see langword="true"/>.
        /// </summary>
        /// <param name="condition"><see langword="false"/> to prevent a message being displayed; otherwise, <see langword="true"/>.</param>
        /// <param name="parameterName">The name of the parameter being tested.</param>
        /// <param name="message">A message to be used in the resulting exception.</param>
        /// <exception cref="ArgumentException">The condition is <see langword="true"/>.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Reviewed.")]
        [DebuggerStepThrough]
        public static void IsFalse(bool condition, string parameterName, string message)
        {
            if (condition)
            {
                throw ExceptionBuilder.CreateArgumentException(parameterName, message);
            }
        }
        #endregion

        #endregion

        #region IsTrue

        #region IsTrue(bool condition)
        /// <summary>
        /// Checks for a condition and throws an exception if the condition is <see langword="false"/>.
        /// </summary>
        /// <param name="condition"><see langword="true"/> to prevent a message being displayed; otherwise, <see langword="false"/>.</param>
        /// <exception cref="InvalidOperationException">The condition is <see langword="false"/>.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Reviewed.")]
        [DebuggerStepThrough]
        public static void IsTrue(bool condition)
        {
            Requires.IsTrue(condition, null);
        }
        #endregion

        #region IsTrue(bool condition, string message)
        /// <summary>
        /// Checks for a condition and throws an exception if the condition is <see langword="false"/>.
        /// </summary>
        /// <param name="condition"><see langword="true"/> to prevent a message being displayed; otherwise, <see langword="false"/>.</param>
        /// <param name="message">A message to be used in the resulting exception.</param>
        /// <exception cref="InvalidOperationException">The condition is <see langword="false"/>.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Reviewed.")]
        [DebuggerStepThrough]
        public static void IsTrue(bool condition, string message)
        {
            if (!condition)
            {
                throw ExceptionBuilder.CreateInvalidOperation(message);
            }
        }
        #endregion

        #region IsTrue(bool condition, string parameterName, string message)
        /// <summary>
        /// Checks for a condition and throws an exception if the condition is <see langword="false"/>.
        /// </summary>
        /// <param name="condition"><see langword="true"/> to prevent a message being displayed; otherwise, <see langword="false"/>.</param>
        /// <param name="parameterName">The name of the parameter being tested.</param>
        /// <param name="message">A message to be used in the resulting exception.</param>
        /// <exception cref="ArgumentException">The condition is <see langword="false"/>.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Reviewed.")]
        [DebuggerStepThrough]
        public static void IsTrue(bool condition, string parameterName, string message)
        {
            if (!condition)
            {
                throw ExceptionBuilder.CreateArgumentException(parameterName, message);
            }
        }
        #endregion

        #endregion

        #region NotNull
        /// <summary>
        /// Checks that <paramref name="value"/> is not <see langword="null"/>.
        /// </summary>
        /// <typeparam name="T">The type of the value to test.</typeparam>
        /// <param name="value">The parameter to test.</param>
        /// <param name="parameterName">The name of the parameter being tested.</param>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        [DebuggerStepThrough]
        public static void NotNull<T>([ValidatedNotNull]T value, string parameterName) where T : class
        {
            if (value == null)
            {
                throw ExceptionBuilder.CreateArgumentNullException(parameterName);
            }
        }
        #endregion

        #region NotNullOrEmpty
        /// <summary>
        /// Checks that <paramref name="value"/> is not <see langword="null"/> or a zero-length string.
        /// </summary>
        /// <param name="value">The parameter to test.</param>
        /// <param name="parameterName">The name of the parameter being tested.</param>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="value"/> is a zero-length string.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Reviewed.")]
        [DebuggerStepThrough]
        public static void NotNullOrEmpty([ValidatedNotNull]string value, string parameterName)
        {
            Requires.NotNull(value, parameterName);
            if (value.Length == 0)
            {
                throw ExceptionBuilder.CreateArgumentException(parameterName, ExceptionBuilder.Format(Resources.ArgumentException_EmptyString, parameterName));
            }
        }
        #endregion

        #region ValidRange

        #region ValidRange(bool condition, string parameterName)
        /// <summary>
        /// Checks for a condition and throws an exception if the condition is <see langword="true"/>.
        /// </summary>
        /// <param name="condition"><see langword="false"/> to prevent a message being displayed; otherwise, <see langword="true"/>.</param>
        /// <param name="parameterName">The name of the parameter being tested.</param>
        /// <exception cref="ArgumentOutOfRangeException">The condition is <see langword="true"/>.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Reviewed.")]
        [DebuggerStepThrough]
        public static void ValidRange(bool condition, string parameterName)
        {
            Requires.ValidRange(condition, parameterName, String.Empty);
        }
        #endregion

        #region ValidRange(bool condition, string parameterName, string message)
        /// <summary>
        /// Checks for a condition and throws an exception if the condition is <see langword="true"/>.
        /// </summary>
        /// <param name="condition"><see langword="false"/> to prevent a message being displayed; otherwise, <see langword="true"/>.</param>
        /// <param name="parameterName">The name of the parameter being tested.</param>
        /// <param name="message">A message to be used in the resulting exception.</param>
        /// <exception cref="ArgumentOutOfRangeException">The condition is <see langword="true"/>.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Reviewed.")]
        [DebuggerStepThrough]
        public static void ValidRange(bool condition, string parameterName, string message)
        {
            Assumes.NotNullOrEmpty(parameterName);
            Assumes.NotNull(message);

            if (condition)
            {
                throw ExceptionBuilder.CreateArgumentOutOfRangeException(parameterName, message);
            }
        }
        #endregion

        #endregion

        #endregion
    }
}
