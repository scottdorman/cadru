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

using System;
using System.Collections.Generic;
using System.Linq;

using Cadru.Polly.Resources;

using Polly;

namespace Cadru.Polly.Sql
{
    /// <summary>
    /// Represents the policies used for performing database operations.
    /// </summary>
    public abstract class SqlStrategy : ISqlStrategy
    {
        /// <summary>
        /// The asynchronous policy key.
        /// </summary>
        public const string AsyncPolicyKey = "SQLAsync";

        /// <summary>
        /// The synchronous policy key.
        /// </summary>
        public const string SyncPolicyKey = "SQL";

        private IAsyncPolicy asyncPolicy;

        private ISyncPolicy syncPolicy;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlStrategy"/> class.
        /// </summary>
        protected SqlStrategy()
        {
            this.asyncPolicy = NoOpAsync();
            this.syncPolicy = NoOp();
        }

        /// <inheritdoc/>
        public IAsyncPolicy AsyncPolicy
        {
            get => this.asyncPolicy;
            private set => this.asyncPolicy = value ?? NoOpAsync();
        }

        /// <inheritdoc/>
        public ISyncPolicy SyncPolicy
        {
            get => this.syncPolicy;
            private set => this.syncPolicy = value ?? NoOp();
        }

        internal IList<IsPolicy> Policies { get; } = new List<IsPolicy>();

        /// <summary>
        /// Builds a NoOp <see cref="SyncPolicy"/> that will execute without any custom behavior.
        /// </summary>
        /// <returns>The policy instance.</returns>
        public static ISyncPolicy NoOp() => Policy.NoOp().WithPolicyKey(SyncPolicyKey);

        /// <summary>
        /// Builds a NoOp <see cref="AsyncPolicy"/> that will execute without any custom behavior.
        /// </summary>
        /// <returns>The policy instance.</returns>
        public static IAsyncPolicy NoOpAsync() => Policy.NoOpAsync().WithPolicyKey(AsyncPolicyKey);

        ISqlStrategy ISqlStrategy.BuildPolicies()
        {
            if (this.Policies.Any())
            {
                this.Validate();

                // The order of policies into the list is important (not mandatory) in order to get a consistent resilience strategy.
                // https://github.com/App-vNext/Polly/wiki/PolicyWrap#usage-recommendations
                this.SyncPolicy = Policy.Wrap(this.GetOrderedSyncPolicies().ToArray()).WithPolicyKey(SyncPolicyKey);
                this.AsyncPolicy = Policy.WrapAsync(this.GetOrderedAsyncPolicies().ToArray()).WithPolicyKey(AsyncPolicyKey);
            }

            return this;
        }

        /// <summary>
        /// Gets the ordered set of asynchronous policies to build.
        /// </summary>
        /// <returns>An ordered set of asynchronous policies.</returns>
        protected virtual IOrderedEnumerable<IAsyncPolicy> GetOrderedAsyncPolicies()
        {
            return this.Policies.OfType<IAsyncPolicy>().OrderBy(x => x.PolicyKey);
        }

        /// <summary>
        /// Gets the ordered set of synchronous policies to build.
        /// </summary>
        /// <returns>An ordered set of synchronous policies.</returns>
        protected virtual IOrderedEnumerable<ISyncPolicy> GetOrderedSyncPolicies()
        {
            return this.Policies.OfType<ISyncPolicy>().OrderBy(x => x.PolicyKey);
        }

        /// <summary>
        /// Validates the policies before building them.
        /// </summary>
        protected void Validate()
        {
            if (this.Policies.GroupBy(x => x.PolicyKey).Any(g => g.Count() > 1))
            {
                throw new InvalidOperationException(Strings.SqlServer_DuplicatedPolicies);
            }

            this.ValidatePolicies();
        }

        /// <summary>
        /// When overridden in a derived class, provides additional validation
        /// for the policies before building them.
        /// </summary>
        protected abstract void ValidatePolicies();
    }
}