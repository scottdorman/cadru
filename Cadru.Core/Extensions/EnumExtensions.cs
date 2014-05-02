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
        /// <summary>
        /// Gets the <see cref="EnumDescriptionAttribute"/> of an <see cref="Enum"/> type value.
        /// </summary>
        /// <param name="value">The <see cref="Enum"/> type value.</param>
        /// <returns>A string containing the text of the <see cref="EnumDescriptionAttribute"/>.</returns>
        public static string GetDescription(this Enum value)
        {
            Contracts.Requires.NotNull(value, "value");

            Type type = value.GetType();
            string description = value.ToString();
            FieldInfo fieldInfo = type.GetField(description);
            if (fieldInfo.IsNotNull())
            {
                EnumDescriptionAttribute attribute = ((EnumDescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(EnumDescriptionAttribute), false)).FirstOrDefault();
                if (attribute.IsNotNull())
                {
                    description = attribute.Description;
                }
            }

            return description;
        }
        #endregion

        #endregion
    }
}