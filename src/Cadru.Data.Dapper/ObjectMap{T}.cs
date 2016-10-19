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

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class ExtendedPropertyAttribute : Attribute
    {
        public ExtendedPropertyAttribute(string name, object value)
        {
            this.Name = name;
            this.Value = value;
        }

        public string Name { get; private set; }

        public object Value { get; private set; }
    }

    public abstract class ObjectMap<T> : IObjectMap where T : class
    {
        private Type entityType = typeof(T);
        private Dictionary<string, object> additionalValues = new Dictionary<string, object>();

        public static IObjectMap CreateMap(DatabaseObjectType databaseObjectType)
        {
            IObjectMap map = null;
            switch (databaseObjectType)
            {
                case DatabaseObjectType.Table:
                    map = new TableMap<T>();
                    break;
                case DatabaseObjectType.View:
                    map = new ViewMap<T>();
                    break;
            }

            return map;
        }

        protected ObjectMap()
        {
            Properties = new List<IPropertyMap>();
            foreach(var attribute in entityType.GetCustomAttributes<ExtendedPropertyAttribute>())
            {
                this.additionalValues.Add(attribute.Name, attribute.Value);
            }
        }

        public IReadOnlyDictionary<string, object> AdditionalValues => additionalValues;

        public string FullyQualifiedObjectName { get; internal set; }

        /// <summary>
        /// Gets the schema to use when referring to the corresponding table name in the database.
        /// </summary>
        public string Schema { get; protected set; }

        /// <summary>
        /// Gets or sets the table to use in the database.
        /// </summary>
        public string ObjectName { get; protected set; }

        public DatabaseObjectType ObjectType { get; protected set; }

        public Type EntityType
        {
            get { return this.entityType; }
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
                if (Properties.Any(p => p.PropertyName.Equals(propertyInfo.Name, StringComparison.OrdinalIgnoreCase)))
                {
                    continue;
                }

                Properties.Add(new PropertyMap(propertyInfo));
            }
        }

        protected string GetFullyQualifiedObjectName()
        {
            return $"{(String.IsNullOrWhiteSpace(this.Schema) ? String.Empty : $"[{this.Schema}].")}[{this.ObjectName}]";
        }
    }
}
