//------------------------------------------------------------------------------
// <copyright file="RequestResponseLoggingMiddlewareExtensions.cs"
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

using System.Diagnostics.CodeAnalysis;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cadru.AspNetCore.Http
{
    /// <summary>
    /// Extension methods for setting up request/response related services.
    /// </summary>
    public static class RequestResponseLoggingMiddlewareExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <param name="configuration">A set of key/value configuration properties.</param>
        /// <returns>The <see cref="IServiceCollection" /> so that additional calls can be chained.</returns>
        public static IServiceCollection AddRequestResponseLogging([NotNull] this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RequestResponseLoggingOptions>(configuration as IConfigurationSection ?? configuration.GetSection(RequestResponseLoggingOptions.SectionKey));
            return services;
        }

        /// <summary>
        /// Adds the <see cref="RequestResponseLoggingMiddleware" /> to the application's request pipeline.
        /// </summary>
        /// <param name="builder">The <see cref="IApplicationBuilder" /> to add middleware to.</param>
        /// <returns>The <see cref="IApplicationBuilder" /> so that additional calls can be chained.</returns>
        public static IApplicationBuilder UseRequestResponseLogging([NotNull] this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestResponseLoggingMiddleware>();
        }
    }
}