//------------------------------------------------------------------------------
// <copyright file="RetryCondition.cs"
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

namespace Cadru.TransientFaultHandling
{
    using System;

    /// <summary>
    /// Defines a retry condition.
    /// </summary>
    public class RetryCondition
    {
        /// <param name="retryAllowed">Is retry allowed.</param>
        /// <param name="delay">The delay that indicates how long the current thread will be suspended before.
        /// the next iteration is invoked.</param>
        public RetryCondition(bool retryAllowed, TimeSpan delay)
        {
            RetryAllowed = retryAllowed;
            DelayBeforeRetry = delay;
        }

        /// <summary>
        /// Gets or sets the retry interval value for retry.
        /// </summary>
        public TimeSpan DelayBeforeRetry { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether retry attempt is allowed.
        /// </summary>
        public bool RetryAllowed { get; set; }

    }
}