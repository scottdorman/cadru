//------------------------------------------------------------------------------
// <copyright file="SqlServerStrategyFactory.cs"
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

using Microsoft.Extensions.DependencyInjection;

namespace Cadru.Polly.Data.SqlServer
{
    /// <summary>
    /// Represents a set of methods for creating instances of an <see
    /// cref="SqlServerStrategyBuilder"/>.
    /// </summary>
    public sealed class SqlServerStrategyFactory : SqlStrategyFactory
    {
        private SqlServerStrategyFactory()
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="SqlServerStrategyFactory"/>.
        /// </summary>
        public static SqlServerStrategyFactory Instance => new SqlServerStrategyFactory();

        /// <inheritdoc/>
        protected override SqlStrategyBuilder CreateStrategyBuilder(IServiceProvider serviceProvider, IEnumerable<IExceptionHandlingStrategy> exceptionHandlingStrategies)
        {
            return ActivatorUtilities.CreateInstance<SqlServerStrategyBuilder>(serviceProvider, exceptionHandlingStrategies);
        }
    }
}