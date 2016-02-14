//------------------------------------------------------------------------------
// <copyright file="ExponentialBackoffRetryStrategy.cs"
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
    /// A retry strategy with back-off parameters for calculating the exponential delay between retries.
    /// </summary>
    public class ExponentialBackoffRetryStrategy : RetryStrategy
    {
        #region fields
        private readonly int retryCount;
        private readonly TimeSpan minBackoff;
        private readonly TimeSpan maxBackoff;
        private readonly TimeSpan deltaBackoff;
        #endregion

        #region events
        #endregion

        #region constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ExponentialBackoffRetryStrategy"/> class.
        /// </summary>
        public ExponentialBackoffRetryStrategy()
            : this(DefaultClientRetryCount, DefaultMinBackoff, DefaultMaxBackoff, DefaultClientBackoff)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExponentialBackoffRetryStrategy"/> class with the specified retry settings.
        /// </summary>
        /// <param name="retryCount">The maximum number of retry attempts.</param>
        /// <param name="minimum">The minimum back-off time</param>
        /// <param name="maximum">The maximum back-off time.</param>
        /// <param name="delta">The value that will be used to calculate a random delta in the exponential delay between retries.</param>
        public ExponentialBackoffRetryStrategy(int retryCount, TimeSpan minimum, TimeSpan maximum, TimeSpan delta)
            : this(null, retryCount, minimum, maximum, delta, DefaultFirstFastRetry)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExponentialBackoffRetryStrategy"/> class with the specified name and retry settings.
        /// </summary>
        /// <param name="name">The name of the retry strategy.</param>
        /// <param name="retryCount">The maximum number of retry attempts.</param>
        /// <param name="minimum">The minimum back-off time</param>
        /// <param name="maximum">The maximum back-off time.</param>
        /// <param name="delta">The value that will be used to calculate a random delta in the exponential delay between retries.</param>
        public ExponentialBackoffRetryStrategy(string name, int retryCount, TimeSpan minimum, TimeSpan maximum, TimeSpan delta)
            : this(name, retryCount, minimum, maximum, delta, DefaultFirstFastRetry)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExponentialBackoffRetryStrategy"/> class with the specified name, retry settings, and fast retry option.
        /// </summary>
        /// <param name="name">The name of the retry strategy.</param>
        /// <param name="retryCount">The maximum number of retry attempts.</param>
        /// <param name="minimum">The minimum back-off time</param>
        /// <param name="maximum">The maximum back-off time.</param>
        /// <param name="delta">The value that will be used to calculate a random delta in the exponential delay between retries.</param>
        /// <param name="firstFastRetry">true to immediately retry in the first attempt; otherwise, false. The subsequent retries will remain subject to the configured retry interval.</param>
        public ExponentialBackoffRetryStrategy(string name, int retryCount, TimeSpan minimum, TimeSpan maximum, TimeSpan delta, bool firstFastRetry)
            : base(name, firstFastRetry)
        {
            Contracts.Requires.ValidRange(retryCount > 0, "retryCount");
            Contracts.Requires.ValidRange(minimum.Ticks > 0, "minimum");
            Contracts.Requires.ValidRange(maximum.Ticks > 0, "maximum");
            Contracts.Requires.ValidRange(delta.Ticks > 0, "delta");
            Contracts.Requires.ValidRange(minimum.TotalMilliseconds <= maximum.TotalMilliseconds, "minimum");

            this.retryCount = retryCount;
            this.minBackoff = minimum;
            this.maxBackoff = maximum;
            this.deltaBackoff = delta;
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
                    var random = new Random();

                    var delta = (int)((Math.Pow(2.0, currentRetryCount) - 1.0) * random.Next((int)(this.deltaBackoff.TotalMilliseconds * 0.8), (int)(this.deltaBackoff.TotalMilliseconds * 1.2)));
                    var interval = (int)Math.Min(checked(this.minBackoff.TotalMilliseconds + delta), this.maxBackoff.TotalMilliseconds);

                    retryInterval = TimeSpan.FromMilliseconds(interval);

                    return true;
                }

                retryInterval = TimeSpan.Zero;
                return false;
            };
            #endregion
        }
    }
}
