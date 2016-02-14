//------------------------------------------------------------------------------
// <copyright file="FixedIntervalRetryStategy.cs"
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
    /// A retry strategy with a specified number of retry attempts and a default, fixed time interval between retries.
    /// </summary>
    public class FixedIntervalRetryStrategy : RetryStrategy
    {
        #region fields
        private readonly int retryCount;
        private readonly TimeSpan retryInterval;
        #endregion

        #region events
        #endregion

        #region constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="FixedIntervalRetryStrategy"/> class.
        /// </summary>
        public FixedIntervalRetryStrategy()
            : this(DefaultClientRetryCount)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FixedIntervalRetryStrategy"/> class with the specified number of retry attempts.
        /// </summary>
        /// <param name="retryCount">The number of retry attempts.</param>
        public FixedIntervalRetryStrategy(int retryCount)
            : this(retryCount, DefaultRetryInterval)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FixedIntervalRetryStrategy"/> class with the specified number of retry attempts and time interval.
        /// </summary>
        /// <param name="retryCount">The number of retry attempts.</param>
        /// <param name="retryInterval">The time interval between retries.</param>
        public FixedIntervalRetryStrategy(int retryCount, TimeSpan retryInterval)
            : this(null, retryCount, retryInterval, DefaultFirstFastRetry)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FixedIntervalRetryStrategy"/> class with the specified number of retry attempts, time interval, and retry strategy.
        /// </summary>
        /// <param name="name">The retry strategy name.</param>
        /// <param name="retryCount">The number of retry attempts.</param>
        /// <param name="retryInterval">The time interval between retries.</param>
        public FixedIntervalRetryStrategy(string name, int retryCount, TimeSpan retryInterval)
            : this(name, retryCount, retryInterval, DefaultFirstFastRetry)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FixedIntervalRetryStrategy"/> class with the specified number of retry attempts, time interval, retry strategy, and fast start option.
        /// </summary>
        /// <param name="name">The retry strategy name.</param>
        /// <param name="retryCount">The number of retry attempts.</param>
        /// <param name="retryInterval">The time interval between retries.</param>
        /// <param name="firstFastRetry">true to immediately retry in the first attempt; otherwise, false. The subsequent retries will remain subject to the configured retry interval.</param>
        public FixedIntervalRetryStrategy(string name, int retryCount, TimeSpan retryInterval, bool firstFastRetry)
            : base(name, firstFastRetry)
        {
            Contracts.Requires.ValidRange(retryCount > 0, "retryCount");
            Contracts.Requires.ValidRange(retryInterval.Ticks > 0, "retryInterval");

            this.retryCount = retryCount;
            this.retryInterval = retryInterval;
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
            if (this.retryCount == 0)
            {
                return delegate(int currentRetryCount, Exception lastException, out TimeSpan interval)
                {
                    interval = TimeSpan.Zero;
                    return false;
                };
            }

            return delegate(int currentRetryCount, Exception lastException, out TimeSpan interval)
            {
                if (currentRetryCount < this.retryCount)
                {
                    interval = this.retryInterval;
                    return true;
                }

                interval = TimeSpan.Zero;
                return false;
            };
        }
        #endregion
    }
}
