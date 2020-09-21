//------------------------------------------------------------------------------
// <copyright file="TypeExtensions.cs"
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
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

using Cadru.Resources;

using Validation;

namespace Cadru.Extensions
{
    /// <summary>
    /// Provides basic routines for common type manipulation.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Retrieves a custom attribute of a specified type that is applied to
        /// a specified field, and optionally inspects the ancestors of that field.
        /// </summary>
        /// <typeparam name="T">The type of attribute to search for.</typeparam>
        /// <param name="element">The field to inspect.</param>
        /// <param name="inherit">
        /// <see langword="true"/> to inspect the ancestors of element;
        /// otherwise, <see langword="false"/>.
        /// </param>
        /// <returns>
        /// A custom attribute that matches <typeparamref name="T"/>, or
        /// <see langword="null"/> if no such attribute is found.
        /// </returns>
        public static T GetAttributeOfType<T>(this FieldInfo element, bool inherit = false) where T : Attribute
        {
            return element.GetCustomAttributes<T>(inherit).FirstOrDefault();
        }

        /// <summary>
        /// Retrieves a custom description that is applied to a specified field,
        /// and optionally inspects the ancestors of that field.
        /// </summary>
        /// <param name="field">The field to inspect.</param>
        /// <param name="useNameAsFallback">
        /// If <see langword="true"/>, the name of the field is used if no
        /// description is found; otherwise, <see langword="null"/>.
        /// </param>
        /// <returns>
        /// A string containing the description of the field; or
        /// <see langword="null"/> if no such constant is found and
        /// <paramref name="useNameAsFallback"/> is <see langword="false"/>;
        /// otherwise, the name of the field.
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
        public static string? GetDescription(this FieldInfo field, bool useNameAsFallback)
        {
            return field.GetAttributeOfType<EnumDescriptionAttribute>()?.Description
                ?? field.GetAttributeOfType<DisplayAttribute>()?.Name
                ?? field.GetAttributeOfType<DescriptionAttribute>()?.Description
                ?? (useNameAsFallback ? field.Name : null);
        }

        /// <summary>
        /// Determines whether the specified type has a custom attribute
        /// </summary>
        /// <typeparam name="T">The type of attribute to search for.</typeparam>
        /// <param name="element">The member to inspect.</param>
        /// <param name="inherit">
        /// <see langword="true"/> to inspect the ancestors of element;
        /// otherwise, <see langword="false"/>.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the specified element has the attribute;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="element"/> is null.
        /// </exception>
        /// <exception cref="System.NotSupportedException">
        /// <paramref name="element"/> is not a constructor, method, property,
        /// event, type, or field.
        /// </exception>
        /// <exception cref="System.Reflection.AmbiguousMatchException">
        /// More than one of the requested attributes was found.
        /// </exception>
        /// <exception cref="System.TypeLoadException">
        /// A custom attribute type cannot be loaded.
        /// </exception>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "Reviewed.")]
        public static bool HasCustomAttribute<T>(this Type element, bool inherit = false) where T : Attribute
        {
            Requires.NotNull(element, nameof(element));
            return element.GetTypeInfo().HasCustomAttribute<T>(inherit);
        }

        /// <summary>
        /// Determines whether the specified type has a custom attribute
        /// </summary>
        /// <typeparam name="T">The type of attribute to search for.</typeparam>
        /// <param name="element">The member to inspect.</param>
        /// <param name="inherit">
        /// <see langword="true"/> to inspect the ancestors of element;
        /// otherwise, <see langword="false"/>.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the specified element has the attribute;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="element"/> is null.
        /// </exception>
        /// <exception cref="System.NotSupportedException">
        /// <paramref name="element"/> is not a constructor, method, property,
        /// event, type, or field.
        /// </exception>
        /// <exception cref="System.Reflection.AmbiguousMatchException">
        /// More than one of the requested attributes was found.
        /// </exception>
        /// <exception cref="System.TypeLoadException">
        /// A custom attribute type cannot be loaded.
        /// </exception>
        public static bool HasCustomAttribute<T>(this MemberInfo element, bool inherit = false) where T : Attribute
        {
            return element.GetCustomAttribute<T>(inherit) != null;
        }

