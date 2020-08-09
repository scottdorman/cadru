//------------------------------------------------------------------------------
// <copyright file="HttpRetryPolicy.cs"
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

namespace Cadru.Net.Http
{
    using System;
    using Cadru.TransientFaultHandling;
    using Cadru.TransientFaultHandling.RetryStrategies;

    /// <summary>
    /// Returns a policy that implements an exponential delay between retries and uses
    /// a <see cref="HttpTransientErrorDetectionStrategy"/> to determine which HTTP
    /// status codes should be treated as transient errors.
    /// </summary>
    public class HttpRetryPolicy : RetryPolicy<HttpTransientErrorDetectionStrategy>
    {
        #region fields
        #endregion

        #region events
        #endregion

        #region constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpRetryPolicy"/> class with the
        /// default error detection and retry strategies.
        /// </summary>
        public HttpRetryPolicy()
             : base(new ExponentialBackoffRetryStrategy(3, TimeSpan.FromSeconds(10.0), TimeSpan.FromSeconds(10.0), TimeSpan.FromSeconds(1.0)))
        {
        }
        #endregion

        #region properties
        #endregion

        #region methods
        #endregion
    }
}