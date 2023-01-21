using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

using System;

namespace Cadru.Core.Logging
{
    /// <summary>
    /// <see cref="ILoggerFactory"/> extension methods for common scenarios.
    /// </summary>
    public static class LoggerFactoryExtensions
    {
        /// <summary>
        /// Creates a new <see cref="ILogger"/> instance using the full
        /// name of the given type.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="factory">The factory.</param>
        /// <returns>The <see cref="ILogger"/> that was created.</returns>
        /// <remarks>If the provider <paramref name="factory"/> is <see langword="null"/>,
        /// then the <see cref="NullLoggerFactory"/> is used to create the
        /// <see cref="ILogger"/> instance.</remarks>
        public static ILogger<T> SafeCreateLogger<T>(this ILoggerFactory factory) => (factory ?? NullLoggerFactory.Instance).CreateLogger<T>();

        /// <summary>
        /// Creates a new <see cref="ILogger"/> instance using the full
        /// name of the given type.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="type">The type.</param>
        /// <returns>The <see cref="ILogger"/> that was created.</returns>
        /// <remarks>If the provider <paramref name="factory"/> is <see langword="null"/>,
        /// then the <see cref="NullLoggerFactory"/> is used to create the
        /// <see cref="ILogger"/> instance.</remarks>
        public static ILogger SafeCreateLogger(this ILoggerFactory factory, Type type) => (factory ?? NullLoggerFactory.Instance).CreateLogger(type);
    }
}
