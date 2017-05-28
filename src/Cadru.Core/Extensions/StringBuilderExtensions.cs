//------------------------------------------------------------------------------
// <copyright file="StringBuilderExtensions.cs"
//  company="Scott Dorman"
//  library="Cadru">
//    Copyright (C) 2001-2017 Scott Dorman.
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

namespace Cadru.Extensions
{
    using System;
    using System.Globalization;
    using System.Text;

    /// <summary>
    /// Provides basic routines for common <see cref="StringBuilder"/> manipulation.
    /// </summary>
    public static class StringBuilderExtensions
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

        #region AppendAsHexadecimal

        #region AppendAsHexadecimal(this StringBuilder source, int value)
        /// <summary>
        /// Appends the hexadecimal string representation of a specified
        /// <see cref="Int32"/> value to this instance.
        /// </summary>
        /// <param name="source">The source <see cref="StringBuilder"/> instance.</param>
        /// <param name="value">The <see cref="Int32"/> value to append.</param>
        /// <returns>A reference to this instance after the append operation has, optionally, completed.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// Enlarging the value of this instance would exceed <see cref="p:StringBuilder.MaxCapacity"/>.
        /// </exception>
        public static StringBuilder AppendAsHexadecimal(this StringBuilder source, int value)
        {
            Contracts.Requires.NotNull(source, nameof(source));

            source.Append(ToHex((value >> 28) & 0xf));
            source.Append(ToHex((value >> 24) & 0xf));
            source.Append(ToHex((value >> 20) & 0xf));
            source.Append(ToHex((value >> 16) & 0xf));
            source.Append(ToHex((value >> 12) & 0xf));
            source.Append(ToHex((value >> 8) & 0xf));
            source.Append(ToHex((value >> 4) & 0xf));
            source.Append(ToHex(value & 0xf));

            return source;
        }
        #endregion

        #region AppendAsHexadecimal(this StringBuilder source, short value)
        /// <summary>
        /// Appends the hexadecimal string representation of a specified
        /// <see cref="Int16"/> value to this instance.
        /// </summary>
        /// <param name="source">The source <see cref="StringBuilder"/> instance.</param>
        /// <param name="value">The <see cref="Int16"/> value to append.</param>
        /// <returns>A reference to this instance after the append operation has, optionally, completed.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// Enlarging the value of this instance would exceed <see cref="p:StringBuilder.MaxCapacity"/>.
        /// </exception>
        public static StringBuilder AppendAsHexadecimal(this StringBuilder source, short value)
        {
            Contracts.Requires.NotNull(source, nameof(source));

            source.Append(ToHex((value >> 12) & 0xf));
            source.Append(ToHex((value >> 8) & 0xf));
            source.Append(ToHex((value >> 4) & 0xf));
            source.Append(ToHex(value & 0xf));

            return source;
        }
        #endregion

        #region AppendAsHexadecimal(this StringBuilder source, byte value)
        /// <summary>
        /// Appends the hexadecimal string representation of a specified
        /// <see cref="Byte"/> value to this instance.
        /// </summary>
        /// <param name="source">The source <see cref="StringBuilder"/> instance.</param>
        /// <param name="value">The <see cref="Byte"/> value to append.</param>
        /// <returns>A reference to this instance after the append operation has, optionally, completed.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// Enlarging the value of this instance would exceed <see cref="p:StringBuilder.MaxCapacity"/>.
        /// </exception>
        public static StringBuilder AppendAsHexadecimal(this StringBuilder source, byte value)
        {
            Contracts.Requires.NotNull(source, nameof(source));

            source.Append(ToHex((value >> 4) & 0xf));
            source.Append(ToHex(value & 0xf));

            return source;
        }
        #endregion

        #region AppendAsHexadecimal(this StringBuilder source, params byte[] values)
        /// <summary>
        /// Appends the hexadecimal string representation of a specified
        /// <see cref="Byte"/> array to this instance.
        /// </summary>
        /// <param name="source">The source <see cref="StringBuilder"/> instance.</param>
        /// <param name="values">The <see cref="Byte"/> array to append.</param>
        /// <returns>A reference to this instance after the append operation has, optionally, completed.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// Enlarging the value of this instance would exceed <see cref="p:StringBuilder.MaxCapacity"/>.
        /// </exception>
        public static StringBuilder AppendAsHexadecimal(this StringBuilder source, params byte[] values)
        {
            Contracts.Requires.NotNull(source, nameof(source));
            Contracts.Requires.NotNull(values, nameof(values));

            foreach (var value in values)
            {
                source.AppendAsHexadecimal(value);
            }

            return source;
        }
        #endregion

