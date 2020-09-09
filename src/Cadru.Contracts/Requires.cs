//------------------------------------------------------------------------------
// <copyright file="Requires.cs"
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
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;

using Cadru.Contracts.Internal;

namespace Cadru.Contracts
{
    /// <summary>
    /// Provides a set of methods to simplify code contract requirements.
    /// </summary>
    public static class Requires
    {
        /// <summary>
        /// Checks that <paramref name="value"/> is an enumerated type.
        /// </summary>
        /// <param name="value">The parameter to test.</param>
        /// <param name="parameterName">The name of the parameter being tested.</param>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="value"/>  is not an enumerated type.</exception>
        [DebuggerStepThrough]
        [ContractArgumentValidator]
        public static void IsEnum([ValidatedNotNull] Enum value, string parameterName)
        {
            NotNull(value, parameterName);
            IsTrue(value.GetType().GetTypeInfo().IsEnum);
            Contract.EndContractBlock();
        }

        /// <summary>
        /// Checks that <paramref name="value"/> is an enumerated type.
        /// </summary>
        /// <param name="value">The parameter to test.</param>
        /// <param name="parameterName">The name of the parameter being tested.</param>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="value"/>  is not an enumerated type.</exception>
        [DebuggerStepThrough]
        [ContractArgumentValidator]
        public static void IsEnum([ValidatedNotNull] object value, string parameterName)
        {
            NotNull(value, parameterName);
            IsTrue(value.GetType().GetTypeInfo().IsEnum);
            Contract.EndContractBlock();
        }

        /// <summary>
        /// Checks for a condition and throws an exception if the condition is <see langword="true"/>.
        /// </summary>
        /// <param name="condition"><see langword="false"/> to prevent a message being displayed; otherwise, <see langword="true"/>.</param>
        /// <exception cref="InvalidOperationException">The condition is <see langword="true"/>.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Reviewed.")]
        [DebuggerStepThrough]
        [ContractArgumentValidatorAttribute]
        public static void IsFalse(bool condition)
        {
            IsFalse(condition, null);
            Contract.EndContractBlock();
        }

        /// <summary>
        /// Checks for a condition and throws an exception if the condition is <see langword="true"/>.
        /// </summary>
        /// <param name="condition"><see langword="false"/> to prevent a message being displayed; otherwise, <see langword="true"/>.</param>
        /// <param name="message">A message to be used in the resulting exception.</param>
        /// <exception cref="InvalidOperationException">The condition is <see langword="true"/>.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Reviewed.")]
        [DebuggerStepThrough]
        [ContractArgumentValidatorAttribute]
        public static void IsFalse(bool condition, string message)
        {
            if (condition)
            {
                throw ExceptionBuilder.CreateInvalidOperation(message);
            }

            Contract.EndContractBlock();
        }

        /// <summary>
        /// Checks for a condition and throws an exception if the condition is <see langword="true"/>.
        /// </summary>
        /// <param name="condition"><see langword="false"/> to prevent a message being displayed; otherwise, <see langword="true"/>.</param>
        /// <param name="parameterName">The name of the parameter being tested.</param>
        /// <param name="message">A message to be used in the resulting exception.</param>
        /// <exception cref="ArgumentException">The condition is <see langword="true"/>.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Reviewed.")]
        [DebuggerStepThrough]
        [ContractArgumentValidatorAttribute]
        public static void IsFalse(bool condition, string parameterName, string message)
        {
            if (condition)
            {
                throw ExceptionBuilder.CreateArgumentException(parameterName, message);
            }

            Contract.EndContractBlock();
        }

        /// <summary>
        /// Checks for a condition and throws an exception if the condition is <see langword="false"/>.
        /// </summary>
        /// <param name="condition"><see langword="true"/> to prevent a message being displayed; otherwise, <see langword="false"/>.</param>
        /// <exception cref="InvalidOperationException">The condition is <see langword="false"/>.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Reviewed.")]
        [DebuggerStepThrough]
        [ContractArgumentValidatorAttribute]
        public static void IsTrue(bool condition)
        {
            IsTrue(condition, null);
            Contract.EndContractBlock();
        }

