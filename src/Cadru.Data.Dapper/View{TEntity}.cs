//------------------------------------------------------------------------------
// <copyright file="View{TEntity}.cs"
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
    using System.Text;
    using System.Threading.Tasks;

    using Cadru.Data.Dapper.Predicates;

    using Contracts;

    using global::Dapper;

    public partial class View<TEntity> : IDatabaseObject where TEntity : class
    {
        private readonly Database database;
        private readonly IObjectMap viewMap;
        private readonly Type underlyingType = typeof(TEntity);
        private static readonly ConcurrentDictionary<Type, List<string>> paramNameCache = new ConcurrentDictionary<Type, List<string>>();

        public View(Database database)
        {
            this.database = database;
            this.viewMap = database.MapObject<TEntity>(this.ObjectType);
        }

        public string Schema => this.viewMap.Schema;

        public string ObjectName => this.viewMap.ObjectName;

        public string FullyQualifiedObjectName => this.viewMap.FullyQualifiedObjectName;

        public DatabaseObjectType ObjectType => DatabaseObjectType.View;

        protected Database Database => this.database;

        private void AppendWhere(StringBuilder builder, IPredicate predicate, DynamicParameters parameters)
        {
            builder.Append(CommandAdapter.Where);
            builder.Append(predicate.GetSql(parameters));
        }

        private StringBuilder GetStatementBuilder()
        {
            var builder = new StringBuilder();
            builder.Append(CommandAdapter.SelectStar);
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
            return this.database.Query<TEntity>(builder.ToString(), parameters, commandTimeout: commandTimeout).FirstOrDefault();
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
            return (await this.database.Connection.QueryAsync<TEntity>(builder.ToString(), parameters, commandTimeout: commandTimeout)).FirstOrDefault();
        }

        private StringBuilder FirstStatementBuilder()
        {
            var builder = new StringBuilder();
            builder.Append(CommandAdapter.SelectTopOne);
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
