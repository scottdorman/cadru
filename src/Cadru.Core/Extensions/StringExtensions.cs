//------------------------------------------------------------------------------
// <copyright file="StringExtensions.cs"
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
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;

using Cadru.Internal;
using Cadru.Resources;
using Cadru.Text;

using Validation;

namespace Cadru.Extensions
{
    /// <summary>
    /// Provides basic routines for common string manipulation.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Returns a new string whose textual value is the normalized form of <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The <see cref="String"/> to normalize.</param>
        /// <returns>A new, normalized string.</returns>
        /// <remarks>
        /// <para>
        /// The <see cref="Clean(String)"/> method removes all occurrences of
        /// white space and control characters from the beginning and end of the
        /// given string as well as collapsing all internal white space
        /// characters to a single white space character.
        /// </para>
        /// </remarks>
        public static string Clean(this string source) => Clean(source, NormalizationOptions.All);

        /// <summary>
        /// Returns a new string whose textual value is the normalized form of <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The <see cref="String"/> to normalize.</param>
        /// <param name="options">
        /// One of the <see cref="NormalizationOptions"/> values.
        /// </param>
        /// <returns>A new, normalized string.</returns>
        public static string Clean(this string source, NormalizationOptions options)
        {
            Requires.NotNull(source, nameof(source));

            if ((int)options < 0 || ((int)options & (int)~(NormalizationOptions.ControlCharacters | NormalizationOptions.Whitespace)) != 0)
            {
                throw ExceptionBuilder.CreateArgumentException("options", String.Format(CultureInfo.CurrentUICulture, Strings.Argument_EnumIllegalVal, (int)options));
            }

            ReadOnlySpan<char> normalized;

            if ((options & NormalizationOptions.Whitespace) == NormalizationOptions.Whitespace)
            {
                normalized = source.AsSpan().Trim();
            }
            else
            {
                normalized = source.AsSpan();
            }

            var index = 0;
            var whitespaceCount = 0;
            var controlCount = 0;
            var builder = new StringBuilder(source.Length);
            while (index < normalized.Length)
            {
                if ((options & NormalizationOptions.Whitespace) == NormalizationOptions.Whitespace)
                {
                    var position = index;
                    if (Char.IsWhiteSpace(normalized[position]))
                    {
                        while ((position + 1) < normalized.Length && Char.IsWhiteSpace(normalized[++position]))
                        {
                            // we found a whitespace character, so look ahead
                            // until we find the next non-whitespace character.
                            whitespaceCount++;
                        }

                        if (whitespaceCount >= 0)
                        {
                            builder.Append(" ");
                        }

                        whitespaceCount = 0;
                        index = position;
                    }
                }

                if ((options & NormalizationOptions.ControlCharacters) == NormalizationOptions.ControlCharacters && Char.IsControl(normalized[index]))
                {
                    var position = index;
                    while ((position + 1) < normalized.Length && Char.IsControl(normalized[++position]))
                    {
                        // we found a control character, so look ahead until we
                        // find the next non-control character.
                        controlCount++;
                    }

                    controlCount = 0;
                    index = position;
                }

                builder.Append(normalized[index]);
                index++;
            }

            return builder.ToString();
        }

        /// <summary>
        /// Returns a value indicating whether the specified
        /// <see cref="String"/> object occurs within this string.
        /// </summary>
        /// <param name="source">The source <see cref="String"/>.</param>
        /// <param name="value">The string to seek.</param>
        /// <param name="comparisonType">
        /// One of the enumeration values that specifies how the strings will be compared.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the <paramref name="value"/> parameter
        /// occurs within this string, or if <paramref name="value"/> is the
        /// empty string (""); otherwise, <see langword="false"/>.
        /// </returns>
        public static bool Contains(this string source, string value, StringComparison comparisonType)
        {
            Requires.NotNull(source, nameof(source));
            Requires.NotNull(value, nameof(value));

            return source.IndexOf(value, comparisonType) >= 0;
        }

        /// <summary>
        /// Determines whether the end of this string instance matches any of
        /// the specified strings.
        /// </summary>
        /// <param name="source">The source <see cref="String"/>.</param>
        /// <param name="values">A collection of string instances.</param>
        /// <returns>
        /// <see langword="true"/> if the end of this string instance matches
        /// any of the specified strings; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool EndsWithAny(this string source, IEnumerable<string> values) => source.EndsWithAny(values, StringComparison.CurrentCulture);

