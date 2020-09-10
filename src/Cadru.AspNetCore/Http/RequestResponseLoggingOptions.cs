//------------------------------------------------------------------------------
// <copyright file="RequestResponseLoggingOptions.cs"
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

using Microsoft.Extensions.Logging;

namespace Cadru.AspNetCore.Http
{
    /// <summary>
    /// The options for <see cref="RequestResponseLoggingDelegatingHandler"/> or <see cref="RequestResponseLoggingMiddleware"/>.
    /// </summary>
    public class RequestResponseLoggingOptions
    {
        /// <summary>
        /// The configuration section key.
        /// </summary>
        public const string SectionKey = "RequestResponseLogging";

        /// <summary>
        /// Gets or sets a value indicating whether logging scopes are being
        /// captured. Defaults to <see langword="true"/>.
        /// </summary>
        public bool CaptureScopes { get; set; } = true;

        /// <summary>
        /// Gets or sets the log level of messages.
        /// </summary>
        public LogLevel LogLevel { get; set; } = LogLevel.Trace;
    }
}