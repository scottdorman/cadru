//------------------------------------------------------------------------------
// <copyright file="SqlServerTransientErrorDetector.cs"
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
using System.Diagnostics.CodeAnalysis;

using Cadru.Polly.Resources;

using Microsoft.Data.SqlClient;

namespace Cadru.Polly.Data.SqlServer
{
    /// <summary>
    /// Defines a strategy for determining transient SQL Server errors.
    /// </summary>
    public class SqlServerTransientExceptionHandlingStrategy : IExceptionHandlingStrategy
    {
        /// <inheritdoc/>
        public bool IsDefaultStrategy => true;

        /// <inheritdoc/>
        public bool ShouldHandle([NotNull] Exception exception)
        {
            if (exception is SqlException sqlException)
            {
                foreach (SqlError err in sqlException.Errors)
                {
                    switch (err.Number)
                    {
                        // SQL Error Code: 40501
                        // The service is currently busy. Retry the request after 10 seconds. Code: (reason code to be decoded).
                        case ThrottlingCondition.ThrottlingErrorNumber:
                            // Decode the reason code from the error message to determine the grounds for throttling.
                            var condition = ThrottlingCondition.FromError(err);

                            // Attach the decoded values as additional attributes to the original SQL exception.
                            exception.Data[condition.ThrottlingMode.GetType().Name] = condition.ThrottlingMode.ToString();
                            exception.Data[condition.GetType().Name] = condition;
                            return true;

                        case 0:
                            if ((err.Class == 20 || err.Class == 11) && err.State == 0 && err.Server != null && exception.InnerException == null && String.Equals(err.Message, Strings.SQL_SevereError, StringComparison.CurrentCultureIgnoreCase))
                            {
                                return true;
                            }
                            return false;

                        // SQL Error Code: 49920
                        // Cannot process request. Too many operations in progress for subscription "%ld".
                        // The service is busy processing multiple requests for this subscription.
                        // Requests are currently blocked for resource optimization. Query sys.dm_operation_status for operation status.
                        // Wait until pending requests are complete or delete one of your pending requests and retry your request later.
                        case 49920:
                        // SQL Error Code: 49919
                        // Cannot process create or update request. Too many create or update operations in progress for subscription "%ld".
                        // The service is busy processing multiple create or update requests for your subscription or server.
                        // Requests are currently blocked for resource optimization. Query sys.dm_operation_status for pending operations.
                        // Wait till pending create or update requests are complete or delete one of your pending requests and
                        // retry your request later.
                        case 49919:
                        // SQL Error Code: 49918
                        // Cannot process request. Not enough resources to process request.
                        // The service is currently busy.Please retry the request later.
                        case 49918:
                        // SQL Error Code: 41839
                        // Transaction exceeded the maximum number of commit dependencies.
                        case 41839:
                        // SQL Error Code: 41325
                        // The current transaction failed to commit due to a serializable validation failure.
                        case 41325:
                        // SQL Error Code: 41305
                        // The current transaction failed to commit due to a repeatable read validation failure.
                        case 41305:
                        // SQL Error Code: 41302
                        // The current transaction attempted to update a record that has been updated since the transaction started.
                        case 41302:
                        // SQL Error Code: 41301
                        // Dependency failure: a dependency was taken on another transaction that later failed to commit.
                        case 41301:
                        // SQL Error Code: 40613
                        // Database XXXX on server YYYY is not currently available. Please retry the connection later.
                        // If the problem persists, contact customer support, and provide them the session tracing ID of ZZZZZ.
                        case 40613:
                        // SQL Error Code: 40197
                        // The service has encountered an error processing your request. Please try again.
                        case 40197:
                        // SQL Error Code: 10936
                        // Resource ID : %d. The request limit for the elastic pool is %d and has been reached.
                        // See 'http://go.microsoft.com/fwlink/?LinkId=267637' for assistance.
                        case 10936:
                        // SQL Error Code: 10929
                        // Resource ID: %d. The %s minimum guarantee is %d, maximum limit is %d and the current usage for the database is %d.
                        // However, the server is currently too busy to support requests greater than %d for this database.
                        // For more information, see http://go.microsoft.com/fwlink/?LinkId=267637. Otherwise, please try again.
                        case 10929:
                        // SQL Error Code: 10928
                        // Resource ID: %d. The %s limit for the database is %d and has been reached. For more information,
                        // see http://go.microsoft.com/fwlink/?LinkId=267637.
                        case 10928:
                        // SQL Error Code: 10060
                        // A network-related or instance-specific error occurred while establishing a connection to SQL Server.
                        // The server was not found or was not accessible. Verify that the instance name is correct and that SQL Server
                        // is configured to allow remote connections. (provider: TCP Provider, error: 0 - A connection attempt failed
                        // because the connected party did not properly respond after a period of time, or established connection failed
                        // because connected host has failed to respond.)"
                        case 10060:
                        // SQL Error Code: 10054
                        // A transport-level error has occurred when sending the request to the server.
                        // (provider: TCP Provider, error: 0 - An existing connection was forcibly closed by the remote host.)
                        case 10054:
                        // SQL Error Code: 10053
                        // A transport-level error has occurred when receiving results from the server.
                        // An established connection was aborted by the software in your host machine.
                        case 10053:
                        // SQL Error Code: 4221
                        // Login to read-secondary failed due to long wait on 'HADR_DATABASE_WAIT_FOR_TRANSITION_TO_VERSIONING'.
                        // The replica is not available for login because row versions are missing for transactions that were
                        // in-flight when the replica was recycled. The issue can be resolved by rolling back or committing the
                        // active transactions on the primary replica. Occurrences of this condition can be minimized by
                        // avoiding long write transactions on the primary.
                        case 4221:
                        // SQL Error Code: 4060
                        // Cannot open database "%.*ls" requested by the login. The login failed.
                        case 4060:
                        // SQL Error Code: 1205
                        // Deadlock
                        case 1205:
                        // SQL Error Code: 233
                        // The client was unable to establish a connection because of an error during connection initialization process before login.
                        // Possible causes include the following: the client tried to connect to an unsupported version of SQL Server; the
                        // server was too busy to accept new connections; or there was a resource limitation (insufficient memory or maximum
                        // allowed connections) on the server. (provider: TCP Provider, error: 0 - An existing connection was forcibly closed by
                        // the remote host.)
                        case 233:
                        // SQL Error Code: 121
                        // The semaphore timeout period has expired
                        case 121:
                        // SQL Error Code: 64
                        // A connection was successfully established with the server, but then an error occurred during the login process.
                        // (provider: TCP Provider, error: 0 - The specified network name is no longer available.)
                        case 64:
                        // DBNETLIB Error Code: 20
                        // The instance of SQL Server you attempted to connect to does not support encryption.
                        case 20:
                            return true;
                    }
                }
            }

            return false;
        }
    }

