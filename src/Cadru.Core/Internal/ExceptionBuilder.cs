//------------------------------------------------------------------------------
// <copyright file="ExceptionBuilder.cs"
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
using System.Globalization;

using Cadru.Contracts;
using Cadru.Extensions;
using Cadru.Resources;

namespace Cadru.Internal
{
    /// <summary>
    /// Provides methods to create specific exceptions.
    /// </summary>
    public static class ExceptionBuilder
    {
        /// <summary>
        /// Create a new <see cref="ArgumentException" />.
        /// </summary>
        /// <param name="parameterName">The name of the parameter that caused the exception.</param>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <returns>A new <see cref="ArgumentException" />.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Reviewed.")]
        public static ArgumentException CreateArgumentException(string parameterName, string message)
        {
            Assumes.NotNull(parameterName);
            Assumes.NotNull(message);

            return new ArgumentException(message, parameterName);
        }

        /// <summary>
        /// Create a new <see cref="ArgumentNullException" />.
        /// </summary>
        /// <param name="parameterName">The name of the parameter that caused the exception.</param>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <returns>A new <see cref="ArgumentNullException" />.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Reviewed.")]
        public static ArgumentNullException CreateArgumentNullException(string parameterName, string? message = null)
        {
            Assumes.NotNull(parameterName);

            if (message.IsNull())
            {
                return new ArgumentNullException(parameterName);
            }

            return new ArgumentNullException(parameterName, message);
        }

        /// <summary>
        /// Create a new <see cref="ArgumentOutOfRangeException" />.
        /// </summary>
        /// <param name="parameterName">The name of the parameter that caused the exception.</param>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <returns>A new <see cref="ArgumentOutOfRangeException" />.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Reviewed.")]
        public static ArgumentOutOfRangeException CreateArgumentOutOfRangeException(string parameterName, string message)
        {
            Assumes.NotNull(parameterName);

            if (message.IsNull())
            {
                return new ArgumentOutOfRangeException(parameterName);
            }

            return new ArgumentOutOfRangeException(parameterName, message);
        }

        /// <summary>
        /// Create an exception indicating that an array or collection element was <see langword="null" />.
        /// </summary>
        /// <param name="parameterName">The name of the parameter that caused the exception.</param>
        /// <returns>A new <see cref="ArgumentException" />.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Reviewed.")]
        public static ArgumentException CreateContainsNullElement(string parameterName)
        {
            Assumes.NotNull(parameterName);

            var message = Format(Strings.Argument_NullElement, parameterName);

            return new ArgumentException(message, parameterName);
        }

        /// <summary>
        /// Create a new <see cref="FormatException" />.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <returns>A new <see cref="FormatException" />.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Reviewed.")]
        public static FormatException CreateFormatException(string message)
        {
            Assumes.NotNull(message);

            if (message.IsNull())
            {
                return new FormatException();
            }

            return new FormatException(message);
        }

        /// <summary>
        /// Create a new <see cref="ArgumentException" />.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <returns>A new <see cref="InvalidOperationException" />.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Reviewed.")]
        public static InvalidOperationException CreateInvalidOperation(string message)
        {
            return new InvalidOperationException(message);
        }

        /// <summary>
        /// Create a new <see cref="ObjectDisposedException" />.
        /// </summary>
        /// <param name="objectName">A string containing the name of the disposed object.</param>
        /// <returns>A new <see cref="ObjectDisposedException" />.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Reviewed.")]
        public static ObjectDisposedException CreateObjectDisposed(string objectName)
        {
            Assumes.NotNullOrEmpty(objectName);

            return new ObjectDisposedException(objectName);
        }

        /// <summary>
        /// Replaces the format item in a specified <see cref="String" /> with the text equivalent
        /// of the value of a corresponding <see cref="String" /> instance in a specified array.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="arguments">A <see cref="String" /> array containing zero or more strings to format.</param>
        /// <returns>A copy of format in which the format items have been replaced by the corresponding
        /// instances of <see cref="String" /> in args.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Reviewed.")]
        internal static string Format(string format, params object[] arguments)
        {
            return String.Format(CultureInfo.CurrentCulture, format, arguments);
        }
    }
}