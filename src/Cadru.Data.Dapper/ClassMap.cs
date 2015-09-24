//------------------------------------------------------------------------------
// <copyright file="ClassMap.cs"
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
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    /// Maps an entity to a table through a collection of property maps.
    /// </summary>
    public class ClassMap<T> : IClassMap where T : class
    {
        private static Dictionary<Type, KeyType> keyTypeMap = new Dictionary<Type, KeyType>
        {
            { typeof(byte), KeyType.Identity}, { typeof(byte?), KeyType.Identity },
            { typeof(sbyte), KeyType.Identity }, { typeof(sbyte?), KeyType.Identity },
            { typeof(short), KeyType.Identity }, { typeof(short?), KeyType.Identity },
            { typeof(ushort), KeyType.Identity }, { typeof(ushort?), KeyType.Identity },
            { typeof(int), KeyType.Identity }, { typeof(int?), KeyType.Identity },
            { typeof(uint), KeyType.Identity}, { typeof(uint?), KeyType.Identity },
            { typeof(long), KeyType.Identity }, { typeof(long?), KeyType.Identity },
            { typeof(ulong), KeyType.Identity }, { typeof(ulong?), KeyType.Identity },
            { typeof(Guid), KeyType.Guid }, { typeof(Guid?), KeyType.Guid },
        };

        public ClassMap(string tableName)
        {
            Properties = new List<IPropertyMap>();
            TableName = String.IsNullOrWhiteSpace(tableName) ? typeof(T).Name : tableName;
        }

        public Type EntityType
        {
            get { return typeof(T); }
        }

        /// <summary>
        /// A collection of properties that will map to columns in the database table.
        /// </summary>
        public IList<IPropertyMap> Properties { get; private set; }

        /// <summary>
        /// Gets or sets the schema to use when referring to the corresponding table name in the database.
        /// </summary>
        public string SchemaName { get; set; }

        /// <summary>
        /// Gets or sets the table to use in the database.
        /// </summary>
        public string TableName { get; set; }

        protected static Dictionary<Type, KeyType> PropertyTypeKeyTypeMapping => keyTypeMap;

        public virtual void Map()
        {
            AutoMap(null);
        }

        public virtual void Schema(string schemaName)
        {
            SchemaName = schemaName;
        }

        public virtual void Table(string tableName)
        {
            TableName = tableName;
        }

        protected void AutoMap()
        {
            AutoMap(null);
        }

        protected void AutoMap(Func<Type, PropertyInfo, bool> canMap)
        {
            var type = typeof(T);
            var hasDefinedKey = Properties.Any(p => p.KeyType != KeyType.NotAKey);
            PropertyMap keyMap = null;
            foreach (var propertyInfo in type.GetProperties())
            {
                if (Properties.Any(p => p.Name.Equals(propertyInfo.Name, StringComparison.OrdinalIgnoreCase)))
                {
                    continue;
                }

                if ((canMap != null && !canMap(type, propertyInfo)))
                {
                    continue;
                }

                if (!hasDefinedKey)
                {
                    var map = Map(propertyInfo);
                    if (String.Equals(map.PropertyInfo.Name, "id", StringComparison.OrdinalIgnoreCase))
                    {
                        keyMap = map;
                    }

                    if (keyMap == null && map.PropertyInfo.Name.EndsWith("id", StringComparison.OrdinalIgnoreCase))
                    {
                        keyMap = map;
                    }
                }
            }

            if (keyMap != null)
            {
                keyMap.KeyType = (PropertyTypeKeyTypeMapping.ContainsKey(keyMap.PropertyInfo.PropertyType)
                    ? PropertyTypeKeyTypeMapping[keyMap.PropertyInfo.PropertyType]
                    : KeyType.Assigned);
            }
        }

        /// <summary>
        /// Fluently, maps an entity property to a column
        /// </summary>
        protected PropertyMap Map(Expression<Func<T, object>> expression)
        {
            var propertyInfo = GetProperty(expression) as PropertyInfo;
            return Map(propertyInfo);
        }

        /// <summary>
        /// Fluently, maps an entity property to a column
        /// </summary>
        protected PropertyMap Map(PropertyInfo propertyInfo)
        {
            var result = new PropertyMap(propertyInfo);
            if (Properties.Any(p => p.Name.Equals(result.Name)))
            {
                throw new ArgumentException(string.Format("Duplicate mapping for property {0} detected.", result.Name));
            }

            Properties.Add(result);
            return result;
        }

        private static MemberInfo GetProperty(LambdaExpression lambda)
        {
            Expression expr = lambda;
            for (; ;)
            {
                switch (expr.NodeType)
                {
                    case ExpressionType.Lambda:
                        expr = ((LambdaExpression)expr).Body;
                        break;
                    case ExpressionType.Convert:
                        expr = ((UnaryExpression)expr).Operand;
                        break;
                    case ExpressionType.MemberAccess:
                        return ((MemberExpression)expr).Member;
                    default:
                        return null;
                }
            }
        }
    }
}
