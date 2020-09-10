//------------------------------------------------------------------------------
// <copyright file="TimeoutHelper.cs"
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

namespace Cadru.Polly.Utilities
{
    /// <summary>
    /// Helper methods for creating timeout strategies.
    /// </summary>
    public static class TimeoutHelper
    {
        /// <summary>
        /// Generates a timeout based retries and exponential back-off.
        /// </summary>
        /// <param name="initialDelay">The initial delay added to the timeout.</param>
        /// <param name="factor">The linear factor to use for increasing the duration.</param>
        /// <param name="retryCount">The maximum number of retries to use, in addition to the original call.</param>
        /// <returns>A <see cref="TimeSpan" /> representing the timeout.</returns>
        public static TimeSpan GetTimeout(TimeSpan initialDelay, int retryCount, double factor = 2.0)
        {
            var delay = initialDelay;
            for (var i = 0; i < retryCount; i++)
            {
                delay += TimeSpan.FromSeconds(Math.Pow(factor, i));
            }

            return delay + TimeSpan.FromSeconds(10);
        }
    }
}