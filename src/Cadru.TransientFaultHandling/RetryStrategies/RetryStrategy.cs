//------------------------------------------------------------------------------
// <copyright file="RetryStrategy.cs"
//  company="Scott Dorman"
//  library="Cadru">
//    Copyright (C) 2001-2017 Scott Dorman.
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
    /// Defines a callback delegate that will be invoked whenever a retry condition is encountered.
    /// </summary>
    /// <param name="retryCount">The current retry attempt count.</param>
    /// <param name="lastException">The exception that caused the retry conditions to occur.</param>
    /// <returns>A <see cref="RetryCondition"/> instance.</returns>
    public delegate RetryCondition ShouldRetry(int retryCount, Exception lastException);

    /// <summary>
    /// A retry strategy that determines the number of retry attempts and the interval between retries.
    /// </summary>
    public abstract class RetryStrategy
    {
        #region fields
        /// <summary>
        /// Represents the default number of retry attempts.
        /// </summary>
        public static readonly int DefaultClientRetryCount = 10;

        /// <summary>
        /// Represents the default interval between retries.
        /// </summary>
        public static readonly TimeSpan DefaultRetryInterval = TimeSpan.FromSeconds(1.0);

        /// <summary>
        /// Represents the default flag indicating whether the first retry attempt will be made immediately,
        /// whereas subsequent retries will remain subject to the retry interval.
        /// </summary>
        public static readonly bool DefaultFirstFastRetry = true;
        #endregion

        #region events
        #endregion

        #region constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="RetryStrategy"/> class.
        /// </summary>
        /// <param name="name">The name of the retry strategy.</param>
        /// <param name="firstFastRetry">true to immediately retry in the first attempt; otherwise, false. The subsequent retries will remain subject to the configured retry interval.</param>
        protected RetryStrategy(string name, bool firstFastRetry)
        {
            this.Name = name;
            this.FastFirstRetry = firstFastRetry;
        }
        #endregion

        #region properties
        /// <summary>
        /// Gets or sets a value indicating whether the first retry attempt will be made immediately,
        /// whereas subsequent retries will remain subject to the retry interval.
        /// </summary>
        public bool FastFirstRetry { get; set; }

        /// <summary>
        /// Gets the name of the retry strategy.
        /// </summary>
        public string Name { get; }
        #endregion

        #region methods
        /// <summary>
        /// Returns the corresponding ShouldRetry delegate.
        /// </summary>
        /// <returns>The ShouldRetry delegate.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Needs to be a new instance each time.")]
        public abstract ShouldRetry GetShouldRetry();
        #endregion
    }
}
