//------------------------------------------------------------------------------
// <copyright file="ApiClientBuilderExtensions.cs"
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

using Cadru.ApiClient.Configuration.DependencyInjection;
using Cadru.ApiClient.Services;
using Cadru.AspNetCore.Http;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.Options;

namespace Cadru.ApiClient.Configuration.DependencyInjection
{
    /// <summary>
    /// Extension methods for configuring an <see cref="IApiClientBuilder"/>
    /// </summary>
    public static class ApiClientBuilderExtensions
    {
        /// <summary>
        /// Adds an <see cref="IResponseParser"/> implementation to the <see cref="Services.ApiClient"/>.
        /// </summary>
        /// <typeparam name="TApiResponseParser">The <see cref="IResponseParser"/> implementation.</typeparam>
        /// <param name="builder">The <see cref="IApiClientBuilder"/>.</param>
        /// <returns>An <see cref="IApiClientBuilder"/> that can be used to configure the client.</returns>
        public static IApiClientBuilder AddResponseParser<TApiResponseParser>(this IApiClientBuilder builder)
             where TApiResponseParser : class, IResponseParser
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.Services.TryAddSingleton<TApiResponseParser>();
            return builder;
        }

        public static IApiClientBuilder AddResponseParser<TApiResponseParser>(this IApiClientBuilder builder, Func<IServiceProvider, TApiResponseParser> implementationFactory)
             where TApiResponseParser : class, IResponseParser
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.Services.TryAddSingleton(implementationFactory);
            return builder;
        }

        /// <summary>
        /// Adds an additional message handler to the <see cref="HttpClient"/> which logs requests.
        /// </summary>
        /// <param name="builder">The <see cref="IApiClientBuilder"/>.</param>
        /// <returns>An <see cref="IApiClientBuilder"/> that can be used to configure the client.</returns>
        public static IApiClientBuilder AddRequestResponseLogging(this IApiClientBuilder builder)
        {
            builder.HttpClientBuilder.AddHttpMessageHandler<RequestResponseLoggingDelegatingHandler>();
            return builder;
        }

        /// <summary>
        /// Adds a delegate that will be used to configure the <see cref="HttpClient"/>.
        /// </summary>
        /// <param name="builder">The <see cref="IApiClientBuilder"/>.</param>
        /// <param name="configureHttpClient">A delegate that is used to configure the <see cref="HttpClient"/></param>
        /// <returns>An <see cref="IApiClientBuilder"/> that can be used to configure the client.</returns>
        /// <remarks>
        /// The <see cref="IServiceProvider"/> provided to <paramref name="configureHttpClient"/> will be the
        /// same application's root service provider instance.
        /// </remarks>
        public static IApiClientBuilder ConfigureHttpClient(this IApiClientBuilder builder, Action<IServiceProvider, HttpClient> configureHttpClient)
        {
            builder.HttpClientBuilder.ConfigureHttpClient(configureHttpClient);
            return builder;
        }

        public static IApiClientBuilder ConfigureHttpMessageHandlerBuilder(this IApiClientBuilder builder, Action<HttpMessageHandlerBuilder> configureBuilder)
        {
            builder.HttpClientBuilder.ConfigureHttpMessageHandlerBuilder(configureBuilder);
            return builder;
        }

        /// <summary>
        /// Returns a configured <typeparamref name="TOptions"/> instance with the given name.
        /// </summary>
        /// <typeparam name="TOptions">The type of options.</typeparam>
        /// <param name="serviceProvider">The <see cref="IServiceProvider"/>.</param>
        /// <param name="name">The name of the configuration instance.</param>
        /// <returns>A configured <typeparamref name="TOptions"/> instance.</returns>
        public static TOptions GetScopedOptions<TOptions>(this IServiceProvider serviceProvider, string name)
             where TOptions : class, IApiClientOptions
        {
            var snapshot = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IOptionsSnapshot<TOptions>>();
            return snapshot.Get(name);
        }
    }
}
