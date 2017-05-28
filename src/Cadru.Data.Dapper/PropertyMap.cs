//------------------------------------------------------------------------------
// <copyright file="PropertyMap.cs"
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
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Maps an entity property to its corresponding column in the database.
    /// </summary>
    public class PropertyMap : IPropertyMap
    {
        public PropertyMap(PropertyInfo propertyInfo)
        {
            this.PropertyInfo = propertyInfo;

            var attributes = propertyInfo.GetCustomAttributes();

            this.Ignored = attributes.OfType<NotMappedAttribute>().SingleOrDefault() != null;
            this.IsKey = attributes.OfType<KeyAttribute>().SingleOrDefault() != null;
            var requiredAttribute = attributes.OfType<RequiredAttribute>().SingleOrDefault();
            if (requiredAttribute != null)
            {
                this.IsRequired = true;
                this.AllowEmptyStrings = requiredAttribute.AllowEmptyStrings;
            }

            var columnAttribute = attributes.OfType<ColumnAttribute>().SingleOrDefault();
            this.ColumnName = columnAttribute?.Name ?? PropertyInfo.Name;

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

        /// <summary>
        /// Gets the option for handling string values.
        /// </summary>
        public StringHandlingOption StringHandlingOption { get; private set; }

        /// <summary>
        /// Gets a value that indicates whether a field is exportable.
        /// </summary>
        public bool IsExportable { get; private set; }

        /// <summary>
        /// Gets a value that can be used to display a description in the UI.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets a value that can be used to set the watermark for prompts in the UI.
        /// </summary>
        public string Prompt { get; private set; }

        /// <summary>
        /// Gets a value that can be used for the grid column label.
        /// </summary>
        public string Caption { get; private set; }

        /// <summary>
        /// Gets a value that is used for field display in the UI.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the order weight of the column.
        /// </summary>
        public int? Order { get; private set; }

        /// <summary>
        /// Gets the column name for the current property.
        /// </summary>
        public string ColumnName { get; private set; }

        /// <summary>
        /// Gets the ignore status of the current property. If ignored, the current property will not be included in queries.
        /// </summary>
        public bool Ignored { get; private set; }

        public DatabaseGeneratedOption DatabaseGeneratedOption { get; private set; }

        public bool AllowEmptyStrings { get; private set; }

        public bool IsRequired { get; private set; }

        /// <summary>
        /// Gets the read-only status of the current property. If read-only, the current property will not be included in INSERT and UPDATE queries.
        /// </summary>
        public bool IsReadOnly { get; private set; }

        public bool IsUpdatable { get { return !(this.Ignored || this.IsReadOnly || this.DatabaseGeneratedOption != DatabaseGeneratedOption.None); } }

        public bool IsKey { get; private set; }

        /// <summary>
        /// Gets the name of the property by using the specified propertyInfo.
        /// </summary>
        public string PropertyName
        {
            get { return PropertyInfo.Name; }
        }

        /// <summary>
        /// Gets the property info for the current property.
        /// </summary>
        public PropertyInfo PropertyInfo { get; private set; }
    }
}
