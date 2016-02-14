//------------------------------------------------------------------------------
// <copyright file="RetryPolicy{T}.cs"
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
    using Cadru.TransientFaultHandling.RetryStrategies;

    /// <summary>
    /// Provides a generic version of the <see cref="RetryPolicy"/> class.
    /// </summary>
    /// <typeparam name="T">The type that implements the <see cref="ITransientErrorDetectionStrategy"/> interface that is responsible for detecting transient conditions.</typeparam>
    public class RetryPolicy<T> : RetryPolicy where T : ITransientErrorDetectionStrategy, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RetryPolicy{T}"/> class with the specified number of retry attempts and parameters defining the progressive delay between retries.
        /// </summary>
        /// <param name="retryStrategy">The strategy to use for this retry policy.</param>
        public RetryPolicy(RetryStrategy retryStrategy)
            : base(new T(), retryStrategy)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RetryPolicy{T}"/> class with the specified number of retry attempts and the default fixed time interval between retries.
        /// </summary>
        /// <param name="retryCount">The number of retry attempts.</param>
        public RetryPolicy(int retryCount)
            : base(new T(), retryCount)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RetryPolicy{T}"/> class with the specified number of retry attempts and a fixed time interval between retries.
        /// </summary>
        /// <param name="retryCount">The number of retry attempts.</param>
        /// <param name="retryInterval">The interval between retries.</param>
        public RetryPolicy(int retryCount, TimeSpan retryInterval)
            : base(new T(), retryCount, retryInterval)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RetryPolicy{T}"/> class with the specified number of retry attempts and back-off parameters for calculating the exponential delay between retries.
        /// </summary>
        /// <param name="retryCount">The number of retry attempts.</param>
        /// <param name="minimum">The minimum back-off time.</param>
        /// <param name="maximum">The maximum back-off time.</param>
        /// <param name="delta">The time value that will be used to calculate a random delta in the exponential delay between retries.</param>
        public RetryPolicy(int retryCount, TimeSpan minimum, TimeSpan maximum, TimeSpan delta)
            : base(new T(), retryCount, minimum, maximum, delta)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RetryPolicy{T}"/> class with the specified number of retry attempts and parameters defining the progressive delay between retries.
        /// </summary>
        /// <param name="retryCount">The number of retry attempts.</param>
        /// <param name="initialInterval">The initial interval that will apply for the first retry.</param>
        /// <param name="increment">The incremental time value that will be used to calculate the progressive delay between retries.</param>
        public RetryPolicy(int retryCount, TimeSpan initialInterval, TimeSpan increment)
            : base(new T(), retryCount, initialInterval, increment)
        {
        }
    }
}
