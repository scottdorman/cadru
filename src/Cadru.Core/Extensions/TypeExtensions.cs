//------------------------------------------------------------------------------
// <copyright file="TypeExtensions.cs"
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
    using System.Linq;
    using System.Reflection;

    using Cadru.Resources;

    using Contracts;

    /// <summary>
    /// Provides basic routines for common type manipulation.
    /// </summary>
    public static class TypeExtensions
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

        #region HasInterface
        /// <summary>
        /// Determines whether the specified type implements an interface
        /// </summary>
        /// <typeparam name="TInterface">The interface for which the type will be tested.</typeparam>
        /// <param name="type">The type to test.</param>
        /// <returns><see langword="true"/> if the specified type implements the interface;
        /// otherwise, <see langword="false"/>.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "Reviewed.")]
        public static bool HasInterface<TInterface>(this Type type)
        {
            Requires.NotNull(type, nameof(type));

            return type.GetTypeInfo().HasInterface<TInterface>();
        }

        public static bool HasInterface<TInterface>(this TypeInfo typeInfo)
        {
            Requires.NotNull(typeInfo, nameof(typeInfo));

            var result = false;
            var interfaceType = typeof(TInterface);
            try
            {
                result = typeInfo.ImplementedInterfaces.SingleOrDefault(t => t == interfaceType) != null;
            }
            catch (InvalidOperationException)
            {
                throw new AmbiguousMatchException(Strings.Arg_AmbiguousMatchException);
            }

            return result;
        }
        #endregion

        #region IsNullable
        /// <summary>
        /// Determines whether the specified type is nullable.
        /// </summary>
        /// <param name="type">The type to test.</param>
        /// <returns>
        ///   <see langword="true"/> if the specified type is nullable; otherwise, <see langword="false"/>.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
        public static bool IsNullable(this Type type)
        {
            Contracts.Requires.NotNull(type, nameof(type));

            return type.GetTypeInfo().IsNullable();
        }

        public static bool IsNullable(this TypeInfo typeInfo)
        {
            Contracts.Requires.NotNull(typeInfo, nameof(typeInfo));
            return typeInfo.IsGenericType && !typeInfo.IsGenericTypeDefinition && typeInfo.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
        #endregion

        #region IsBoolean
        /// <summary>
        /// Determines whether the specified type is a <see cref="Boolean"/>.
        /// </summary>
        /// <param name="type">The type to test.</param>
        /// <returns>
        ///   <see langword="true"/> if the specified type is a <see cref="Boolean"/>; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsBoolean(this Type type)
        {
            Contracts.Requires.NotNull(type, nameof(type));
            return type.GetTypeInfo().IsBoolean();
        }

        public static bool IsBoolean(this TypeInfo type)
        {
            Contracts.Requires.NotNull(type, nameof(type));
            return type.AsType() == typeof(bool);
        }
        #endregion

        #region IsNumeric
        /// <summary>
        /// Determines whether the specified type is a numeric type.
        /// </summary>
        /// <param name="type">The type to test.</param>
        /// <returns>
        ///   <see langword="true"/> if the specified type is a numeric type; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsNumeric(this Type type)
        {
            Contracts.Requires.NotNull(type, nameof(type));
            return type.GetTypeInfo().IsNumeric();
        }

        public static bool IsNumeric(this TypeInfo typeInfo)
        {
            Contracts.Requires.NotNull(typeInfo, nameof(typeInfo));

            var type = typeInfo.AsType();

            if (typeInfo.IsPrimitive)
            {
                return type != typeof(bool) && type != typeof(char) && type != typeof(IntPtr) && type != typeof(UIntPtr);
            }

            if (typeInfo.IsNullable())
            {
                var underlyingType = Nullable.GetUnderlyingType(type);
                return underlyingType.IsNumeric();
            }

            return type == typeof(decimal);
        }
        #endregion

        #region IsDate
        /// <summary>
        /// Determines whether the specified type is a <see cref="DateTime"/>.
        /// </summary>
        /// <param name="type">The type to test.</param>
        /// <returns>
        ///   <see langword="true"/> if the specified type is a <see cref="DateTime"/>; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsDate(this Type type)
        {
            Contracts.Requires.NotNull(type, nameof(type));
            return type.GetTypeInfo().IsDate();
        }

        public static bool IsDate(this TypeInfo typeInfo)
        {
            Contracts.Requires.NotNull(typeInfo, nameof(typeInfo));

            var type = typeInfo.AsType();
            if (typeInfo.IsNullable())
            {
                var underlyingType = Nullable.GetUnderlyingType(type);
                return underlyingType.IsDate();
            }

            return type == typeof(DateTime);
        }
        #endregion

        #region IsDateOffset
        /// <summary>
        /// Determines whether the specified type is a <see cref="DateTimeOffset"/>.
        /// </summary>
        /// <param name="type">The type to test.</param>
        /// <returns>
        ///   <see langword="true"/> if the specified type is a <see cref="DateTimeOffset"/>; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsDateOffset(this Type type)
        {
            Contracts.Requires.NotNull(type, nameof(type));
            return type.GetTypeInfo().IsDateOffset();
        }

        /// <summary>
        /// Determines whether the specified type is a <see cref="DateTimeOffset"/>.
        /// </summary>
        /// <param name="typeInfo">The TypeInfo to test.</param>
        /// <returns>
        ///   <see langword="true"/> if the specified type is a <see cref="DateTimeOffset"/>; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsDateOffset(this TypeInfo typeInfo)
        {
            Contracts.Requires.NotNull(typeInfo, nameof(typeInfo));

            var type = typeInfo.AsType();
            if (typeInfo.IsNullable())
            {
                var underlyingType = Nullable.GetUnderlyingType(type);
                return underlyingType.IsDateOffset();
            }

            return type == typeof(DateTimeOffset);
        }
        #endregion

        #region IsFlagsEnum
        /// <summary>
        /// Determines whether the specified type is an <see cref="Enum"/> with an associated
        /// <see cref="FlagsAttribute"/>.
        /// </summary>
        /// <param name="type">The type to test.</param>
        /// <returns>
        ///   <see langword="true"/> if the specified type is an <see cref="Enum"/> with an associated
        ///   <see cref="FlagsAttribute"/>; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsFlagsEnum(this Type type)
        {
            Contracts.Requires.NotNull(type, nameof(type));

            return type.GetTypeInfo().IsFlagsEnum();
        }

        /// <summary>
        /// Determines whether the specified type is an <see cref="Enum"/> with an associated
        /// <see cref="FlagsAttribute"/>.
        /// </summary>
        /// <param name="typeInfo">The TypeInfo to test.</param>
        /// <returns>
        ///   <see langword="true"/> if the specified type is an <see cref="Enum"/> with an associated
        ///   <see cref="FlagsAttribute"/>; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsFlagsEnum(this TypeInfo typeInfo)
        {
            Contracts.Requires.NotNull(typeInfo, nameof(typeInfo));
            Contracts.Requires.IsTrue(typeInfo.IsEnum);

            return typeInfo.GetCustomAttribute<FlagsAttribute>(inherit: false) != null;
        }
        #endregion

        #endregion
    }
}
