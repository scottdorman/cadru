//------------------------------------------------------------------------------
// <copyright file="ApiClientFactoryServiceCollectionExtensions.cs"
//  company="Scott Dorman"
//  library="Cadru">
//    Copyright (C) 2001-2021 Scott Dorman.
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
using System.Net.Http;

using Microsoft.Extensions.DependencyInjection;

namespace Cadru.ApiClient.Configuration.DependencyInjection
{
    /// <summary>
    /// Extension methods to configure an <see cref="IServiceCollection"/> for an <see cref="Services.ApiClient"/>.
    /// </summary>
    public static class ApiClientFactoryServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the <typeparamref name="TClient"/> and related services to the
        /// <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/>.</param>
        /// <returns>An <see cref="IApiClientBuilder"/> that can be used to configure the client.</returns>
        public static IApiClientBuilder AddApiClient<TClient, TImplementation>(this IServiceCollection services)
            where TClient : class
            where TImplementation : class, TClient
            => AddApiClient<TClient, TImplementation>(services, null);

        /// <summary>
        /// Adds the <typeparamref name="TClient"/> and related services to the
        /// <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/>.</param>
        /// <param name="name">The logical name of the <see cref="Services.ApiClient"/> to configure.</param>
        /// <returns>An <see cref="IApiClientBuilder"/> that can be used to configure the client.</returns>
        /// <remarks>If <paramref name="name"/> is <see langword="null"/>,empty, or consists only of white-space
        /// characters, the type name of <typeparamref name="TClient"/> will be used.</remarks>
        public static IApiClientBuilder AddApiClient<TClient, TImplementation>(this IServiceCollection services, string? name)
            where TClient : class
            where TImplementation : class, TClient
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                name = TypeNameHelper.GetTypeDisplayName(typeof(TClient), fullName: false);
            }

            var httpClientBuilder = services.AddHttpClient<TClient, TImplementation>(name!);
            return new DefaultApiClientBuilder(services, name!, httpClientBuilder);
        }

        public static IApiClientBuilder AddApiClient<TClient, TImplementation>(this IServiceCollection services, string? name, Func<HttpClient, IServiceProvider, TImplementation> factory)
            where TClient : class
            where TImplementation : class, TClient
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                name = TypeNameHelper.GetTypeDisplayName(typeof(TClient), fullName: false);
            }

            var httpClientBuilder = services.AddHttpClient<TClient, TImplementation>(name!, factory);
            return new DefaultApiClientBuilder(services, name!, httpClientBuilder);
        }
    }
}
