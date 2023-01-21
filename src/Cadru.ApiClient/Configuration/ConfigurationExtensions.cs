//------------------------------------------------------------------------------
// <copyright file="ConfigurationExtensions.cs"
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

using Cadru.ApiClient.Configuration.DependencyInjection;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace Cadru.ApiClient.Configuration
{
    /// <summary>
    /// Extension methods for setting up <see cref="IApiClientOptions"/> related
    /// services in an <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Gets an options builder that forwards Configure calls for the same
        /// named <typeparamref name="TOptions"/> to the underlying service
        /// collection and registers it for validation.
        /// </summary>
        /// <typeparam name="TOptions">The options type to be configured.</typeparam>
        /// <typeparam name="TOptionsValidator">The </typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
        /// <param name="configSectionPath">The name of the configuration section to bind from.</param>
        /// <param name="validateOnStart"><see langword="true"/> to enforce
        /// options validation check on start rather than in runtime. The
        /// default is <see langword="false"/>.</param>
        /// <returns>The <see cref="OptionsBuilder{TOptions}"/> so that
        /// additional calls can be chained.</returns>
        /// <remarks>
        /// <para>
        /// The registered <typeparamref name="TOptions"/> instance
        /// is also registered as a singleton which forwards to
        /// <see cref="IOptions{TOptions}.Value"/>, allowing either an
        /// <see cref="IOptions{TOptions}"/> or <typeparamref name="TOptions"/>
        /// instance to be used in dependency injection.
        /// </para>
        /// </remarks>
        public static IServiceCollection AddApiClientOptions<TOptions, TOptionsValidator>(this IServiceCollection services, string configSectionPath, bool validateOnStart = false)
            where TOptions : class, IApiClientOptions
            where TOptionsValidator : ApiClientOptionsValidator<TOptions>
            => AddApiClientOptions<TOptions, TOptionsValidator>(services, configSectionPath, configSectionPath, validateOnStart);

        /// <summary>
        /// Gets an options builder that forwards Configure calls for the same
        /// named <typeparamref name="TOptions"/> to the underlying service
        /// collection and registers it for validation.
        /// </summary>
        /// <typeparam name="TOptions">The options type to be configured.</typeparam>
        /// <typeparam name="TOptionsValidator">The </typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
        /// <param name="configSectionPath">The name of the configuration section to bind from.</param>
        /// <param name="name">The name of the options instance.</param>
        /// <param name="validateOnStart"><see langword="true"/> to enforce
        /// options validation check on start rather than in runtime. The
        /// default is <see langword="false"/>.</param>
        /// <returns>The <see cref="OptionsBuilder{TOptions}"/> so that
        /// additional calls can be chained.</returns>
        /// <remarks>
        /// <para>
        /// The registered <typeparamref name="TOptions"/> instance
        /// is also registered as a singleton which forwards to
        /// <see cref="IOptions{TOptions}.Value"/>, allowing either an
        /// <see cref="IOptions{TOptions}"/> or <typeparamref name="TOptions"/>
        /// instance to be used in dependency injection.
        /// </para>
        /// </remarks>
        public static IServiceCollection AddApiClientOptions<TOptions, TOptionsValidator>(this IServiceCollection services, string name, string configSectionPath, bool validateOnStart = false)
            where TOptions : class, IApiClientOptions
            where TOptionsValidator : ApiClientOptionsValidator<TOptions>
        {
            var optionsBuilder = services.AddOptions<TOptions>(name)
                .BindConfiguration(configSectionPath)
                .ValidateConfiguration<TOptions, TOptionsValidator>();

            if (validateOnStart)
            {
                optionsBuilder.ValidateOnStart();
            }

            services.TryAddSingleton(resolver => resolver.GetRequiredService<IOptions<TOptions>>().Value);
            return services;
        }

        /// <summary>
        /// Gets an options builder that forwards Configure calls for the same
        /// named <typeparamref name="TOptions"/> to the underlying service
        /// collection and registers it for validation.
        /// </summary>
        /// <typeparam name="TOptions">The options type to be configured.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
        /// <param name="configSectionPath">The name of the configuration section to bind from.</param>
        /// <param name="validateOnStart"><see langword="true"/> to enforce
        /// options validation check on start rather than in runtime. The
        /// default is <see langword="false"/>.</param>
        /// <returns>The <see cref="OptionsBuilder{TOptions}"/> so that
        /// additional calls can be chained.</returns>
        /// <remarks>
        /// <para>
        /// The registered <typeparamref name="TOptions"/> instance
        /// is also registered as a singleton which forwards to
        /// <see cref="IOptions{TOptions}.Value"/>, allowing either an
        /// <see cref="IOptions{TOptions}"/> or <typeparamref name="TOptions"/>
        /// instance to be used in dependency injection.
        /// </para>
        /// </remarks>
        public static IServiceCollection AddApiClientOptions<TOptions>(this IServiceCollection services, string configSectionPath, bool validateOnStart = false)
            where TOptions : class, IApiClientOptions
            => AddApiClientOptions<TOptions, ApiClientOptionsValidator<TOptions>>(services, configSectionPath, validateOnStart);

        /// <summary>
        /// Gets an options builder that forwards Configure calls for the same
        /// named <typeparamref name="TOptions"/> to the underlying service
        /// collection and registers it for validation.
        /// </summary>
        /// <typeparam name="TOptions">The options type to be configured.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
        /// <param name="configSectionPath">The name of the configuration section to bind from.</param>
        /// <param name="name">The name of the options instance.</param>
        /// <param name="validateOnStart"><see langword="true"/> to enforce
        /// options validation check on start rather than in runtime. The
        /// default is <see langword="false"/>.</param>
        /// <returns>The <see cref="OptionsBuilder{TOptions}"/> so that
        /// additional calls can be chained.</returns>
        /// <remarks>
        /// <para>
        /// The registered <typeparamref name="TOptions"/> instance
        /// is also registered as a singleton which forwards to
        /// <see cref="IOptions{TOptions}.Value"/>, allowing either an
        /// <see cref="IOptions{TOptions}"/> or <typeparamref name="TOptions"/>
        /// instance to be used in dependency injection.
        /// </para>
        /// </remarks>
        public static IServiceCollection AddApiClientOptions<TOptions>(this IServiceCollection services, string name, string configSectionPath, bool validateOnStart = false)
            where TOptions : class, IApiClientOptions
            => AddApiClientOptions<TOptions, ApiClientOptionsValidator<TOptions>>(services, name, configSectionPath, validateOnStart);

        /// <summary>
        /// Attempts to bind the configuration values to an object instance by matching
        /// property names against configuration keys recursively.
        /// </summary>
        /// <typeparam name="T">The object instance type.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
        /// <param name="configuration">A set of key/value configuration properties.</param>
        /// <param name="instance">The bound object instance.</param>
        /// <returns>
        /// The <see cref="IServiceCollection"/> so that additional calls can be chained.
        /// </returns>
        public static IServiceCollection BindConfigurationSection<T>(this IServiceCollection services, IConfiguration configuration, out T instance) where T : class, new()
        {
            instance = new T();
            configuration.Bind(instance);
            services.AddSingleton(instance);
            return services;
        }

        /// <summary>
        /// Registers the <typeparamref name="TOptions"/> instance for validation.
        /// </summary>
        /// <typeparam name="TOptions">The options type to be configured.</typeparam>
        /// <typeparam name="TValidator">
        /// The <see cref="ApiClientOptionsValidator{TOptions}"/> type to be
        /// registered.</typeparam>
        /// <param name="optionsBuilder">The options builder to add the services to.</param>
        /// <returns>The <see cref="OptionsBuilder{TOptions}"/> so that
        /// additional calls can be chained.</returns>
        public static OptionsBuilder<TOptions> ValidateConfiguration<TOptions, TValidator>(this OptionsBuilder<TOptions> optionsBuilder)
            where TOptions : class, IApiClientOptions
            where TValidator : ApiClientOptionsValidator<TOptions>
        {
            var validator = Activator.CreateInstance(typeof(TValidator), optionsBuilder.Name);
            if (validator == null)
            {
                throw new InvalidOperationException();
            }

            optionsBuilder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IValidateOptions<TOptions>>((TValidator)validator));
            return optionsBuilder;
        }
    }
}
