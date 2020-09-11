//------------------------------------------------------------------------------
// <copyright file="SqlStrategyLoggingDelegates.cs"
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
using System.Threading.Tasks;

using Cadru.Polly.Resources;

using Microsoft.Extensions.Logging;

using Polly;

namespace Cadru.Polly.Data
{
    internal static class SqlStrategyLoggingDelegates
    {
        internal static void OnCircuitBreak(Exception exception, TimeSpan timeSpan, Context context)
        {
            if (context.TryGetLogger(out var logger))
            {
                logger.LogError(Strings.SqlServer_LoggingMessage_CircuitBroken, new object[] { timeSpan, exception.Message });
            }
        }

        internal static void OnCircuitReset(Context context)
        {
            if (context.TryGetLogger(out var logger))
            {
                logger.LogError(Strings.SqlServer_LoggingMessage_CircuitReset);
            }
        }

        [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Required to match delegate call site.")]
        internal static void OnRetry(Exception exception, TimeSpan timeSpan, int retries, Context context)
        {
            if (context.TryGetLogger(out var logger))
            {
                logger.LogError(Strings.SqlServer_LoggingMessage_RetryFailure, new object[] { retries, exception.Message });
            }
        }

        [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Required to match delegate call site.")]
        internal static Task OnRetryAsync(Exception exception, TimeSpan timeSpan, int retries, Context context)
        {
            if (context.TryGetLogger(out var logger))
            {
                logger.LogError(Strings.SqlServer_LoggingMessage_RetryFailure, new object[] { retries, exception.Message });
            }

            return Task.CompletedTask;
        }

        [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Required to match delegate call site.")]
        internal static void OnTimeout(Context context, TimeSpan timeSpan, Task task, Exception exception)
        {
            if (context.TryGetLogger(out var logger))
            {
                logger.LogError(Strings.SqlServer_LoggingMessage_Timeout, new object[] { exception.Message });
            }
        }

        [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Required to match delegate call site.")]
        internal static Task OnTimeoutAsync(Context context, TimeSpan timeSpan, Task task, Exception exception)
        {
            if (context.TryGetLogger(out var logger))
            {
                logger.LogError(Strings.SqlServer_LoggingMessage_Timeout, new object[] { exception.Message });
            }

            return Task.CompletedTask;
        }
    }
}