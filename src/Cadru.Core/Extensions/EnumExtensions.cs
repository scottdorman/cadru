//------------------------------------------------------------------------------
// <copyright file="EnumExtensions.cs" 
//  company="Scott Dorman" 
//  library="Cadru">
//    Copyright (C) 2001-2014 Scott Dorman.
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

    /// <summary>
    /// Provides basic routines for common enumerated type manipulation.
    /// </summary>
    public static class EnumExtensions
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

        #region GetDescription

        #region GetDescription(this Enum value)
        /// <summary>
        /// Gets the <see cref="EnumDescriptionAttribute"/> of an <see cref="Enum"/> type value.
        /// </summary>
        /// <param name="value">The <see cref="Enum"/> type value.</param>
        /// <returns>A string containing the text of the <see cref="EnumDescriptionAttribute"/>.</returns>
        public static string GetDescription(this Enum value)
        {
            return value.GetDescription(useNameAsFallback: true);
        }
        #endregion

        #region GetDescription(this Enum value, bool useNameAsFallback)
        /// <summary>
        /// Gets the <see cref="EnumDescriptionAttribute"/> of an <see cref="Enum"/> type value.
        /// </summary>
        /// <param name="value">The <see cref="Enum"/> type value.</param>
        /// <param name="useNameAsFallback">If <see langword="true"/>, the
        /// name of the enumerated constant is used if no description is found;
        /// otherwise, <see langword="null"/>.</param>
        /// <returns>A string containing the text of the <see cref="EnumDescriptionAttribute"/>.</returns>
        public static string GetDescription(this Enum value, bool useNameAsFallback)
        {
            Contracts.Requires.IsEnum(value, "value");

            var fieldValue = value.ToString();
            var attribute = ((EnumDescriptionAttribute[])value.GetType().GetField(fieldValue)?.GetCustomAttributes(typeof(EnumDescriptionAttribute), false)).FirstOrDefault();
            return attribute?.Description ?? (useNameAsFallback ? fieldValue : null);
        }
        #endregion

        #endregion

        #endregion
    }
}