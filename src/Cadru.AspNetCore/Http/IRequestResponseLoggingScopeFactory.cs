//------------------------------------------------------------------------------
// <copyright file="ILoggingScopeFactory.cs"
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

using System.Net.Http;
using System.Threading.Tasks;

using Cadru.Core.Logging;

using Microsoft.AspNetCore.Http;

namespace Cadru.AspNetCore.Http
{
    /// <summary>
    /// Provides methods for creating a
    /// <see cref="LoggingScope"/> from an
    /// <see cref="HttpRequestMessage"/> or <see cref="HttpRequest"/>.
    /// </summary>
    public interface IRequestResponseLoggingScopeFactory
    {
        /// <summary>
        /// Creates a <see cref="LoggingScope"/> instance from
        /// the given <see cref="HttpRequestMessage"/>.
        /// </summary>
        /// <param name="requestMessage">
        /// The <see cref="HttpRequestMessage"/> whose properties will be added
        /// to the logging scope.
        /// </param>
        /// <returns>
        /// A <see cref="LoggingScope"/> with properties
        /// populated from the <paramref name="requestMessage"/>.
        /// </returns>
        Task<LoggingScope> ToScopeObjectAsync(HttpRequestMessage requestMessage);

        /// <summary>
        /// Creates a <see cref="LoggingScope"/> instance from
        /// the given <see cref="HttpRequest"/>.
        /// </summary>
        /// <param name="request">
        /// The <see cref="HttpRequest"/> whose properties will be added to the
        /// logging scope.
        /// </param>
        /// <returns>
        /// A <see cref="LoggingScope"/> with properties
        /// populated from the <paramref name="request"/>.
        /// </returns>
        Task<LoggingScope> ToScopeObjectAsync(HttpRequest request);

        /// <summary>
        /// Creates a <see cref="LoggingScope"/> instance from
        /// the given <see cref="HttpContext"/>.
        /// </summary>
        /// <param name="httpContext">
        /// The <see cref="HttpContext"/> whose properties will be added to the
        /// logging scope.
        /// </param>
        /// <returns>
        /// A <see cref="LoggingScope"/> with properties
        /// populated from the <paramref name="httpContext"/>.
        /// </returns>
        Task<LoggingScope> ToScopeObjectAsync(HttpContext httpContext);
    }
}