        #endregion

        #region AppendFormatIf

        #region AppendFormatIf(this StringBuilder source, bool condition, string format, params object[] args)
        /// <overloads>
        /// <summary>
        /// Appends the string returned by processing a composite format string, which
        /// contains zero or more format items, followed by the default line terminator to
        /// the end of this instance if <paramref name="condition"/> is <see langword="true"/>.
        /// </summary>
        /// </overloads>
        /// <summary>
        /// Appends the string returned by processing a composite format string, which
        /// contains zero or more format items, followed by the default line terminator to
        /// the end of this instance if <paramref name="condition"/> is <see langword="true"/>.
        /// </summary>
        /// <param name="source">The source <see cref="StringBuilder"/> instance.</param>
        /// <param name="condition"><see langword="true"/> to append <paramref name="format"/>;
        /// otherwise, <see langword="false"/>.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An array of objects to format.</param>
        /// <returns>A reference to <paramref name="source"/> with <paramref name="format"/>
        /// appended. Each format item in <paramref name="format"/> is replaced by the
        /// string representation of the corresponding object argument.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="format"/> or <paramref name="args"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="System.FormatException">
        /// <para><paramref name="format"/> is invalid.</para>
        /// <para>-or-</para>
        /// <para>The index of a format item is less than 0 (zero), or greater than or
        /// equal to the length of the args array.</para>
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// The length of the expanded string would exceed
        /// <see cref="p:StringBuilder.MaxCapacity"/>.
        /// </exception>
        public static StringBuilder AppendFormatIf(this StringBuilder source, bool condition, string format, params object[] args)
        {
            return source.AppendFormatIf(condition, CultureInfo.CurrentCulture, format, args);
        }
        #endregion

        #region AppendFormatIf(this StringBuilder source, bool condition, IFormatProvider provider, string format, params object[] args)
        /// <summary>
        /// Appends the string returned by processing a composite format string, which
        /// contains zero or more format items, followed by the default line terminator to
        /// the end of this instance if <paramref name="condition"/> is <see langword="true"/>.
        /// Each format item is replaced by the string representation of a corresponding
        /// argument in a parameter array using a specified format provider.
        /// </summary>
        /// <param name="source">The source <see cref="StringBuilder"/> instance.</param>
        /// <param name="condition"><see langword="true"/> to append <paramref name="format"/>;
        /// otherwise, <see langword="false"/>.</param>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An array of objects to format.</param>
        /// <returns>A reference to <paramref name="source"/> with <paramref name="format"/>
        /// appended, if <paramref name="condition"/> is <see langword="true"/>. Each format item in <paramref name="format"/> is replaced by the
        /// string representation of the corresponding object argument.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="format"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="System.FormatException">
        /// <para><paramref name="format"/> is invalid.</para>
        /// <para>-or-</para>
        /// <para>The index of a format item is less than 0 (zero), or greater than or
        /// equal to the length of the args array.</para>
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// The length of the expanded string would exceed
        /// <see cref="p:StringBuilder.MaxCapacity"/>.
        /// </exception>
        public static StringBuilder AppendFormatIf(this StringBuilder source, bool condition, IFormatProvider provider, string format, params object[] args)
        {
            Contracts.Requires.NotNull(source, nameof(source));

            if (condition)
            {
                source.AppendFormat(provider, format, args);
            }

            return source;
        }
        #endregion

        #endregion

        #region AppendFormatLine

        #region AppendFormatLine(this StringBuilder source, string format, params object[] args)
        /// <overoads>
        /// <summary>
        /// Appends the string returned by processing a composite format string, which
        /// contains zero or more format items, followed by the default line terminator to
        /// the end of this instance.
        /// </summary>
        /// </overoads>
        /// <summary>
        /// Appends the string returned by processing a composite format string, which
        /// contains zero or more format items, followed by the default line terminator to
        /// the end of this instance.
        /// </summary>
        /// <param name="source">The source <see cref="StringBuilder"/> instance.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An array of objects to format.</param>
        /// <returns>A reference to <paramref name="source"/> with <paramref name="format"/>
        /// appended. Each format item in <paramref name="format"/> is replaced by the
        /// string representation of the corresponding object argument.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="format"/> or <paramref name="args"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="System.FormatException">
        /// <para><paramref name="format"/> is invalid.</para>
        /// <para>-or-</para>
        /// <para>The index of a format item is less than 0 (zero), or greater than or
        /// equal to the length of the args array.</para>
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// The length of the expanded string would exceed
        /// <see cref="p:StringBuilder.MaxCapacity"/>.
        /// </exception>
        public static StringBuilder AppendFormatLine(this StringBuilder source, string format, params object[] args)
        {
            return source.AppendFormatLine(CultureInfo.CurrentCulture, format, args);
        }
        #endregion

