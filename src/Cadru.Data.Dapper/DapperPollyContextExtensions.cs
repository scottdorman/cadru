//------------------------------------------------------------------------------
// <copyright file="DapperPollyContextExtensions.cs"
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

using Cadru.Polly;

using Microsoft.Extensions.Logging;

using Polly;

namespace Cadru.Data.Dapper
{
    /// <summary>
    /// Helper methods for getting a <see cref="PolicyExecutionEnvironment" /> from the <see cref="IDapperContext" />.
    /// </summary>
    public static class DapperPollyContextExtensions
    {
        /// <summary>
        /// Gets an <see cref="AsyncExecutionEnvironment" /> for performing
        /// asynchronous operations from the context.
        /// </summary>
        /// <param name="dapperContext">The context instance.</param>
        /// <returns>An <see cref="AsyncExecutionEnvironment" /> containing the
        /// <see cref="IAsyncPolicy" /> and <see cref="Context" />.</returns>
        public static AsyncExecutionEnvironment GetAsyncExecutionEnvironment(this IDapperContext dapperContext)
        {
            return dapperContext.GetAsyncExecutionEnvironment(dapperContext.Logger);
        }

        /// <summary>
        /// Gets an <see cref="AsyncExecutionEnvironment" /> for performing
        /// asynchronous operations from the context.
        /// </summary>
        /// <param name="dapperContext">The context instance.</param>
        /// <param name="logger">The <see cref="ILogger" /> instance to be added
        /// to the <see cref="Context" />.</param>
        /// <returns>An <see cref="AsyncExecutionEnvironment" /> containing the
        /// <see cref="IAsyncPolicy" /> and <see cref="Context" />.</returns>
        public static AsyncExecutionEnvironment GetAsyncExecutionEnvironment(this IDapperContext dapperContext, ILogger logger)
        {
            var dapperPollyContext = (IDapperPollyContext)dapperContext;
            return new AsyncExecutionEnvironment(dapperPollyContext.SqlStrategy.AsyncPolicy, new Context().WithLogger(logger));
        }

        /// <summary>
        /// Gets an <see cref="ExecutionEnvironment" /> for performing
        /// asynchronous operations from the context.
        /// </summary>
        /// <param name="dapperContext">The context instance.</param>
        /// <returns>An <see cref="ExecutionEnvironment" /> containing the
        /// <see cref="ISyncPolicy" /> and <see cref="Context" />.</returns>
        public static ExecutionEnvironment GetSyncExecutionEnvironment(this IDapperContext dapperContext)
        {
            return dapperContext.GetSyncExecutionEnvironment(dapperContext.Logger);
        }

        /// <summary>
        /// Gets an <see cref="ExecutionEnvironment" /> for performing
        /// asynchronous operations from the context.
        /// </summary>
        /// <param name="dapperContext">The context instance.</param>
        /// <param name="logger">The <see cref="ILogger" /> instance to be added
        /// to the <see cref="Context" />.</param>
        /// <returns>An <see cref="ExecutionEnvironment" /> containing the
        /// <see cref="ISyncPolicy" /> and <see cref="Context" />.</returns>
        public static ExecutionEnvironment GetSyncExecutionEnvironment(this IDapperContext dapperContext, ILogger logger)
        {
            var dapperPollyContext = (IDapperPollyContext)dapperContext;
            return new ExecutionEnvironment(dapperPollyContext.SqlStrategy.SyncPolicy, new Context().WithLogger(logger));
        }
    }
}