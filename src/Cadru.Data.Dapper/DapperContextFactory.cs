//------------------------------------------------------------------------------
// <copyright file="Database.cs"
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

using System;

using Microsoft.Extensions.DependencyInjection;

namespace Cadru.Data.Dapper
{
    /// <summary>
    /// Represents a set of methods for creating instances of an <see
    /// cref="IDapperContext"/>.
    /// </summary>
    public static class DapperContextFactory
    {
        /// <summary>
        /// Create a new <typeparamref name="TDatabaseContext"/> instance.
        /// </summary>
        /// <returns>A new <typeparamref name="TDatabaseContext"/>.</returns>
        /// <typeparam name="TDatabaseContext"></typeparam>
        public static TDatabaseContext Create<TDatabaseContext>(IServiceProvider serviceProvider) where TDatabaseContext : class, IDapperContext
        {
            var dbContext = ActivatorUtilities.CreateInstance<TDatabaseContext>(serviceProvider);

            if (dbContext is IDapperPollyContext dapperPollyContext && dapperPollyContext.PollyEnabled && dapperPollyContext.SqlStrategyFactory != null)
            {
                dapperPollyContext.SqlStrategy = dapperPollyContext.SqlStrategyFactory.Create(serviceProvider, dapperPollyContext.ExceptionHandlingStrategies);
            }

            return dbContext;
        }
    }
}