        #region AppendFormatLine(this StringBuilder source, IFormatProvider provider, string format, params object[] args)
        /// <summary>
        /// Appends the string returned by processing a composite format string, which
        /// contains zero or more format items, followed by the default line terminator to
        /// the end of this instance. Each format item is
        /// replaced by the string representation of a corresponding argument in a parameter
        /// array using a specified format provider.
        /// </summary>
        /// <param name="source">The source <see cref="StringBuilder"/> object.</param>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An array of objects to format.</param>
        /// <returns>A reference to <paramref name="source"/> with <paramref name="format"/>
        /// appended. Each format item in <paramref name="format"/> is replaced by the
        /// string representation of the corresponding object argument.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="format"/> or <paramref name="args"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="System.FormatException">
        /// <para><paramref name="format"/> is invalid.</para>
        /// <para>-or-</para>
        /// <para>The index of a format item is less than 0 (zero), or greater than or
        /// equal to the length of the args array.</para>
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// The length of the expanded string would exceed
        /// <see cref="p:StringBuilder.MaxCapacity"/>.
        /// </exception>
        public static StringBuilder AppendFormatLine(this StringBuilder source, IFormatProvider provider, string format, params object[] args)
        {
            Contracts.Requires.NotNull(source, nameof(source));

            return source.AppendLine(String.Format(provider, format, args));
        }
        #endregion

        #endregion

        #region AppendFormatLineIf

        #region AppendFormatLineIf(this StringBuilder source, bool condition, string format, params object[] args)
        /// <overloads>
        /// <summary>
        /// Appends the string returned by processing a composite format string, which
        /// contains zero or more format items, followed by the default line terminator to
        /// the end of this instance if <paramref name="condition"/> is <see langword="true"/>.
        /// </summary>
        /// </overloads>
        /// <summary>
        /// Appends the string returned by processing a composite format string, which
        /// contains zero or more format items, followed by the default line terminator to
        /// the end of this instance if <paramref name="condition"/> is <see langword="true"/>.
        /// </summary>
        /// <param name="source">The source <see cref="StringBuilder"/> object.</param>
        /// <param name="condition"><see langword="true"/> to append <paramref name="format"/>;
        /// otherwise, <see langword="false"/>.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An array of objects to format.</param>
        /// <returns>A reference to <paramref name="source"/> with <paramref name="format"/>
        /// appended. Each format item in <paramref name="format"/> is replaced by the
        /// string representation of the corresponding object argument.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="format"/> or <paramref name="args"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="System.FormatException">
        /// <para><paramref name="format"/> is invalid.</para>
        /// <para>-or-</para>
        /// <para>The index of a format item is less than 0 (zero), or greater than or
        /// equal to the length of the args array.</para>
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// The length of the expanded string would exceed
        /// <see cref="p:StringBuilder.MaxCapacity"/>.
        /// </exception>
        public static StringBuilder AppendFormatLineIf(this StringBuilder source, bool condition, string format, params object[] args)
        {
            Contracts.Requires.NotNull(source, nameof(source));

            if (condition)
            {
                source.AppendFormatLine(format, args);
            }

            return source;
        }
        #endregion

        #region AppendFormatLineIf(this StringBuilder source, IFormatProvider provider, bool condition, string format, params object[] args)
        /// <summary>
        /// Appends the string returned by processing a composite format string, which
        /// contains zero or more format items, followed by the default line terminator to
        /// the end of this instance if <paramref name="condition"/> is <see langword="true"/>.
        /// Each format item is replaced by the string representation of a corresponding
        /// argument in a parameter array using a specified format provider.
        /// </summary>
        /// <param name="source">The source <see cref="StringBuilder"/> object.</param>
        /// <param name="condition"><see langword="true"/> to append <paramref name="format"/>;
        /// otherwise, <see langword="false"/>.</param>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An array of objects to format.</param>
        /// <returns>A reference to <paramref name="source"/> with <paramref name="format"/>
        /// appended. Each format item in <paramref name="format"/> is replaced by the
        /// string representation of the corresponding object argument.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="format"/> or <paramref name="args"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="System.FormatException">
        /// <para><paramref name="format"/> is invalid.</para>
        /// <para>-or-</para>
        /// <para>The index of a format item is less than 0 (zero), or greater than or
        /// equal to the length of the args array.</para>
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// The length of the expanded string would exceed
        /// <see cref="p:StringBuilder.MaxCapacity"/>.
        /// </exception>
        public static StringBuilder AppendFormatLineIf(this StringBuilder source, bool condition, IFormatProvider provider, string format, params object[] args)
        {
            Contracts.Requires.NotNull(source, nameof(source));

            if (condition)
            {
                source.AppendFormatLine(provider, format, args);
            }

            return source;
        }
        #endregion