        /// <summary>
        /// Determines whether the specified type implements an interface
        /// </summary>
        /// <typeparam name="TInterface">
        /// The interface for which the type will be tested.
        /// </typeparam>
        /// <param name="element">The type to test.</param>
        /// <returns>
        /// <see langword="true"/> if the specified type implements the
        /// interface; otherwise, <see langword="false"/>.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "Reviewed.")]
        public static bool HasInterface<TInterface>(this Type element)
        {
            Requires.NotNull(element, nameof(element));

            return element.GetTypeInfo().HasInterface<TInterface>();
        }

        /// <summary>
        /// Determines whether the specified type implements an interface
        /// </summary>
        /// <typeparam name="TInterface">
        /// The interface for which the type will be tested.
        /// </typeparam>
        /// <param name="element">The type to test.</param>
        /// <returns>
        /// <see langword="true"/> if the specified type implements the
        /// interface; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool HasInterface<TInterface>(this TypeInfo element)
        {
            Requires.NotNull(element, nameof(element));

            var result = false;
            var interfaceType = typeof(TInterface);
            try
            {
                result = element.ImplementedInterfaces.SingleOrDefault(t => t == interfaceType) != null;
            }
            catch (InvalidOperationException)
            {
                throw new AmbiguousMatchException(Strings.Arg_AmbiguousMatchException);
            }

            return result;
        }

        /// <summary>
        /// Determines whether the specified type is a <see cref="Boolean"/>.
        /// </summary>
        /// <param name="element">The type to test.</param>
        /// <returns>
        /// <see langword="true"/> if the specified type is a
        /// <see cref="Boolean"/>; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsBoolean(this Type element)
        {
            Requires.NotNull(element, nameof(element));
            return element.GetTypeInfo().IsBoolean();
        }

        /// <summary>
        /// Determines whether the specified type is a <see cref="Boolean"/>.
        /// </summary>
        /// <param name="element">The type to test.</param>
        /// <returns>
        /// <see langword="true"/> if the specified type is a
        /// <see cref="Boolean"/>; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsBoolean(this TypeInfo element)
        {
            Requires.NotNull(element, nameof(element));
            return element.AsType() == typeof(bool);
        }

        /// <summary>
        /// Determines whether the specified type is a <see cref="DateTime"/>.
        /// </summary>
        /// <param name="element">The type to test.</param>
        /// <returns>
        /// <see langword="true"/> if the specified type is a
        /// <see cref="DateTime"/>; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsDate(this Type element)
        {
            Requires.NotNull(element, nameof(element));
            return element.GetTypeInfo().IsDate();
        }

        /// <summary>
        /// Determines whether the specified type is a <see cref="DateTime"/>.
        /// </summary>
        /// <param name="element">The type to test.</param>
        /// <returns>
        /// <see langword="true"/> if the specified type is a
        /// <see cref="DateTime"/>; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsDate(this TypeInfo element)
        {
            Requires.NotNull(element, nameof(element));

            var type = element.AsType();
            if (element.IsNullable())
            {
                var underlyingType = Nullable.GetUnderlyingType(type);
                return underlyingType.IsDate();
            }

            return type == typeof(DateTime);
        }

        /// <summary>
        /// Determines whether the specified type is a <see cref="DateTimeOffset"/>.
        /// </summary>
        /// <param name="element">The type to test.</param>
        /// <returns>
        /// <see langword="true"/> if the specified type is a
        /// <see cref="DateTimeOffset"/>; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsDateOffset(this Type element)
        {
            Requires.NotNull(element, nameof(element));
            return element.GetTypeInfo().IsDateOffset();
        }

