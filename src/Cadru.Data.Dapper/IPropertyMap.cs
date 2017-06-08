//------------------------------------------------------------------------------
// <copyright file="IPropertyMap.cs"
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

namespace Cadru.Data.Dapper
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Reflection;
    using Cadru.Data.Annotations;

    public interface IPropertyMap
    {
        /// <summary>
        /// Gets the option for handling string values.
        /// </summary>
        StringHandlingOption StringHandlingOption { get; }

        /// <summary>
        /// Gets a value that indicates whether a field is exportable.
        /// </summary>
        bool IsExportable { get; }

        /// <summary>
        /// Gets a value that can be used to display a description in the UI.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Gets a value that can be used to set the watermark for prompts in the UI.
        /// </summary>
        string Prompt { get; }

        /// <summary>
        /// Gets a value that can be used for the grid column label.
        /// </summary>
        string Caption { get; }

        /// <summary>
        /// Gets the order weight of the column.
        /// </summary>
        int? Order { get; }

        /// <summary>
        /// Gets the column name for the current property.
        /// </summary>
        string ColumnName { get; }

        /// <summary>
        /// Gets a value that is used for field display in the UI.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the ignore status of the current property. If ignored, the current property will not be included in queries.
        /// </summary>
        bool Ignored { get; }

        DatabaseGeneratedOption DatabaseGeneratedOption { get; }

        bool IsKey { get; }

        bool AllowEmptyStrings { get; }

        bool IsRequired { get; }

        /// <summary>
        /// Gets the read-only status of the current property.
        /// If read-only, the current property will not be included in INSERT and UPDATE queries.
        /// </summary>
        bool IsReadOnly { get; }

        bool IsUpdatable { get; }

        /// <summary>
        /// Gets the name of the property by using the specified <see cref="PropertyInfo"/>.
        /// </summary>
        string PropertyName { get; }

        /// <summary>
        /// Gets the <see cref="System.Reflection.PropertyInfo"/> for the current property.
        /// </summary>
        PropertyInfo PropertyInfo { get; }
    }
}