        #endregion

        #region AppendIf

        #region AppendIf(this StringBuilder source, bool condition, bool value)
        /// <overloads>
        /// <summary>Appends the string representation of a specified object to this instance.</summary>
        /// </overloads>
        /// <summary>
        /// Appends the string representation of a specified <see cref="Boolean"/> value to this instance
        /// if <paramref name="condition"/> is <see langword="true"/>.
        /// </summary>
        /// <param name="source">The source <see cref="StringBuilder"/> instance.</param>
        /// <param name="condition"><see langword="true"/> to append <paramref name="value"/>;
        /// otherwise, <see langword="false"/>.</param>
        /// <param name="value">The <see cref="Boolean"/> value to append.</param>
        /// <returns>A reference to this instance after the append operation has, optionally, completed.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// Enlarging the value of this instance would exceed <see cref="p:StringBuilder.MaxCapacity"/>.
        /// </exception>
        public static StringBuilder AppendIf(this StringBuilder source, bool condition, bool value)
        {
            Contracts.Requires.NotNull(source, nameof(source));

            if (condition)
            {
                source.Append(value);
            }

            return source;
        }
        #endregion

        #region AppendIf(this StringBuilder source, bool condition, byte value)
        /// <summary>
        /// Appends the string representation of a specified 8-bit unsigned integer to this instance
        /// if <paramref name="condition"/> is <see langword="true"/>.
        /// </summary>
        /// <param name="source">The source <see cref="StringBuilder"/> instance.</param>
        /// <param name="condition"><see langword="true"/> to append <paramref name="value"/>;
        /// otherwise, <see langword="false"/>.</param>
        /// <param name="value">The value to append.</param>
        /// <returns>A reference to this instance after the append operation has, optionally, completed.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// Enlarging the value of this instance would exceed <see cref="p:StringBuilder.MaxCapacity"/>.
        /// </exception>
        public static StringBuilder AppendIf(this StringBuilder source, bool condition, byte value)
        {
            Contracts.Requires.NotNull(source, nameof(source));

            if (condition)
            {
                source.Append(value);
            }

            return source;
        }
        #endregion

        #region AppendIf(this StringBuilder source, bool condition, char value)
        /// <summary>
        /// Appends the string representation of a specified Unicode character to this instance
        /// if <paramref name="condition"/> is <see langword="true"/>.
        /// </summary>
        /// <param name="source">The source <see cref="StringBuilder"/> instance.</param>
        /// <param name="condition"><see langword="true"/> to append <paramref name="value"/>;
        /// otherwise, <see langword="false"/>.</param>
        /// <param name="value">The Unicode character to append.</param>
        /// <returns>A reference to this instance after the append operation has, optionally, completed.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// Enlarging the value of this instance would exceed <see cref="p:StringBuilder.MaxCapacity"/>.
        /// </exception>
        public static StringBuilder AppendIf(this StringBuilder source, bool condition, char value)
        {
            Contracts.Requires.NotNull(source, nameof(source));

            if (condition)
            {
                source.Append(value);
            }

            return source;
        }
        #endregion

        #region AppendIf(this StringBuilder source, bool condition, char[] value)
        /// <summary>
        /// Appends the string representation of Unicode characters in a specified array to this instance
        /// if <paramref name="condition"/> is <see langword="true"/>.
        /// </summary>
        /// <param name="source">The source <see cref="StringBuilder"/> instance.</param>
        /// <param name="condition"><see langword="true"/> to append <paramref name="value"/>;
        /// otherwise, <see langword="false"/>.</param>
        /// <param name="value">The array of characters to append.</param>
        /// <returns>A reference to this instance after the append operation has, optionally, completed.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// Enlarging the value of this instance would exceed <see cref="p:StringBuilder.MaxCapacity"/>.
        /// </exception>
        public static StringBuilder AppendIf(this StringBuilder source, bool condition, char[] value)
        {
            Contracts.Requires.NotNull(source, nameof(source));

            if (condition)
            {
                source.Append(value);
            }

            return source;
        }
        #endregion

