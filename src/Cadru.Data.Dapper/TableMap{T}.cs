//------------------------------------------------------------------------------
// <copyright file="TableMap.cs"
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
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    public class TableMap<T> : ITableMap where T : class
    {
        private Type tableType;

        public TableMap()
        {
            this.tableType = typeof(T);
            Properties = new List<IPropertyMap>();

            var tableAttribute = this.tableType.GetCustomAttribute<TableAttribute>(inherit: true);
            if (tableAttribute != null)
            {
                this.SchemaName = tableAttribute.Schema;
                this.TableName = tableAttribute.Name;
            }
            else
            {
                this.SchemaName = String.Empty;
                this.TableName = this.tableType.Name;
            }

            this.FullyQualifiedTableName = $"{(this.SchemaName == null ? String.Empty : $"{this.SchemaName}.")}{this.TableName}";
            this.AutoMap();
        }

        public string FullyQualifiedTableName { get; private set; }

        /// <summary>
        /// Gets the schema to use when referring to the corresponding table name in the database.
        /// </summary>
        public string SchemaName { get; private set; }

        /// <summary>
        /// Gets or sets the table to use in the database.
        /// </summary>
        public string TableName { get; private set; }

        public Type EntityType
        {
            get { return typeof(T); }
        }

        /// <summary>
        /// A collection of properties that will map to columns in the database table.
        /// </summary>
        public IList<IPropertyMap> Properties { get; private set; }

        public virtual void Map()
        {
            AutoMap();
        }

        protected void AutoMap()
        {
            var type = typeof(T);
            foreach (var propertyInfo in type.GetProperties())
            {
                if (Properties.Any(p => p.Name.Equals(propertyInfo.Name, StringComparison.OrdinalIgnoreCase)))
                {
                    continue;
                }

                Properties.Add(new PropertyMap(propertyInfo));
            }
        }
    }
}
