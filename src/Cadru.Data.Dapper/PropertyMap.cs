//------------------------------------------------------------------------------
// <copyright file="PropertyMap.cs"
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

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

using Cadru.Data.Annotations;

namespace Cadru.Data.Dapper
{
    /// <summary>
    /// Maps an entity property to its corresponding column in the database.
    /// </summary>
    public class PropertyMap : IPropertyMap
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyMap"/> class.
        /// </summary>
        /// <param name="propertyInfo">The <see cref="PropertyInfo"/> for the
        /// entity model property that maps to the database column.</param>
        public PropertyMap(PropertyInfo propertyInfo)
        {
            this.PropertyInfo = propertyInfo;
            this.ColumnName = this.PropertyInfo.Name;

            var attributes = propertyInfo.GetCustomAttributes();
            if (attributes != null)
            {
                this.Ignored = attributes.OfType<NotMappedAttribute>().SingleOrDefault() != null;
                this.IsKey = attributes.OfType<KeyAttribute>().SingleOrDefault() != null;

                var requiredAttribute = attributes.OfType<RequiredAttribute>().SingleOrDefault();
                if (requiredAttribute != null)
                {
                    this.IsRequired = true;
                    this.AllowEmptyStrings = requiredAttribute.AllowEmptyStrings;
                }

                var columnAttribute = attributes.OfType<ColumnAttribute>().SingleOrDefault();
                if (columnAttribute != null)
                {
                    this.ColumnName = columnAttribute.Name;
                }

                var databaseGeneratedAttribute = attributes.OfType<DatabaseGeneratedAttribute>().SingleOrDefault();
                this.DatabaseGeneratedOption = databaseGeneratedAttribute?.DatabaseGeneratedOption ?? DatabaseGeneratedOption.None;

                var displayAttribute = attributes.OfType<DisplayAttribute>().SingleOrDefault();
                if (displayAttribute != null)
                {
                    this.Name = displayAttribute.GetName();
                    this.Caption = displayAttribute.GetShortName();
                    this.Prompt = displayAttribute.GetPrompt();
                    this.Description = displayAttribute.GetDescription();
                    this.Order = displayAttribute.GetOrder();
                }

                var editableAttribute = attributes.OfType<EditableAttribute>().SingleOrDefault();
                this.IsReadOnly = (!editableAttribute?.AllowEdit) ?? false;

                var exportAttribute = attributes.OfType<ExportableAttribute>().SingleOrDefault();
                this.IsExportable = exportAttribute?.AllowExport ?? true;

                var stringHandlingAttribute = attributes.OfType<StringHandlingAttribute>().SingleOrDefault();
                this.StringHandlingOption = stringHandlingAttribute?.StringHandlingOption ?? StringHandlingOption.None;
            }
        }

        /// <inheritdoc/>
        public bool AllowEmptyStrings { get; private set; }

        /// <inheritdoc/>
        public string? Caption { get; }

        /// <inheritdoc/>
        public string ColumnName { get; private set; }

        /// <inheritdoc/>
        public DatabaseGeneratedOption DatabaseGeneratedOption { get; private set; }

        /// <inheritdoc/>
        public string? Description { get; }

        /// <inheritdoc/>
        public bool Ignored { get; private set; }

        /// <inheritdoc/>
        public bool IsExportable { get; }

        /// <inheritdoc/>
        public bool IsKey { get; private set; }

        /// <inheritdoc/>
        public bool IsReadOnly { get; private set; }

        /// <inheritdoc/>
        public bool IsRequired { get; private set; }

        /// <inheritdoc/>
        public bool IsUpdatable => !(this.Ignored || this.IsReadOnly || this.DatabaseGeneratedOption != DatabaseGeneratedOption.None);

        /// <inheritdoc/>
        public string? Name { get; }

        /// <inheritdoc/>
        public int? Order { get; }

        /// <inheritdoc/>
        public string? Prompt { get; }

        /// <inheritdoc/>
        public PropertyInfo PropertyInfo { get; private set; }

        /// <inheritdoc/>
        public string PropertyName => this.PropertyInfo.Name;

        /// <inheritdoc/>
        public StringHandlingOption StringHandlingOption { get; }
    }
}