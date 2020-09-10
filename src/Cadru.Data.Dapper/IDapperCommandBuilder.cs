//------------------------------------------------------------------------------
// <copyright file="IDapperCommandBuilder.cs"
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
using System.Threading;

using Cadru.Data.Dapper.Predicates;

using Dapper;

namespace Cadru.Data.Dapper
{
    /// <summary>
    /// Represents a way to create <see cref="CommandDefinition" /> instances.
    /// </summary>
    public interface IDapperCommandBuilder
    {
        /// <summary>
        /// Gets a <see cref="CommandDefinition" /> representing a SQL DELETE statement.
        /// </summary>
        /// <param name="predicate">An optional <see cref="IPredicate" /> used to create the WHERE clause for this command.</param>
        /// <param name="transaction">An optional transaction for this command to participate in.</param>
        /// <param name="commandTimeout">An optional timeout (in seconds) for this command.</param>
        /// <param name="commandType">An optional <see cref="CommandType" /> for this command.</param>
        /// <param name="flags">The <see cref="CommandFlags" /> for this command.</param>
        /// <param name="cancellationToken">An optional <see cref="CancellationToken" /> for this command.</param>
        /// <returns>A <see cref="CommandDefinition" /> instance representing the SQL operation.</returns>
        CommandDefinition GetDeleteCommand(IPredicate? predicate = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a <see cref="CommandDefinition" /> representing a SQL INSERT statement.
        /// </summary>
        /// <param name="data">The object representing the data to be inserted.</param>
        /// <param name="transaction">An optional transaction for this command to participate in.</param>
        /// <param name="commandTimeout">An optional timeout (in seconds) for this command.</param>
        /// <param name="commandType">An optional <see cref="CommandType" /> for this command.</param>
        /// <param name="flags">The <see cref="CommandFlags" /> for this command.</param>
        /// <param name="cancellationToken">An optional <see cref="CancellationToken" /> for this command.</param>
        /// <returns>A <see cref="CommandDefinition" /> instance representing the SQL operation.</returns>
        CommandDefinition GetInsertCommand(object data, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a <see cref="CommandDefinition" /> representing a SQL SELECT statement.
        /// </summary>
        /// <param name="predicate">An optional <see cref="IPredicate" /> used to create the WHERE clause for this command.</param>
        /// <param name="transaction">An optional transaction for this command to participate in.</param>
        /// <param name="commandTimeout">An optional timeout (in seconds) for this command.</param>
        /// <param name="commandType">An optional <see cref="CommandType" /> for this command.</param>
        /// <param name="flags">The <see cref="CommandFlags" /> for this command.</param>
        /// <param name="cancellationToken">An optional <see cref="CancellationToken" /> for this command.</param>
        /// <returns>A <see cref="CommandDefinition" /> instance representing the SQL operation.</returns>
        CommandDefinition GetSelectCommand(IPredicate? predicate = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a <see cref="CommandDefinition" /> representing a SQL TOP(n) statement.
        /// </summary>
        /// <param name="count">An optional integer value for the TOP(n) statement.</param>
        /// <param name="predicate">An optional <see cref="IPredicate" /> used to create the WHERE clause for this command.</param>
        /// <param name="transaction">An optional transaction for this command to participate in.</param>
        /// <param name="commandTimeout">An optional timeout (in seconds) for this command.</param>
        /// <param name="commandType">An optional <see cref="CommandType" /> for this command.</param>
        /// <param name="flags">The <see cref="CommandFlags" /> for this command.</param>
        /// <param name="cancellationToken">An optional <see cref="CancellationToken" /> for this command.</param>
        /// <returns>A <see cref="CommandDefinition" /> instance representing the SQL operation.</returns>
        CommandDefinition GetSelectTopCommand(int count = 1, IPredicate? predicate = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a <see cref="CommandDefinition" /> representing a SQL UPDATE statement.
        /// </summary>
        /// <param name="data">The object representing the data to be updated.</param>
        /// <param name="predicate">An optional <see cref="IPredicate" /> used to create the WHERE clause for this command.</param>
        /// <param name="transaction">An optional transaction for this command to participate in.</param>
        /// <param name="commandTimeout">An optional timeout (in seconds) for this command.</param>
        /// <param name="commandType">An optional <see cref="CommandType" /> for this command.</param>
        /// <param name="flags">The <see cref="CommandFlags" /> for this command.</param>
        /// <param name="cancellationToken">An optional <see cref="CancellationToken" /> for this command.</param>
        /// <returns>A <see cref="CommandDefinition" /> instance representing the SQL operation.</returns>
        CommandDefinition GetUpdateCommand(object data, IPredicate? predicate = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default);
    }
}