//------------------------------------------------------------------------------
// <copyright file="ObjectMap{T}.cs"
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
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Reflection;

    using Cadru.Data.Annotations;

    public class ObjectMap<T> : IObjectMap where T : class
    {
        private readonly Dictionary<string, object> additionalValues = new Dictionary<string, object>();
        private readonly List<IPropertyMap> properties = new List<IPropertyMap>();

        protected internal ObjectMap(ICommandAdapter commandAdapter, DatabaseObjectType databaseObjectType)
        {
            this.CommandAdapter = commandAdapter;
            this.ObjectType = databaseObjectType;
            this.Schema = String.Empty;
            this.ObjectName = this.EntityType.Name;

            switch (databaseObjectType)
            {
                case DatabaseObjectType.Table:
                    var tableAttribute = this.EntityType.GetCustomAttribute<TableAttribute>(inherit: true);
                    if (tableAttribute != null)
                    {
                        this.Schema = tableAttribute.Schema;
                        this.ObjectName = tableAttribute.Name;
                    }

                    break;

                case DatabaseObjectType.View:
                    var viewAttribute = this.EntityType.GetCustomAttribute<ViewAttribute>(inherit: true);
                    if (viewAttribute != null)
                    {
                        this.Schema = viewAttribute.Schema;
                        this.ObjectName = viewAttribute.Name;
                    }

                    break;
            }

            this.FullyQualifiedObjectName = this.GetFullyQualifiedObjectName();
            this.AutoMap();

            foreach (var attribute in this.EntityType.GetCustomAttributes<ExtendedPropertyAttribute>())
            {
                this.additionalValues.Add(attribute.Name, attribute.Value);
            }
        }

        public IReadOnlyDictionary<string, object> AdditionalValues => this.additionalValues;

        public ICommandAdapter CommandAdapter { get; private set; }

        public TypeInfo EntityType { get; } = typeof(T).GetTypeInfo();

        public string FullyQualifiedObjectName { get; internal set; }

        /// <summary>
        /// Gets the object to use in the database.
        /// </summary>
        public string ObjectName { get; }

        public DatabaseObjectType ObjectType { get; }

        /// <summary>
        /// A collection of properties that will map to columns in the database table.
        /// </summary>
        public IList<IPropertyMap> Properties => this.properties;

        /// <summary>
        /// Gets the schema to use when referring to the corresponding table name in the database.
        /// </summary>
        public string Schema { get; protected set; }

        public virtual void Map()
        {
            this.AutoMap();
        }

        internal static IObjectMap CreateMap(DatabaseObjectType databaseObjectType, ICommandAdapter commandAdapter)
        {
            return databaseObjectType switch
            {
                DatabaseObjectType.Table => new ObjectMap<T>(commandAdapter, databaseObjectType),
                DatabaseObjectType.View => new ObjectMap<T>(commandAdapter, databaseObjectType),
                _ => throw new InvalidOperationException("Unknown database object type.")
            };
        }

        protected void AutoMap()
        {
            var type = typeof(T);
            foreach (var propertyInfo in type.GetProperties())
            {
                if (this.properties.Any(p => p.PropertyName.Equals(propertyInfo.Name, StringComparison.OrdinalIgnoreCase)))
                {
                    continue;
                }

                this.properties.Add(new PropertyMap(propertyInfo));
            }
        }

        protected string GetFullyQualifiedObjectName()
        {
            return $"{(String.IsNullOrWhiteSpace(this.Schema) ? String.Empty : $"{ this.CommandAdapter.QuoteIdentifier(this.Schema)}{ this.CommandAdapter.SchemaSeparator }")}{this.CommandAdapter.QuoteIdentifier(this.ObjectName)}";
        }
    }
}
