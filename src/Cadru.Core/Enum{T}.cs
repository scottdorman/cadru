//------------------------------------------------------------------------------
// <copyright file="Enum{T}.cs"
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
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

using Cadru.Extensions;

namespace Cadru
{
    /// <summary>
    /// Provides a class for working with enumerations.
    /// </summary>
    /// <typeparam name="TEnum">The enumeration type.</typeparam>
    [SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix", Justification = "Reviewed.")]
    [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", Justification = "Reviewed.")]
    public static class Enum<TEnum> where TEnum : struct
    {
        /// <summary>
        /// Retrieves the description of the constant in the enumeration that
        /// has the specified value.
        /// </summary>
        /// <param name="value">
        /// The value of a particular enumerated constant in terms of its
        /// underlying type.
        /// </param>
        /// <returns>
        /// A string containing the description of the enumerated constant in
        /// <typeparamref name="TEnum"/> whose value is
        /// <paramref name="value"/>; or <see langword="null"/> if no such
        /// constant is found.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="value"/> is <see langword="null"/>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <p><typeparamref name="TEnum"/> is not an <see cref="Enum"/></p>
        /// <p>-or</p>
        /// <p>
        /// <paramref name="value"/> is neither of type
        /// <typeparamref name="TEnum"/> nor does it have the same underlying
        /// type as <typeparamref name="TEnum"/>.
        /// </p>
        /// </exception>
        [SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "Reviewed.")]
        public static string? GetDescription(TEnum value)
        {
            return GetDescription(value, useNameAsFallback: true);
        }

        /// <summary>
        /// Retrieves the description of the constant in the enumeration that
        /// has the specified value.
        /// </summary>
        /// <param name="value">
        /// The value of a particular enumerated constant in terms of its
        /// underlying type.
        /// </param>
        /// <param name="useNameAsFallback">
        /// If <see langword="true"/>, the name of the enumerated constant is
        /// used if no description is found; otherwise, <see langword="null"/>.
        /// </param>
        /// <returns>
        /// A string containing the description of the enumerated constant in
        /// <typeparamref name="TEnum"/> whose value is
        /// <paramref name="value"/>; or <see langword="null"/> if no such
        /// constant is found and <paramref name="useNameAsFallback"/> is
        /// <see langword="false"/>; otherwise, the name of the enumerated constant.
        /// </returns>
        /// <remarks>
        /// This method will use the value from an
        /// <see cref="EnumDescriptionAttribute"/>,
        /// <see cref="DisplayAttribute"/>, or
        /// <see cref="DescriptionAttribute"/> if found, in that respective
        /// order. If none of those attributes are found, or the value is
        /// <see langword="null"/>, and <paramref name="useNameAsFallback"/> is
        /// <see langword="true"/>, then the name of the enumerated constant is
        /// used; otherwise, a <see langword="null"/> is used.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="value"/> is <see langword="null"/>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <p><typeparamref name="TEnum"/> is not an <see cref="Enum"/></p>
        /// <p>-or</p>
        /// <p>
        /// <paramref name="value"/> is neither of type
        /// <typeparamref name="TEnum"/> nor does it have the same underlying
        /// type as <typeparamref name="TEnum"/>.
        /// </p>
        /// </exception>
        [SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "Reviewed.")]
        public static string? GetDescription(TEnum value, bool useNameAsFallback)
        {
            var fieldInfo = value.GetType().GetTypeInfo().GetDeclaredField(value.ToString());
            return fieldInfo.GetDescription(useNameAsFallback);
        }

        /// <summary>
        /// Retrieves an array of the descriptions of the constants in the enumeration.
        /// </summary>
        /// <returns>A collection of the names of the constants in <typeparamref name="TEnum"/>.</returns>
        /// <exception cref="ArgumentException">
        /// <typeparamref name="TEnum"/> is not an <see cref="Enum"/>.
        /// </exception>
        [SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "Reviewed.")]
        public static IEnumerable<string?> GetDescriptions()
        {
            return GetDescriptions(useNameAsFallback: true);
        }

