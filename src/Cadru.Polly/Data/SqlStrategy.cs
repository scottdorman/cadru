//------------------------------------------------------------------------------
// <copyright file="SqlStrategy.cs"
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

using Polly;

namespace Cadru.Polly.Data
{
    /// <summary>
    /// Represents the policies used for performing database operations.
    /// </summary>
    public sealed class SqlStrategy : ISqlStrategy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlStrategy" /> class.
        /// </summary>
        internal SqlStrategy()
        {
            this.AsyncPolicy = NoOpAsync();
            this.SyncPolicy = NoOp();
        }

        /// <summary>
        /// Gets a <see cref="SqlStrategy" /> containing policies that will
        /// execute without any custom behavior.
        /// </summary>
        public static SqlStrategy Default => new SqlStrategy();

        /// <inheritdoc/>
        public IAsyncPolicy AsyncPolicy { get; internal set; }

        /// <inheritdoc/>
        public ISyncPolicy SyncPolicy { get; internal set; }

        /// <summary>
        /// Builds a NoOp <see cref="SyncPolicy" /> that will execute without any custom behavior.
        /// </summary>
        /// <returns>The policy instance.</returns>
        public static ISyncPolicy NoOp() => SqlStrategyBuilder.NoOp();

        /// <summary>
        /// Builds a NoOp <see cref="AsyncPolicy" /> that will execute without any custom behavior.
        /// </summary>
        /// <returns>The policy instance.</returns>
        public static IAsyncPolicy NoOpAsync() => SqlStrategyBuilder.NoOpAsync();
    }
}