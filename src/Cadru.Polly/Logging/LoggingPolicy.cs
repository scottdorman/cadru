//------------------------------------------------------------------------------
// <copyright file="LoggingPolicy.cs"
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
using System.Threading;

using Microsoft.Extensions.Logging;

using Polly;

namespace Cadru.Polly.Logging
{
    /// <summary>
    /// A Logging policy that can be applied to delegates.
    /// </summary>
    public class LoggingPolicy : Policy, ILoggingPolicy
    {
        private readonly Action<ILogger, Context, Exception> _logAction;
        private readonly Func<Context, ILogger> _loggerProvider;

        internal LoggingPolicy(PolicyBuilder policyBuilder, Func<Context, ILogger> loggerProvider, Action<ILogger, Context, Exception> logAction)
            : base(policyBuilder)
        {
            this._loggerProvider = loggerProvider ?? throw new NullReferenceException(nameof(loggerProvider));
            this._logAction = logAction ?? throw new NullReferenceException(nameof(logAction));
        }

        /// <inheritdoc/>
        protected override TResult Implementation<TResult>(Func<Context, CancellationToken, TResult> action, Context context, CancellationToken cancellationToken)
        {
            return LoggingEngine.Implementation(
                action,
                context,
                this.ExceptionPredicates,
                ResultPredicates<TResult>.None,
                this._loggerProvider,
                (logger, ctx, delegateResult) => this._logAction(logger, ctx, delegateResult.Exception)
,
                cancellationToken);
        }
    }

    /// <summary>
    /// A Logging policy that can be applied to delegates returning a value of type <typeparamref name="TResult" />
    /// </summary>
    /// <typeparam name="TResult">The type of return values this policy will handle.</typeparam>
    public class LoggingPolicy<TResult> : Policy<TResult>, ILoggingPolicy<TResult>
    {
        private readonly Action<ILogger, Context, DelegateResult<TResult>> _logAction;
        private readonly Func<Context, ILogger> _loggerProvider;

        internal LoggingPolicy(PolicyBuilder<TResult> policyBuilder, Func<Context, ILogger> loggerProvider, Action<ILogger, Context, DelegateResult<TResult>> logAction)
            : base(policyBuilder)
        {
            this._loggerProvider = loggerProvider ?? throw new NullReferenceException(nameof(loggerProvider));
            this._logAction = logAction ?? throw new NullReferenceException(nameof(logAction));
        }

        /// <inheritdoc/>
        protected override TResult Implementation(Func<Context, CancellationToken, TResult> action, Context context, CancellationToken cancellationToken)
        {
            return LoggingEngine.Implementation(
                action,
                context,
                this.ExceptionPredicates,
                this.ResultPredicates,
                this._loggerProvider,
                this._logAction
,
                cancellationToken);
        }
    }
}