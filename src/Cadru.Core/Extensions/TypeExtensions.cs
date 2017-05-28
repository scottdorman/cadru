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
    using Cadru.Core.Resources;
    using Contracts;
    using System;
    using System.Linq;
    using System.Reflection;

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

        public static bool HasInterface<TInterface>(this TypeInfo type)
        {
            Requires.NotNull(type, nameof(type));

            var result = false;
            var interfaceType = typeof(TInterface);
            try
            {
                result = type.ImplementedInterfaces.SingleOrDefault(t => t == interfaceType) != null;
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

        public static bool IsNullable(this TypeInfo type)
        {
            Contracts.Requires.NotNull(type, nameof(type));
            return type.IsGenericType && !type.IsGenericTypeDefinition && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
        #endregion

        public static bool IsBoolean(this Type type)
        {
            Contracts.Requires.NotNull(type, nameof(type));
            return type == typeof(Boolean);
        }

        public static bool IsBoolean(this TypeInfo type)
        {
            Contracts.Requires.NotNull(type, nameof(type));
            return type.AsType() == typeof(Boolean);
        }

        public static bool IsNumeric(this Type type)
        {
            Contracts.Requires.NotNull(type, nameof(type));
            if (type.GetTypeInfo().IsPrimitive)
            {
                return type != typeof(bool) && type != typeof(char) && type != typeof(IntPtr) && type != typeof(UIntPtr);
            }

            if (type.IsNullable())
            {
                var underlyingType = Nullable.GetUnderlyingType(type);
                return underlyingType.IsNumeric();
            }

            return type == typeof(decimal);
        }

        public static bool IsDate(this Type type)
        {
            Contracts.Requires.NotNull(type, nameof(type));
            if (type.IsNullable())
            {
                var underlyingType = Nullable.GetUnderlyingType(type);
                return underlyingType.IsDate();
            }

            return type == typeof(DateTime);
        }

        public static bool IsDateOffset(this Type type)
        {
            Contracts.Requires.NotNull(type, nameof(type));
            if (type.IsNullable())
            {
                var underlyingType = Nullable.GetUnderlyingType(type);
                return underlyingType.IsDateOffset();
            }

            return type == typeof(DateTimeOffset);
        }
        #endregion
    }
}
