//------------------------------------------------------------------------------
// <copyright file="Table{TEntity}.Operations.cs"
//  company="Scott Dorman"
//  library="Cadru">
//    Copyright (C) 2001-2020 Scott Dorman.
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

using Cadru.Data.Dapper.Predicates;

using Dapper;

using Validation;

namespace Cadru.Data.Dapper
{
    partial class Table<TEntity>
    {
        /// <summary>
        /// Deletes all records which match the given predicate.
        /// </summary>
        /// <param name="predicate">The predicate to match.</param>
        /// <param name="commandTimeout">
        /// An optional command timeout, in seconds, for this command.
        /// </param>
        /// <returns>The number of records deleted.</returns>
        public virtual int Delete(IPredicate predicate, int? commandTimeout = null)
        {
            Requires.NotNull(predicate, "predicate");
            var command = this.CommandBuilder.GetSelectCommand(predicate: predicate, commandTimeout: commandTimeout);
            this.LogCommandDefinition(command);

            var executionContext = this.Context.GetSyncExecutionEnvironment();
            OnActionStarting(new(CommandOperation.Delete, this.ObjectMap, executionContext));
            var results = executionContext.Policy.Execute((context) => this.Context.Connection.Execute(command), executionContext.Context);
            OnActionCompleted(new(CommandOperation.Delete, this.ObjectMap));
            return results;
        }

        /// <summary>
        /// Deletes all records which match the given predicate.
        /// </summary>
        /// <param name="predicate">The predicate to match.</param>
        /// <param name="commandTimeout">
        /// An optional command timeout, in seconds, for this command.
        /// </param>
        /// <param name="cancellationToken">
        /// An optional <see cref="CancellationToken"/> for this command.
        /// </param>
        /// <returns>The number of records deleted.</returns>
        public virtual async Task<int> DeleteAsync(IPredicate predicate, int? commandTimeout = null, CancellationToken cancellationToken = default)
        {
            Requires.NotNull(predicate, "predicate");
            var command = this.CommandBuilder.GetSelectCommand(predicate: predicate, commandTimeout: commandTimeout, cancellationToken: cancellationToken);
            this.LogCommandDefinition(command);

            var executionContext = this.Context.GetAsyncExecutionEnvironment();
            OnActionStarting(new(CommandOperation.Delete, this.ObjectMap, executionContext));
            var results = await executionContext.Policy.ExecuteAsync(async (context) => await this.Context.Connection.ExecuteAsync(command), executionContext.Context);
            OnActionCompleted(new(CommandOperation.Delete, this.ObjectMap));
            return results;
        }

        /// <summary>
        /// Inserts a row into the database.
        /// </summary>
        /// <param name="data">
        /// A <see cref="DynamicParameters"/> bag, an anonymous type, or a
        /// concrete type representing the data to be inserted.
        /// </param>
        /// <returns></returns>
        public virtual int? Insert(dynamic data)
        {
            Requires.NotNull(data, "data");
            CommandDefinition command = this.CommandBuilder.GetInsertCommand(data);
            this.LogCommandDefinition(command);

            var executionContext = this.Context.GetSyncExecutionEnvironment();
            OnActionStarting(new(CommandOperation.Insert, this.ObjectMap, executionContext));
            var results = executionContext.Policy.Execute((context) => this.Context.Connection.QuerySingle<int?>(command), executionContext.Context);
            OnActionCompleted(new(CommandOperation.Insert, this.ObjectMap));
            return results;
        }

        /// <summary>
        /// Inserts a row into the database.
        /// </summary>
        /// <param name="data">
        /// A concrete type representing the data to be inserted.
        /// </param>
        /// <returns></returns>
        public virtual int? Insert(TEntity data)
        {
            Requires.NotNull(data, "data");
            var command = this.CommandBuilder.GetInsertCommand(data);
            this.LogCommandDefinition(command);

            var executionContext = this.Context.GetSyncExecutionEnvironment();
            OnActionStarting(new(CommandOperation.Insert, this.ObjectMap, executionContext));
            var results = executionContext.Policy.Execute((context) => this.Context.Connection.QuerySingle<int?>(command), executionContext.Context);
            OnActionCompleted(new(CommandOperation.Insert, this.ObjectMap));
            return results;
        }

        /// <summary>
        /// Inserts a row into the database.
        /// </summary>
        /// <param name="data">
        /// A <see cref="DynamicParameters"/> bag, an anonymous type, or a
        /// concrete type representing the data to be inserted.
        /// </param>
        /// <param name="cancellationToken">
        /// An optional <see cref="CancellationToken"/> for this command.
        /// </param>
        /// <returns></returns>
        public virtual async Task<int?> InsertAsync(dynamic data, CancellationToken cancellationToken = default)
        {
            Requires.NotNull(data, "data");
            CommandDefinition command = this.CommandBuilder.GetInsertCommand(data, cancellationToken: cancellationToken);
            this.LogCommandDefinition(command);

            var executionContext = this.Context.GetAsyncExecutionEnvironment();
            OnActionStarting(new(CommandOperation.Insert, this.ObjectMap, executionContext));
            var results = await executionContext.Policy.ExecuteAsync(async (context) => await this.Context.Connection.QuerySingleAsync<int?>(command), executionContext.Context);
            OnActionCompleted(new(CommandOperation.Insert, this.ObjectMap));
            return results;
        }

