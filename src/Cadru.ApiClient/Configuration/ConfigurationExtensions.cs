using System;

using Cadru.Extensions;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cadru.ApiClient.Configuration
{
    /// <summary>
    /// Extension methods for setting up <see cref="IApiConfiguration"/> related
    /// services in an <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ConfigurationExtensions
    {
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
        public static IServiceCollection BindConfigurationSection<T>(this IServiceCollection services, IConfiguration configuration, out T instance) where T: class, new()
        {
            instance = new T();
            configuration.Bind(instance);
            services.AddSingleton(instance);
            return services;
        }
    }
}
