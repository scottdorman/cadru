//------------------------------------------------------------------------------
// <copyright file="LoggingPolicySyntax.cs"
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

using Microsoft.Extensions.Logging;

using Polly;

namespace Cadru.Polly.Logging
{
    /// <summary>
    /// Fluent API for defining an <see cref="AsyncLoggingPolicy"/>.
    /// </summary>
    public static class LoggingPolicySyntax
    {
        /// <summary>
        /// Constructs a new instance of <see cref="AsyncLoggingPolicy"/>,
        /// configured to handle the exceptions specified in the <paramref name="policyBuilder"/>.
        /// </summary>
        /// <param name="policyBuilder">The policy builder.</param>
        /// <param name="loggerProvider">A func returning a logger to use.</param>
        /// <param name="logAction">A logging action.</param>
        /// <returns><see cref="AsyncLoggingPolicy"/></returns>
        public static AsyncLoggingPolicy AsyncLog(this PolicyBuilder policyBuilder, Func<Context, ILogger> loggerProvider, Action<ILogger, Context, Exception> logAction
        )
        {
            return new AsyncLoggingPolicy(policyBuilder, loggerProvider, logAction);
        }

        /// <summary>
        /// Constructs a new instance of
        /// <see cref="AsyncLoggingPolicy{TResult}"/>, configured to handle the
        /// exceptions and results specified in the <paramref name="policyBuilder"/>.
        /// </summary>
        /// <typeparam name="TResult">
        /// The return type of delegates which may be executed through the policy.
        /// </typeparam>
        /// <param name="policyBuilder">The policy builder.</param>
        /// <param name="loggerProvider">A func returning a logger to use.</param>
        /// <param name="logAction">A logging action.</param>
        /// <returns><see cref="AsyncLoggingPolicy{TResult}"/></returns>
        public static AsyncLoggingPolicy<TResult> AsyncLog<TResult>(this PolicyBuilder<TResult> policyBuilder, Func<Context, ILogger> loggerProvider, Action<ILogger, Context, DelegateResult<TResult>> logAction)
        {
            return new AsyncLoggingPolicy<TResult>(policyBuilder, loggerProvider, logAction);
        }

        /// <summary>
        /// Constructs a new instance of <see cref="LoggingPolicy"/>, configured
        /// to handle the exceptions specified in the <paramref name="policyBuilder"/>.
        /// </summary>
        /// <param name="policyBuilder">The policy builder.</param>
        /// <param name="loggerProvider">A func returning a logger to use.</param>
        /// <param name="logAction">A logging action.</param>
        /// <returns><see cref="LoggingPolicy"/></returns>
        public static LoggingPolicy Log(this PolicyBuilder policyBuilder, Func<Context, ILogger> loggerProvider, Action<ILogger, Context, Exception> logAction)
        {
            return new LoggingPolicy(policyBuilder, loggerProvider, logAction);
        }

        /// <summary>
        /// Constructs a new instance of <see cref="LoggingPolicy{TResult}"/>,
        /// configured to handle the exceptions and results specified in the <paramref name="policyBuilder"/>.
        /// </summary>
        /// <typeparam name="TResult">
        /// The return type of delegates which may be executed through the policy.
        /// </typeparam>
        /// <param name="policyBuilder">The policy builder.</param>
        /// <param name="loggerProvider">A func returning a logger to use.</param>
        /// <param name="logAction">A logging action.</param>
        /// <returns><see cref="LoggingPolicy{TResult}"/></returns>
        public static LoggingPolicy<TResult> Log<TResult>(this PolicyBuilder<TResult> policyBuilder, Func<Context, ILogger> loggerProvider, Action<ILogger, Context, DelegateResult<TResult>> logAction)
        {
            return new LoggingPolicy<TResult>(policyBuilder, loggerProvider, logAction);
        }
    }
}