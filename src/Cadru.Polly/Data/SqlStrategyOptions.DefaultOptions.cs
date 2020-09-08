//------------------------------------------------------------------------------
// <copyright file="SqlStrategyOptions.DefaultOptions.cs"
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

using Cadru.Polly.Utilities;

namespace Cadru.Polly.Data
{
    public partial class SqlStrategyOptions
    {
        internal const int retryCountDefault = 5;
        internal const int exeptionsAllowedBeforeBreakingDefault = 3;
        internal const int durationOfBreakSecondsDefault = 30;

        /// <summary>
        /// Gets the default options values.
        /// </summary>
        public static SqlStrategyOptions Defaults => new SqlStrategyOptions
        {
            DurationOfBreak = TimeSpan.FromSeconds(durationOfBreakSecondsDefault),
            ExceptionsAllowedBeforeBreaking = exeptionsAllowedBeforeBreakingDefault,
            RetryCount = retryCountDefault,
            OverallTimeout = TimeoutHelper.GetTimeout(TimeSpan.Zero, retryCountDefault),
            TimeoutPerRetry = TimeoutHelper.GetTimeout(TimeSpan.Zero, retryCountDefault),
        };
    }
}