using System;
using System.Diagnostics.CodeAnalysis;

using Cadru.Polly.Data;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cadru.Data.Dapper.Configuration
{
    /// <summary>
    /// Extension methods for setting up <see cref="DapperContext"/> related services in an
    /// <see cref="IServiceCollection"/>.
    /// </summary>
    public static class DapperConfigurationExtensions
    {
        /// <summary>
        /// Registers the given <see cref="DapperContext"/> as a service in the
        /// <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TDatabaseContext">The type of context to be registered.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <param name="configuration">A set of key/value configuration properties.</param>
        /// <param name="contextBuilderAction">An optional action to configure the
        /// <see cref="DapperContextBuilder"/> for the context.</param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection AddDapperContext<TDatabaseContext>([NotNull] this IServiceCollection services, IConfiguration configuration, [MaybeNull] Action<IServiceProvider, DapperContextBuilder>? contextBuilderAction) where TDatabaseContext : class, IDapperContext
        {
            services.AddDapperConfiguration(configuration);
            services.AddTransient(sp => CreateContextBuilder(sp, contextBuilderAction));
            services.AddSingleton(sp => DapperContextFactory.Create<TDatabaseContext>(sp));
            return services;
        }

        private static IServiceCollection AddDapperConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DapperContextOptions>(configuration as IConfigurationSection ?? configuration.GetSection(DapperContextOptions.SectionKey));
            services.Configure<SqlStrategyOptions>(configuration as IConfigurationSection ?? configuration.GetSection(SqlStrategyOptions.SectionKey));
            return services;
        }

        private static DapperContextBuilder CreateContextBuilder(IServiceProvider serviceProvider, Action<IServiceProvider, DapperContextBuilder>? contextBuilderAction)
        {
            var contextBuilder = new DapperContextBuilder();
            contextBuilderAction?.Invoke(serviceProvider, contextBuilder);
            return contextBuilder;
        }
    }
}