        #region AppendIf(this StringBuilder source, bool condition, double value)
        /// <summary>
        /// Appends the string representation of a specified double-precision floating-point number to this instance
        /// if <paramref name="condition"/> is <see langword="true"/>.
        /// </summary>
        /// <param name="source">The source <see cref="StringBuilder"/> instance.</param>
        /// <param name="condition"><see langword="true"/> to append <paramref name="value"/>;
        /// otherwise, <see langword="false"/>.</param>
        /// <param name="value">The value to append.</param>
        /// <returns>A reference to this instance after the append operation has, optionally, completed.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// Enlarging the value of this instance would exceed <see cref="p:StringBuilder.MaxCapacity"/>.
        /// </exception>
        public static StringBuilder AppendIf(this StringBuilder source, bool condition, double value)
        {
            Contracts.Requires.NotNull(source, nameof(source));

            if (condition)
            {
                source.Append(value);
            }

            return source;
        }
        #endregion

        #region AppendIf(this StringBuilder source, bool condition, float value)
        /// <summary>
        /// Appends the string representation of a specified single-precision floating-point number to this instance
        /// if <paramref name="condition"/> is <see langword="true"/>.
        /// </summary>
        /// <param name="source">The source <see cref="StringBuilder"/> instance.</param>
        /// <param name="condition"><see langword="true"/> to append <paramref name="value"/>;
        /// otherwise, <see langword="false"/>.</param>
        /// <param name="value">The value to append.</param>
        /// <returns>A reference to this instance after the append operation has, optionally, completed.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// Enlarging the value of this instance would exceed <see cref="p:StringBuilder.MaxCapacity"/>.
        /// </exception>
        public static StringBuilder AppendIf(this StringBuilder source, bool condition, float value)
        {
            Contracts.Requires.NotNull(source, nameof(source));

            if (condition)
            {
                source.Append(value);
            }

            return source;
        }
        #endregion

        #region AppendIf(this StringBuilder source, bool condition, int value)
        /// <summary>
        /// Appends the string representation of a specified 32-bit signed integer to this instance
        /// if <paramref name="condition"/> is <see langword="true"/>.
        /// </summary>
        /// <param name="source">The source <see cref="StringBuilder"/> instance.</param>
        /// <param name="condition"><see langword="true"/> to append <paramref name="value"/>;
        /// otherwise, <see langword="false"/>.</param>
        /// <param name="value">The value to append.</param>
        /// <returns>A reference to this instance after the append operation has, optionally, completed.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// Enlarging the value of this instance would exceed <see cref="p:StringBuilder.MaxCapacity"/>.
        /// </exception>
        public static StringBuilder AppendIf(this StringBuilder source, bool condition, int value)
        {
            Contracts.Requires.NotNull(source, nameof(source));

            if (condition)
            {
                source.Append(value);
            }

            return source;
        }
        #endregion

        #region AppendIf(this StringBuilder source, bool condition, long value)
        /// <summary>
        /// Appends the string representation of a specified 64-bit signed integer to this instance
        /// if <paramref name="condition"/> is <see langword="true"/>.
        /// </summary>
        /// <param name="source">The source <see cref="StringBuilder"/> instance.</param>
        /// <param name="condition"><see langword="true"/> to append <paramref name="value"/>;
        /// otherwise, <see langword="false"/>.</param>
        /// <param name="value">The value to append.</param>
        /// <returns>A reference to this instance after the append operation has, optionally, completed.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// Enlarging the value of this instance would exceed <see cref="p:StringBuilder.MaxCapacity"/>.
        /// </exception>
        public static StringBuilder AppendIf(this StringBuilder source, bool condition, long value)
        {
            Contracts.Requires.NotNull(source, nameof(source));

            if (condition)
            {
                source.Append(value);
            }

            return source;
        }
        #endregion

