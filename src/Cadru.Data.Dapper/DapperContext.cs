//------------------------------------------------------------------------------
// <copyright file="DapperContext.cs"
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
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using Cadru.Data.Dapper.Configuration;
using Cadru.Extensions;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Cadru.Data.Dapper
{
    /// <summary>
    /// A <see cref="DapperContext"/> represents a session with the database and
    /// can be used to query and save instances of your entities.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Typically you create a class that derives from DbContext and contains
    /// <see cref="IDatabaseObject"/> properties for each entity in the model.
    /// </para>
    /// <para>
    /// The model is discovered by running a set of conventions over the entity
    /// classes found in the <see cref="IDatabaseObject"/> properties on the
    /// derived context.
    /// </para>
    /// </remarks>
    public abstract partial class DapperContext : IDapperContext
    {
        private readonly DapperContextBuilder contextBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="DapperContext"/> class.
        /// </summary>
        /// <param name="contextBuilder">
        /// Additional information for creating the context.
        /// </param>
        /// <param name="optionsAccessor">The context configuration options.</param>
        /// <param name="loggerFactory">
        /// An <see cref="ILoggerFactory"/> instance used to create a logger.
        /// </param>
        protected DapperContext([NotNull] DapperContextBuilder contextBuilder, IOptions<DapperContextOptions> optionsAccessor, ILoggerFactory loggerFactory)
        {
            this.Options = optionsAccessor.Value;
            this.contextBuilder = contextBuilder;
            this.Logger = loggerFactory.CreateLogger<IDapperContext>();
            this.Connection = this.contextBuilder.CreateConnection();

            var contextProperties = this.GetType().GetProperties()
                .Where(p => p.PropertyType.HasInterface<IDatabaseObject>())
                .Select(p => new
                {
                    MethodInfo = p.GetSetMethod(true),
                    p.PropertyType
                });

            foreach (var contextProperty in contextProperties)
            {
                var instance = Activator.CreateInstance(contextProperty.PropertyType, this);
                if (instance != null)
                {
                    contextProperty.MethodInfo?.Invoke(this, new[] { instance });
                }
            }
        }

        /// <inheritdoc/>
        public ICommandAdapter CommandAdapter => this.contextBuilder.CommandAdapter;

        /// <inheritdoc/>
        public IDbConnection? Connection { get; }

        /// <inheritdoc/>
        public bool HasActiveTransaction => this.Transaction != null;

        /// <inheritdoc/>
        public ILogger<IDapperContext> Logger { get; }

        /// <inheritdoc/>
        public ObjectMappingDictionary Mappings { get; } = new ObjectMappingDictionary();

        /// <inheritdoc/>
        public DapperContextOptions Options { get; internal set; }

        /// <inheritdoc/>
        public IDbTransaction? Transaction { get; private set; }

        /// <inheritdoc/>
        public void BeginTransaction(bool ensureOpenConnection, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            if (this.Connection != null)
            {
                if (ensureOpenConnection && this.Connection.State != ConnectionState.Open)
                {
                    this.Connection.Open();
                }

                this.Transaction = this.Connection.BeginTransaction(isolationLevel);
            }
        }

        /// <inheritdoc/>
        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            this.BeginTransaction(false, isolationLevel);
        }

        /// <inheritdoc/>
        public void CommitTransaction()
        {
            if (this.HasActiveTransaction)
            {
                this.Transaction!.Commit();
                this.Transaction = null;
            }
        }

        /// <inheritdoc/>
        public void RollbackTransaction()
        {
            if (this.HasActiveTransaction)
            {
                this.Transaction!.Rollback();
                this.Transaction = null;
            }
        }
    }
}