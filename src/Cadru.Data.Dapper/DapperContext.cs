//------------------------------------------------------------------------------
// <copyright file="Database.cs"
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
    using System.Data;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using Cadru.Data.Dapper.Configuration;
    using Cadru.Extensions;
    using Cadru.Polly.Data;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    public abstract partial class DapperContext : IDapperContext
    {
        protected readonly ILogger<IDapperContext> logger;
        private readonly DapperContextBuilder contextBuilder;

        protected DapperContext([NotNull] DapperContextBuilder contextBuilder, IOptions<DapperContextOptions> optionsAccessor, ILoggerFactory loggerFactory)
        {
            this.Options = optionsAccessor.Value;
            this.contextBuilder = contextBuilder;
            this.logger = loggerFactory.CreateLogger<IDapperContext>();
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
                    contextProperty.MethodInfo.Invoke(this, new[] { instance });
                }
            }
        }

        public ICommandAdapter CommandAdapter => this.contextBuilder.CommandAdapter;
        public IDbConnection? Connection { get; }
        public bool HasActiveTransaction => this.Transaction != null;
        public ILogger<IDapperContext> Logger => this.logger;
        public ObjectMappingDictionary Mappings { get; } = new ObjectMappingDictionary();
        public DapperContextOptions Options { get; internal set; }
        public IDbTransaction? Transaction { get; private set; }

        /// <summary>
        /// Starts a database transaction.
        /// </summary>
        /// <param name="ensureOpenConnection"></param>
        /// <param name="isolation">Specifies the isolation level for the transaction.</param>
        /// <remarks>If you do not specify an isolation level, the isolation level for <see cref="IsolationLevel.ReadCommitted"/> is used.</remarks>
        public void BeginTransaction(bool ensureOpenConnection, IsolationLevel isolation = IsolationLevel.ReadCommitted)
        {
            if (this.Connection != null)
            {
                if (ensureOpenConnection && this.Connection.State != ConnectionState.Open)
                {
                    this.Connection.Open();
                }

                this.Transaction = this.Connection.BeginTransaction(isolation);
            }
        }

        public void BeginTransaction(IsolationLevel isolation = IsolationLevel.ReadCommitted)
        {
            this.BeginTransaction(false, isolation);
        }

        public void CommitTransaction()
        {
            if (this.HasActiveTransaction)
            {
                this.Transaction!.Commit();
                this.Transaction = null;
            }
        }

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