//------------------------------------------------------------------------------
// <copyright file="DatabaseObject{TEntity}.Operations.cs"
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

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;

using Cadru.Data.Dapper.Predicates;

using Dapper;

using Microsoft.Extensions.Logging;

using Validation;

namespace Cadru.Data.Dapper
{
    public partial class DatabaseObject<TEntity>
    {
        /// <summary>
        /// Gets all of the records which matches the predicate, if provided.
        /// </summary>
        /// <param name="predicate">The predicate to match.</param>
        /// <param name="commandTimeout">
        /// An optional command timeout, in seconds, for this command.
        /// </param>
        /// <returns>
        /// An instance of <typeparamref name="TEntity"/> representing the
        /// matched record.
        /// </returns>
        public virtual IEnumerable<TEntity> All(IPredicate? predicate = null, int? commandTimeout = null)
        {
            var command = this.CommandBuilder.GetSelectCommand(predicate: predicate, commandTimeout: commandTimeout);
            this.LogCommandDefinition(command);

            var executionContext = this.Context.GetSyncExecutionEnvironment();
            OnActionStarting(new(CommandOperation.Read, this.ObjectMap, executionContext));
            var results = executionContext.Policy.Execute((context) => this.Context.Connection.Query<TEntity>(command), executionContext.Context);
            OnActionCompleted(new(CommandOperation.Read, this.ObjectMap));
            return results;
        }

        /// <summary>
        /// Gets all of the records which matches the predicate, if provided.
        /// </summary>
        /// <param name="predicate">The predicate to match.</param>
        /// <param name="commandTimeout">
        /// An optional command timeout, in seconds, for this command.
        /// </param>
        /// <param name="cancellationToken">
        /// An optional <see cref="CancellationToken"/> for this command.
        /// </param>
        /// <returns>
        /// An instance of <typeparamref name="TEntity"/> representing the
        /// matched record.
        /// </returns>
        public virtual async Task<IEnumerable<TEntity>> AllAsync(IPredicate? predicate = null, int? commandTimeout = null, CancellationToken cancellationToken = default)
        {
            var command = this.CommandBuilder.GetSelectCommand(predicate: predicate, commandTimeout: commandTimeout, cancellationToken: cancellationToken);
            this.LogCommandDefinition(command);

            var executionContext = this.Context.GetAsyncExecutionEnvironment();
            OnActionStarting(new(CommandOperation.Read, this.ObjectMap, executionContext));
            var results = await executionContext.Policy.ExecuteAsync(async (context) => await this.Context.Connection.QueryAsync<TEntity>(command), executionContext.Context);
            OnActionCompleted(new(CommandOperation.Read, this.ObjectMap));
            return results;
        }

        /// <summary>
        /// Gets the first record which matches the predicate, if provided.
        /// </summary>
        /// <param name="predicate">The predicate to match.</param>
        /// <param name="commandTimeout">
        /// An optional command timeout, in seconds, for this command.
        /// </param>
        /// <returns>
        /// An instance of <typeparamref name="TEntity"/> representing the
        /// matched record.
        /// </returns>
        public TEntity First(IPredicate? predicate = null, int? commandTimeout = null)
        {
            var command = this.CommandBuilder.GetSelectTopCommand(predicate: predicate, commandTimeout: commandTimeout);
            this.LogCommandDefinition(command);

            var executionContext = this.Context.GetSyncExecutionEnvironment();
            OnActionStarting(new(CommandOperation.Read, this.ObjectMap, executionContext));
            var results = executionContext.Policy.Execute((context) => this.Context.Connection.QueryFirstOrDefault<TEntity>(command), executionContext.Context);
            OnActionCompleted(new(CommandOperation.Read, this.ObjectMap));
            return results;
        }

