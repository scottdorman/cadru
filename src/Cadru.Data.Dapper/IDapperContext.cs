//------------------------------------------------------------------------------
// <copyright file="IDapperContext.cs"
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

using System.Data;

using Cadru.Data.Dapper.Configuration;

using Microsoft.Extensions.Logging;

namespace Cadru.Data.Dapper
{
    /// <summary>
    /// Represents a session with the database and can be used to query and save
    /// instances of your entities.
    /// </summary>
    public interface IDapperContext
    {
        /// <summary>
        /// Gets the <see cref="ICommandAdapter"/> used by the context to create
        /// SQL statements.
        /// </summary>
        ICommandAdapter CommandAdapter { get; }

        /// <summary>
        /// Gets the <see cref="IDbConnection"/> for the context.
        /// </summary>
        IDbConnection? Connection { get; }

        /// <summary>
        /// Gets a value indicating if the current operation as an active transaction.
        /// </summary>
        bool HasActiveTransaction { get; }

        /// <summary>
        /// Gets the instance of the <see cref="ILogger"/> used by the context.
        /// </summary>
        ILogger<IDapperContext> Logger { get; }

        /// <summary>
        /// Gets the <see cref="ObjectMappingDictionary"/> for all database
        /// objects contained in the context.
        /// </summary>
        ObjectMappingDictionary Mappings { get; }

        /// <summary>
        /// Gets the context configuration options.
        /// </summary>
        DapperContextOptions Options { get; }

        /// <summary>
        /// Starts a database transaction with the specified isolation level.
        /// </summary>
        /// <param name="ensureOpenConnection">Indicates whether the connection
        /// should be opened before starting the transaction.</param>
        /// <param name="isolationLevel">An optional isolation level under which
        /// the transaction should run.</param>
        /// <remarks>If you do not specify an isolation level, the isolation
        /// level for <see cref="IsolationLevel.ReadCommitted"/> is
        /// used.</remarks>
        void BeginTransaction(bool ensureOpenConnection, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);

        /// <summary>
        /// Starts a database transaction with the specified isolation level.
        /// </summary>
        /// <param name="isolationLevel">An optional isolation level under which the transaction should run.</param>
        /// <remarks>If you do not specify an isolation level, the isolation
        /// level for <see cref="IsolationLevel.ReadCommitted"/> is
        /// used.</remarks>
        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);

        /// <summary>
        /// Commits the database transaction, if active.
        /// </summary>
        void CommitTransaction();

        /// <summary>
        /// Rolls back a transaction from a pending state, if active.
        /// </summary>
        void RollbackTransaction();
    }
}