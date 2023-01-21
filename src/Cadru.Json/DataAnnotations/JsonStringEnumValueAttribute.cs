//------------------------------------------------------------------------------
// <copyright file="JsonStringEnumValueAttribute.cs"
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
using System.Text;
using System.Text.Json.Serialization;

namespace Cadru.Json.DataAnnotations
{
    /// <summary>
    /// Specifies the enum string value that is present in the JSON when serializing and deserializing.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class JsonStringEnumValueAttribute : JsonAttribute
    {
        /// <summary>
        /// Initializes a new instance of <see cref="JsonStringEnumValueAttribute"/> with the specified string value.
        /// </summary>
        /// <param name="value">The string value of the enum.</param>
        public JsonStringEnumValueAttribute(string value)
        {
            this.Value = value;
        }

        /// <summary>
        /// The string value of the enum.
        /// </summary>
        public string Value { get; }
    }
}