        /// <summary>
        /// Gets the first record which matches the predicate, if provided.
        /// </summary>
        /// <param name="predicate">The predicate to match.</param>
        /// <param name="commandTimeout">
        /// An optional command timeout, in seconds, for this command.
        /// </param>
        /// <param name="cancellationToken">
        /// An optional <see cref="CancellationToken"/> for this command.
        /// </param>
        /// <returns>
        /// An instance of <typeparamref name="TEntity"/> representing the
        /// matched record.
        /// </returns>
        public virtual async Task<TEntity> FirstAsync(IPredicate? predicate = null, int? commandTimeout = null, CancellationToken cancellationToken = default)
        {
            var command = this.CommandBuilder.GetSelectTopCommand(predicate: predicate, commandTimeout: commandTimeout, cancellationToken: cancellationToken);
            this.LogCommandDefinition(command);

            var executionContext = this.Context.GetAsyncExecutionEnvironment();
            OnActionStarting(new(CommandOperation.Read, this.ObjectMap, executionContext));
            var results = await executionContext.Policy.ExecuteAsync(async (context) => await this.Context.Connection.QueryFirstOrDefaultAsync<TEntity>(command), executionContext.Context);
            OnActionCompleted(new(CommandOperation.Read, this.ObjectMap));
            return results;
        }

        /// <summary>
        /// Gets the record which matches the predicate, if provided.
        /// </summary>
        /// <param name="predicate">The predicate to match.</param>
        /// <param name="commandTimeout">
        /// An optional command timeout, in seconds, for this command.
        /// </param>
        /// <returns>
        /// An instance of <typeparamref name="TEntity"/> representing the
        /// matched record.
        /// </returns>
        public virtual TEntity Get(IPredicate predicate, int? commandTimeout = null)
        {
            Requires.NotNull(predicate, "predicate");
            var command = this.CommandBuilder.GetSelectCommand(predicate: predicate, commandTimeout: commandTimeout);
            this.LogCommandDefinition(command);

            var executionContext = this.Context.GetSyncExecutionEnvironment();
            OnActionStarting(new(CommandOperation.Read, this.ObjectMap, executionContext));
            var results = executionContext.Policy.Execute((context) => this.Context.Connection.QueryFirstOrDefault<TEntity>(command), executionContext.Context);
            OnActionCompleted(new(CommandOperation.Read, this.ObjectMap));
            return results;
        }

        /// <summary>
        /// Gets the record which matches the predicate, if provided.
        /// </summary>
        /// <param name="predicate">The predicate to match.</param>
        /// <param name="commandTimeout">
        /// An optional command timeout, in seconds, for this command.
        /// </param>
        /// <param name="cancellationToken">
        /// An optional <see cref="CancellationToken"/> for this command.
        /// </param>
        /// <returns>
        /// An instance of <typeparamref name="TEntity"/> representing the
        /// matched record.
        /// </returns>
        public virtual async Task<TEntity> GetAsync(IPredicate predicate, int? commandTimeout = null, CancellationToken cancellationToken = default)
        {
            Requires.NotNull(predicate, "predicate");
            var command = this.CommandBuilder.GetSelectCommand(predicate: predicate, commandTimeout: commandTimeout, cancellationToken: cancellationToken);
            this.LogCommandDefinition(command);

            var executionContext = this.Context.GetAsyncExecutionEnvironment();
            OnActionStarting(new(CommandOperation.Read, this.ObjectMap, executionContext));
            var results = await executionContext.Policy.ExecuteAsync(async (context) => await this.Context.Connection.QueryFirstOrDefaultAsync<TEntity>(command), executionContext.Context);
            OnActionCompleted(new(CommandOperation.Read, this.ObjectMap));
            return results;
        }

        /// <summary>
        /// Logs the given <see cref="CommandDefinition"/>
        /// </summary>
        /// <param name="commandDefinition">
        /// The <see cref="CommandDefinition"/> instance to be logged.
        /// </param>
        protected void LogCommandDefinition(CommandDefinition commandDefinition)
        {
            if (this.Context.Options.Logging.CommandDefinitionLoggingEnabled)
            {
                var jsonString = JsonSerializer.Serialize<object>(new
                {
                    commandDefinition.CommandText,
                    CommandType = commandDefinition.CommandType?.ToString() ?? String.Empty,
                    commandDefinition.CommandTimeout,
                    HasTransaction = commandDefinition.Transaction != null,
                });

                this.Context.Logger.LogDebug(jsonString);
            }
        }
    }
}