        #region AppendIf(this StringBuilder source, bool condition, object value)
        /// <summary>
        /// Appends the string representation of a specified object to this instance
        /// if <paramref name="condition"/> is <see langword="true"/>.
        /// </summary>
        /// <param name="source">The source <see cref="StringBuilder"/> instance.</param>
        /// <param name="condition"><see langword="true"/> to append <paramref name="value"/>;
        /// otherwise, <see langword="false"/>.</param>
        /// <param name="value">The value to append.</param>
        /// <returns>A reference to this instance after the append operation has, optionally, completed.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// Enlarging the value of this instance would exceed <see cref="p:StringBuilder.MaxCapacity"/>.
        /// </exception>
        public static StringBuilder AppendIf(this StringBuilder source, bool condition, object value)
        {
            Contracts.Requires.NotNull(source, nameof(source));

            if (condition)
            {
                source.Append(value);
            }

            return source;
        }
        #endregion

        #region AppendIf(this StringBuilder source, bool condition, sbyte value)
        /// <summary>
        /// Appends the string representation of a specified 8-bit signed integer to this instance
        /// if <paramref name="condition"/> is <see langword="true"/>.
        /// </summary>
        /// <param name="source">The source <see cref="StringBuilder"/> instance.</param>
        /// <param name="condition"><see langword="true"/> to append <paramref name="value"/>;
        /// otherwise, <see langword="false"/>.</param>
        /// <param name="value">The value to append.</param>
        /// <returns>A reference to this instance after the append operation has, optionally, completed.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// Enlarging the value of this instance would exceed <see cref="p:StringBuilder.MaxCapacity"/>.
        /// </exception>
        public static StringBuilder AppendIf(this StringBuilder source, bool condition, sbyte value)
        {
            Contracts.Requires.NotNull(source, nameof(source));

            if (condition)
            {
                source.Append(value);
            }

            return source;
        }
        #endregion

        #region AppendIf(this StringBuilder source, bool condition, short value)
        /// <summary>
        /// Appends the string representation of a specified 16-bit signed integer to this instance
        /// if <paramref name="condition"/> is <see langword="true"/>.
        /// </summary>
        /// <param name="source">The source <see cref="StringBuilder"/> instance.</param>
        /// <param name="condition"><see langword="true"/> to append <paramref name="value"/>;
        /// otherwise, <see langword="false"/>.</param>
        /// <param name="value">The value to append.</param>
        /// <returns>A reference to this instance after the append operation has, optionally, completed.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// Enlarging the value of this instance would exceed <see cref="p:StringBuilder.MaxCapacity"/>.
        /// </exception>
        public static StringBuilder AppendIf(this StringBuilder source, bool condition, short value)
        {
            Contracts.Requires.NotNull(source, nameof(source));

            if (condition)
            {
                source.Append(value);
            }

            return source;
        }
        #endregion

        #region AppendIf(this StringBuilder source, bool condition, string value)
        /// <summary>
        /// Appends the string representation of the specified 8-bit unsigned integer to this instance
        /// if <paramref name="condition"/> is <see langword="true"/>.
        /// </summary>
        /// <param name="source">The source <see cref="StringBuilder"/> instance.</param>
        /// <param name="condition"><see langword="true"/> to append <paramref name="value"/>;
        /// otherwise, <see langword="false"/>.</param>
        /// <param name="value">The string to append.</param>
        /// <returns>A reference to this instance after the append operation has, optionally, completed.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// Enlarging the value of this instance would exceed <see cref="p:StringBuilder.MaxCapacity"/>.
        /// </exception>
        public static StringBuilder AppendIf(this StringBuilder source, bool condition, string value)
        {
            Contracts.Requires.NotNull(source, nameof(source));

            if (condition)
            {
                source.Append(value);
            }

            return source;
        }
        #endregion

        #region AppendIf(this StringBuilder source, bool condition, uint value)
        /// <summary>
        /// Appends the string representation of a specified 32-bit unsigned integer to this instance
        /// if <paramref name="condition"/> is <see langword="true"/>.
        /// </summary>
        /// <param name="source">The source <see cref="StringBuilder"/> instance.</param>
        /// <param name="condition"><see langword="true"/> to append <paramref name="value"/>;
        /// otherwise, <see langword="false"/>.</param>
        /// <param name="value">The value to append.</param>
        /// <returns>A reference to this instance after the append operation has, optionally, completed.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// Enlarging the value of this instance would exceed <see cref="p:StringBuilder.MaxCapacity"/>.
        /// </exception>
        public static StringBuilder AppendIf(this StringBuilder source, bool condition, uint value)
        {
            Contracts.Requires.NotNull(source, nameof(source));

            if (condition)
            {
                source.Append(value);
            }

            return source;
        }
        #endregion

