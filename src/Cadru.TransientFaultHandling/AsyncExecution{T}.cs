//------------------------------------------------------------------------------
// <copyright file="AsyncExecution{T}.cs"
//  company="Scott Dorman"
//  library="Cadru">
//    Copyright (C) 2001-2016 Scott Dorman.
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

namespace Cadru.TransientFaultHandling
{
    using System;
    using System.Globalization;
    using System.Threading;
    using System.Threading.Tasks;
    using Cadru.TransientFaultHandling.RetryStrategies;

    /// <summary>
    /// Handles the execution and retries of the user-initiated task.
    /// </summary>
    /// <typeparam name="TResult">The result type of the user-initiated task.</typeparam>
    internal class AsyncExecution<TResult>
    {
        #region fields
        private readonly Func<Task<TResult>> taskFunc;
        private readonly ShouldRetry shouldRetry;
        private readonly Func<Exception, bool> isTransient;
        private readonly Action<int, Exception, TimeSpan> onRetrying;
        private readonly bool fastFirstRetry;
        private readonly CancellationToken cancellationToken;
        private Task<TResult> previousTask;
        private int retryCount;
        #endregion

        #region events
        #endregion

        #region constructors
        public AsyncExecution(Func<Task<TResult>> taskFunc, ShouldRetry shouldRetry, Func<Exception, bool> isTransient, Action<int, Exception, TimeSpan> onRetrying, bool fastFirstRetry, CancellationToken cancellationToken)
        {
            this.taskFunc = taskFunc;
            this.shouldRetry = shouldRetry;
            this.isTransient = isTransient;
            this.onRetrying = onRetrying;
            this.fastFirstRetry = fastFirstRetry;
            this.cancellationToken = cancellationToken;
        }
        #endregion

        #region properties
        #endregion

        #region methods
        internal Task<TResult> ExecuteAsync()
        {
            return this.ExecuteAsyncImpl(null);
        }

        private Task<TResult> ExecuteAsyncImpl(Task ignore)
        {
            if (this.cancellationToken.IsCancellationRequested)
            {
                if (this.previousTask != null)
                {
                    return this.previousTask;
                }
                else
                {
                    var tcs = new TaskCompletionSource<TResult>();
                    tcs.TrySetCanceled();
                    return tcs.Task;
                }
            }

            // This is a little different from ExecuteAction using APM. If an exception occurs synchronously when
            // starting the task, then the exception is checked for transient errors and if the exception is not
            // transient, it will bubble up synchronously. Otherwise it will be retried. The reason for bubbling up
            // synchronously -instead of returning a failed task- is that TAP design guidelines dictate that task
            // creation should only fail synchronously in response to a usage error, which can be avoided by changing
            // the code that calls the method, and hence should not be considered transient. Nevertheless, as this is
            // a general purpose transient error detection library, we cannot guarantee that other libraries or user
            // code will follow the design guidelines.
            Task<TResult> task;
            try
            {
                task = this.taskFunc.Invoke();
            }
            catch (Exception ex)
            {
                if (this.isTransient(ex))
                {
                    var tcs = new TaskCompletionSource<TResult>();
                    tcs.TrySetException(ex);
                    task = tcs.Task;
                }
                else
                {
                    throw;
                }
            }

            if (task == null)
            {
                throw new ArgumentException(String.Format(CultureInfo.InvariantCulture, Cadru.TransientFaultHandling.Resources.Strings.TaskCannotBeNull, "taskFunc"), "taskFunc");
            }

            // Fast path if the user-initiated task is already completed.
            if (task.Status == TaskStatus.RanToCompletion) return task;

            if (task.Status == TaskStatus.Created)
            {
                throw new ArgumentException(String.Format(CultureInfo.InvariantCulture, Cadru.TransientFaultHandling.Resources.Strings.TaskMustBeScheduled, "taskFunc"), "taskFunc");
            }

            return task
                .ContinueWith<Task<TResult>>(this.ExecuteAsyncContinueWith, CancellationToken.None, TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default)
                .Unwrap();
        }

        private Task<TResult> ExecuteAsyncContinueWith(Task<TResult> runningTask)
        {
            if (!runningTask.IsFaulted || this.cancellationToken.IsCancellationRequested)
            {
                return runningTask;
            }

            TimeSpan delay = TimeSpan.Zero;
            Exception lastError = runningTask.Exception.InnerException;

#pragma warning disable 0618
            if (lastError is RetryLimitExceededException)
#pragma warning restore 0618
            {
                // This is here for backwards compatibility only. The correct way to force a stop is by using cancellation tokens.
                // The user code can throw a RetryLimitExceededException to force the exit from the retry loop.
                // The RetryLimitExceeded exception can have an inner exception attached to it. This is the exception
                // which we will have to throw up the stack so that callers can handle it.
                var tcs = new TaskCompletionSource<TResult>();
                if (lastError.InnerException != null)
                {
                    tcs.TrySetException(lastError.InnerException);
                }
                else
                {
                    tcs.TrySetCanceled();
                }

                return tcs.Task;
            }

            if (!(this.isTransient(lastError) && this.shouldRetry(this.retryCount++, lastError, out delay)))
            {
                // if not transient, return the faulted running task.
                return runningTask;
            }

            // Perform an extra check in the delay interval.
            if (delay < TimeSpan.Zero)
            {
                delay = TimeSpan.Zero;
            }

            this.onRetrying(this.retryCount, lastError, delay);

            this.previousTask = runningTask;
            if (delay > TimeSpan.Zero && (this.retryCount > 1 || !this.fastFirstRetry))
            {
                return Cadru.Extensions.TaskExtensions.Delay(delay)
                    .ContinueWith<Task<TResult>>(this.ExecuteAsyncImpl, CancellationToken.None, TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default)
                    .Unwrap();
            }

            return this.ExecuteAsyncImpl(null);
        }
        #endregion
    }
}
