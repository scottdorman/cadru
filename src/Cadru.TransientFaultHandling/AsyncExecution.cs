//------------------------------------------------------------------------------
// <copyright file="AsyncExecution.cs"
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
    /// Provides a wrapper for a non-generic <see cref="Task"/> and calls into the pipeline
    /// to retry only the generic version of the <see cref="Task"/>.
    /// </summary>
    internal class AsyncExecution : AsyncExecution<bool>
    {
        #region fields
        private static Task<bool> cachedBoolTask;
        #endregion

        #region events
        #endregion

        #region constructors
        public AsyncExecution(Func<Task> taskAction, ShouldRetry shouldRetry, Func<Exception, bool> isTransient, Action<int, Exception, TimeSpan> onRetrying, bool fastFirstRetry, CancellationToken cancellationToken)
           : base(() => StartAsGenericTask(taskAction), shouldRetry, isTransient, onRetrying, fastFirstRetry, cancellationToken)
        {
        }
        #endregion

        #region properties
        #endregion

        #region methods
        /// <summary>
        /// Wraps the non-generic <see cref="Task"/> into a generic <see cref="Task"/>.
        /// </summary>
        /// <param name="taskAction">The task to wrap.</param>
        /// <returns>A <see cref="Task"/> that wraps the non-generic <see cref="Task"/>.</returns>
        private static Task<bool> StartAsGenericTask(Func<Task> taskAction)
        {
            var task = taskAction.Invoke();
            if (task == null)
            {
                throw new ArgumentException(String.Format(CultureInfo.InvariantCulture, Cadru.TransientFaultHandling.Resources.Strings.TaskCannotBeNull, "taskAction"), "taskAction");
            }

            if (task.Status == TaskStatus.RanToCompletion)
            {
                return GetCachedTask();
            }

            if (task.Status == TaskStatus.Created)
            {
                throw new ArgumentException(String.Format(CultureInfo.InvariantCulture, Cadru.TransientFaultHandling.Resources.Strings.TaskMustBeScheduled, "taskAction"), "taskAction");
            }

            var tcs = new TaskCompletionSource<bool>();
            task.ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    tcs.TrySetException(t.Exception.InnerExceptions);
                }
                else if (t.IsCanceled)
                {
                    tcs.TrySetCanceled();
                }
                else
                {
                    tcs.TrySetResult(true);
                }
            }, TaskContinuationOptions.ExecuteSynchronously);
            return tcs.Task;
        }

        private static Task<bool> GetCachedTask()
        {
            if (cachedBoolTask == null)
            {
                var tcs = new TaskCompletionSource<bool>();
                tcs.TrySetResult(true);
                cachedBoolTask = tcs.Task;
            }

            return cachedBoolTask;
        }
        #endregion
    }
}
