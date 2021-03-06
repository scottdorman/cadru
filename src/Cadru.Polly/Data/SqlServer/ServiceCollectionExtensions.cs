﻿//------------------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs"
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

using Microsoft.Extensions.DependencyInjection;

namespace Cadru.Polly.Data.SqlServer
{
    /// <summary>
    /// Extension methods for adding exception handling strategies in an <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds default exception handling strategies to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">
        /// The <see cref="IServiceCollection"/> to add services to.
        /// </param>
        /// <returns>
        /// The <see cref="IServiceCollection"/> so that additional calls can be chained.
        /// </returns>
        public static IServiceCollection UseExceptionHandlingStrategies(this IServiceCollection services)
        {
            services.AddTransient<IExceptionHandlingStrategy, SqlServerTransientExceptionHandlingStrategy>();
            services.AddTransient<IExceptionHandlingStrategy, NetworkConnectivityExceptionHandlingStrategy>();
            services.AddTransient<IExceptionHandlingStrategy, SqlServerTransientTransactionExceptionHandlingStrategy>();
            services.AddTransient<IExceptionHandlingStrategy, SqlServerTimeoutExceptionHandlingStrategy>();
            return services;
        }
    }
}