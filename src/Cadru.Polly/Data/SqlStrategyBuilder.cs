//------------------------------------------------------------------------------
// <copyright file="SqlStrategyBuilder.cs"
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

using Cadru.Contracts;
using Cadru.Polly.Resources;

using Microsoft.Extensions.Options;

using Polly;

namespace Cadru.Polly.Data
{
    /// <summary>
    /// Builder class that holds the list of current exception predicates
    /// </summary>
    public abstract class SqlStrategyBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlStrategyBuilder"/> class.
        /// </summary>
        /// <param name="exceptionHandlingStrategies">The collection of <see
        /// cref="IExceptionHandlingStrategy"/> strategies to use.</param>
        /// <param name="strategyOptionsAccessor">An optional strategy
        /// configuration.</param>
        protected SqlStrategyBuilder(IEnumerable<IExceptionHandlingStrategy> exceptionHandlingStrategies, IOptions<SqlStrategyOptions>? strategyOptionsAccessor)
        {
            Requires.NotNullOrEmpty(exceptionHandlingStrategies, nameof(exceptionHandlingStrategies));
            Requires.IsTrue(exceptionHandlingStrategies.Count(e => e.IsDefaultStrategy) == 1);

            this.ExceptionHandlingStrategies = exceptionHandlingStrategies;
            this.StrategyOptions = strategyOptionsAccessor?.Value ?? SqlStrategyOptions.Defaults;
            this.DefaultStrategy = exceptionHandlingStrategies.Single(e => e.IsDefaultStrategy);
        }

        /// <summary>
        /// Gets the list of policies which make up the strategy.
        /// </summary>
        public IList<IsPolicy> Policies { get; } = new List<IsPolicy>();

        /// <summary>
        /// Gets the <see cref="IExceptionHandlingStrategy">exception handling
        /// strategies</see> used.
        /// </summary>
        public IEnumerable<IExceptionHandlingStrategy> ExceptionHandlingStrategies { get; }

        /// <summary>
        /// Gets the <see cref="SqlStrategyOptions"/> used to configure the policies.
        /// </summary>
        public SqlStrategyOptions StrategyOptions { get; }

        /// <summary>
        /// Gets the default <see cref="IExceptionHandlingStrategy"/>.
        /// </summary>
        public IExceptionHandlingStrategy DefaultStrategy { get; }

        /// <summary>
        /// Builds a NoOp <see cref="Policy"/> that will execute without any custom behavior.
        /// </summary>
        /// <returns>The policy instance.</returns>
        public static ISyncPolicy NoOp() => Policy.NoOp();

        /// <summary>
        /// Builds a NoOp <see cref="AsyncPolicy"/> that will execute without any custom behavior.
        /// </summary>
        /// <returns>The policy instance.</returns>
        public static IAsyncPolicy NoOpAsync() => Policy.NoOpAsync();

        /// <summary>
        /// Gets a <see cref="PolicyBuilder"/> which handles all of the exceptions
        /// in <see cref="ExceptionHandlingStrategies"/>.
        /// </summary>
        /// <returns>A <see cref="PolicyBuilder"/> instance.</returns>
        public PolicyBuilder GetPolicyBuilder()
        {
            var policyBuilder = this.GetDefaultPolicyBuilder();
            foreach (var strategy in this.ExceptionHandlingStrategies.Where(e => !e.IsDefaultStrategy))
            {
                policyBuilder = policyBuilder.Or<Exception>(strategy.ShouldHandle);
            }

            return policyBuilder;
        }

        /// <summary>
        /// Gets a <see cref="PolicyBuilder"/> which handles the exceptions
        /// in the default <see cref="IExceptionHandlingStrategy"/>.
        /// </summary>
        /// <returns>A <see cref="PolicyBuilder"/> instance.</returns>
        public PolicyBuilder GetDefaultPolicyBuilder()
        {
            return Policy.Handle<Exception>(this.DefaultStrategy.ShouldHandle);
        }

        /// <summary>
        /// Builds a <see cref="ISqlStrategy"/>.
        /// </summary>
        /// <returns>The SQL strategy instance.</returns>
        public ISqlStrategy Build()
        {
            if (this.Policies.GroupBy(x => x.PolicyKey).Any(g => g.Count() > 1))
            {
                throw new InvalidOperationException(Strings.SqlServer_DuplicatedPolicies);
            }

            this.Validate();

            // The order of policies into the list is important (not mandatory) in order to get a consistent resilience strategy.
            // https://github.com/App-vNext/Polly/wiki/PolicyWrap#usage-recommendations

            var sqlStrategy = new SqlStrategy
            {
                SyncPolicy = this.GetSyncPolicies(),
                AsyncPolicy = this.GetAsyncPolicies()
            };

            return sqlStrategy;
        }

        /// <summary>
        /// Gets the ordered set of asynchronous policies to build.
        /// </summary>
        /// <returns>An ordered set of asynchronous policies.</returns>
        protected virtual IAsyncPolicy GetAsyncPolicies()
        {
            var policies = this.Policies.OfType<IAsyncPolicy>().OrderBy(x => x.PolicyKey);
            var policy = policies switch
            {
                IEnumerable<IAsyncPolicy> _ when policies.Count() > 1 => Policy.WrapAsync(policies.ToArray()),
                IEnumerable<IAsyncPolicy> _ when policies.Count() == 1 => policies.First(),
                _ => NoOpAsync()
            };

            return policy;
        }

        /// <summary>
        /// Gets the ordered set of synchronous policies to build.
        /// </summary>
        /// <returns>An ordered set of synchronous policies.</returns>
        protected virtual ISyncPolicy GetSyncPolicies()
        {
            var policies = this.Policies.OfType<ISyncPolicy>().OrderBy(x => x.PolicyKey);
            var policy = policies switch
            {
                IEnumerable<ISyncPolicy> _ when policies.Count() > 1 => Policy.Wrap(policies.ToArray()),
                IEnumerable<ISyncPolicy> _ when policies.Count() == 1 => policies.First(),
                _ => NoOp()
            };

            return policy;
        }

        /// <summary>
        /// When overridden in a derived class, provides additional validation
        /// for the policies before building them.
        /// </summary>
        protected virtual void Validate()
        {
        }
    }
}