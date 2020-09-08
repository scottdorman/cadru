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

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Cadru.Contracts;
using Cadru.Data.Dapper.Predicates;

using Dapper;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Cadru.Data.Dapper
{
    public abstract class DatabaseObject<TEntity> : IDatabaseObject where TEntity : class
    {
        protected readonly IObjectMap objectMap;

        protected DatabaseObject(IDapperContext database)
        {
            this.Database = database;
            this.objectMap = database.MapObject<TEntity>(this.ObjectType);
            this.UnderlyingType = typeof(TEntity);
            this.CommandBuilder = new DapperCommandBuilder(this);
        }

        protected void LogCommandDefinition(CommandDefinition commandDefinition)
        {
            if (this.Database.Options.Logging.CommandDefinitionLoggingEnabled)
            {
                var jsonString = JsonConvert.SerializeObject(new JObject
                {
                    { "CommandText", commandDefinition.CommandText },
                    { "CommandType", commandDefinition.CommandType?.ToString() ?? String.Empty },
                    { "CommandTimeout", commandDefinition.CommandTimeout },
                    { "HasTransaction", commandDefinition.Transaction != null },
                });

                this.Database.Logger.LogDebug(jsonString);
            }
        }

        ICommandAdapter IDatabaseObject.CommandAdapter => this.Database.CommandAdapter;
        IObjectMap IDatabaseObject.ObjectMap => this.objectMap;

        public string Schema => this.objectMap.Schema;

        public string ObjectName => this.objectMap.ObjectName;

        public string FullyQualifiedObjectName => this.objectMap.FullyQualifiedObjectName;

        public abstract DatabaseObjectType ObjectType { get; }

        public Type UnderlyingType { get; private set; }

        protected IDapperContext Database { get; private set; }

        protected IDapperCommandBuilder CommandBuilder { get; set; }

        /// <summary>
        /// Gets the first record which matches the predicate, if provided.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual TEntity First(IPredicate? predicate = null, int? commandTimeout = null)
        {
            var command = this.CommandBuilder.GetSelectTopCommand(predicate: predicate, commandTimeout: commandTimeout);
            this.LogCommandDefinition(command);

            var executionContext = this.Database.GetSyncExecutionEnvironment();
            return executionContext.Policy.Execute((context) => this.Database.Connection.QueryFirstOrDefault<TEntity>(command), executionContext.Context);
        }

        /// <summary>
        /// Gets the first record which matches the predicate, if provided.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> FirstAsync(IPredicate? predicate = null, int? commandTimeout = null, CancellationToken cancellationToken = default)
        {
            var command = this.CommandBuilder.GetSelectTopCommand(predicate: predicate, commandTimeout: commandTimeout, cancellationToken: cancellationToken);
            this.LogCommandDefinition(command);

            var executionContext = this.Database.GetAsyncExecutionEnvironment();
            return await executionContext.Policy.ExecuteAsync(async (context) => await this.Database.Connection.QueryFirstOrDefaultAsync<TEntity>(command), executionContext.Context);
        }

        /// <summary>
        /// Gets the first record which matches the given predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual TEntity Get(IPredicate predicate, int? commandTimeout = null)
        {
            Requires.NotNull(predicate, "predicate");
            var command = this.CommandBuilder.GetSelectCommand(predicate: predicate, commandTimeout: commandTimeout);
            this.LogCommandDefinition(command);

            var executionContext = this.Database.GetSyncExecutionEnvironment();
            return executionContext.Policy.Execute((context) => this.Database.Connection.QueryFirstOrDefault<TEntity>(command), executionContext.Context);
        }

        /// <summary>
        /// Gets the first record which matches the given predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetAsync(IPredicate predicate, int? commandTimeout = null, CancellationToken cancellationToken = default)
        {
            Requires.NotNull(predicate, "predicate");
            var command = this.CommandBuilder.GetSelectCommand(predicate: predicate, commandTimeout: commandTimeout, cancellationToken: cancellationToken);
            this.LogCommandDefinition(command);

            var executionContext = this.Database.GetAsyncExecutionEnvironment();
            return await executionContext.Policy.ExecuteAsync(async (context) => await this.Database.Connection.QueryFirstOrDefaultAsync<TEntity>(command), executionContext.Context);
        }

        /// <summary>
        /// Gets all of the records which match the predicate, if provided.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> All(IPredicate? predicate = null, int? commandTimeout = null)
        {
            var command = this.CommandBuilder.GetSelectCommand(predicate: predicate, commandTimeout: commandTimeout);
            this.LogCommandDefinition(command);

            var executionContext = this.Database.GetSyncExecutionEnvironment();
            return executionContext.Policy.Execute((context) => this.Database.Connection.Query<TEntity>(command), executionContext.Context);
        }

        /// <summary>
        /// Gets all of the records which match the predicate, if provided.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TEntity>> AllAsync(IPredicate? predicate = null, int? commandTimeout = null, CancellationToken cancellationToken = default)
        {
            var command = this.CommandBuilder.GetSelectCommand(predicate: predicate, commandTimeout: commandTimeout, cancellationToken: cancellationToken);
            this.LogCommandDefinition(command);

            var executionContext = this.Database.GetAsyncExecutionEnvironment();
            return await executionContext.Policy.ExecuteAsync(async (context) => await this.Database.Connection.QueryAsync<TEntity>(command), executionContext.Context);
        }
    }
}