        #region AppendIf(this StringBuilder source, bool condition, ulong value)
        /// <summary>
        /// Appends the string representation of a specified 64-bit unsigned integer to this instance
        /// if <paramref name="condition"/> is <see langword="true"/>.
        /// </summary>
        /// <param name="source">The source <see cref="StringBuilder"/> instance.</param>
        /// <param name="condition"><see langword="true"/> to append <paramref name="value"/>;
        /// otherwise, <see langword="false"/>.</param>
        /// <param name="value">The value to append.</param>
        /// <returns>A reference to this instance after the append operation has, optionally, completed.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// Enlarging the value of this instance would exceed <see cref="p:StringBuilder.MaxCapacity"/>.
        /// </exception>
        public static StringBuilder AppendIf(this StringBuilder source, bool condition, ulong value)
        {
            Contracts.Requires.NotNull(source, nameof(source));

            if (condition)
            {
                source.Append(value);
            }

            return source;
        }
        #endregion

        #region AppendIf(this StringBuilder source, bool condition, ushort value)
        /// <summary>
        /// Appends the string representation of a specified 16-bit unsigned integer to this instance
        /// if <paramref name="condition"/> is <see langword="true"/>.
        /// </summary>
        /// <param name="source">The source <see cref="StringBuilder"/> instance.</param>
        /// <param name="condition"><see langword="true"/> to append <paramref name="value"/>;
        /// otherwise, <see langword="false"/>.</param>
        /// <param name="value">The value to append.</param>
        /// <returns>A reference to this instance after the append operation has, optionally, completed.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// Enlarging the value of this instance would exceed <see cref="p:StringBuilder.MaxCapacity"/>.
        /// </exception>
        public static StringBuilder AppendIf(this StringBuilder source, bool condition, ushort value)
        {
            Contracts.Requires.NotNull(source, nameof(source));

            if (condition)
            {
                source.Append(value);
            }

            return source;
        }
        #endregion

        #region AppendIf(this StringBuilder source, bool condition, char value, int repeatCount)
        /// <summary>
        /// Appends a specified number of copies of the string representation of a
        /// Unicode character to this instance
        /// if <paramref name="condition"/> is <see langword="true"/>.
        /// </summary>
        /// <param name="source">The source <see cref="StringBuilder"/> instance.</param>
        /// <param name="condition"><see langword="true"/> to append <paramref name="value"/>;
        /// otherwise, <see langword="false"/>.</param>
        /// <param name="value">The character to append.</param>
        /// <param name="repeatCount">The number of times to append <paramref name="value"/>.</param>
        /// <returns>A reference to this instance after the append operation has, optionally, completed.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// <para><paramref name="repeatCount"/> is less than zero.</para>
        /// <para>-or-</para>
        /// <para>Enlarging the value of this instance would exceed <see cref="p:StringBuilder.MaxCapacity"/>.</para>
        /// </exception>
        /// <exception cref="System.OutOfMemoryException">Out of memory.</exception>
        public static StringBuilder AppendIf(this StringBuilder source, bool condition, char value, int repeatCount)
        {
            Contracts.Requires.NotNull(source, nameof(source));

            if (condition)
            {
                source.Append(value, repeatCount);
            }

            return source;
        }
        #endregion

        #region AppendIf(this StringBuilder source, bool condition, char[] value, int startIndex, int charCount)
        /// <summary>
        /// Appends the string representation of a specified subarray of Unicode characters to this instance
        /// if <paramref name="condition"/> is <see langword="true"/>.
        /// </summary>
        /// <param name="source">The source <see cref="StringBuilder"/> instance.</param>
        /// <param name="condition"><see langword="true"/> to append <paramref name="value"/>;
        /// otherwise, <see langword="false"/>.</param>
        /// <param name="value">A character array.</param>
        /// <param name="startIndex">The starting position in <paramref name="value"/>.</param>
        /// <param name="charCount">The number of characters to append.</param>
        /// <returns>A reference to this instance after the append operation has, optionally, completed.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="value"/> is <see langword="null"/>, and <paramref name="startIndex"/>
        /// and <paramref name="charCount"/> are not zero.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// <para><paramref name="charCount"/> is less than zero.</para>
        /// <para>-or-</para>
        /// <para><paramref name="startIndex"/> is less than zero.</para>
        /// <para>-or-</para>
        /// <para><paramref name="startIndex"/> + <paramref name="charCount"/> is greater than the length of <paramref name="value"/>.</para>
        /// <para>-or-</para>
        /// <para>Enlarging the value of this instance would exceed <see cref="p:StringBuilder.MaxCapacity"/>.</para>
        /// </exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
        public static StringBuilder AppendIf(this StringBuilder source, bool condition, char[] value, int startIndex, int charCount)
        {
            Contracts.Requires.NotNull(source, nameof(source));

            if (condition)
            {
                source.Append(value, startIndex, charCount);
            }

            return source;
        }
        #endregion