    /// <summary>
    /// Defines a strategy for determining transient SQL Server network connectivity errors.
    /// </summary>
    public class NetworkConnectivityExceptionHandlingStrategy : IExceptionHandlingStrategy
    {
        /// <inheritdoc/>
        public bool IsDefaultStrategy => false;

        /// <inheritdoc/>
        public bool ShouldHandle([NotNull] Exception exception)
        {
            if (exception is SqlException sqlException)
            {
                return sqlException.Number switch
                {
                    // SQL Error Code: 11001
                    // A network-related or instance-specific error occurred while establishing a connection to SQL Server.
                    // The server was not found or was not accessible. Verify that the instance name is correct and that SQL
                    // Server is configured to allow remote connections. (provider: TCP Provider, error: 0 - No such host is known.)
                    11001 => true,
                    _ => false,
                };
            }

            return false;
        }
    }

    /// <summary>
    /// Defines a strategy for determining transient SQL Server transaction errors.
    /// </summary>
    public class SqlServerTransientTransactionExceptionHandlingStrategy : IExceptionHandlingStrategy
    {
        /// <inheritdoc/>
        public bool IsDefaultStrategy => false;

        /// <inheritdoc/>
        public bool ShouldHandle([NotNull] Exception exception)
        {
            if (exception is SqlException sqlException)
            {
                foreach (SqlError err in sqlException.Errors)
                {
                    switch (err.Number)
                    {
                        // SQL Error Code: 40549
                        case 40549:
                        // SQL Error Code: 40550
                        case 40550:
                            return true;
                    }
                }
            }

            return false;
        }
    }

    /// <summary>
    /// Defines a strategy for determining SQL Server timeout errors.
    /// </summary>
    public class SqlServerTimeoutExceptionHandlingStrategy : IExceptionHandlingStrategy
    {
        /// <inheritdoc/>
        public bool IsDefaultStrategy => false;

        /// <inheritdoc/>
        public bool ShouldHandle([NotNull] Exception exception)
        {
            if (exception is SqlException sqlException)
            {
                foreach (SqlError err in sqlException.Errors)
                {
                    switch (err.Number)
                    {
                        // SQL Error Code: -2
                        case -2:
                            return true;
                    }
                }
            }

            return false;
        }
    }
}