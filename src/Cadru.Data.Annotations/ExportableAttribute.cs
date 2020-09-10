//------------------------------------------------------------------------------
// <copyright file="ExportableAttribute.cs"
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
    /// Denotes that a property should be excluded from being exported.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public sealed class ExportableAttribute : Attribute
    {
        /// <summary>
        /// The default export order
        /// </summary>
        public const int DefaultOrder = 10000;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportableAttribute"/> class.
        /// </summary>
        /// <param name="allowExport">
        /// <see langword="true"/> to specify that the field is exportable;
        /// otherwise, <see langword="false"/>.
        /// </param>
        public ExportableAttribute(bool allowExport)
        {
            this.AllowExport = allowExport;
        }

        /// <summary>
        /// Gets a value that indicates whether a field is exportable.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the field is exportable; otherwise, <see langword="false"/>.
        /// </value>
        public bool AllowExport { get; private set; }

        /// <summary>
        /// Gets or sets the order weight of the column.
        /// </summary>
        /// <remarks>
        /// Columns are sorted in increasing order based on the order value.
        /// Columns without this attribute have an order value of 0. Negative
        /// values are valid and can be used to position a column before all
        /// non-negative columns. If an order is not specified, presentation
        /// layers should consider using the value 10000. This value lets
        /// explicitly-ordered fields be displayed before and after the fields
        /// that do not have a specified order.
        /// </remarks>
        public int Order { get; set; } = DefaultOrder;
    }
}