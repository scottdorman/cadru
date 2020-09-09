﻿//------------------------------------------------------------------------------
// <copyright file="SqlServerPolicyKeys.cs"
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

namespace Cadru.Polly.Data.SqlServer
{
    internal static class SqlServerPolicyKeys
    {
        internal const string CircuitBreakerPolicy = "CircuitBreakerPolicy";
        internal const string CircuitBreakerPolicyAsync = "CircuitBreakerPolicyAsync";
        internal const string CommonTransientErrorsPolicy = "C.CommonTransientErrorsPolicy";
        internal const string CommonTransientErrorsPolicyAsync = "C.CommonTransientErrorsPolicyAsync";
        internal const string FallbackPolicy = "A.FallbackPolicy";
        internal const string FallbackPolicyAsync = "A.FallbackPolicyAsync";
        internal const string OverallTimeoutPolicy = "B.OverallTimeoutPolicy";
        internal const string OverallTimeoutPolicyAsync = "B.OverallTimeoutPolicyAsync";
        internal const string TimeoutPerRetryPolicy = "E.TimeoutPerRetryPolicy";
        internal const string TimeoutPerRetryPolicyAsync = "E.TimeoutPerRetryPolicyAsync";
        internal const string TransactionPolicy = "D.TransactionPolicy";
        internal const string TransactionPolicyAsync = "D.TransactionPolicyAsync";
    }
}