        /// <summary>
        /// Checks for a condition and throws an exception if the condition is <see langword="false"/>.
        /// </summary>
        /// <param name="condition"><see langword="true"/> to prevent a message being displayed; otherwise, <see langword="false"/>.</param>
        /// <param name="message">A message to be used in the resulting exception.</param>
        /// <exception cref="InvalidOperationException">The condition is <see langword="false"/>.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Reviewed.")]
        [DebuggerStepThrough]
        [ContractArgumentValidatorAttribute]
        public static void IsTrue(bool condition, string message)
        {
            if (!condition)
            {
                throw ExceptionBuilder.CreateInvalidOperation(message);
            }

            Contract.EndContractBlock();
        }

        /// <summary>
        /// Checks for a condition and throws an exception if the condition is <see langword="false"/>.
        /// </summary>
        /// <param name="condition"><see langword="true"/> to prevent a message being displayed; otherwise, <see langword="false"/>.</param>
        /// <param name="parameterName">The name of the parameter being tested.</param>
        /// <param name="message">A message to be used in the resulting exception.</param>
        /// <exception cref="ArgumentException">The condition is <see langword="false"/>.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Reviewed.")]
        [DebuggerStepThrough]
        [ContractArgumentValidatorAttribute]
        public static void IsTrue(bool condition, string parameterName, string message)
        {
            if (!condition)
            {
                throw ExceptionBuilder.CreateArgumentException(parameterName, message);
            }

            Contract.EndContractBlock();
        }

        /// <summary>
        /// Checks that <paramref name="value"/> is an enumerated type.
        /// </summary>
        /// <param name="value">The parameter to test.</param>
        /// <param name="expectedType">The type <paramref name="value"/> is expected to be.</param>
        /// <param name="parameterName">The name of the parameter being tested.</param>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="value"/>  is not an enumerated type.</exception>
        [DebuggerStepThrough]
        [ContractArgumentValidator]
        public static void IsType([ValidatedNotNull] object value, Type expectedType, string parameterName)
        {
            NotNull(value, parameterName);
            IsTrue(value.GetType() == expectedType);
            Contract.EndContractBlock();
        }

        /// <summary>
        /// Checks that <paramref name="value"/> is an enumerated type.
        /// </summary>
        /// <param name="value">The parameter to test.</param>
        /// <param name="parameterName">The name of the parameter being tested.</param>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="value"/>  is not an enumerated type.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "Reviewed.")]
        [DebuggerStepThrough]
        [ContractArgumentValidator]
        public static void IsType<T>([ValidatedNotNull] object value, string parameterName)
        {
            IsType(value, typeof(T), parameterName);
            Contract.EndContractBlock();
        }

        /// <summary>
        /// Checks that <paramref name="value"/> is not <see langword="null"/>.
        /// </summary>
        /// <typeparam name="T">The type of the value to test.</typeparam>
        /// <param name="value">The parameter to test.</param>
        /// <param name="parameterName">The name of the parameter being tested.</param>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        [DebuggerStepThrough]
        [ContractArgumentValidatorAttribute]
        public static void NotNull<T>([ValidatedNotNull] T value, string parameterName) where T : class
        {
            NotNull(value, parameterName, null);
            Contract.EndContractBlock();
        }

        /// <summary>
        /// Checks that <paramref name="value"/> is not <see langword="null"/>.
        /// </summary>
        /// <typeparam name="T">The type of the value to test.</typeparam>
        /// <param name="value">The parameter to test.</param>
        /// <param name="parameterName">The name of the parameter being tested.</param>
        /// <param name="message">A message to be used in the resulting exception.</param>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        [DebuggerStepThrough]
        [ContractArgumentValidatorAttribute]
        public static void NotNull<T>([ValidatedNotNull] T value, string parameterName, string message) where T : class
        {
            if (value == null)
            {
                throw ExceptionBuilder.CreateArgumentNullException(parameterName, message);
            }

            Contract.EndContractBlock();
        }

