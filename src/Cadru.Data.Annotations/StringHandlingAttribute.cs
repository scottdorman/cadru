//------------------------------------------------------------------------------
// <copyright file="StringHandlingAttribute.cs"
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

namespace Cadru.Data.Annotations
{
    /// <summary>
    /// Specifies how string values should be handled.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public sealed class StringHandlingAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see
        /// cref="StringHandlingAttribute" /> class.
        /// </summary>
        /// <param name="stringHandlingOption">The string handling option.</param>
        public StringHandlingAttribute(StringHandlingOption stringHandlingOption)
        {
            this.StringHandlingOption = stringHandlingOption;
        }

        /// <summary>
        /// Gets the option for handling string values.
        /// </summary>
        /// <value>The string handling option.</value>
        public StringHandlingOption StringHandlingOption { get; private set; }
    }
}