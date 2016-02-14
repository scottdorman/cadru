//------------------------------------------------------------------------------
// <copyright file="RetryPolicy.cs"
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
    using System.Threading;
    using System.Threading.Tasks;
    using Cadru.TransientFaultHandling.RetryStrategies;

    /// <summary>
    /// Provides the base implementation of the retry mechanism for unreliable actions and transient conditions.
    /// </summary>
    public partial class RetryPolicy
    {
        #region fields
        private static RetryPolicy noRetry = new RetryPolicy(new TransientErrorIgnoreStrategy(), RetryStrategy.NoRetry);
        private static RetryPolicy defaultFixed = new RetryPolicy(new TransientErrorCatchAllStrategy(), RetryStrategy.DefaultFixed);
        private static RetryPolicy defaultProgressive = new RetryPolicy(new TransientErrorCatchAllStrategy(), RetryStrategy.DefaultProgressive);
        private static RetryPolicy defaultExponential = new RetryPolicy(new TransientErrorCatchAllStrategy(), RetryStrategy.DefaultExponential);
        #endregion

        #region events
        /// <summary>
        /// The event that will be invoked whenever a retry condition is encountered.
        /// </summary>
        public event EventHandler<RetryingEventArgs> Retrying;
        #endregion

        #region constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="RetryPolicy"/> class with the specified number of retry attempts and parameters defining the progressive delay between retries.
        /// </summary>
        /// <param name="errorDetectionStrategy">The <see cref="ITransientErrorDetectionStrategy"/> that is responsible for detecting transient conditions.</param>
        /// <param name="retryStrategy">The strategy to use for this retry policy.</param>
        public RetryPolicy(ITransientErrorDetectionStrategy errorDetectionStrategy, RetryStrategy retryStrategy)
        {
            Contracts.Requires.NotNull(errorDetectionStrategy, nameof(errorDetectionStrategy));
            Contracts.Requires.NotNull(retryStrategy, nameof(retryStrategy));

            this.ErrorDetectionStrategy = errorDetectionStrategy;

            if (errorDetectionStrategy == null)
            {
                throw new InvalidOperationException("The error detection strategy type must implement the ITransientErrorDetectionStrategy interface.");
            }

            this.RetryStrategy = retryStrategy;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RetryPolicy"/> class with the specified number of retry attempts and default fixed time interval between retries.
        /// </summary>
        /// <param name="errorDetectionStrategy">The <see cref="ITransientErrorDetectionStrategy"/> that is responsible for detecting transient conditions.</param>
        /// <param name="retryCount">The number of retry attempts.</param>
        public RetryPolicy(ITransientErrorDetectionStrategy errorDetectionStrategy, int retryCount)
            : this(errorDetectionStrategy, new FixedIntervalRetryStrategy(retryCount))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RetryPolicy"/> class with the specified number of retry attempts and fixed time interval between retries.
        /// </summary>
        /// <param name="errorDetectionStrategy">The <see cref="ITransientErrorDetectionStrategy"/> that is responsible for detecting transient conditions.</param>
        /// <param name="retryCount">The number of retry attempts.</param>
        /// <param name="retryInterval">The interval between retries.</param>
        public RetryPolicy(ITransientErrorDetectionStrategy errorDetectionStrategy, int retryCount, TimeSpan retryInterval)
            : this(errorDetectionStrategy, new FixedIntervalRetryStrategy(retryCount, retryInterval))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RetryPolicy"/> class with the specified number of retry attempts and back-off parameters for calculating the exponential delay between retries.
        /// </summary>
        /// <param name="errorDetectionStrategy">The <see cref="ITransientErrorDetectionStrategy"/> that is responsible for detecting transient conditions.</param>
        /// <param name="retryCount">The number of retry attempts.</param>
        /// <param name="minimum">The minimum back-off time.</param>
        /// <param name="maximum">The maximum back-off time.</param>
        /// <param name="delta">The time value that will be used to calculate a random delta in the exponential delay between retries.</param>
        public RetryPolicy(ITransientErrorDetectionStrategy errorDetectionStrategy, int retryCount, TimeSpan minimum, TimeSpan maximum, TimeSpan delta)
            : this(errorDetectionStrategy, new ExponentialBackoffRetryStrategy(retryCount, minimum, maximum, delta))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RetryPolicy"/> class with the specified number of retry attempts and parameters defining the progressive delay between retries.
        /// </summary>
        /// <param name="errorDetectionStrategy">The <see cref="ITransientErrorDetectionStrategy"/> that is responsible for detecting transient conditions.</param>
        /// <param name="retryCount">The number of retry attempts.</param>
        /// <param name="initialInterval">The initial interval that will apply for the first retry.</param>
        /// <param name="increment">The incremental time value that will be used to calculate the progressive delay between retries.</param>
        public RetryPolicy(ITransientErrorDetectionStrategy errorDetectionStrategy, int retryCount, TimeSpan initialInterval, TimeSpan increment)
            : this(errorDetectionStrategy, new IncrementalRetryStrategy(retryCount, initialInterval, increment))
        {
        }
        #endregion

        #region properties
        /// <summary>
        /// Returns a default policy that performs no retries, but invokes the action only once.
        /// </summary>
        public static RetryPolicy NoRetry
        {
            get { return noRetry; }
        }

        /// <summary>
        /// Returns a default policy that implements a fixed retry interval configured with the default <see cref="FixedInterval"/> retry strategy.
        /// The default retry policy treats all caught exceptions as transient errors.
        /// </summary>
        public static RetryPolicy DefaultFixed
        {
            get { return defaultFixed; }
        }

        /// <summary>
        /// Returns a default policy that implements a progressive retry interval configured with the default <see cref="Incremental"/> retry strategy.
        /// The default retry policy treats all caught exceptions as transient errors.
        /// </summary>
        public static RetryPolicy DefaultProgressive
        {
            get { return defaultProgressive; }
        }

        /// <summary>
        /// Returns a default policy that implements a random exponential retry interval configured with the default <see cref="FixedInterval"/> retry strategy.
        /// The default retry policy treats all caught exceptions as transient errors.
        /// </summary>
        public static RetryPolicy DefaultExponential
        {
            get { return defaultExponential; }
        }

        /// <summary>
        /// Gets the retry strategy.
        /// </summary>
        public RetryStrategy RetryStrategy { get; private set; }

        /// <summary>
        /// Gets the instance of the error detection strategy.
        /// </summary>
        public ITransientErrorDetectionStrategy ErrorDetectionStrategy { get; private set; }

        #endregion

        #region methods
        /// <summary>
        /// Repetitively executes the specified action while it satisfies the current retry policy.
        /// </summary>
        /// <param name="action">A delegate that represents the executable action that doesn't return any results.</param>
        public virtual void ExecuteAction(Action action)
        {
            Contracts.Requires.NotNull(action, nameof(action));

            this.ExecuteAction(() => { action(); return default(object); });
        }

        /// <summary>
        /// Repetitively executes the specified action while it satisfies the current retry policy.
        /// </summary>
        /// <typeparam name="TResult">The type of result expected from the executable action.</typeparam>
        /// <param name="func">A delegate that represents the executable action that returns the result of type <typeparamref name="TResult"/>.</param>
        /// <returns>The result from the action.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "Validated with Guard")]
        public virtual TResult ExecuteAction<TResult>(Func<TResult> func)
        {
            Contracts.Requires.NotNull(func, nameof(func));

            int retryCount = 0;
            TimeSpan delay = TimeSpan.Zero;
            Exception lastError;

            var shouldRetry = this.RetryStrategy.GetShouldRetry();

            for (;;)
            {
                lastError = null;

                try
                {
                    return func();
                }
#pragma warning disable 0618
                catch (RetryLimitExceededException limitExceededEx)
#pragma warning restore 0618
                {
                    // The user code can throw a RetryLimitExceededException to force the exit from the retry loop.
                    // The RetryLimitExceeded exception can have an inner exception attached to it. This is the exception
                    // which we will have to throw up the stack so that callers can handle it.
                    if (limitExceededEx.InnerException != null)
                    {
                        throw limitExceededEx.InnerException;
                    }
                    else
                    {
                        return default(TResult);
                    }
                }
                catch (Exception ex)
                {
                    lastError = ex;

                    if (!(this.ErrorDetectionStrategy.IsTransient(lastError) && shouldRetry(retryCount++, lastError, out delay)))
                    {
                        throw;
                    }
                }

                // Perform an extra check in the delay interval. Should prevent from accidentally ending up with the value of -1 that will block a thread indefinitely.
                // In addition, any other negative numbers will cause an ArgumentOutOfRangeException fault that will be thrown by Thread.Sleep.
                if (delay.TotalMilliseconds < 0)
                {
                    delay = TimeSpan.Zero;
                }

                this.OnRetrying(retryCount, lastError, delay);

                if (retryCount > 1 || !this.RetryStrategy.FastFirstRetry)
                {
                    Cadru.Extensions.TaskExtensions.Delay(delay).Wait();
                }
            }
        }

        /// <summary>
        /// Repetitively executes the specified asynchronous task while it satisfies the current retry policy.
        /// </summary>
        /// <param name="taskAction">A function that returns a started task (also known as "hot" task).</param>
        /// <returns>
        /// A task that will run to completion if the original task completes successfully (either the
        /// first time or after retrying transient failures). If the task fails with a non-transient error or
        /// the retry limit is reached, the returned task will transition to a faulted state and the exception must be observed.
        /// </returns>
        public Task ExecuteAsync(Func<Task> taskAction)
        {
            return this.ExecuteAsync(taskAction, default(CancellationToken));
        }

        /// <summary>
        /// Repetitively executes the specified asynchronous task while it satisfies the current retry policy.
        /// </summary>
        /// <param name="taskAction">A function that returns a started task (also known as "hot" task).</param>
        /// <param name="cancellationToken">The token used to cancel the retry operation. This token does not cancel the execution of the asynchronous task.</param>
        /// <returns>
        /// Returns a task that will run to completion if the original task completes successfully (either the
        /// first time or after retrying transient failures). If the task fails with a non-transient error or
        /// the retry limit is reached, the returned task will transition to a faulted state and the exception must be observed.
        /// </returns>
        public Task ExecuteAsync(Func<Task> taskAction, CancellationToken cancellationToken)
        {
            if (taskAction == null) throw new ArgumentNullException("taskAction");

            return new AsyncExecution(
                    taskAction,
                    this.RetryStrategy.GetShouldRetry(),
                    this.ErrorDetectionStrategy.IsTransient,
                    this.OnRetrying,
                    this.RetryStrategy.FastFirstRetry,
                    cancellationToken)
                .ExecuteAsync();
        }

        /// <summary>
        /// Repeatedly executes the specified asynchronous task while it satisfies the current retry policy.
        /// </summary>
        /// <param name="taskFunc">A function that returns a started task (also known as "hot" task).</param>
        /// <returns>
        /// Returns a task that will run to completion if the original task completes successfully (either the
        /// first time or after retrying transient failures). If the task fails with a non-transient error or
        /// the retry limit is reached, the returned task will transition to a faulted state and the exception must be observed.
        /// </returns>
        public Task<TResult> ExecuteAsync<TResult>(Func<Task<TResult>> taskFunc)
        {
            return this.ExecuteAsync<TResult>(taskFunc, default(CancellationToken));
        }

        /// <summary>
        /// Repeatedly executes the specified asynchronous task while it satisfies the current retry policy.
        /// </summary>
        /// <param name="taskFunc">A function that returns a started task (also known as "hot" task).</param>
        /// <param name="cancellationToken">The token used to cancel the retry operation. This token does not cancel the execution of the asynchronous task.</param>
        /// <returns>
        /// Returns a task that will run to completion if the original task completes successfully (either the
        /// first time or after retrying transient failures). If the task fails with a non-transient error or
        /// the retry limit is reached, the returned task will transition to a faulted state and the exception must be observed.
        /// </returns>
        public Task<TResult> ExecuteAsync<TResult>(Func<Task<TResult>> taskFunc, CancellationToken cancellationToken)
        {
            if (taskFunc == null) throw new ArgumentNullException("taskFunc");

            return new AsyncExecution<TResult>(
                    taskFunc,
                    this.RetryStrategy.GetShouldRetry(),
                    this.ErrorDetectionStrategy.IsTransient,
                    this.OnRetrying,
                    this.RetryStrategy.FastFirstRetry,
                    cancellationToken)
                .ExecuteAsync();
        }

        /// <summary>
        /// Notifies the subscribers whenever a retry condition is encountered.
        /// </summary>
        /// <param name="retryCount">The current retry attempt count.</param>
        /// <param name="lastError">The exception that caused the retry conditions to occur.</param>
        /// <param name="delay">The delay that indicates how long the current thread will be suspended before the next iteration is invoked.</param>
        protected virtual void OnRetrying(int retryCount, Exception lastError, TimeSpan delay)
        {
            if (this.Retrying != null)
            {
                this.Retrying(this, new RetryingEventArgs(retryCount, delay, lastError));
            }
        }
        #endregion
    }
}
