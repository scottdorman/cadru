//------------------------------------------------------------------------------
// <copyright file="IPolicyContextFactory.cs"
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

using Microsoft.Extensions.Logging;

using Polly;

namespace Cadru.Polly
{
    /// <summary>
    /// Represents a set of methods for creating instances of a <see
    /// cref="Context"/>.
    /// </summary>
    public interface IPolicyContextFactory
    {
        /// <summary>
        /// Create a new <see cref="Context"/> instance.
        /// </summary>
        /// <returns>A new <see cref="Context"/>.</returns>
        Context CreateContext();

        /// <summary>
        /// Create a new <see cref="Context"/> instance with the specified <see
        /// cref="ILogger"/>.
        /// </summary>
        /// <returns>A new <see cref="Context"/>.</returns>
        Context CreateContext(ILogger logger);

        /// <summary>
        /// Create a new <see cref="Context"/> instance with a <see
        /// cref="ILogger"/> using the provided category name for messages
        /// produced by the logger.
        /// </summary>
        /// <returns>A new <see cref="Context"/>.</returns>
        Context CreateContext(string categoryName);

        /// <summary>
        /// Create a new <see cref="Context"/> instance with a <see
        /// cref="ILogger"/> using the full name of the given type for messages
        /// produced by the logger.
        /// </summary>
        /// <returns>A new <see cref="Context"/>.</returns>
        Context CreateContext(Type type);

        /// <summary>
        /// Create a new <see cref="Context"/> instance with a <see
        /// cref="ILogger"/> using the full name of the given type for messages
        /// produced by the logger.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <returns>A new <see cref="Context"/>.</returns>
        Context CreateContext<T>();
    }
}