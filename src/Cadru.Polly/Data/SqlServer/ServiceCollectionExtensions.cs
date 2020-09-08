
using Microsoft.Extensions.DependencyInjection;

namespace Cadru.Polly.Data.SqlServer
{
    /// <summary>
    /// Extension methods for adding exception handling strategies in an
    /// <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds default exception handling strategies to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
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
