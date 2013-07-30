//------------------------------------------------------------------------------
// <copyright file="EnumDescriptionAttribute.cs" 
//  company="Scott Dorman" 
//  library="Cadru">
//    Copyright (C) 2001-2013 Scott Dorman.
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

namespace Cadru
{
    using System;

    /// <summary>
    /// Provides a description for an enumerated type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field, AllowMultiple = false)]
    public sealed class EnumDescriptionAttribute : Attribute
    {
        #region fields
        private string description;
        #endregion

        #region constructors
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="EnumDescriptionAttribute"/> class.
        /// </summary>
        /// <param name="description">The description to store in this attribute.</param>
        public EnumDescriptionAttribute(string description)
            : base()
        {
            this.description = description;
        }
        #endregion

        #region events
        #endregion

        #region properties

        #region Description
        /// <summary>
        /// Gets the description stored in this attribute.
        /// </summary>
        /// <value>The description stored in the attribute.</value>
        public string Description
        {
            get
            {
                return this.description;
            }
        }
        #endregion

        #endregion

        #region methods
        #endregion
    }
}
