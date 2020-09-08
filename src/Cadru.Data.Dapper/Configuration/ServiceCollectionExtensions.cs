using System;

using Cadru.Polly.Data;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cadru.Data.Dapper.Configuration
{
    public static class DapperConfigurationExtensions
    {
        public static IServiceCollection AddDapperContext<TDatabaseContext>(this IServiceCollection services, IConfiguration configuration, Action<IServiceProvider, DapperContextBuilder> contextBuilderAction) where TDatabaseContext : class, IDapperContext
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

        private static DapperContextBuilder CreateContextBuilder(IServiceProvider serviceProvider, Action<IServiceProvider, DapperContextBuilder> contextBuilderAction)
        {
            var contextBuilder = new DapperContextBuilder();
            contextBuilderAction?.Invoke(serviceProvider, contextBuilder);
            return contextBuilder;
        }
    }
}
