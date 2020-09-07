//------------------------------------------------------------------------------
// <copyright file="PolicyContextFactory.cs"
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
using Microsoft.Extensions.Logging.Abstractions;

using Polly;

namespace Cadru.Polly
{
    /// <summary>
    /// Represents a set of methods for creating instances of a <see
    /// cref="Context"/>.
    /// </summary>
    public class PolicyContextFactory : IPolicyContextFactory
    {
        private readonly ILoggerFactory _loggerFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="PolicyContextFactory"/>
        /// class using a <see cref="NullLoggerFactory"/> instance.
        /// </summary>
        public PolicyContextFactory() : this(new NullLoggerFactory())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolicyContextFactory"/>
        /// class using the provided <see cref="ILoggerFactory"/> instance.
        /// </summary>
        /// <param name="loggerFactory">The <see cref="ILoggerFactory"/>
        /// instance used to create an <see cref="ILogger"/> item in the <see
        /// cref="Context"/>.</param>
        public PolicyContextFactory(ILoggerFactory loggerFactory)
        {
            this._loggerFactory = loggerFactory;
        }

        /// <inheritdoc/>
        public Context CreateContext()
        {
            return new Context();
        }

        /// <inheritdoc/>
        public Context CreateContext(string categoryName)
        {
            return new Context().WithLogger(this._loggerFactory.CreateLogger(categoryName));
        }

        /// <inheritdoc/>
        public Context CreateContext(Type type)
        {
            return new Context().WithLogger(this._loggerFactory.CreateLogger(type));
        }

        /// <inheritdoc/>
        public Context CreateContext<T>()
        {
            return new Context().WithLogger(this._loggerFactory.CreateLogger<T>());
        }

        /// <inheritdoc/>
        public Context CreateContext(ILogger logger)
        {
            return new Context().WithLogger(logger);
        }
    }
}