        /// <summary>
        /// Checks that <paramref name="values"/> is not <see langword="null"/>
        /// and contains no <see langword="null"/> elements.
        /// </summary>
        /// <param name="values">The parameter to test.</param>
        /// <param name="parameterName">The name of the parameter being tested.</param>
        /// <exception cref="ArgumentNullException"><paramref name="values"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="values"/> contains a <see langword="null"/> element.</exception>
        [DebuggerStepThrough]
        [ContractArgumentValidatorAttribute]
        public static void NotNullElements([ValidatedNotNull] IEnumerable values, string parameterName)
        {
            NotNull(values, "values");
            foreach (var value in values)
            {
                if (value == null)
                {
                    throw ExceptionBuilder.CreateContainsNullElement(parameterName);
                }
            }

            Contract.EndContractBlock();
        }

        /// <summary>
        /// Checks that <paramref name="value"/> is not <see langword="null"/> or a zero-length string.
        /// </summary>
        /// <param name="value">The parameter to test.</param>
        /// <param name="parameterName">The name of the parameter being tested.</param>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="value"/> is a zero-length string.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Reviewed.")]
        [DebuggerStepThrough]
        [ContractArgumentValidatorAttribute]
        public static void NotNullOrEmpty([ValidatedNotNull] string value, string parameterName)
        {
            NotNullOrEmpty(value, parameterName, ExceptionBuilder.Format(Resources.Strings.ArgumentException_EmptyString, parameterName));
            Contract.EndContractBlock();
        }

        /// <summary>
        /// Checks that <paramref name="value"/> is not <see langword="null"/> or a zero-length string.
        /// </summary>
        /// <param name="value">The parameter to test.</param>
        /// <param name="parameterName">The name of the parameter being tested.</param>
        /// <param name="message">A message to be used in the resulting exception.</param>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="value"/> is a zero-length string.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Reviewed.")]
        [DebuggerStepThrough]
        [ContractArgumentValidatorAttribute]
        public static void NotNullOrEmpty([ValidatedNotNull] string value, string parameterName, string message)
        {
            NotNull(value, parameterName);
            if (value.Length == 0)
            {
                throw ExceptionBuilder.CreateArgumentException(parameterName, message);
            }

            Contract.EndContractBlock();
        }

        /// <summary>
        /// Checks that <paramref name="values"/> is not <see langword="null"/> or empty.
        /// </summary>
        /// <param name="values">The parameter to test.</param>
        /// <param name="parameterName">The name of the parameter being tested.</param>
        /// <exception cref="ArgumentNullException"><paramref name="values"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="values"/> is empty.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Reviewed.")]
        [DebuggerStepThrough]
        [ContractArgumentValidatorAttribute]
        public static void NotNullOrEmpty([ValidatedNotNull] IEnumerable values, string parameterName)
        {
            if (values == null)
            {
                throw ExceptionBuilder.CreateArgumentNullException(parameterName);
            }

            if (values.IsEmpty())
            {
                throw ExceptionBuilder.CreateArgumentException(parameterName, String.Empty);
            }

            Contract.EndContractBlock();
        }

        /// <summary>
        /// Checks that <paramref name="values"/> is not <see langword="null"/> or empty.
        /// </summary>
        /// <param name="values">The parameter to test.</param>
        /// <param name="parameterName">The name of the parameter being tested.</param>
        /// <param name="message">A message to be used in the resulting exception.</param>
        /// <exception cref="ArgumentNullException"><paramref name="values"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="values"/> is empty.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Reviewed.")]
        [DebuggerStepThrough]
        [ContractArgumentValidatorAttribute]
        public static void NotNullOrEmpty([ValidatedNotNull] IEnumerable values, string parameterName, string message)
        {
            if (values == null)
            {
                throw ExceptionBuilder.CreateArgumentNullException(parameterName, message);
            }

            if (values.IsEmpty())
            {
                throw ExceptionBuilder.CreateArgumentException(parameterName, message);
            }

            Contract.EndContractBlock();
        }

