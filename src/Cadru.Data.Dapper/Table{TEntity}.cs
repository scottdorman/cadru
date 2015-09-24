//------------------------------------------------------------------------------
// <copyright file="%FileName%"
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
    using Cadru.Data.Dapper.Predicates;
    using global::Dapper;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    public partial class Table<TEntity> : ITable where TEntity : class
    {
        internal Database database;
        internal List<string> keys;
        internal ITableMap tableMap;
        static ConcurrentDictionary<Type, List<string>> paramNameCache = new ConcurrentDictionary<Type, List<string>>();

        public Table(Database database)
        {
            this.database = database;
            this.tableMap = database.DetermineTableName<TEntity>();
            this.keys = GetPrimaryKeys();
        }

        public string Schema => tableMap.SchemaName;

        public string TableName => tableMap.TableName;

        internal static List<string> GetParamNames(object o)
        {
            List<string> paramNames;
            if (!paramNameCache.TryGetValue(o.GetType(), out paramNames))
            {
                paramNames = new List<string>();
                foreach (var prop in o.GetType().GetRuntimeProperties())
                {
                    var attr = prop.GetCustomAttribute<NotMappedAttribute>(inherit: true);
                    if (attr == null)
                    {
                        paramNames.Add(prop.Name);
                    }
                }

                paramNameCache[o.GetType()] = paramNames;
            }

            return paramNames;
        }

        /// <summary>
        /// Insert a row into the db
        /// </summary>
        /// <param name="data">Either DynamicParameters or an anonymous type or concrete type</param>
        /// <returns></returns>
        public virtual void Insert(dynamic data)
        {
            var paramNames = GetParamNames((object)data);
            var parameters = new DynamicParameters(data);

            var cols = string.Join(",", paramNames);
            var cols_params = string.Join(",", paramNames.Select(p => $"@{p}"));
            var sql = $"set nocount on insert {TableName} ({cols}) values ({cols_params})";

            database.Query<int?>(sql, parameters);
        }

        /// <summary>
        /// Update a record in the DB
        /// </summary>
        /// <param name="data"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public int Update(dynamic data, IPredicate predicate)
        {
            var paramNames = tableMap.ClassMap.Properties.Where(p => p.IsUpdatable).Select(c => c.ColumnName).ToList();
            var parameters = new DynamicParameters(data);

            var builder = new StringBuilder();
            builder.Append($"update {TableName} set ");
            builder.AppendLine(string.Join(", ", paramNames.Select(p => $"{p} = @{p}")));
            AppendWhere(builder, predicate, parameters);
            return database.Execute(builder.ToString(), parameters);
        }

        private void AppendWhere(StringBuilder builder, IPredicate predicate, DynamicParameters parameters)
        {
            builder.Append(" where ");
            builder.Append(predicate.GetSql(parameters));
        }

        /// <summary>
        /// Delete a record for the DB
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Delete(dynamic data)
        {
            var paramNames = GetParamNames((object)data);
            var parameters = new DynamicParameters(data);

            var builder = new StringBuilder();
            builder.Append($"delete from {TableName}");
            AppendWhere(builder, null, parameters);
            return database.Execute(builder.ToString(), parameters) > 0;
        }

        /// <summary>
        /// Grab a record with a particular Id from the DB
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public TEntity Get(dynamic data)
        {
            var paramNames = GetParamNames((object)data);
            var parameters = new DynamicParameters(data);
            var builder = new StringBuilder();
            builder.Append($"select * from {TableName}");
            AppendWhere(builder, null, parameters);
            return database.Query<TEntity>(builder.ToString(), parameters).FirstOrDefault();
        }

        public virtual TEntity First()
        {
            return database.Query<TEntity>($"select top 1 * from {TableName}").FirstOrDefault();
        }

        public IEnumerable<TEntity> All()
        {
            return database.Query<TEntity>($"select * from {TableName}");
        }

        private static List<string> GetPrimaryKeys()
        {
            var keys = new List<string>();
            foreach (var prop in typeof(TEntity).GetRuntimeProperties())
            {
                var attr = prop.GetCustomAttribute<KeyAttribute>(inherit: true);
                if (attr != null)
                {
                    keys.Add(prop.Name);
                }
            }

            return keys;
        }
    }
}
