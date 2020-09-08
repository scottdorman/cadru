//------------------------------------------------------------------------------
// <copyright file="DapperContextOptions.cs"
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

namespace Cadru.Data.Dapper.Configuration
{
    /// <summary>
    /// Configuration options for a <see cref="DapperContext"/>
    /// </summary>
    public class DapperContextOptions
    {
        /// <summary>
        /// The configuration section key.
        /// </summary>
        public const string SectionKey = "DapperContext";

        /// <summary>
        /// Command logging options for a <see cref="DapperContext"/>
        /// </summary>
        public LoggingOptions Logging { get; set; } = new LoggingOptions();

        /// <summary>
        /// Command timeout options for a <see cref="DapperContext"/>
        /// </summary>
        public TimeoutOptions Timeout { get; set; } = new TimeoutOptions();
    }

    /// <summary>
    /// Command logging options for a <see cref="DapperContext"/>
    /// </summary>
    public class LoggingOptions
    {
        /// <summary>
        /// Gets a value indicating whether command definitions should be logged.
        /// </summary>
        public bool CommandDefinitionLoggingEnabled { get; set; }
    }

    /// <summary>
    /// Command timeout options for a <see cref="DapperContext"/>
    /// </summary>
    public class TimeoutOptions
    {
        /// <summary>
        /// Gets the default command timeout.
        /// </summary>
        public int DefaultCommandTimeout { get; set; }

        /// <summary>
        /// Gets an extended command timeout.
        /// </summary>
        public int ExtendedCommandTimeout { get; set; }
    }
}