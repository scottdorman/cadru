//------------------------------------------------------------------------------
// <copyright file="Table{TEntity}.cs"
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
    using Contracts;
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
        internal ITableMap tableMap;
        static ConcurrentDictionary<Type, List<string>> paramNameCache = new ConcurrentDictionary<Type, List<string>>();

        public Table(Database database)
        {
            this.database = database;
            this.tableMap = database.MapTable<TEntity>();
        }

        public string Schema => tableMap.SchemaName;

        public string TableName => tableMap.TableName;

        internal IList<string> GetParamNames(object o)
        {
            List<string> paramNames;
            if (!paramNameCache.TryGetValue(o.GetType(), out paramNames))
            {
                paramNames = new List<string>();
                foreach (var prop in o.GetType().GetRuntimeProperties())
                {
                    var mappedProperty = this.tableMap.Properties.SingleOrDefault(p => p.Name == prop.Name);
                    if (mappedProperty != null)
                    {
                        if (mappedProperty.IsUpdatable)
                        {
                            paramNames.Add(mappedProperty.ColumnName);
                        }
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
            List<string> paramNames = GetParamNames(data);
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
            List<string> paramNames = this.GetParamNames(data);
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
        /// Deletes all records which match the given predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public int Delete(IPredicate predicate)
        {
            Requires.NotNull(predicate, "predicate");
            var parameters = new DynamicParameters();
            return database.Execute($"delete from {TableName} where {predicate.GetSql(parameters)}", parameters);
        }

        /// <summary>
        /// Gets the first record which matches the given predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public TEntity Get(IPredicate predicate)
        {
            Requires.NotNull(predicate, "predicate");
            var parameters = new DynamicParameters();
            return database.Query<TEntity>($"select * from {TableName} where {predicate.GetSql(parameters)}", parameters).FirstOrDefault();
        }

        /// <summary>
        /// Gets the first record.
        /// </summary>
        /// <returns></returns>
        public virtual TEntity First()
        {
            return database.Query<TEntity>($"select top 1 * from {TableName}").FirstOrDefault();
        }

        /// <summary>
        /// Gets the first record which matches the given predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual TEntity First(IPredicate predicate)
        {
            Requires.NotNull(predicate, "predicate");
            var parameters = new DynamicParameters();
            return database.Query<TEntity>($"select top 1 * from {TableName} where {predicate.GetSql(parameters)}").FirstOrDefault();
        }

        /// <summary>
        /// Gets all of the records.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TEntity> All()
        {
            return database.Query<TEntity>($"select * from {TableName}");
        }

        /// <summary>
        /// Gets all of the records which match the given predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> All(IPredicate predicate)
        {
            Requires.NotNull(predicate, "predicate");
            var parameters = new DynamicParameters();
            return database.Query<TEntity>($"select * from {TableName} where {predicate.GetSql(parameters)}", parameters);
        }
    }
}