        #region AppendIf(this StringBuilder source, bool condition, string value, int startIndex, int count)
        /// <summary>
        /// Appends a copy of a specified substring to this instance
        /// if <paramref name="condition"/> is <see langword="true"/>.
        /// </summary>
        /// <param name="source">The source <see cref="StringBuilder"/> instance.</param>
        /// <param name="condition"><see langword="true"/> to append <paramref name="value"/>;
        /// otherwise, <see langword="false"/>.</param>
        /// <param name="value">The string that contains the substring to append.</param>
        /// <param name="startIndex">The starting position of the substring within <paramref name="value"/>.</param>
        /// <param name="count">The number of characters in <paramref name="value"/> to append.</param>
        /// <returns>A reference to this instance after the append operation has, optionally, completed.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="value"/> is <see langword="null"/>, and <paramref name="startIndex"/>
        /// and <paramref name="count"/> are not zero.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// <para><paramref name="count"/> is less than zero.</para>
        /// <para>-or-</para>
        /// <para><paramref name="startIndex"/> is less than zero.</para>
        /// <para>-or-</para>
        /// <para><paramref name="startIndex"/> + <paramref name="count"/> is greater than the length of <paramref name="value"/>.</para>
        /// <para>-or-</para>
        /// <para>Enlarging the value of this instance would exceed <see cref="p:StringBuilder.MaxCapacity"/>.</para>
        /// </exception>
        public static StringBuilder AppendIf(this StringBuilder source, bool condition, string value, int startIndex, int count)
        {
            Contracts.Requires.NotNull(source, nameof(source));

            if (condition)
            {
                source.Append(value, startIndex, count);
            }

            return source;
        }
        #endregion

        #endregion

        #region AppendLineIf

        #region AppendLineIf(this StringBuilder source, bool condition)
        /// <overloads>
        /// <summary>
        /// Appends the default line terminator to the end of this instance if
        /// <paramref name="condition"/> is <see langword="true"/>.
        /// </summary>
        /// </overloads>
        /// <summary>
        /// Appends the default line terminator to the end of this instance if
        /// <paramref name="condition"/> is <see langword="true"/>.
        /// </summary>
        /// <param name="source">The source <see cref="StringBuilder"/> object.</param>
        /// <param name="condition"><see langword="true"/> to append the default line terminator;
        /// otherwise, <see langword="false"/>.</param>
        /// <returns>A reference to this instance after the append operation has, optionally, completed.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// The length of the expanded string would exceed
        /// <see cref="p:StringBuilder.MaxCapacity"/>.
        /// </exception>
        public static StringBuilder AppendLineIf(this StringBuilder source, bool condition)
        {
            Contracts.Requires.NotNull(source, nameof(source));

            if (condition)
            {
                source.AppendLine();
            }

            return source;
        }
        #endregion

        #region AppendLineIf(this StringBuilder source, bool condition, string value)
        /// <summary>
        /// Appends a copy of the specified string followed by the default line terminator
        /// to the end of this instance if <paramref name="condition"/> is <see langword="true"/>.
        /// </summary>
        /// <param name="source">The source <see cref="StringBuilder"/> object.</param>
        /// <param name="condition"><see langword="true"/> to append <paramref name="value"/>;
        /// otherwise, <see langword="false"/>.</param>
        /// <param name="value">The string to append.</param>
        /// <returns>A reference to this instance after the append operation has, optionally, completed.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// The length of the expanded string would exceed
        /// <see cref="p:StringBuilder.MaxCapacity"/>.
        /// </exception>
        public static StringBuilder AppendLineIf(this StringBuilder source, bool condition, string value)
        {
            Contracts.Requires.NotNull(source, nameof(source));

            if (condition)
            {
                source.AppendLine(value);
            }

            return source;
        }
        #endregion

        #endregion

        #region ToHex
        internal static char ToHex(int b)
        {
            return (char)((b < 0xA) ? ('0' + b) : ('a' + b - 0xA));
        }
        #endregion

        #endregion
    }
}