        /// <summary>
        /// Checks that <paramref name="value"/> is not <see langword="null"/> or a zero-length string.
        /// </summary>
        /// <param name="value">The parameter to test.</param>
        /// <param name="parameterName">The name of the parameter being tested.</param>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="value"/> is a zero-length string.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Reviewed.")]
        [DebuggerStepThrough]
        [ContractArgumentValidatorAttribute]
        public static void NotNullOrWhiteSpace([ValidatedNotNull] string value, string parameterName)
        {
            NotNullOrWhiteSpace(value, parameterName, ExceptionBuilder.Format(Resources.Strings.ArgumentException_EmptyString, parameterName));
            Contract.EndContractBlock();
        }

        /// <summary>
        /// Checks that <paramref name="value"/> is not <see langword="null"/> or a zero-length string.
        /// </summary>
        /// <param name="value">The parameter to test.</param>
        /// <param name="parameterName">The name of the parameter being tested.</param>
        /// <param name="message">A message to be used in the resulting exception.</param>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="value"/> is a zero-length string.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Reviewed.")]
        [DebuggerStepThrough]
        [ContractArgumentValidatorAttribute]
        public static void NotNullOrWhiteSpace([ValidatedNotNull] string value, string parameterName, string message)
        {
            NotNull(value, parameterName);
            if (String.IsNullOrWhiteSpace(value))
            {
                throw ExceptionBuilder.CreateArgumentException(parameterName, message);
            }

            Contract.EndContractBlock();
        }

        /// <summary>
        /// Checks that <paramref name="values"/> is not <see langword="null"/>
        /// and contains valid elements based on the given predicate.
        /// </summary>
        /// <typeparam name="T">The type of the members of <paramref name="values"/>.</typeparam>
        /// <param name="values">The parameter to test.</param>
        /// <param name="match">The predicate used to test the elements.</param>
        /// <param name="parameterName">The name of the parameter being tested.</param>
        /// <param name="message">A message to be used in the resulting exception.</param>
        /// <exception cref="ArgumentNullException"><paramref name="values"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="values"/> contains an element which does not match the given predicate.</exception>
        [DebuggerStepThrough]
        [ContractArgumentValidatorAttribute]
        public static void ValidElements<T>([ValidatedNotNull] IEnumerable<T> values, Predicate<T> match, string parameterName, string message)
        {
            NotNull(values, "values");
            if (values.Any(x => !match(x)))
            {
                throw ExceptionBuilder.CreateArgumentException(parameterName, message);
            }

            Contract.EndContractBlock();
        }

        /// <summary>
        /// Checks for a condition and throws an exception if the condition is <see langword="true"/>.
        /// </summary>
        /// <param name="condition"><see langword="false"/> to prevent a message being displayed; otherwise, <see langword="true"/>.</param>
        /// <param name="parameterName">The name of the parameter being tested.</param>
        /// <exception cref="ArgumentOutOfRangeException">The condition is <see langword="true"/>.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Reviewed.")]
        [DebuggerStepThrough]
        [ContractArgumentValidatorAttribute]
        public static void ValidRange(bool condition, string parameterName)
        {
            ValidRange(condition, parameterName, String.Empty);
            Contract.EndContractBlock();
        }

        /// <summary>
        /// Checks for a condition and throws an exception if the condition is <see langword="true"/>.
        /// </summary>
        /// <param name="condition"><see langword="false"/> to prevent a message being displayed; otherwise, <see langword="true"/>.</param>
        /// <param name="parameterName">The name of the parameter being tested.</param>
        /// <param name="message">A message to be used in the resulting exception.</param>
        /// <exception cref="ArgumentOutOfRangeException">The condition is <see langword="true"/>.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Reviewed.")]
        [DebuggerStepThrough]
        [ContractArgumentValidatorAttribute]
        public static void ValidRange(bool condition, string parameterName, string message)
        {
            Assumes.NotNullOrEmpty(parameterName);

            if (condition)
            {
                throw ExceptionBuilder.CreateArgumentOutOfRangeException(parameterName, message);
            }

            Contract.EndContractBlock();
        }
    }
}