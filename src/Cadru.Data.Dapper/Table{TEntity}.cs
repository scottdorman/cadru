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
using System.Threading;
using System.Threading.Tasks;

using Cadru.Contracts;
using Cadru.Data.Dapper.Predicates;

using Dapper;

namespace Cadru.Data.Dapper
{
    public partial class Table<TEntity> : DatabaseObject<TEntity> where TEntity : class
    {
        public Table(IDapperContext database) : base(database)
        {
        }

        public override DatabaseObjectType ObjectType => DatabaseObjectType.Table;

        /// <summary>
        /// Deletes all records which match the given predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual int Delete(IPredicate predicate, int? commandTimeout = null)
        {
            Requires.NotNull(predicate, "predicate");
            var command = this.CommandBuilder.GetSelectCommand(predicate: predicate, commandTimeout: commandTimeout);
            this.LogCommandDefinition(command);

            var executionContext = this.Database.GetSyncExecutionEnvironment();
            return executionContext.Policy.Execute((context) => this.Database.Connection.Execute(command), executionContext.Context);
        }

        /// <summary>
        /// Deletes all records which match the given predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<int> DeleteAsync(IPredicate predicate, int? commandTimeout = null, CancellationToken cancellationToken = default)
        {
            Requires.NotNull(predicate, "predicate");
            var command = this.CommandBuilder.GetSelectCommand(predicate: predicate, commandTimeout: commandTimeout, cancellationToken: cancellationToken);
            this.LogCommandDefinition(command);

            var executionContext = this.Database.GetAsyncExecutionEnvironment();
            return await executionContext.Policy.ExecuteAsync(async (context) => await this.Database.Connection.ExecuteAsync(command), executionContext.Context);
        }

        /// <summary>
        /// Insert a row into the db
        /// </summary>
        /// <param name="data">Either DynamicParameters or an anonymous type or concrete type</param>
        /// <returns></returns>
        public virtual int? Insert(dynamic data)
        {
            Requires.NotNull(data, "data");
            CommandDefinition command = this.CommandBuilder.GetInsertCommand(data);
            this.LogCommandDefinition(command);

            var executionContext = this.Database.GetSyncExecutionEnvironment();
            return executionContext.Policy.Execute((context) => this.Database.Connection.QuerySingle<int?>(command), executionContext.Context);
        }

        public virtual int? Insert(TEntity data)
        {
            Requires.NotNull(data, "data");
            var command = this.CommandBuilder.GetInsertCommand(data);
            this.LogCommandDefinition(command);

            var executionContext = this.Database.GetSyncExecutionEnvironment();
            return executionContext.Policy.Execute((context) => this.Database.Connection.QuerySingle<int?>(command), executionContext.Context);
        }

        /// <summary>
        /// Insert a row into the db
        /// </summary>
        /// <param name="data">Either DynamicParameters or an anonymous type or concrete type</param>
        /// <returns></returns>
        public virtual async Task<int?> InsertAsync(dynamic data, CancellationToken cancellationToken = default)
        {
            Requires.NotNull(data, "data");
            CommandDefinition command = this.CommandBuilder.GetInsertCommand(data, cancellationToken: cancellationToken);
            this.LogCommandDefinition(command);

            var executionContext = this.Database.GetAsyncExecutionEnvironment();
            return await executionContext.Policy.ExecuteAsync(async (context) => await this.Database.Connection.QuerySingleAsync<int?>(command), executionContext.Context);
        }

        public virtual async Task<int?> InsertAsync(TEntity data, CancellationToken cancellationToken = default)
        {
            Requires.NotNull(data, "data");
            var command = this.CommandBuilder.GetInsertCommand(data, cancellationToken: cancellationToken);
            this.LogCommandDefinition(command);

            var executionContext = this.Database.GetAsyncExecutionEnvironment();
            return await executionContext.Policy.ExecuteAsync(async (context) => await this.Database.Connection.QuerySingleAsync<int?>(command), executionContext.Context);
        }

        public virtual int Update(TEntity data, IPredicate predicate)
        {
            Requires.NotNull(data, "data");
            Requires.NotNull(predicate, "predicate");
            var command = this.CommandBuilder.GetUpdateCommand(data, predicate: predicate);
            this.LogCommandDefinition(command);

            var executionContext = this.Database.GetSyncExecutionEnvironment();
            return executionContext.Policy.Execute((context) => this.Database.Connection.Execute(command), executionContext.Context);
        }

        /// <summary>
        /// Update a record in the DB
        /// </summary>
        /// <param name="data"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual int Update(dynamic data, IPredicate predicate)
        {
            Requires.NotNull(data, "data");
            Requires.NotNull(predicate, "predicate");
            CommandDefinition command = this.CommandBuilder.GetUpdateCommand(data, predicate: predicate);
            this.LogCommandDefinition(command);

            var executionContext = this.Database.GetSyncExecutionEnvironment();
            return executionContext.Policy.Execute((context) => this.Database.Connection.Execute(command), executionContext.Context);
        }

        public virtual async Task<int> UpdateAsync(TEntity data, IPredicate predicate, CancellationToken cancellationToken = default)
        {
            Requires.NotNull(data, "data");
            Requires.NotNull(predicate, "predicate");
            var command = this.CommandBuilder.GetUpdateCommand(data, predicate: predicate, cancellationToken: cancellationToken);
            this.LogCommandDefinition(command);

            var executionContext = this.Database.GetAsyncExecutionEnvironment();
            return await executionContext.Policy.ExecuteAsync(async (context) => await this.Database.Connection.ExecuteAsync(command), executionContext.Context);
        }

        /// <summary>
        /// Update a record in the DB
        /// </summary>
        /// <param name="data"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<int> UpdateAsync(dynamic data, IPredicate predicate, CancellationToken cancellationToken = default)
        {
            Requires.NotNull(data, "data");
            Requires.NotNull(predicate, "predicate");
            CommandDefinition command = this.CommandBuilder.GetUpdateCommand(data, predicate: predicate, cancellationToken: cancellationToken);
            this.LogCommandDefinition(command);

            var executionContext = this.Database.GetAsyncExecutionEnvironment();
            return await executionContext.Policy.ExecuteAsync(async (context) => await this.Database.Connection.ExecuteAsync(command), executionContext.Context);
        }
    }
}