        /// <summary>
        /// Determines whether the specified type is a <see cref="DateTimeOffset"/>.
        /// </summary>
        /// <param name="element">The type to test.</param>
        /// <returns>
        /// <see langword="true"/> if the specified type is a
        /// <see cref="DateTimeOffset"/>; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsDateOffset(this TypeInfo element)
        {
            Requires.NotNull(element, nameof(element));

            var type = element.AsType();
            if (element.IsNullable())
            {
                var underlyingType = Nullable.GetUnderlyingType(type);
                return underlyingType.IsDateOffset();
            }

            return type == typeof(DateTimeOffset);
        }

        /// <summary>
        /// Determines if the type is an enumerable type.
        /// </summary>
        /// <param name="element">The type to test.</param>
        /// <returns>
        /// <see langword="true"/> if the specified type is an enumerable type;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsEnumerableType(this Type element)
        {
            return element.HasInterface<IEnumerable>();
        }

        /// <summary>
        /// Determines if the type is an enumerable type.
        /// </summary>
        /// <param name="element">The type to test.</param>
        /// <returns>
        /// <see langword="true"/> if the specified type is an enumerable type;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsEnumerableType(this TypeInfo element)
        {
            return element.HasInterface<IEnumerable>();
        }

        /// <summary>
        /// Determines whether the specified type is an <see cref="Enum"/> with
        /// an associated <see cref="FlagsAttribute"/>.
        /// </summary>
        /// <param name="element">The type to test.</param>
        /// <returns>
        /// <see langword="true"/> if the specified type is an
        /// <see cref="Enum"/> with an associated <see cref="FlagsAttribute"/>;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsFlagsEnum(this Type element)
        {
            Requires.NotNull(element, nameof(element));

            return element.GetTypeInfo().IsFlagsEnum();
        }

        /// <summary>
        /// Determines whether the specified type is an <see cref="Enum"/> with
        /// an associated <see cref="FlagsAttribute"/>.
        /// </summary>
        /// <param name="element">The type to test.</param>
        /// <returns>
        /// <see langword="true"/> if the specified type is an
        /// <see cref="Enum"/> with an associated <see cref="FlagsAttribute"/>;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsFlagsEnum(this TypeInfo element)
        {
            Requires.NotNull(element, nameof(element));
            Requires.Argument(element.IsEnum, nameof(element), Strings.ArgumentExceptionMustBeEnum);

            return element.GetCustomAttribute<FlagsAttribute>(inherit: false) != null;
        }

        /// <summary>
        /// Determines whether the specified type is nullable.
        /// </summary>
        /// <param name="element">The type to test.</param>
        /// <returns>
        /// <see langword="true"/> if the specified type is nullable; otherwise, <see langword="false"/>.
        /// </returns>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
        public static bool IsNullable(this Type element)
        {
            Requires.NotNull(element, nameof(element));

            return element.GetTypeInfo().IsNullable();
        }

        /// <summary>
        /// Determines whether the specified type is nullable.
        /// </summary>
        /// <param name="element">The type to test.</param>
        /// <returns>
        /// <see langword="true"/> if the specified type is nullable; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsNullable(this TypeInfo element)
        {
            Requires.NotNull(element, nameof(element));
            return element.IsGenericType && !element.IsGenericTypeDefinition && element.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        /// <summary>
        /// Determines whether the specified type is a numeric type.
        /// </summary>
        /// <param name="element">The type to test.</param>
        /// <returns>
        /// <see langword="true"/> if the specified type is a numeric type;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsNumeric(this Type element)
        {
            Requires.NotNull(element, nameof(element));
            return element.GetTypeInfo().IsNumeric();
        }

        /// <summary>
        /// Determines whether the specified type is a numeric type.
        /// </summary>
        /// <param name="element">The type to test.</param>
        /// <returns>
        /// <see langword="true"/> if the specified type is a numeric type;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsNumeric(this TypeInfo element)
        {
            Requires.NotNull(element, nameof(element));

            var type = element.AsType();

            if (element.IsPrimitive)
            {
                return type != typeof(bool) && type != typeof(char) && type != typeof(IntPtr) && type != typeof(UIntPtr);
            }

            if (element.IsNullable())
            {
                var underlyingType = Nullable.GetUnderlyingType(type);
                return underlyingType.IsNumeric();
            }

            return type == typeof(decimal);
        }
    }
}