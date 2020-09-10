//------------------------------------------------------------------------------
// <copyright file="ExtendedPropertyAttribute.cs"
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
    /// Specifies additional attributes about a class, as a name/value pair.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class ExtendedPropertyAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="ExtendedPropertyAttribute"/> class.
        /// </summary>
        /// <param name="name">The name of the extended property.</param>
        /// <param name="value">The value of the extended property.</param>
        public ExtendedPropertyAttribute(string name, object value)
        {
            this.Name = name;
            this.Value = value;
        }

        /// <summary>
        /// The name of the extended property.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The value of the extended property.
        /// </summary>
        public object Value { get; private set; }
    }
}