        /// <summary>
        /// Determines whether the end of this string instance matches any of
        /// the specified strings.
        /// </summary>
        /// <param name="source">The source <see cref="String"/>.</param>
        /// <param name="values">A collection of string instances.</param>
        /// <param name="comparisonType">
        /// One of the enumeration values that specifies the rules for the comparison.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the end of this string instance matches
        /// any of the specified strings; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool EndsWithAny(this string source, IEnumerable<string> values, StringComparison comparisonType)
        {
            Requires.NotNull(source, nameof(source));
            Requires.NotNullOrEmpty(values, nameof(values));

            foreach (var value in values)
            {
                if (source.EndsWith(value, comparisonType))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Returns a new string with <paramref name="trailingCharacter"/> at the end of <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The source <see cref="String"/>.</param>
        /// <param name="trailingCharacter">The character to compare to the character at the end of this instance.</param>
        /// <returns>The string instance with the <paramref name="trailingCharacter"/> at the end.</returns>
        public static string EnsureTrailingCharacter(this string source, char trailingCharacter)
        {
#if NETSTANDARD2_0
            return source.EndsWith(trailingCharacter.ToString()) ? source : $"{source}{trailingCharacter}";
#else
            return source.EndsWith(trailingCharacter) ? source : $"{source}{trailingCharacter}";
#endif
        }

        /// <summary>
        /// Determines whether this string instance is equal to any of the
        /// specified strings.
        /// </summary>
        /// <param name="source">The source <see cref="String"/>.</param>
        /// <param name="values">A collection of string instances.</param>
        /// <returns>
        /// <see langword="true"/> if the string instance is equal to any of the
        /// specified strings; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool EqualsAny(this string source, IEnumerable<string> values) => source.EqualsAny(values, StringComparison.CurrentCulture);

        /// <summary>
        /// Determines whether this string instance is equal to any of the
        /// specified strings.
        /// </summary>
        /// <param name="source">The source <see cref="String"/>.</param>
        /// <param name="values">A collection of string instances.</param>
        /// <param name="comparisonType">
        /// One of the enumeration values that specifies the rules for the comparison.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the string instance is equal to any of the
        /// specified strings; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool EqualsAny(this string source, IEnumerable<string> values, StringComparison comparisonType)
        {
            Requires.NotNull(source, nameof(source));
            Requires.NotNullOrEmpty(values, nameof(values));

            foreach (var value in values)
            {
                if (String.Equals(source, value, comparisonType))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Replaces the format item in a specified System.String with the text
        /// equivalent of the value of a corresponding System.Object instance in
        /// a specified array.
        /// </summary>
        /// <param name="instance">A string to format.</param>
        /// <param name="args">
        /// An System.Object array containing zero or more objects to format.
        /// </param>
        /// <returns>
        /// A copy of format in which the format items have been replaced by the
        /// System.String equivalent of the corresponding instances of
        /// System.Object in args.
        /// </returns>
        public static string FormatWith(this string instance, params object[] args)
        {
            return String.Format(CultureInfo.CurrentCulture, instance, args);
        }

        /// <summary>
        /// Reports the zero-based index of the nth occurrence of the specified
        /// character in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The source <see cref="String"/>.</param>
        /// <param name="value">The character to seek.</param>
        /// <param name="occurrence">The occurrence to find.</param>
        /// <returns>
        /// The index position of <paramref name="value"/> if that character is
        /// found, or -1 if it is not. If <paramref name="value"/> is
        /// <see cref="String.Empty">String.Empty</see>, the return value is 0.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <para><paramref name="source"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para><paramref name="value"/> is <see langword="null"/>.</para>
        /// </exception>
        public static int IndexOfOccurrence(this string source, char value, int occurrence)
        {
            Requires.NotNull(source, nameof(source));

            return source.IndexOfOccurrence(value, 0, occurrence);
        }

        /// <summary>
        /// Reports the zero-based index of the nth occurrence of the specified
        /// string in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The source <see cref="String"/>.</param>
        /// <param name="value">The string to seek.</param>
        /// <param name="occurrence">The occurrence to find.</param>
        /// <returns>
        /// The index position of <paramref name="value"/> if that string is
        /// found, or -1 if it is not. If <paramref name="value"/> is
        /// <see cref="String.Empty">String.Empty</see>, the return value is 0.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <para><paramref name="source"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para><paramref name="value"/> is <see langword="null"/>.</para>
        /// </exception>
        public static int IndexOfOccurrence(this string source, string value, int occurrence)
        {
            Requires.NotNull(source, nameof(source));

            return source.IndexOfOccurrence(value, 0, occurrence);
        }

        /// <summary>
        /// Reports the zero-based index of the nth occurrence of the specified
        /// character in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The source <see cref="String"/>.</param>
        /// <param name="value">The string to seek.</param>
        /// <param name="startIndex">The search starting position.</param>
        /// <param name="occurrence">The occurrence to find.</param>
        /// <returns>
        /// The index position of <paramref name="value"/> if that character is
        /// found, or -1 if it is not. If <paramref name="value"/> is
        /// <see cref="String.Empty">String.Empty</see>, the return value is 0.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <para><paramref name="source"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para><paramref name="value"/> is <see langword="null"/>.</para>
        /// </exception>
        public static int IndexOfOccurrence(this string source, char value, int startIndex, int occurrence)
        {
            Requires.NotNull(source, nameof(source));

            return source.IndexOfOccurrence(value, startIndex, source.Length - startIndex, occurrence);
        }

        /// <summary>
        /// Reports the zero-based index of the nth occurrence of the specified
        /// string in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The source <see cref="String"/>.</param>
        /// <param name="value">The string to seek.</param>
        /// <param name="startIndex">The search starting position.</param>
        /// <param name="occurrence">The occurrence to find.</param>
        /// <returns>
        /// The index position of <paramref name="value"/> if that string is
        /// found, or -1 if it is not. If <paramref name="value"/> is
        /// <see cref="String.Empty">String.Empty</see>, the return value is 0.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <para><paramref name="source"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para><paramref name="value"/> is <see langword="null"/>.</para>
        /// </exception>
        public static int IndexOfOccurrence(this string source, string value, int startIndex, int occurrence)
        {
            Requires.NotNull(source, nameof(source));

            return source.IndexOfOccurrence(value, startIndex, source.Length - startIndex, occurrence, StringComparison.Ordinal);
        }

        /// <summary>
        /// Reports the zero-based index of the nth occurrence of the specified
        /// string in <paramref name="source"/> using the specified string comparison.
        /// </summary>
        /// <param name="source">The source <see cref="String"/>.</param>
        /// <param name="value">The string to seek.</param>
        /// <param name="occurrence">The occurrence to find.</param>
        /// <param name="comparisonType">
        /// One of the <see cref="StringComparison"/> values.
        /// </param>
        /// <returns>
        /// The index position of <paramref name="value"/> if that string is
        /// found, or -1 if it is not. If <paramref name="value"/> is
        /// <see cref="String.Empty">String.Empty</see>, the return value is 0.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <para><paramref name="source"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para><paramref name="value"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="comparisonType"/> is not a valid
        /// <see cref="System.StringComparison"/> System.StringComparison value.
        /// </exception>
        public static int IndexOfOccurrence(this string source, string value, int occurrence, StringComparison comparisonType)
        {
            Requires.NotNull(source, nameof(source));

            return source.IndexOfOccurrence(value, 0, source.Length, occurrence, comparisonType);
        }

        /// <summary>
        /// Reports the zero-based index of the nth occurrence of the specified
        /// string in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The source <see cref="String"/>.</param>
        /// <param name="value">The string to seek.</param>
        /// <param name="startIndex">The search starting position.</param>
        /// <param name="count">The number of character positions to examine.</param>
        /// <param name="occurrence">The occurrence to find.</param>
        /// <returns>
        /// The index position of <paramref name="value"/> if that string is
        /// found, or -1 if it is not. If <paramref name="value"/> is
        /// <see cref="String.Empty">String.Empty</see>, the return value is 0.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <para><paramref name="source"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para><paramref name="value"/> is <see langword="null"/>.</para>
        /// </exception>
        public static int IndexOfOccurrence(this string source, char value, int startIndex, int count, int occurrence)
        {
            Requires.NotNull(source, nameof(source));
            Requires.Range(startIndex >= 0, nameof(startIndex), Strings.ArgumentOutOfRange_IndexLessThanZero);
            Requires.Range(startIndex < source.Length, nameof(startIndex), Strings.ArgumentOutOfRange_IndexLessThanLength);

            var index = source.IndexOf(value, startIndex, count);
            if (index == -1)
            {
                return -1;
            }

            for (var i = 1; i < occurrence; i++)
            {
                index = source.IndexOf(value, index + 1, source.Length - index - 1);
                if (index == -1)
                {
                    return -1;
                }
            }

            return index;
        }

        /// <summary>
        /// Reports the zero-based index of the nth occurrence of the specified
        /// string in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The source <see cref="String"/>.</param>
        /// <param name="value">The string to seek.</param>
        /// <param name="startIndex">The search starting position.</param>
        /// <param name="count">The number of character positions to examine.</param>
        /// <param name="occurrence">The occurrence to find.</param>
        /// <returns>
        /// The index position of <paramref name="value"/> if that string is
        /// found, or -1 if it is not. If <paramref name="value"/> is
        /// <see cref="String.Empty">String.Empty</see>, the return value is 0.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <para><paramref name="source"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para><paramref name="value"/> is <see langword="null"/>.</para>
        /// </exception>
        public static int IndexOfOccurrence(this string source, string value, int startIndex, int count, int occurrence)
        {
            Requires.NotNull(source, nameof(source));

            return source.IndexOfOccurrence(value, startIndex, count, occurrence, StringComparison.Ordinal);
        }

        /// <summary>
        /// Reports the zero-based index of the nth occurrence of the specified
        /// string in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The source <see cref="String"/>.</param>
        /// <param name="value">The string to seek.</param>
        /// <param name="startIndex">The search starting position.</param>
        /// <param name="occurrence">The occurrence to find.</param>
        /// <param name="comparisonType">
        /// One of the <see cref="StringComparison"/> values.
        /// </param>
        /// <returns>
        /// The index position of <paramref name="value"/> if that string is
        /// found, or -1 if it is not. If <paramref name="value"/> is
        /// <see cref="String.Empty">String.Empty</see>, the return value is 0.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <para><paramref name="source"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para><paramref name="value"/> is <see langword="null"/>.</para>
        /// </exception>
        public static int IndexOfOccurrence(this string source, string value, int startIndex, int occurrence, StringComparison comparisonType)
        {
            Requires.NotNull(source, nameof(source));

            return source.IndexOfOccurrence(value, startIndex, source.Length - startIndex, occurrence, comparisonType);
        }

        /// <summary>
        /// Reports the zero-based index of the nth occurrence of the specified
        /// string in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The source <see cref="String"/>.</param>
        /// <param name="value">The string to seek.</param>
        /// <param name="startIndex">The search starting position.</param>
        /// <param name="count">The number of character positions to examine.</param>
        /// <param name="occurrence">The occurrence to find.</param>
        /// <param name="comparisonType">
        /// One of the <see cref="StringComparison"/> values.
        /// </param>
        /// <returns>
        /// The index position of <paramref name="value"/> if that string is
        /// found, or -1 if it is not. If <paramref name="value"/> is
        /// <see cref="String.Empty">String.Empty</see>, the return value is 0.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <para><paramref name="source"/> is <see langword="null"/>.</para>
        /// <para>-or-</para>
        /// <para><paramref name="value"/> is <see langword="null"/>.</para>
        /// </exception>
        public static int IndexOfOccurrence(this string source, string value, int startIndex, int count, int occurrence, StringComparison comparisonType)
        {
            Requires.NotNull(source, nameof(source));
            Requires.Range(startIndex >= 0, nameof(startIndex), Strings.ArgumentOutOfRange_IndexLessThanZero);
            Requires.Range(startIndex < source.Length, nameof(startIndex), Strings.ArgumentOutOfRange_IndexLessThanLength);

            var index = source.IndexOf(value, startIndex, count, comparisonType);
            if (index == -1)
            {
                return -1;
            }

            for (var i = 1; i < occurrence; i++)
            {
                index = source.IndexOf(value, index + 1, source.Length - index - 1, comparisonType);
                if (index == -1)
                {
                    return -1;
                }
            }

            return index;
        }

        /// <summary>
        /// Determines whether this instance and another specified System.String
        /// object have the same value.
        /// </summary>
        /// <param name="instance">The string to check equality.</param>
        /// <param name="comparing">The comparing with string.</param>
        /// <returns>
        /// <c>true</c> if the value of the comparing parameter is the same as
        /// this string; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsCaseInsensitiveEqual(this string? instance, string comparing)
        {
            return String.Compare(instance, comparing, StringComparison.OrdinalIgnoreCase) == 0;
        }

        /// <summary>
        /// Determines whether this instance and another specified System.String
        /// object have the same value.
        /// </summary>
        /// <param name="instance">The string to check equality.</param>
        /// <param name="comparing">The comparing with string.</param>
        /// <returns>
        /// <c>true</c> if the value of the comparing parameter is the same as
        /// this string; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsCaseSensitiveEqual(this string? instance, string comparing)
        {
            return String.CompareOrdinal(instance, comparing) == 0;
        }

        /// <summary>
        /// Indicates whether the specified string is not <see langword="null"/>
        /// or an <see cref="String.Empty"/> string.
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="value"/> is not
        /// <see langword="null"/> or an <see cref="String.Empty"/> string ("");
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsNotNullOrEmpty(this string? value) => !value.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether a specified string is not <see langword="null"/>,
        /// <see cref="String.Empty">empty</see>, or consists only of
        /// white-space characters.
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <returns>
        /// <see langword="true"/> if the <paramref name="value"/> parameter is
        /// not <see langword="null"/> null or
        /// <see cref="String.Empty">String.Empty</see>, or if
        /// <paramref name="value"/> does not consist exclusively of white-space characters.
        /// </returns>
        /// <remarks>
        /// White-space characters are defined by the Unicode standard. The
        /// <see cref="IsNotNullOrWhiteSpace"/> method interprets any character
        /// that returns a value of <see langword="true"/> when it is passed to
        /// the <see cref="Char.IsWhiteSpace(Char)"/> method as a white-space character.
        /// </remarks>
        public static bool IsNotNullOrWhiteSpace(this string? value) => !value.IsNullOrWhiteSpace();

        /// <summary>
        /// Indicates whether the specified string is <see langword="null"/> or
        /// an <see cref="String.Empty"/> string.
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="value"/> is
        /// <see langword="null"/> or an <see cref="String.Empty"/> string ("");
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsNullOrEmpty([NotNullWhen(false)] this string? value) => String.IsNullOrEmpty(value);

        /// <summary>
        /// Indicates whether a specified string is <see langword="null"/>,
        /// <see cref="String.Empty">empty</see>, or consists only of
        /// white-space characters.
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <returns>
        /// <see langword="true"/> if the <paramref name="value"/> parameter is
        /// <see langword="null"/> null or
        /// <see cref="String.Empty">String.Empty</see>, or if
        /// <paramref name="value"/> consists exclusively of white-space characters.
        /// </returns>
        /// <remarks>
        /// White-space characters are defined by the Unicode standard. The
        /// <see cref="IsNullOrWhiteSpace"/> method interprets any character
        /// that returns a value of <see langword="true"/> when it is passed to
        /// the <see cref="Char.IsWhiteSpace(Char)"/> method as a white-space character.
        /// </remarks>
        public static bool IsNullOrWhiteSpace(this string? value) => String.IsNullOrWhiteSpace(value);

        /// <summary>
        /// Returns the last character in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The source <see cref="String"/>.</param>
        /// <returns>
        /// The last character in the string of the null character ('\0') if the
        /// string has a zero length.
        /// </returns>
        public static char LastCharacter(this string source)
        {
            Requires.NotNull(source, nameof(source));

            var lastCharacter = '\0';
            if (source.Length > 0)
            {
                lastCharacter = source[source.Length - 1];
            }

            return lastCharacter;
        }

        /// <summary>
        /// Retrieves a substring from <paramref name="source"/>. The substring
        /// ends at the specified character position.
        /// </summary>
        /// <param name="source">The source <see cref="String"/>.</param>
        /// <param name="value">The ending character of a substring.</param>
        /// <returns>
        /// A <see cref="String"/> object equivalent to the substring that ends
        /// at the position of <paramref name="value"/> in
        /// <paramref name="source"/>, or the entire string if
        /// <paramref name="value"/> is not found in the string.
        /// </returns>
        public static string LeftSubstring(this string source, char value) => LeftSubstring(source, value, 1);

        /// <summary>
        /// Retrieves a substring from <paramref name="source"/>. The substring
        /// ends at the specified string position.
        /// </summary>
        /// <param name="source">The source <see cref="String"/>.</param>
        /// <param name="value">The ending string of a substring.</param>
        /// <param name="occurrence">The occurrence of <paramref name="value"/>.</param>
        /// <returns>
        /// A <see cref="String"/> object equivalent to the substring that ends
        /// at the position of <paramref name="value"/> in
        /// <paramref name="source"/>, or the entire string if
        /// <paramref name="value"/> is not found in the string.
        /// </returns>
        public static string LeftSubstring(this string source, char value, int occurrence)
        {
            Requires.NotNull(source, nameof(source));

            var substring = source;
            var index = source.IndexOf(value);

            while (index != -1 && occurrence > 1)
            {
                index = source.IndexOf(value, index + 1);
                --occurrence;
            }

            if (index != -1)
            {
                substring = source.Substring(0, index + 1);
            }

            return substring;
        }

        /// <summary>
        /// Retrieves a substring from <paramref name="source"/>. The substring
        /// ends at the specified character position.
        /// </summary>
        /// <param name="source">The source <see cref="String"/>.</param>
        /// <param name="endingIndex">The index of the end of the substring.</param>
        /// <returns>
        /// A <see cref="String"/> object equivalent to the substring that ends
        /// at <paramref name="endingIndex"/> in <paramref name="source"/>, or
        /// the entire string if <paramref name="endingIndex"/> is not found in
        /// the string.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="endingIndex"/> is less than zero.
        /// </exception>
        public static string LeftSubstring(this string source, int endingIndex) => LeftSubstring(source, endingIndex, true);

        /// <summary>
        /// Retrieves a substring from <paramref name="source"/>. The substring
        /// ends at the specified character position.
        /// </summary>
        /// <param name="source">The source <see cref="String"/>.</param>
        /// <param name="endingIndex">The index of the end of the substring.</param>
        /// <param name="inclusive">
        /// Indicates if the substring should include the ending character position.
        /// </param>
        /// <returns>
        /// A <see cref="String"/> object equivalent to the substring that ends
        /// at <paramref name="endingIndex"/> in <paramref name="source"/>, or
        /// the entire string if <paramref name="endingIndex"/> is not found in
        /// the string.
        /// </returns>
        public static string LeftSubstring(this string source, int endingIndex, bool inclusive)
        {
            Requires.NotNull(source, nameof(source));
            Requires.Range(endingIndex > 0, nameof(endingIndex), Strings.ArgumentOutOfRange_IndexLessThanZero);
            Requires.Range(endingIndex < source.Length, nameof(endingIndex), Strings.ArgumentOutOfRange_IndexLessThanLength);

            if (inclusive)
            {
                endingIndex++;
            }

            return source.Substring(0, endingIndex);
        }

        /// <summary>
        /// Retrieves a substring from <paramref name="source"/>. The substring
        /// ends at the specified string position.
        /// </summary>
        /// <param name="source">The source <see cref="String"/>.</param>
        /// <param name="value">The ending string of a substring.</param>
        /// <returns>
        /// A <see cref="String"/> object equivalent to the substring that ends
        /// at the position of <paramref name="value"/> in
        /// <paramref name="source"/>, or the entire string if
        /// <paramref name="value"/> is not found in the string.
        /// </returns>
        public static string LeftSubstring(this string source, string value) => LeftSubstring(source, value, 1);

        /// <summary>
        /// Retrieves a substring from <paramref name="source"/>. The substring
        /// ends at the specified string position.
        /// </summary>
        /// <param name="source">The source <see cref="String"/>.</param>
        /// <param name="value">The ending string of a substring.</param>
        /// <param name="occurrence">The occurrence of <paramref name="value"/>.</param>
        /// <returns>
        /// A <see cref="String"/> object equivalent to the substring that ends
        /// at the position of <paramref name="value"/> in
        /// <paramref name="source"/>, or the entire string if
        /// <paramref name="value"/> is not found in the string.
        /// </returns>
        public static string LeftSubstring(this string source, string value, int occurrence) => LeftSubstring(source, value, occurrence, StringComparison.Ordinal);

        /// <summary>
        /// Retrieves a substring from <paramref name="source"/>. The substring
        /// ends at the specified string position.
        /// </summary>
        /// <param name="source">The source <see cref="String"/>.</param>
        /// <param name="value">The ending string of a substring.</param>
        /// <param name="occurrence">The occurrence of <paramref name="value"/>.</param>
        /// <param name="comparisonType">
        /// One of the <see cref="StringComparison"/> values.
        /// </param>
        /// <returns>
        /// A <see cref="String"/> object equivalent to the substring that ends
        /// at the position of <paramref name="value"/> in
        /// <paramref name="source"/>, or the entire string if
        /// <paramref name="value"/> is not found in the string.
        /// </returns>
        public static string LeftSubstring(this string source, string value, int occurrence, StringComparison comparisonType)
        {
            Requires.NotNull(source, nameof(source));
            Requires.NotNull(value, nameof(value));

            var substring = source;
            var index = source.IndexOf(value, comparisonType);

            while (index != -1 && occurrence > 1)
            {
                index = source.IndexOf(value, index + 1, comparisonType);
                --occurrence;
            }

            if (index != -1)
            {
                substring = source.Substring(0, index + 1);
            }

            return substring;
        }

        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether the
        /// length of <paramref name="source"/> is between the minimum and
        /// maximum indicated.
        /// </summary>
        /// <param name="source">Any string expression.</param>
        /// <param name="minimum">The minimum string length.</param>
        /// <param name="maximum">The maximum string length.</param>
        /// <returns>
        /// MinMax returns <see langword="true"/> if <paramref name="source"/>
        /// is greater than the minimum value but less than the maximum value;
        /// otherwise it returns <see langword="false"/>.
        /// </returns>
        public static bool LengthBetween(this string source, int minimum, int maximum) => LengthBetween(source, minimum, maximum, NumericComparisonOptions.IncludeBoth);

        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether the
        /// length of <paramref name="source"/> is between the minimum and
        /// maximum indicated.
        /// </summary>
        /// <param name="source">Any string expression.</param>
        /// <param name="minimum">The minimum string length.</param>
        /// <param name="maximum">The maximum string length.</param>
        /// <param name="options">
        /// A bitwise combination of enumeration values that defines whether the
        /// comparison is inclusive.
        /// </param>
        /// <returns>
        /// MinMax returns <see langword="true"/> if <paramref name="source"/>
        /// is greater than the minimum value but less than the maximum value;
        /// otherwise it returns <see langword="false"/>.
        /// </returns>
        public static bool LengthBetween(this string source, int minimum, int maximum, NumericComparisonOptions options)
        {
            Requires.NotNull(source, nameof(source));
            var length = source.Length;
            var success = options switch
            {
                NumericComparisonOptions.IncludeBoth => length >= minimum && length <= maximum,
                NumericComparisonOptions.IncludeMinimum => length >= minimum && length < maximum,
                NumericComparisonOptions.IncludeMaximum => length > minimum && length <= maximum,
                _ => length > minimum && length < maximum,
            };
            return success;
        }

        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether the
        /// length of <paramref name="source"/> is greater than the minimum indicated.
        /// </summary>
        /// <param name="source">The value to test.</param>
        /// <param name="minimum">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if the length of <paramref name="source"/> is
        /// greater than the minimum indicated; otherwise <see langword="false"/>.
        /// </returns>
        public static bool LengthGreaterThan(this string source, int minimum)
        {
            Requires.NotNull(source, nameof(source));

            return source.Length > minimum;
        }

        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether the
        /// length of <paramref name="source"/> is greater than or equal to the
        /// minimum indicated.
        /// </summary>
        /// <param name="source">The value to test.</param>
        /// <param name="minimum">The minimum value to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if the length of <paramref name="source"/> is
        /// greater than or equal to the minimum indicated; otherwise <see langword="false"/>.
        /// </returns>
        public static bool LengthGreaterThanOrEqualTo(this string source, int minimum)
        {
            Requires.NotNull(source, nameof(source));

            return source.Length >= minimum;
        }

        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether the
        /// length of <paramref name="source"/> is less than the minimum indicated.
        /// </summary>
        /// <param name="source">The value to test.</param>
        /// <param name="maximum">The maximum value to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if the length of <paramref name="source"/> is
        /// less than the minimum indicated; otherwise <see langword="false"/>.
        /// </returns>
        public static bool LengthLessThan(this string source, int maximum)
        {
            Requires.NotNull(source, nameof(source));

            return source.Length < maximum;
        }

        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether the
        /// length of <paramref name="source"/> is less than or equal to the
        /// minimum indicated.
        /// </summary>
        /// <param name="source">The value to test.</param>
        /// <param name="maximum">The maximum value to compare against.</param>
        /// <returns>
        /// <see langword="true"/> if the length of <paramref name="source"/> is
        /// less than or equal to the minimum indicated; otherwise <see langword="false"/>.
        /// </returns>
        public static bool LengthLessThanOrEqualTo(this string source, int maximum)
        {
            Requires.NotNull(source, nameof(source));

            return source.Length <= maximum;
        }

        /// <summary>
        /// Returns <see langword="null"/> if the specified string is already
        /// <see langword="null"/> or <see cref="String.Empty"/>. This is useful
        /// <see href="https://docs.microsoft.com/dotnet/csharp/language-reference/operators/null-coalescing-operator">Null
        /// coalescing operator</see>
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <returns>
        /// <see langword="null"/> if <paramref name="value"/> is
        /// <see langword="null"/> or <see cref="String.Empty"/>; otherwise, <paramref name="value"/>.
        /// </returns>
        public static string? NullIfEmpty(this string? value) => String.IsNullOrEmpty(value) ? null : value;

        /// <summary>
        /// Returns <see langword="null"/> if the specified string is already
        /// <see langword="null"/>, <see cref="String.Empty"/>, or consists only
        /// of white-space characters. This is useful to use with the
        /// <see href="https://docs.microsoft.com/dotnet/csharp/language-reference/operators/null-coalescing-operator">Null
        /// coalescing operator</see>
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <returns>
        /// <see langword="null"/> if <paramref name="value"/> is
        /// <see langword="null"/>, <see cref="String.Empty"/>, or consists only
        /// of white-space characters; otherwise, <paramref name="value"/>.
        /// </returns>
        public static string? NullIfWhiteSpace(this string value) => String.IsNullOrWhiteSpace(value) ? null : value;

        /// <summary>
        /// Returns the number of times <paramref name="value"/> appears in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The source <see cref="String"/>.</param>
        /// <param name="value">The character to count.</param>
        /// <returns>
        /// The number of times <paramref name="value"/> appears in
        /// <paramref name="source"/> or 0 if <paramref name="value"/> is not
        /// found in the string.
        /// </returns>
        public static int OccurrencesOf(this string source, char value)
        {
            Requires.NotNull(source, nameof(source));

            var count = 0;
            foreach (var c in source)
            {
                if (c == value)
                {
                    ++count;
                }
            }

            return count;
        }

        /// <summary>
        /// Returns the number of times <paramref name="value"/> appears in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The source <see cref="String"/>.</param>
        /// <param name="value">The string to count.</param>
        /// <returns>
        /// The number of times <paramref name="value"/> appears in
        /// <paramref name="source"/> or 0 if <paramref name="value"/> is not
        /// found in the string.
        /// </returns>
        public static int OccurrencesOf(this string source, string value)
        {
            return OccurrencesOf(source, value, StringComparison.Ordinal);
        }

        /// <summary>
        /// Returns the number of times <paramref name="value"/> appears in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The source <see cref="String"/>.</param>
        /// <param name="value">The string to count.</param>
        /// <param name="comparisonType">
        /// One of the <see cref="StringComparison"/> values.
        /// </param>
        /// <returns>
        /// The number of times <paramref name="value"/> appears in
        /// <paramref name="source"/> or 0 if <paramref name="value"/> is not
        /// found in the string.
        /// </returns>
        public static int OccurrencesOf(this string source, string value, StringComparison comparisonType)
        {
            Requires.NotNull(source, nameof(source));
            Requires.NotNull(value, nameof(value));

            var count = 0;
            var index = source.IndexOf(value, comparisonType);

            if (index != -1)
            {
                count++;

                while (index >= 0)
                {
                    index = source.IndexOf(value, index + 1, comparisonType);
                    if (index != -1)
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        /// <summary>
        /// Returns a new string whose textual value is
        /// <paramref name="source"/> with all whitespace characters removed.
        /// </summary>
        /// <param name="source">
        /// The <see cref="String"/> from which whitespace characters will be removed.
        /// </param>
        /// <returns>
        /// A new string representing <paramref name="source"/> with all of the
        /// whitespace characters removed.
        /// </returns>
        public static string RemoveWhiteSpace(this string source)
        {
            Requires.NotNull(source, nameof(source));
            var position = 0;
            var length = source.Length;
            var buffer = new char[length];
            char current;
            for (var i = 0; i < length; i++)
            {
                current = source[i];
                if (!Char.IsWhiteSpace(current))
                {
                    buffer[position++] = current;
                }
            }

            return new string(buffer, 0, position);
        }

        /// <summary>
        /// Returns a new string where <paramref name="oldValue"/> has been
        /// replaced by <paramref name="newValue"/>.
        /// </summary>
        /// <param name="source">The source <see cref="String"/>.</param>
        /// <param name="oldValue">The character to replace.</param>
        /// <param name="newValue">The replacement character.</param>
        /// <param name="occurrences">
        /// The occurrences of <paramref name="oldValue"/> to replace.
        /// </param>
        /// <returns>
        /// A new string where <paramref name="oldValue"/> has been replaced by <paramref name="newValue"/>.
        /// </returns>
        public static string ReplaceOccurrences(this string source, char oldValue, char newValue, int occurrences)
        {
            Requires.NotNull(source, nameof(source));

            var count = 0;
            var index = 0;
            var newString = source.ToCharArray();

            while (index < (newString.Length - 1) && count < occurrences)
            {
                if (newString[index] == oldValue)
                {
                    newString[index] = newValue;
                    count++;
                }

                index++;
            }

            return new string(newString);
        }

        /// <summary>
        /// Returns a new string where <paramref name="oldValue"/> has been
        /// replaced by <paramref name="newValue"/>.
        /// </summary>
        /// <param name="source">The source <see cref="String"/>.</param>
        /// <param name="oldValue">The character to replace.</param>
        /// <param name="newValue">The replacement character.</param>
        /// <param name="occurrences">
        /// The occurrences of <paramref name="oldValue"/> to replace.
        /// </param>
        /// <returns>
        /// A new string where <paramref name="oldValue"/> has been replaced by <paramref name="newValue"/>.
        /// </returns>
        public static string ReplaceOccurrences(this string source, string oldValue, string newValue, int occurrences) => ReplaceOccurrences(source, oldValue, newValue, occurrences, StringComparison.Ordinal);

        /// <summary>
        /// Returns a new string where <paramref name="oldValue"/> has been
        /// replaced by <paramref name="newValue"/>.
        /// </summary>
        /// <param name="source">The source <see cref="String"/>.</param>
        /// <param name="oldValue">The character to replace.</param>
        /// <param name="newValue">The replacement character.</param>
        /// <param name="occurrences">
        /// The occurrences of <paramref name="oldValue"/> to replace.
        /// </param>
        /// <param name="comparisonType">
        /// One of the <see cref="StringComparison"/> values.
        /// </param>
        /// <returns>
        /// A new string where <paramref name="oldValue"/> has been replaced by <paramref name="newValue"/>.
        /// </returns>
        public static string ReplaceOccurrences(this string source, string oldValue, string newValue, int occurrences, StringComparison comparisonType)
        {
            Requires.NotNull(source, nameof(source));
            Requires.NotNull(oldValue, nameof(oldValue));
            Requires.NotNull(newValue, nameof(newValue));

            var newString = source;
            var index = source.IndexOf(oldValue, comparisonType);
            var length = oldValue.Length;
            var count = 0;

            if (index != -1 && occurrences > 0)
            {
                var builder = new StringBuilder(source.Length);

                // we found an occurrence, so replace it.
                builder.Append(source.Substring(0, index));
                builder.Append(newValue);

                count++;
                var lastIndex = index;
                var startingIndex = lastIndex + length;

                while (index >= 0 && count < occurrences)
                {
                    index = source.IndexOf(oldValue, index + 1, comparisonType);
                    lastIndex = index;
                    if (index != -1)
                    {
                        builder.Append(source.Substring(startingIndex, index - startingIndex));
                        builder.Append(newValue);
                        startingIndex = lastIndex + length;
                        count++;
                    }
                }

                builder.Append(source.Substring(startingIndex));
                newString = builder.ToString();
            }

            return newString;
        }

        /// <summary>
        /// Returns a new string where the text between <paramref name="start"/>
        /// and <parameref name="end"/> has been replaced by <paramref name="newValue"/>.
        /// </summary>
        /// <param name="source">The source <see cref="String"/>.</param>
        /// <param name="start">The starting character of the replacement substring.</param>
        /// <param name="end">The ending character of the replacement substring.</param>
        /// <param name="newValue">The replacement text.</param>
        /// <returns>
        /// A new string where the text between <paramref name="start"/> and
        /// <parameref name="end"/> has been replaced by <paramref name="newValue"/>.
        /// </returns>
        public static string ReplaceBetween(this string source, char start, char end, string newValue) => ReplaceBetween(source, start, end, newValue, false);

        /// <summary>
        /// Returns a new string where the text between <paramref name="start"/>
        /// and <parameref name="end"/> has been replaced by <paramref name="newValue"/>.
        /// </summary>
        /// <param name="source">The source <see cref="String"/>.</param>
        /// <param name="start">The starting character of the replacement substring.</param>
        /// <param name="end">The ending character of the replacement substring.</param>
        /// <param name="newValue">The replacement text.</param>
        /// <param name="inclusive">
        /// Indicates if the substring should include the start and end characters.
        /// </param>
        /// <returns>
        /// A new string where the text between <paramref name="start"/> and
        /// <parameref name="end"/> has been replaced by <paramref name="newValue"/>.
        /// </returns>
        public static string ReplaceBetween(this string source, char start, char end, string newValue, bool inclusive)
        {
            Requires.NotNull(source, nameof(source));
            Requires.NotNull(newValue, nameof(newValue));

            var newString = source;
            var startIndex = source.IndexOf(start);

            if (startIndex != -1)
            {
                var endIndex = source.IndexOf(end, startIndex + 1);

                if (endIndex != -1)
                {
                    if (inclusive)
                    {
                        // We need to offset the endIndex by 1 to include the
                        // bracketing character.
                        endIndex++;
                    }
                    else
                    {
                        // We need to offset the startIndex by 1 to exclude the
                        // bracketing character.
                        startIndex++;
                    }

                    newString = String.Concat(source.Substring(0, startIndex), newValue, source.Substring(endIndex));
                }
            }

            return newString;
        }

        /// <summary>
        /// Returns a new string where the text between <paramref name="start"/>
        /// and <parameref name="end"/> has been replaced by <paramref name="newValue"/>.
        /// </summary>
        /// <param name="source">The source <see cref="String"/>.</param>
        /// <param name="start">The starting index of the replacement substring.</param>
        /// <param name="end">The ending index of the replacement substring.</param>
        /// <param name="newValue">The replacement text.</param>
        /// <returns>
        /// A new string where the text between <paramref name="start"/> and
        /// <parameref name="end"/> has been replaced by <paramref name="newValue"/>.
        /// </returns>
        public static string ReplaceBetween(this string source, int start, int end, string newValue) => ReplaceBetween(source, start, end, newValue, false);

        /// <summary>
        /// Returns a new string where the text between <paramref name="start"/>
        /// and <parameref name="end"/> has been replaced by <paramref name="newValue"/>.
        /// </summary>
        /// <param name="source">The source <see cref="String"/>.</param>
        /// <param name="start">The starting index of the replacement substring.</param>
        /// <param name="end">The ending index of the replacement substring.</param>
        /// <param name="newValue">The replacement text.</param>
        /// <param name="inclusive">
        /// Indicates if the substring should include the start and end indices.
        /// </param>
        /// <returns>
        /// A new string where the text between <paramref name="start"/> and
        /// <parameref name="end"/> has been replaced by <paramref name="newValue"/>.
        /// </returns>
        public static string ReplaceBetween(this string source, int start, int end, string newValue, bool inclusive)
        {
            Requires.NotNull(source, nameof(source));
            Requires.NotNull(newValue, nameof(newValue));
            Requires.Range(start > 0, nameof(start), Strings.ArgumentOutOfRange_IndexLessThanZero);
            Requires.Range(start < source.Length, nameof(start), Strings.ArgumentOutOfRange_IndexLessThanLength);
            Requires.Range(end > 0, nameof(end), Strings.ArgumentOutOfRange_IndexLessThanZero);
            Requires.Range(end < source.Length, nameof(end), Strings.ArgumentOutOfRange_IndexLessThanLength);
            Requires.Range(start < end, nameof(start), Strings.Argument_StartIndexGreaterThanEndIndexString);
            Requires.Range(start != end, nameof(start), Strings.Argument_InvalidIndexValuesString);

            if (inclusive)
            {
                // We need to offset the ending index by 1 to include the
                // bracketing character.
                end++;
            }
            else
            {
                // We need to offset the starting index by 1 to exclude the
                // bracketing character.
                start++;
            }

            var newString = String.Concat(source.Substring(0, start), newValue, source.Substring(end));
            return newString;
        }

        /// <summary>
        /// Returns a new string where the text between <paramref name="start"/>
        /// and <parameref name="end"/> has been replaced by <paramref name="newValue"/>.
        /// </summary>
        /// <param name="source">The source <see cref="String"/>.</param>
        /// <param name="start">The starting string of the replacement substring.</param>
        /// <param name="end">The ending string of the replacement substring.</param>
        /// <param name="newValue">The replacement text.</param>
        /// <returns>
        /// A new string where the text between <paramref name="start"/> and
        /// <parameref name="end"/> has been replaced by <paramref name="newValue"/>.
        /// </returns>
        public static string ReplaceBetween(this string source, string start, string end, string newValue) => ReplaceBetween(source, start, end, newValue, false, StringComparison.Ordinal);

        /// <summary>
        /// Returns a new string where the text between <paramref name="start"/>
        /// and <parameref name="end"/> has been replaced by <paramref name="newValue"/>.
        /// </summary>
        /// <param name="source">The source <see cref="String"/>.</param>
        /// <param name="start">The starting string of the replacement substring.</param>
        /// <param name="end">The ending string of the replacement substring.</param>
        /// <param name="newValue">The replacement text.</param>
        /// <param name="inclusive">
        /// Indicates if the substring should include the start and end strings.
        /// </param>
        /// <returns>
        /// A new string where the text between <paramref name="start"/> and
        /// <parameref name="end"/> has been replaced by <paramref name="newValue"/>.
        /// </returns>
        public static string ReplaceBetween(this string source, string start, string end, string newValue, bool inclusive) => ReplaceBetween(source, start, end, newValue, inclusive, StringComparison.Ordinal);

        /// <summary>
        /// Returns a new string where the text between <paramref name="start"/>
        /// and <parameref name="end"/> has been replaced by <paramref name="newValue"/>.
        /// </summary>
        /// <param name="source">The source <see cref="String"/>.</param>
        /// <param name="start">The starting string of the replacement substring.</param>
        /// <param name="end">The ending string of the replacement substring.</param>
        /// <param name="newValue">The replacement text.</param>
        /// <param name="inclusive">
        /// Indicates if the substring should include the start and end strings.
        /// </param>
        /// <param name="comparisonType">
        /// One of the <see cref="StringComparison"/> values.
        /// </param>
        /// <returns>
        /// A new string where the text between <paramref name="start"/> and
        /// <parameref name="end"/> has been replaced by <paramref name="newValue"/>.
        /// </returns>
        public static string ReplaceBetween(this string source, string start, string end, string newValue, bool inclusive, StringComparison comparisonType)
        {
            Requires.NotNull(source, nameof(source));
            Requires.NotNull(start, nameof(start));
            Requires.NotNull(end, nameof(end));
            Requires.NotNull(newValue, nameof(newValue));

            var newString = source;
            var startIndex = source.IndexOf(start, comparisonType);

            if (startIndex != -1)
            {
                var endIndex = source.IndexOf(end, startIndex + 1, comparisonType);

                if (endIndex != -1)
                {
                    if (inclusive)
                    {
                        // We need to offset the endIndex to include the
                        // bracketing word.
                        endIndex += end.Length;
                    }
                    else
                    {
                        // We need to offset the starting index to exclude the
                        // bracketing word.
                        startIndex += start.Length;
                    }

                    newString = String.Concat(source.Substring(0, startIndex), newValue, source.Substring(endIndex));
                }
            }

            return newString;
        }

        /// <summary>
        /// Returns a new string whose textual value is the resized form of <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The <see cref="String"/> to resize.</param>
        /// <param name="length">The desired length of the new <see cref="String"/>.</param>
        /// <returns>A new, resized string.</returns>
        /// <remarks>
        /// <para>
        /// If <paramref name="source"/> is less than <paramref name="length"/>,
        /// the returned string is padded with spaces; otherwise it is truncated
        /// to the desired length.
        /// </para>
        /// <para>
        /// If <paramref name="source"/> is <see langword="null"/> or is an
        /// empty string, a new string containing <paramref name="length"/>
        /// number of spaces is returned.
        /// </para>
        /// </remarks>
        public static string ResizeString(this string? source, int length)
        {
            var sizedString = String.Empty;

            if (!String.IsNullOrEmpty(source))
            {
                if (source!.Length > length)
                {
                    sizedString = source.Substring(0, length);
                }
                else
                {
                    sizedString = source.PadRight(length);
                }
            }
            else
            {
                sizedString = sizedString.PadRight(length, ' ');
            }

            return sizedString;
        }

        /// <summary>
        /// Retrieves a substring from <paramref name="source"/>. The substring
        /// ends at the specified character position from the end of the string.
        /// </summary>
        /// <param name="source">The source <see cref="String"/>.</param>
        /// <param name="value">The ending character of a substring.</param>
        /// <returns>
        /// A <see cref="String"/> object equivalent to the substring that ends
        /// at the position of <paramref name="value"/> in
        /// <paramref name="source"/>, or the entire string if
        /// <paramref name="value"/> is not found in the string.
        /// </returns>
        public static string RightSubstring(this string source, char value) => RightSubstring(source, value, 1);

        /// <summary>
        /// Retrieves a substring from <paramref name="source"/>. The substring
        /// ends at the specified string position from the end of the string.
        /// </summary>
        /// <param name="source">The source <see cref="String"/>.</param>
        /// <param name="value">The ending string of a substring.</param>
        /// <param name="occurrence">The occurrence of <paramref name="value"/>.</param>
        /// <returns>
        /// A <see cref="String"/> object equivalent to the substring that ends
        /// at the position of <paramref name="value"/> in
        /// <paramref name="source"/>, or the entire string if
        /// <paramref name="value"/> is not found in the string.
        /// </returns>
        public static string RightSubstring(this string source, char value, int occurrence)
        {
            Requires.NotNull(source, nameof(source));

            var substring = source;
            var index = source.IndexOf(value);

            while (index != -1 && occurrence > 1)
            {
                index = source.IndexOf(value, index + 1);
                --occurrence;
            }

            if (index != -1)
            {
                substring = source.Substring(index + 1);
            }

            return substring;
        }

        /// <summary>
        /// Retrieves a substring from <paramref name="source"/>. The substring
        /// ends at the specified character position from the end of the string.
        /// </summary>
        /// <param name="source">The source <see cref="String"/>.</param>
        /// <param name="endingIndex">The index of the end of the substring.</param>
        /// <returns>
        /// A <see cref="String"/> object equivalent to the substring that ends
        /// at <paramref name="endingIndex"/> in <paramref name="source"/>, or
        /// the entire string if <paramref name="endingIndex"/> is not found in
        /// the string.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="endingIndex"/> is less than zero.
        /// </exception>
        public static string RightSubstring(this string source, int endingIndex) => RightSubstring(source, endingIndex, true);

        /// <summary>
        /// Retrieves a substring from <paramref name="source"/>. The substring
        /// ends at the specified character position from the end of the string.
        /// </summary>
        /// <param name="source">The source <see cref="String"/>.</param>
        /// <param name="endingIndex">The index of the end of the substring.</param>
        /// <param name="inclusive">
        /// Indicates if the substring should include the ending character position.
        /// </param>
        /// <returns>
        /// A <see cref="String"/> object equivalent to the substring that ends
        /// at <paramref name="endingIndex"/> in <paramref name="source"/>, or
        /// the entire string if <paramref name="endingIndex"/> is not found in
        /// the string.
        /// </returns>
        public static string RightSubstring(this string source, int endingIndex, bool inclusive)
        {
            Requires.NotNull(source, nameof(source));
            Requires.Range(endingIndex > 0, nameof(endingIndex), Strings.ArgumentOutOfRange_IndexLessThanZero);
            Requires.Range(endingIndex < source.Length, nameof(endingIndex), Strings.ArgumentOutOfRange_IndexLessThanLength);

            if (inclusive)
            {
                endingIndex--;
            }

            return source.Substring(endingIndex, source.Length - endingIndex);
        }

        /// <summary>
        /// Retrieves a substring from <paramref name="source"/>. The substring
        /// ends at the specified string position.
        /// </summary>
        /// <param name="source">The source <see cref="String"/>.</param>
        /// <param name="value">The ending string of a substring.</param>
        /// <returns>
        /// A <see cref="String"/> object equivalent to the substring that ends
        /// at the position of <paramref name="value"/> in
        /// <paramref name="source"/>, or the entire string if
        /// <paramref name="value"/> is not found in the string.
        /// </returns>
        public static string RightSubstring(this string source, string value) => RightSubstring(source, value, 1);

        /// <summary>
        /// Retrieves a substring from <paramref name="source"/>. The substring
        /// ends at the specified string position from the end of the string.
        /// </summary>
        /// <param name="source">The source <see cref="String"/>.</param>
        /// <param name="value">The ending string of a substring.</param>
        /// <param name="occurrence">The occurrence of <paramref name="value"/>.</param>
        /// <returns>
        /// A <see cref="String"/> object equivalent to the substring that ends
        /// at the position of <paramref name="value"/> in
        /// <paramref name="source"/>, or the entire string if
        /// <paramref name="value"/> is not found in the string.
        /// </returns>
        public static string RightSubstring(this string source, string value, int occurrence) => RightSubstring(source, value, occurrence, StringComparison.Ordinal);

        /// <summary>
        /// Retrieves a substring from <paramref name="source"/>. The substring
        /// ends at the specified string position from the end of the string.
        /// </summary>
        /// <param name="source">The source <see cref="String"/>.</param>
        /// <param name="value">The ending string of a substring.</param>
        /// <param name="occurrence">The occurrence of <paramref name="value"/>.</param>
        /// <param name="comparisonType">
        /// One of the <see cref="StringComparison"/> values.
        /// </param>
        /// <returns>
        /// A <see cref="String"/> object equivalent to the substring that ends
        /// at the position of <paramref name="value"/> in
        /// <paramref name="source"/>, or the entire string if
        /// <paramref name="value"/> is not found in the string.
        /// </returns>
        public static string RightSubstring(this string source, string value, int occurrence, StringComparison comparisonType)
        {
            Requires.NotNull(source, nameof(source));
            Requires.NotNull(value, nameof(value));

            var substring = source;
            var index = source.IndexOf(value, comparisonType);

            while (index != -1 && occurrence > 1)
            {
                index = source.IndexOf(value, index + 1, comparisonType);
                --occurrence;
            }

            if (index != -1)
            {
                substring = source.Substring(index + 1);
            }

            return substring;
        }

        /// <summary>
        /// Determines whether the start of this string instance matches any of
        /// the specified strings.
        /// </summary>
        /// <param name="source">The source <see cref="String"/>.</param>
        /// <param name="values">A collection of string instances.</param>
        /// <returns>
        /// <see langword="true"/> if the start of this string instance matches
        /// any of the specified strings; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool StartsWithAny(this string source, IEnumerable<string> values) => source.StartsWithAny(values, StringComparison.CurrentCulture);

        /// <summary>
        /// Determines whether the start of this string instance matches any of
        /// the specified strings.
        /// </summary>
        /// <param name="source">The source <see cref="String"/>.</param>
        /// <param name="values">A collection of string instances.</param>
        /// <param name="comparisonType">
        /// One of the enumeration values that specifies the rules for the comparison.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the start of this string instance matches
        /// any of the specified strings; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool StartsWithAny(this string source, IEnumerable<string> values, StringComparison comparisonType)
        {
            Requires.NotNull(source, nameof(source));
            Requires.NotNullOrEmpty(values, nameof(values));

            foreach (var value in values)
            {
                if (source.StartsWith(value, comparisonType))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Retrieves a substring from <paramref name="source"/>. The substring
        /// begins at <paramref name="start"/> and ends at <paramref name="end"/>.
        /// </summary>
        /// <param name="source">The source <see cref="String"/>.</param>
        /// <param name="start">The starting character of the substring.</param>
        /// <param name="end">The ending character of the substring.</param>
        /// <returns>
        /// A <see cref="String"/> object equivalent to the substring that ends
        /// at the position of <paramref name="end"/> in
        /// <paramref name="source"/>, or <see cref="String.Empty"/> if
        /// <paramref name="start"/> or <paramref name="end"/> are not found in
        /// the string.
        /// </returns>
        public static string SubstringBetween(this string source, char start, char end) => SubstringBetween(source, start, end, false);

        /// <summary>
        /// Retrieves a substring from <paramref name="source"/>. The substring
        /// begins at <paramref name="start"/> and ends at <paramref name="end"/>.
        /// </summary>
        /// <param name="source">The source <see cref="String"/>.</param>
        /// <param name="start">The starting character of the substring.</param>
        /// <param name="end">The ending character of the substring.</param>
        /// <param name="inclusive">
        /// Indicates if the substring should include the start and end characters.
        /// </param>
        /// <returns>
        /// A <see cref="String"/> object equivalent to the substring that ends
        /// at the position of <paramref name="end"/> in
        /// <paramref name="source"/>, or <see cref="String.Empty"/> if
        /// <paramref name="start"/> or <paramref name="end"/> are not found in
        /// the string.
        /// </returns>
        public static string SubstringBetween(this string source, char start, char end, bool inclusive)
        {
            Requires.NotNull(source, nameof(source));

            var substring = String.Empty;
            var startIndex = source.IndexOf(start);

            if (startIndex != -1)
            {
                var endIndex = source.IndexOf(end, startIndex + 1);

                if (endIndex != -1)
                {
                    if (inclusive)
                    {
                        // We need to offset the endIndex by 1 to include the
                        // bracketing character
                        endIndex++;
                    }
                    else
                    {
                        // We need to skip over the first character.
                        startIndex++;
                    }

                    substring = source.Substring(startIndex, endIndex - startIndex);
                }
            }

            return substring;
        }

        /// <summary>
        /// Retrieves a substring from <paramref name="source"/>. The substring
        /// begins at <paramref name="start"/> and ends at <paramref name="end"/>.
        /// </summary>
        /// <param name="source">The source <see cref="String"/>.</param>
        /// <param name="start">The starting string of the substring.</param>
        /// <param name="end">The ending string of the substring.</param>
        /// <returns>
        /// A <see cref="String"/> object equivalent to the substring that ends
        /// at the position of <paramref name="end"/> in
        /// <paramref name="source"/>, or <see cref="String.Empty"/> if
        /// <paramref name="start"/> or <paramref name="end"/> are not found in
        /// the string.
        /// </returns>
        public static string SubstringBetween(this string source, string start, string end) => SubstringBetween(source, start, end, false, StringComparison.Ordinal);

        /// <summary>
        /// Retrieves a substring from <paramref name="source"/>. The substring
        /// begins at <paramref name="start"/> and ends at <paramref name="end"/>.
        /// </summary>
        /// <param name="source">The source <see cref="String"/>.</param>
        /// <param name="start">The starting string of the substring.</param>
        /// <param name="end">The ending string of the substring.</param>
        /// <param name="inclusive">
        /// Indicates if the substring should include the start and end strings.
        /// </param>
        /// <returns>
        /// A <see cref="String"/> object equivalent to the substring that ends
        /// at the position of <paramref name="end"/> in
        /// <paramref name="source"/>, or <see cref="String.Empty"/> if
        /// <paramref name="start"/> or <paramref name="end"/> are not found in
        /// the string.
        /// </returns>
        public static string SubstringBetween(this string source, string start, string end, bool inclusive) => SubstringBetween(source, start, end, inclusive, StringComparison.Ordinal);

        /// <summary>
        /// Retrieves a substring from <paramref name="source"/>. The substring
        /// begins at <paramref name="start"/> and ends at <paramref name="end"/>.
        /// </summary>
        /// <param name="source">The source <see cref="String"/>.</param>
        /// <param name="start">The starting string of the substring.</param>
        /// <param name="end">The ending string of the substring.</param>
        /// <param name="inclusive">
        /// Indicates if the substring should include the start and end strings.
        /// </param>
        /// <param name="comparisonType">
        /// One of the <see cref="StringComparison"/> values.
        /// </param>
        /// <returns>
        /// A <see cref="String"/> object equivalent to the substring that ends
        /// at the position of <paramref name="end"/> in
        /// <paramref name="source"/>, or <see cref="String.Empty"/> if
        /// <paramref name="start"/> or <paramref name="end"/> are not found in
        /// the string.
        /// </returns>
        public static string SubstringBetween(this string source, string start, string end, bool inclusive, StringComparison comparisonType)
        {
            Requires.NotNull(source, nameof(source));
            Requires.NotNull(start, nameof(start));
            Requires.NotNull(end, nameof(end));

            var substring = String.Empty;
            var startIndex = source.IndexOf(start, comparisonType);

            if (startIndex != -1)
            {
                var endIndex = source.IndexOf(end, startIndex + 1, comparisonType);

                if (endIndex != -1)
                {
                    if (inclusive)
                    {
                        // We need to offset the endIndex to include the
                        // bracketing word.
                        endIndex += end.Length;
                    }
                    else
                    {
                        // We need to offset the starting index to exclude the
                        // bracketing word. The endIndex already excludes the
                        // bracketing word.
                        startIndex += start.Length;
                    }

                    substring = source.Substring(startIndex, endIndex - startIndex);
                }
            }

            return substring;
        }

        /// <summary>
        /// Returns a new string whose textual value is
        /// <paramref name="source"/> which has been truncated at <paramref name="length"/>.
        /// </summary>
        /// <param name="source">The source <see cref="String"/>.</param>
        /// <param name="length">
        /// The maximum number of characters to be included in the new <see cref="String"/>.
        /// </param>
        /// <returns>
        /// If <paramref name="source"/> is greater than
        /// <paramref name="length"/>, a new string representing
        /// <paramref name="source"/> which has been truncated at
        /// <paramref name="length"/>; otherwise, the original value.
        /// </returns>
        public static string? Truncate(this string? source, int length)
        {
            if (!String.IsNullOrEmpty(source) && source!.Length > length)
            {
                return source.Substring(0, length);
            }

            return source;
        }

        internal static string TrimWhiteSpaceAndNull(this string source)
        {
            var num = 0;
            var length = source.Length - 1;
            var chr = '\0';
            while (true)
            {
                if (num < source.Length)
                {
                    if (!Char.IsWhiteSpace(source[num]) && source[num] != chr)
                    {
                        break;
                    }

                    num++;
                }
                else
                {
                    break;
                }
            }

            while (length >= num && (Char.IsWhiteSpace(source[length]) || source[length] == chr))
            {
                length--;
            }

            return source.Substring(num, length - num + 1);
        }
    }
}