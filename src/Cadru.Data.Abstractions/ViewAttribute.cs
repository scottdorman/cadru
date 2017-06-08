//------------------------------------------------------------------------------
// <copyright file="ViewAttribute.cs"
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

namespace Cadru.Data.Annotations
{
    using Contracts;
    using System;

    /// <summary>
    ///     Specifies the database view that a class is mapped to.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ViewAttribute : Attribute
    {
        private readonly string name;
        private string schema;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ViewAttribute" /> class.
        /// </summary>
        /// <param name="name">The name of the view the class is mapped to.</param>
        public ViewAttribute(string name)
        {
            Requires.NotNullOrWhiteSpace(name, nameof(name));
            this.name = name;
        }

        /// <summary>
        ///     The name of the view the class is mapped to.
        /// </summary>
        public string Name => this.name;

        /// <summary>
        ///     The schema of the view the class is mapped to.
        /// </summary>
        public string Schema
        {
            get => this.schema;
            set
            {
                Requires.NotNullOrWhiteSpace(value, nameof(value));
                this.schema = value;
            }
        }
    }
}