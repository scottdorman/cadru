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

using Cadru.Polly;

using Microsoft.Extensions.Logging;

using Polly;

namespace Cadru.Data.Dapper
{
    public static class DapperPollyContextExtensions
    {
        public static AsyncExecutionContext GetAsyncExecutionEnvironment(this IDapperContext dapperContext)
        {
            return dapperContext.GetAsyncExecutionEnvironment(dapperContext.Logger);
        }

        public static AsyncExecutionContext GetAsyncExecutionEnvironment(this IDapperContext dapperContext, ILogger logger)
        {
            var dapperPollyContext = (IDapperPollyContext)dapperContext;
            return new AsyncExecutionContext(dapperPollyContext.SqlStrategy.AsyncPolicy, new Context().WithLogger(logger));
        }

        public static ExecutionContext GetSyncExecutionEnvironment(this IDapperContext dapperContext)
        {
            return dapperContext.GetSyncExecutionEnvironment(dapperContext.Logger);
        }

        public static ExecutionContext GetSyncExecutionEnvironment(this IDapperContext dapperContext, ILogger logger)
        {
            var dapperPollyContext = (IDapperPollyContext)dapperContext;
            return new ExecutionContext(dapperPollyContext.SqlStrategy.SyncPolicy, new Context().WithLogger(logger));
        }
    }
}