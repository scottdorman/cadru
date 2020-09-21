//------------------------------------------------------------------------------
// <copyright file="EnumExtensions.cs"
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
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

using Cadru.Resources;

using Validation;

namespace Cadru.Extensions
{
    /// <summary>
    /// Provides basic routines for common enumerated type manipulation.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets an attribute of type <typeparamref name="T"/> on the provided
        /// enumerated value.
        /// </summary>
        /// <typeparam name="T">The type of attribute to search for.</typeparam>
        /// <param name="element">The member to inspect.</param>
        /// <param name="inherit">
        /// <see langword="true"/> to inspect the ancestors of element;
        /// otherwise, <see langword="false"/>.
        /// </param>
        /// <returns>
        /// A custom attribute that matches <typeparamref name="T"/>, or
        /// <see langword="null"/> if no such attribute is found.
        /// </returns>
        /// <remarks>
        /// If more than one attribute is found, the first match is returned.
        /// </remarks>
        public static T? GetAttributeOfType<T>(this Enum element, bool inherit = false) where T : Attribute
        {
            var typeInfo = element.GetType().GetTypeInfo();
            var enumField = typeInfo.GetDeclaredField(element.ToString());
            return enumField?.GetCustomAttributes<T>(inherit).FirstOrDefault();
        }

        /// <summary>
        /// Gets the description of an <see cref="Enum"/> type value.
        /// </summary>
        /// <param name="value">The <see cref="Enum"/> type value.</param>
        /// <returns>
        /// A string containing the the description or the name of the
        /// enumerated constant if no description is found.
        /// </returns>
        public static string GetDescription(this Enum value)
        {
            return value.GetDescription(useNameAsFallback: true)!;
        }

        /// <summary>
        /// Gets the description of an <see cref="Enum"/> type value.
        /// </summary>
        /// <param name="value">The <see cref="Enum"/> type value.</param>
        /// <param name="useNameAsFallback">
        /// If <see langword="true"/>, the name of the enumerated constant is
        /// used if no description is found; otherwise, <see langword="null"/>.
        /// </param>
        /// <returns>
        /// A string containing the the description. If no description is found
        /// and <paramref name="useNameAsFallback"/> is <see langword="true"/>
        /// then the name of the enumerated constant; otherwise <see langword="null"/>.
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
        public static string? GetDescription(this Enum value, bool useNameAsFallback)
        {
            Requires.NotNull(value, nameof(value));
            Requires.That(value.GetType().IsEnum, nameof(value), Strings.ArgumentExceptionMustBeEnum);

            var fieldInfo = value.GetType().GetTypeInfo().GetDeclaredField(value.ToString());
            return fieldInfo.GetDescription(useNameAsFallback);
        }
    }
}