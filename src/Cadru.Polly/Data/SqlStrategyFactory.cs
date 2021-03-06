﻿//------------------------------------------------------------------------------
// <copyright file="SqlStrategyFactory.cs"
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
using System.Collections.Generic;

namespace Cadru.Polly.Data
{
    /// <summary>
    /// Represents a set of methods for creating instances of an <see cref="ISqlStrategy"/>.
    /// </summary>
    public abstract class SqlStrategyFactory : ISqlStrategyFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlStrategyFactory"/> class.
        /// </summary>
        protected SqlStrategyFactory()
        {
        }

        /// <inheritdoc/>
        public ISqlStrategy Create(IServiceProvider serviceProvider, IEnumerable<IExceptionHandlingStrategy> exceptionHandlingStrategies)
        {
            return this.CreateStrategyBuilder(serviceProvider, exceptionHandlingStrategies).Build();
        }

        /// <summary>
        /// When overridden in a derived class, creates a new
        /// <see cref="ISqlStrategy"/> instance.
        /// </summary>
        /// <returns>A new <see cref="ISqlStrategy"/>.</returns>
        protected abstract SqlStrategyBuilder CreateStrategyBuilder(IServiceProvider serviceProvider, IEnumerable<IExceptionHandlingStrategy> exceptionHandlingStrategies);
    }
}