        /// <summary>
        /// Inserts a row into the database.
        /// </summary>
        /// <param name="data">
        /// A concrete type representing the data to be inserted.
        /// </param>
        /// <param name="cancellationToken">
        /// An optional <see cref="CancellationToken"/> for this command.
        /// </param>
        /// <returns></returns>
        public virtual async Task<int?> InsertAsync(TEntity data, CancellationToken cancellationToken = default)
        {
            Requires.NotNull(data, "data");
            var command = this.CommandBuilder.GetInsertCommand(data, cancellationToken: cancellationToken);
            this.LogCommandDefinition(command);

            var executionContext = this.Context.GetAsyncExecutionEnvironment();
            OnActionStarting(new(CommandOperation.Insert, this.ObjectMap, executionContext));
            var results = await executionContext.Policy.ExecuteAsync(async (context) => await this.Context.Connection.QuerySingleAsync<int?>(command), executionContext.Context);
            OnActionCompleted(new(CommandOperation.Insert, this.ObjectMap));
            return results;
        }

        /// <summary>
        /// Updates a row into the database.
        /// </summary>
        /// <param name="data">
        /// A concrete type representing the data to be updated.
        /// </param>
        /// <param name="predicate">The predicate to match.</param>
        /// <returns></returns>
        public virtual int Update(TEntity data, IPredicate predicate)
        {
            Requires.NotNull(data, "data");
            Requires.NotNull(predicate, "predicate");
            var command = this.CommandBuilder.GetUpdateCommand(data, predicate: predicate);
            this.LogCommandDefinition(command);

            var executionContext = this.Context.GetSyncExecutionEnvironment();
            OnActionStarting(new(CommandOperation.Update, this.ObjectMap, executionContext));
            var results = executionContext.Policy.Execute((context) => this.Context.Connection.Execute(command), executionContext.Context);
            OnActionCompleted(new(CommandOperation.Update, this.ObjectMap));
            return results;
        }

        /// <summary>
        /// Updates a row into the database.
        /// </summary>
        /// <param name="data">
        /// A <see cref="DynamicParameters"/> bag, an anonymous type, or a
        /// concrete type representing the data to be updated.
        /// </param>
        /// <param name="predicate">The predicate to match.</param>
        /// <returns></returns>
        public virtual int Update(dynamic data, IPredicate predicate)
        {
            Requires.NotNull(data, "data");
            Requires.NotNull(predicate, "predicate");
            CommandDefinition command = this.CommandBuilder.GetUpdateCommand(data, predicate: predicate);
            this.LogCommandDefinition(command);

            var executionContext = this.Context.GetSyncExecutionEnvironment();
            OnActionStarting(new(CommandOperation.Update, this.ObjectMap, executionContext));
            var results = executionContext.Policy.Execute((context) => this.Context.Connection.Execute(command), executionContext.Context);
            OnActionCompleted(new(CommandOperation.Update, this.ObjectMap));
            return results;
        }

        /// <summary>
        /// Updates a row into the database.
        /// </summary>
        /// <param name="data">
        /// A concrete type representing the data to be updated.
        /// </param>
        /// <param name="predicate">The predicate to match.</param>
        /// <param name="cancellationToken">
        /// An optional <see cref="CancellationToken"/> for this command.
        /// </param>
        /// <returns></returns>
        public virtual async Task<int> UpdateAsync(TEntity data, IPredicate predicate, CancellationToken cancellationToken = default)
        {
            Requires.NotNull(data, "data");
            Requires.NotNull(predicate, "predicate");
            var command = this.CommandBuilder.GetUpdateCommand(data, predicate: predicate, cancellationToken: cancellationToken);
            this.LogCommandDefinition(command);

            var executionContext = this.Context.GetAsyncExecutionEnvironment();
            OnActionStarting(new(CommandOperation.Update, this.ObjectMap, executionContext));
            var results = await executionContext.Policy.ExecuteAsync(async (context) => await this.Context.Connection.ExecuteAsync(command), executionContext.Context);
            OnActionCompleted(new(CommandOperation.Update, this.ObjectMap));
            return results;
        }

        /// <summary>
        /// Updates a row into the database.
        /// </summary>
        /// <param name="data">
        /// A <see cref="DynamicParameters"/> bag, an anonymous type, or a
        /// concrete type representing the data to be updated.
        /// </param>
        /// <param name="predicate">The predicate to match.</param>
        /// <param name="cancellationToken">
        /// An optional <see cref="CancellationToken"/> for this command.
        /// </param>
        /// <returns></returns>
        public virtual async Task<int> UpdateAsync(dynamic data, IPredicate predicate, CancellationToken cancellationToken = default)
        {
            Requires.NotNull(data, "data");
            Requires.NotNull(predicate, "predicate");
            CommandDefinition command = this.CommandBuilder.GetUpdateCommand(data, predicate: predicate, cancellationToken: cancellationToken);
            this.LogCommandDefinition(command);

            var executionContext = this.Context.GetAsyncExecutionEnvironment();
            OnActionStarting(new(CommandOperation.Update, this.ObjectMap, executionContext));
            var results = await executionContext.Policy.ExecuteAsync(async (context) => await this.Context.Connection.ExecuteAsync(command), executionContext.Context);
            OnActionCompleted(new(CommandOperation.Update, this.ObjectMap));
            return results;
        }
    }
}