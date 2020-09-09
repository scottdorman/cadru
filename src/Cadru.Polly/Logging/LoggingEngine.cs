//------------------------------------------------------------------------------
// <copyright file="LoggingEngine.cs"
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
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Polly;
using Polly.Utilities;

namespace Cadru.Polly.Logging
{
    internal static class LoggingEngine
    {
        internal static TResult Implementation<TResult>(Func<Context, CancellationToken, TResult> action, Context context, ExceptionPredicates shouldHandleExceptionPredicates, ResultPredicates<TResult> shouldHandleResultPredicates, Func<Context, ILogger> loggerProvider, Action<ILogger, Context, DelegateResult<TResult>> logAction, CancellationToken cancellationToken)
        {
            try
            {
                return LogResult(action(context, cancellationToken), context, shouldHandleResultPredicates, loggerProvider, logAction);
            }
            catch (Exception exception)
            {
                LogException(exception, context, shouldHandleExceptionPredicates, loggerProvider, logAction);
                throw;
            }
        }

        internal static async Task<TResult> ImplementationAsync<TResult>(Func<Context, CancellationToken, Task<TResult>> action, Context context, bool continueOnCapturedContext, ExceptionPredicates shouldHandleExceptionPredicates, ResultPredicates<TResult> shouldHandleResultPredicates, Func<Context, ILogger> loggerProvider, Action<ILogger, Context, DelegateResult<TResult>> logAction, CancellationToken cancellationToken)
        {
            try
            {
                return LogResult(await action(context, cancellationToken).ConfigureAwait(continueOnCapturedContext), context, shouldHandleResultPredicates, loggerProvider, logAction);
            }
            catch (Exception exception)
            {
                LogException(exception, context, shouldHandleExceptionPredicates, loggerProvider, logAction);
                throw;
            }
        }

        private static void LogException<TResult>(Exception exception, Context context, ExceptionPredicates shouldHandleExceptionPredicates, Func<Context, ILogger> loggerProvider, Action<ILogger, Context, DelegateResult<TResult>> logAction)
        {
            var handledException = shouldHandleExceptionPredicates.FirstMatchOrDefault(exception);
            if (handledException != null)
            {
                var logger = loggerProvider(context);
                logAction(logger, context, new DelegateResult<TResult>(exception));

                // The policy intentionally bubbles the exception outwards after logging.
                handledException.RethrowWithOriginalStackTraceIfDiffersFrom(exception);
            }
        }

        private static TResult LogResult<TResult>(TResult result, Context context, ResultPredicates<TResult> shouldHandleResultPredicates, Func<Context, ILogger> loggerProvider, Action<ILogger, Context, DelegateResult<TResult>> logAction)
        {
            if (shouldHandleResultPredicates.AnyMatch(result))
            {
                var logger = loggerProvider(context);
                logAction(logger, context, new DelegateResult<TResult>(result));
            }

            return result;
        }
    }
}