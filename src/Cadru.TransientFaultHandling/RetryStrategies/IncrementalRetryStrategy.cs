//------------------------------------------------------------------------------
// <copyright file="IncrementalRetryStrategy.cs"
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

namespace Cadru.TransientFaultHandling.RetryStrategies
{
    using System;

    /// <summary>
    /// A retry strategy with a specified number of retry attempts and an incremental time interval between retries.
    /// </summary>
    public class IncrementalRetryStrategy : RetryStrategy
    {
        #region fields
        private readonly int retryCount;
        private readonly TimeSpan initialInterval;
        private readonly TimeSpan increment;
        #endregion

        #region events
        #endregion

        #region constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="IncrementalRetryStrategy"/> class.
        /// </summary>
        public IncrementalRetryStrategy()
            : this(DefaultClientRetryCount, DefaultRetryInterval, DefaultRetryIncrement)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IncrementalRetryStrategy"/> class with the specified retry settings.
        /// </summary>
        /// <param name="retryCount">The number of retry attempts.</param>
        /// <param name="initialInterval">The initial interval that will apply for the first retry.</param>
        /// <param name="increment">The incremental time value that will be used to calculate the progressive delay between retries.</param>
        public IncrementalRetryStrategy(int retryCount, TimeSpan initialInterval, TimeSpan increment)
            : this(null, retryCount, initialInterval, increment)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IncrementalRetryStrategy"/> class with the specified name and retry settings.
        /// </summary>
        /// <param name="name">The retry strategy name.</param>
        /// <param name="retryCount">The number of retry attempts.</param>
        /// <param name="initialInterval">The initial interval that will apply for the first retry.</param>
        /// <param name="increment">The incremental time value that will be used to calculate the progressive delay between retries.</param>
        public IncrementalRetryStrategy(string name, int retryCount, TimeSpan initialInterval, TimeSpan increment)
            : this(name, retryCount, initialInterval, increment, DefaultFirstFastRetry)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IncrementalRetryStrategy"/> class with the specified number of retry attempts, time interval, retry strategy, and fast start option.
        /// </summary>
        /// <param name="name">The retry strategy name.</param>
        /// <param name="retryCount">The number of retry attempts.</param>
        /// <param name="initialInterval">The initial interval that will apply for the first retry.</param>
        /// <param name="increment">The incremental time value that will be used to calculate the progressive delay between retries.</param>
        /// <param name="firstFastRetry">true to immediately retry in the first attempt; otherwise, false. The subsequent retries will remain subject to the configured retry interval.</param>
        public IncrementalRetryStrategy(string name, int retryCount, TimeSpan initialInterval, TimeSpan increment, bool firstFastRetry)
            : base(name, firstFastRetry)
        {
            Contracts.Requires.ValidRange(retryCount > 0, "retryCount");
            Contracts.Requires.ValidRange(initialInterval.Ticks > 0, "initialInterval");
            Contracts.Requires.ValidRange(increment.Ticks > 0, "increment");

            this.retryCount = retryCount;
            this.initialInterval = initialInterval;
            this.increment = increment;
        }
        #endregion

        #region properties

        #endregion
        #region methods
        /// <summary>
        /// Returns the corresponding ShouldRetry delegate.
        /// </summary>
        /// <returns>The ShouldRetry delegate.</returns>
        public override ShouldRetry GetShouldRetry()
        {
            return delegate(int currentRetryCount, Exception lastException, out TimeSpan retryInterval)
            {
                if (currentRetryCount < this.retryCount)
                {
                    retryInterval = TimeSpan.FromMilliseconds(this.initialInterval.TotalMilliseconds + (this.increment.TotalMilliseconds * currentRetryCount));

                    return true;
                }

                retryInterval = TimeSpan.Zero;

                return false;
            };
        }
        #endregion
    }
}
