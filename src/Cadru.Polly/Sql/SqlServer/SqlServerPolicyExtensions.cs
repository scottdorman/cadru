//------------------------------------------------------------------------------
// <copyright file="SqlServerPolicyExtensions.cs"
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

using Microsoft.Data.SqlClient;

using Polly;

namespace Cadru.Polly.Sql.SqlServer
{
    /// <summary>
    /// Fluent API for defining <see cref="SqlException"/> error handling predicates.
    /// </summary>
    public static class SqlServerPolicyExtensions
    {
        private static readonly IExceptionHandlingStrategy<SqlException> transientExceptionStrategy = new SqlServerTransientExceptionHandlingStrategy();
        private static readonly IExceptionHandlingStrategy<SqlException> transactionExceptionStrategy = new SqlServerTransientTransactionExceptionHandlingStrategy();
        private static readonly IExceptionHandlingStrategy<SqlException> timeoutExceptionStrategy = new SqlServerTimeoutExceptionHandlingStrategy();

        /// <summary>
        /// Handle SQL Server transient and transaction errors.
        /// </summary>
        /// <returns>The <see cref="PolicyBuilder"/> instance.</returns>
        /// <remarks>
        /// This is equivalent to calling <see cref="HandleTransientSqlError"/>,
        /// <see cref="HandleTransientTransactionSqlError"/>, and
        /// <see cref="HandleSqlTimeoutError"/>.
        /// </remarks>
        public static PolicyBuilder HandleAllTransientSqlErrors()
        {
            return Policy.Handle<SqlException>(transientExceptionStrategy.ShouldHandle)
                .Or<SqlException>(transactionExceptionStrategy.ShouldHandle)
                .Or<SqlException>(timeoutExceptionStrategy.ShouldHandle);
        }

        /// <summary>
        /// Handle SQL Server transient errors.
        /// </summary>
        /// <returns>The <see cref="PolicyBuilder"/> instance.</returns>
        public static PolicyBuilder HandleTransientSqlError()
        {
            return Policy.Handle<SqlException>(transientExceptionStrategy.ShouldHandle);
        }

        /// <summary>
        /// Handle SQL Server transient transaction errors.
        /// </summary>
        /// <returns>The <see cref="PolicyBuilder"/> instance.</returns>
        public static PolicyBuilder HandleTransientTransactionSqlError()
        {
            return Policy.Handle<SqlException>(transactionExceptionStrategy.ShouldHandle);
        }

        /// <summary>
        /// Handle SQL Server timeout errors.
        /// </summary>
        /// <returns>The <see cref="PolicyBuilder"/> instance.</returns>
        public static PolicyBuilder HandleSqlTimeoutError()
        {
            return Policy.Handle<SqlException>(timeoutExceptionStrategy.ShouldHandle);
        }

        /// <summary>
        /// Handle a SQL Server error with the provided error number.
        /// </summary>
        /// <param name="handledExceptionNumber">The SQL error number to handle.</param>
        /// <returns>The <see cref="PolicyBuilder"/> instance.</returns>
        public static PolicyBuilder HandleSqlError(int handledExceptionNumber)
        {
            return Policy.Handle<SqlException>(sqlException => sqlException.Number == handledExceptionNumber);
        }
    }
}