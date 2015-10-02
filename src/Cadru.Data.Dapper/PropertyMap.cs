//------------------------------------------------------------------------------
// <copyright file="PropertyMap.cs"
//  company="Scott Dorman"
//  library="Cadru">
//    Copyright (C) 2001-2015 Scott Dorman.
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
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Maps an entity property to its corresponding column in the database.
    /// </summary>
    public class PropertyMap : IPropertyMap
    {
        public PropertyMap(PropertyInfo propertyInfo)
        {
            this.PropertyInfo = propertyInfo;
            this.Ignored = propertyInfo.GetCustomAttribute<NotMappedAttribute>() != null;
            this.IsKey = propertyInfo.GetCustomAttribute<KeyAttribute>() != null;

            var columnAttribute = propertyInfo.GetCustomAttribute<ColumnAttribute>();
            this.ColumnName = columnAttribute?.Name ?? PropertyInfo.Name;

            var databaseGeneratedAttribute = propertyInfo.GetCustomAttribute<DatabaseGeneratedAttribute>();
            this.DatabaseGeneratedOption = databaseGeneratedAttribute?.DatabaseGeneratedOption ?? DatabaseGeneratedOption.None;
        }

        /// <summary>
        /// Gets the column name for the current property.
        /// </summary>
        public string ColumnName { get; private set; }

        /// <summary>
        /// Gets the ignore status of the current property. If ignored, the current property will not be included in queries.
        /// </summary>
        public bool Ignored { get; private set; }

        public DatabaseGeneratedOption DatabaseGeneratedOption { get; private set; }

        /// <summary>
        /// Gets the read-only status of the current property. If read-only, the current property will not be included in INSERT and UPDATE queries.
        /// </summary>
        public bool IsReadOnly { get; internal set; }

        public bool IsUpdatable { get { return !(this.Ignored || this.IsReadOnly || this.DatabaseGeneratedOption != DatabaseGeneratedOption.None); } }

        public bool IsKey { get; private set; }

        /// <summary>
        /// Gets the name of the property by using the specified propertyInfo.
        /// </summary>
        public string Name
        {
            get { return PropertyInfo.Name; }
        }

        /// <summary>
        /// Gets the property info for the current property.
        /// </summary>
        public PropertyInfo PropertyInfo { get; private set; }

    }
}