        /// <summary>
        /// Retrieves an array of the descriptions of the constants in the enumeration.
        /// </summary>
        /// <returns>A collection of the names of the constants in <typeparamref name="TEnum"/>.</returns>
        /// <exception cref="ArgumentException">
        /// <typeparamref name="TEnum"/> is not an <see cref="Enum"/>.
        /// </exception>
        /// <remarks>
        /// This method will use the value from an
        /// <see cref="EnumDescriptionAttribute"/>,
        /// <see cref="DisplayAttribute"/>, or
        /// <see cref="DescriptionAttribute"/> if found, in that respective
        /// order. If none of those attributes are found, or the value is
        /// <see langword="null"/>, and <paramref name="useNameAsFallback"/> is
        /// <see langword="true"/>, then the name of the enumerated constant is
        /// used; otherwise, a <see langword="null"/> is used.
        /// </remarks>
        [SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "Reviewed.")]
        public static IEnumerable<string?> GetDescriptions(bool useNameAsFallback)
        {
            var type = typeof(TEnum).GetTypeInfo();

            Contracts.Requires.IsTrue(type.IsEnum);
            var fields = type.DeclaredFields.Where(f => !f.IsSpecialName);
            var descriptions = new List<string?>();

            foreach (var field in fields)
            {
                descriptions.Add(field.GetDescription(useNameAsFallback));
            }

            return descriptions;
        }

        /// <summary>
        /// Retrieves the name of the constant in the enumeration that has the
        /// specified value.
        /// </summary>
        /// <param name="value">
        /// The value of a particular enumerated constant in terms of its
        /// underlying type.
        /// </param>
        /// <returns>
        /// A string containing the name of the enumerated constant in
        /// <typeparamref name="TEnum"/> whose value is
        /// <paramref name="value"/>; or <see langword="null"/> if no such
        /// constant is found.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="value"/> is <see langword="null"/>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <p><typeparamref name="TEnum"/> is not an <see cref="Enum"/></p>
        /// <p>-or</p>
        /// <p>
        /// <paramref name="value"/> is neither of type
        /// <typeparamref name="TEnum"/> nor does it have the same underlying
        /// type as <typeparamref name="TEnum"/>.
        /// </p>
        /// </exception>
        [SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "Reviewed.")]
        public static string GetName(TEnum value)
        {
            return Enum.GetName(typeof(TEnum), value);
        }

        /// <summary>
        /// Retrieves an array of the names of the constants in the enumeration.
        /// </summary>
        /// <returns>A collection of the names of the constants in <typeparamref name="TEnum"/>.</returns>
        /// <exception cref="ArgumentException">
        /// <typeparamref name="TEnum"/> is not an <see cref="Enum"/>.
        /// </exception>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Reviewed.")]
        [SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "Reviewed.")]
        public static IEnumerable<string> GetNames()
        {
            return Enum.GetNames(typeof(TEnum));
        }

        /// <summary>
        /// Returns the underlying type of the enumeration.
        /// </summary>
        /// <returns>The underlying type of <typeparamref name="TEnum"/>.</returns>
        /// <exception cref="ArgumentException">
        /// <typeparamref name="TEnum"/> is not an <see cref="Enum"/>.
        /// </exception>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Reviewed.")]
        [SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "Reviewed.")]
        public static Type GetUnderlyingType()
        {
            return Enum.GetUnderlyingType(typeof(TEnum));
        }

        /// <summary>
        /// Retrieves collection of the values of the constants in the enumeration.
        /// </summary>
        /// <returns>
        /// A collection that contains the values of the constants in <typeparamref name="TEnum"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// <typeparamref name="TEnum"/> is not an <see cref="Enum"/>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <p>The method is invoked by reflection in a reflection-only context.</p>
        /// <p>-or-</p>
        /// <p>
        /// <typeparamref name="TEnum"/> is a type from an assembly loaded in a
        /// reflection-only context.
        /// </p>
        /// </exception>
        [SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "Reviewed.")]
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Reviewed.")]
        public static IEnumerable<TEnum> GetValues()
        {
            return Enum.GetValues(typeof(TEnum)).OfType<TEnum>();
        }

        /// <summary>
        /// Returns an indication whether a constant with a specified value
        /// exists in the enumeration.
        /// </summary>
        /// <param name="value">The value or name of a constant in <typeparamref name="TEnum"/>.</param>
        /// <returns>
        /// <see langword="true"/> if a constant in <typeparamref name="TEnum"/>
        /// has a value equal to <paramref name="value"/>; otherwise, <see langword="false"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="value"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <p><typeparamref name="TEnum"/> is not an <see cref="Enum"/>.</p>
        /// <p>-or-</p>
        /// <p>
        /// The type of <paramref name="value"/> is an enumeration, but it is
        /// not an enumeration of type <typeparamref name="TEnum"/>.
        /// </p>
        /// <p>-or-</p>
        /// <p>
        /// The type of <paramref name="value"/> is not an underlying type of <typeparamref name="TEnum"/>.
        /// </p>
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="value"/> is not type <see cref="SByte"/>,
        /// <see cref="Int16"/>, <see cref="Int32"/>, <see cref="Int64"/>,
        /// <see cref="Byte"/>, <see cref="UInt16"/>, <see cref="UInt32"/>,
        /// <see cref="UInt64"/>, or <see cref="String"/>.
        /// </exception>
        [SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "Reviewed.")]
        public static bool IsDefined(object value)
        {
            return Enum.IsDefined(typeof(TEnum), value);
        }

        /// <summary>
        /// Converts the string representation of the name or numeric value of
        /// one or more enumerated constants to an equivalent enumerated object.
        /// </summary>
        /// <param name="value">A string containing the name or value to convert.</param>
        /// <returns>
        /// An instance of <typeparamref name="TEnum"/> whose value is
        /// represented by <paramref name="value"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="value"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <p><typeparamref name="TEnum"/> is not an <see cref="Enum"/>.</p>
        /// <p>-or-</p>
        /// <p>
        /// <paramref name="value"/> is either <see langword="String.Empty"/> or
        /// contains only white space.
        /// </p>
        /// <p>-or-</p>
        /// <p>
        /// <paramref name="value"/> is a name, but not one of the named
        /// constants defined for the enumeration.
        /// </p>
        /// </exception>
        /// <exception cref="OverflowException">
        /// <paramref name="value"/> is outside the range of the underlying type
        /// of <typeparamref name="TEnum"/>.
        /// </exception>
        [SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "Reviewed.")]
        public static TEnum Parse(string value)
        {
            return Parse(value, true);
        }

        /// <summary>
        /// Converts the string representation of the name or numeric value of
        /// one or more enumerated constants to an equivalent enumerated object.
        /// A parameter specifies whether the operation is case-insensitive.
        /// </summary>
        /// <param name="value">A string containing the name or value to convert.</param>
        /// <param name="ignoreCase">
        /// <see langword="true"/> to ignore case; <see langword="false"/> to
        /// regard case.
        /// </param>
        /// <returns>
        /// An instance of <typeparamref name="TEnum"/> whose value is
        /// represented by <paramref name="value"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="value"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <p><typeparamref name="TEnum"/> is not an <see cref="Enum"/>.</p>
        /// <p>-or-</p>
        /// <p>
        /// <paramref name="value"/> is either <see langword="String.Empty"/> or
        /// contains only white space.
        /// </p>
        /// <p>-or-</p>
        /// <p>
        /// <paramref name="value"/> is a name, but not one of the named
        /// constants defined for the enumeration.
        /// </p>
        /// </exception>
        /// <exception cref="OverflowException">
        /// <paramref name="value"/> is outside the range of the underlying type
        /// of <typeparamref name="TEnum"/>.
        /// </exception>
        [SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "Reviewed.")]
        public static TEnum Parse(string value, bool ignoreCase)
        {
            return (TEnum)Enum.Parse(typeof(TEnum), value, ignoreCase);
        }

        /// <summary>
        /// Converts the specified object with an integer value to an
        /// enumeration member.
        /// </summary>
        /// <param name="value">The value convert to an enumeration member.</param>
        /// <returns>An enumeration whose value is <paramref name="value"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="value"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <p><typeparamref name="TEnum"/> is not an <see cref="Enum"/>.</p>
        /// <p>-or-</p>
        /// <p>
        /// <paramref name="value"/> is not type <see cref="SByte"/>,
        /// <see cref="Int16"/>, <see cref="Int32"/>, <see cref="Int64"/>,
        /// <see cref="Byte"/>, <see cref="UInt16"/>, <see cref="UInt32"/>,
        /// <see cref="UInt64"/>, or <see cref="String"/>.
        /// </p>
        /// </exception>
        [SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "Reviewed.")]
        public static TEnum ToEnum(object value)
        {
            return (TEnum)Enum.ToObject(typeof(TEnum), value);
        }

        /// <summary>
        /// Converts the string representation of the name or numeric value of
        /// one or more enumerated constants to an equivalent enumerated object.
        /// The return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="value">
        /// The string representation of the enumeration name or underlying
        /// value to convert.
        /// </param>
        /// <param name="result">
        /// When this method returns, contains an object of type
        /// <typeparamref name="TEnum"/> whose value is represented by value.
        /// This parameter is passed uninitialized.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the value parameter was converted
        /// successfully; otherwise, <see langword="false"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// <typeparamref name="TEnum"/> is not an enumeration type.
        /// </exception>
        [SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "Reviewed.")]
        public static bool TryParse(string value, out TEnum result)
        {
            return TryParse(value, true, out result);
        }

        /// <summary>
        /// Converts the string representation of the name or numeric value of
        /// one or more enumerated constants to an equivalent enumerated object.
        /// A parameter specifies whether the operation is case-sensitive. The
        /// return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="value">
        /// The string representation of the enumeration name or underlying
        /// value to convert.
        /// </param>
        /// <param name="ignoreCase">
        /// <see langword="true"/> to ignore case; <see langword="false"/> to
        /// regard case.
        /// </param>
        /// <param name="result">
        /// When this method returns, contains an object of type
        /// <typeparamref name="TEnum"/> whose value is represented by value.
        /// This parameter is passed uninitialized.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the value parameter was converted
        /// successfully; otherwise, <see langword="false"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// <typeparamref name="TEnum"/> is not an enumeration type.
        /// </exception>
        [SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "Reviewed.")]
        public static bool TryParse(string value, bool ignoreCase, out TEnum result)
        {
            return Enum.TryParse<TEnum>(value, ignoreCase, out result);
        }
    }
}