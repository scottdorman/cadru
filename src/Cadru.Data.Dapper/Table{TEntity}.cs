//------------------------------------------------------------------------------
// <copyright file="Table{TEntity}.cs"
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
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    using Cadru.Data.Dapper.Predicates;

    using Contracts;

    using global::Dapper;

    public partial class Table<TEntity> : IDatabaseObject where TEntity : class
    {
        private readonly Database database;
        private readonly IObjectMap tableMap;
        private readonly Type underlyingType = typeof(TEntity);
        private static readonly ConcurrentDictionary<Type, List<string>> paramNameCache = new ConcurrentDictionary<Type, List<string>>();

        public Table(Database database)
        {
            this.database = database;
            this.tableMap = database.MapObject<TEntity>(this.ObjectType);
        }

        public string Schema => this.tableMap.Schema;

        public string ObjectName => this.tableMap.ObjectName;

        public string FullyQualifiedObjectName => this.tableMap.FullyQualifiedObjectName;

        public DatabaseObjectType ObjectType => DatabaseObjectType.Table;

        protected Database Database => this.database;

        internal IList<string> GetParamNames(object o)
        {
            if (!paramNameCache.TryGetValue(o.GetType(), out var paramNames))
            {
                paramNames = new List<string>();
                foreach (var prop in o.GetType().GetRuntimeProperties())
                {
                    var mappedProperty = this.tableMap.Properties.SingleOrDefault(p => p.PropertyName == prop.Name);
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

        private StringBuilder InsertStatementBuilder(IList<string> paramNames)
        {
            var builder = new StringBuilder();
            builder.Append(CommandAdapter.SetNoCountOn);
            builder.Append(CommandAdapter.InsertInto);
            builder.Append(this.FullyQualifiedObjectName);
            builder.Append(CommandAdapter.SpaceLeftParenthesis);
            builder.Append(String.Join(",", paramNames));
            builder.Append(CommandAdapter.RightParenthesis);
            builder.Append(CommandAdapter.Values);
            builder.Append(CommandAdapter.LeftParenthesis);
            builder.Append(String.Join(",", paramNames.Select(p => $"@{p}")));
            builder.Append(CommandAdapter.RightParenthesis);
            return builder;
        }

        private StringBuilder UpdateStatementBuilder(IList<string> paramNames)
        {
            var builder = new StringBuilder();
            builder.Append(CommandAdapter.Update);
            builder.Append(this.FullyQualifiedObjectName);
            builder.Append(CommandAdapter.Set);
            builder.AppendLine(String.Join(", ", paramNames.Select(p => $"{p}{CommandAdapter.Equal}@{p}")));

            return builder;
        }

        /// <summary>
        /// Insert a row into the db
        /// </summary>
        /// <param name="data">Either DynamicParameters or an anonymous type or concrete type</param>
        /// <returns></returns>
        public virtual void Insert(dynamic data)
        {
            var parameters = new DynamicParameters(data);
            var builder = InsertStatementBuilder(GetParamNames(data));
            string sql = builder.ToString();
            this.database.Connection.Query<int?>(sql, param: parameters);
        }

        public virtual void Insert(TEntity data)
        {
            var parameters = new DynamicParameters(data);
            var builder = this.InsertStatementBuilder(this.GetParamNames(data));
            this.database.Connection.Query<int?>(builder.ToString(), param: parameters);
        }

        /// <summary>
        /// Insert a row into the db
        /// </summary>
        /// <param name="data">Either DynamicParameters or an anonymous type or concrete type</param>
        /// <returns></returns>
        public virtual async Task InsertAsync(dynamic data)
        {
            var parameters = new DynamicParameters(data);
            var builder = InsertStatementBuilder(GetParamNames(data));
            string sql = builder.ToString();
            await this.database.Connection.QueryAsync<int?>(sql, param: parameters);
        }

        public virtual async Task InsertAsync(TEntity data)
        {
            var parameters = new DynamicParameters(data);
            var builder = this.InsertStatementBuilder(this.GetParamNames(data));
            await this.database.Connection.QueryAsync<int?>(builder.ToString(), param: parameters);
        }

        public virtual int Update(TEntity data, IPredicate predicate)
        {
            Requires.NotNull(predicate, "predicate");
            var parameters = new DynamicParameters(data);
            var builder = this.UpdateStatementBuilder(this.GetParamNames(data));
            this.AppendWhere(builder, predicate, parameters);
            return this.database.Execute(builder.ToString(), param: parameters);
        }

        /// <summary>
        /// Update a record in the DB
        /// </summary>
        /// <param name="data"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual int Update(dynamic data, IPredicate predicate)
        {
            Requires.NotNull(predicate, "predicate");
            var parameters = new DynamicParameters(data);
            var builder = UpdateStatementBuilder(GetParamNames(data));
            this.AppendWhere(builder, predicate, parameters);
            return this.database.Execute(builder.ToString(), param: parameters);
        }

        public virtual async Task<int> UpdateAsync(TEntity data, IPredicate predicate)
        {
            Requires.NotNull(predicate, "predicate");
            var parameters = new DynamicParameters(data);
            var builder = this.UpdateStatementBuilder(this.GetParamNames(data));
            this.AppendWhere(builder, predicate, parameters);
            return await this.database.Connection.ExecuteAsync(builder.ToString(), param: parameters);
        }

        /// <summary>
        /// Update a record in the DB
        /// </summary>
        /// <param name="data"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<int> UpdateAsync(dynamic data, IPredicate predicate)
        {
            Requires.NotNull(predicate, "predicate");
            var parameters = new DynamicParameters(data);
            var builder = UpdateStatementBuilder(GetParamNames(data));
            this.AppendWhere(builder, predicate, parameters);
            string sql = builder.ToString();
            return await this.database.Connection.ExecuteAsync(sql, param: parameters);
        }

        private void AppendWhere(StringBuilder builder, IPredicate predicate, DynamicParameters parameters)
        {
            builder.Append(CommandAdapter.Where);
            builder.Append(predicate.GetSql(parameters));
        }

        private StringBuilder DeleteStatementBuilder()
        {
            var builder = new StringBuilder();
            builder.Append(CommandAdapter.DeleteFrom);
            builder.Append(this.FullyQualifiedObjectName);

            return builder;
        }

        /// <summary>
        /// Deletes all records which match the given predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual int Delete(IPredicate predicate, int? commandTimeout = null)
        {
            Requires.NotNull(predicate, "predicate");
            var parameters = new DynamicParameters();
            var builder = this.DeleteStatementBuilder();
            this.AppendWhere(builder, predicate, parameters);
            return this.database.Execute(builder.ToString(), param: parameters, commandTimeout: commandTimeout);
        }

        /// <summary>
        /// Deletes all records which match the given predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<int> DeleteAsync(IPredicate predicate, int? commandTimeout = null)
        {
            Requires.NotNull(predicate, "predicate");
            var parameters = new DynamicParameters();
            var builder = this.DeleteStatementBuilder();
            this.AppendWhere(builder, predicate, parameters);
            return await this.database.Connection.ExecuteAsync(builder.ToString(), param: parameters, commandTimeout: commandTimeout);
        }

        private StringBuilder GetStatementBuilder()
        {
            var builder = new StringBuilder();
            builder.Append("SELECT * FROM ");
            builder.Append(this.FullyQualifiedObjectName);

            return builder;
        }

        /// <summary>
        /// Gets the first record which matches the given predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual TEntity Get(IPredicate predicate, int? commandTimeout = null)
        {
            Requires.NotNull(predicate, "predicate");
            var parameters = new DynamicParameters();
            var builder = this.GetStatementBuilder();
            this.AppendWhere(builder, predicate, parameters);
            return this.database.Query<TEntity>(builder.ToString(), param: parameters, commandTimeout: commandTimeout).FirstOrDefault();
        }

        /// <summary>
        /// Gets the first record which matches the given predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetAsync(IPredicate predicate, int? commandTimeout = null)
        {
            Requires.NotNull(predicate, "predicate");
            var parameters = new DynamicParameters();
            var builder = this.GetStatementBuilder();
            this.AppendWhere(builder, predicate, parameters);
            return (await this.database.Connection.QueryAsync<TEntity>(builder.ToString(), param: parameters, commandTimeout: commandTimeout)).FirstOrDefault();
        }

        private StringBuilder FirstStatementBuilder()
        {
            var builder = new StringBuilder();
            builder.Append("SELECT TOP 1 * FROM ");
            builder.Append(this.FullyQualifiedObjectName);

            return builder;
        }

        /// <summary>
        /// Gets the first record which matches the predicate, if provided.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual TEntity First(IPredicate predicate = null, int? commandTimeout = null)
        {
            DynamicParameters parameters = null;
            var builder = this.FirstStatementBuilder();
            if (predicate != null)
            {
                parameters = new DynamicParameters();
                this.AppendWhere(builder, predicate, parameters);
            }

            return this.database.Query<TEntity>(builder.ToString(), param: parameters, commandTimeout: commandTimeout).FirstOrDefault();
        }

        /// <summary>
        /// Gets the first record which matches the predicate, if provided.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> FirstAsync(IPredicate predicate = null, int? commandTimeout = null)
        {
            DynamicParameters parameters = null;
            var builder = this.FirstStatementBuilder();
            if (predicate != null)
            {
                parameters = new DynamicParameters();
                this.AppendWhere(builder, predicate, parameters);
            }

            return (await this.database.Connection.QueryAsync<TEntity>(builder.ToString(), param: parameters, commandTimeout: commandTimeout)).FirstOrDefault();
        }

        /// <summary>
        /// Gets all of the records which match the predicate, if provided.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> All(IPredicate predicate = null, int? commandTimeout = null)
        {
            DynamicParameters parameters = null;
            var builder = this.GetStatementBuilder();
            if (predicate != null)
            {
                parameters = new DynamicParameters();
                this.AppendWhere(builder, predicate, parameters);
            }

            return this.database.Connection.Query<TEntity>(builder.ToString(), param: parameters, commandTimeout: commandTimeout);
        }

        /// <summary>
        /// Gets all of the records which match the predicate, if provided.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TEntity>> AllAsync(IPredicate predicate = null, int? commandTimeout = null)
        {
            DynamicParameters parameters = null;
            var builder = this.GetStatementBuilder();
            if (predicate != null)
            {
                parameters = new DynamicParameters();
                this.AppendWhere(builder, predicate, parameters);
            }

            return await this.database.Connection.QueryAsync<TEntity>(builder.ToString(), param: parameters, commandTimeout: commandTimeout);
        }
    }
}
