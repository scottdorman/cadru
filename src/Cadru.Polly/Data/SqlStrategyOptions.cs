//------------------------------------------------------------------------------
// <copyright file="SqlStrategyOptions.cs"
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

namespace Cadru.Polly.Data
{
    /// <summary>
    /// The configuration options for a retry strategy.
    /// </summary>
    public partial class SqlStrategyOptions
    {
        /// <summary>
        /// The configuration section key.
        /// </summary>
        public const string SectionKey = "RetryStrategy";

        /// <summary>
        /// The duration the circuit will stay open before resetting.
        /// </summary>
        public TimeSpan? DurationOfBreak { get; set; }

        /// <summary>
        /// The number of exceptions that are allowed before opening the circuit.
        /// </summary>
        public int? ExceptionsAllowedBeforeBreaking { get; set; }

        /// <summary>
        /// The timeout for the overall strategy.
        /// </summary>
        public TimeSpan? OverallTimeout { get; set; }

        /// <summary>
        /// The retry count.
        /// </summary>
        public int? RetryCount { get; set; }

        /// <summary>
        /// The timeout for each retry.
        /// </summary>
        public TimeSpan? TimeoutPerRetry { get; set; }
    }
}