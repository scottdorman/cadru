//------------------------------------------------------------------------------
// <copyright file="RetryManager.cs"
//  company="Scott Dorman"
//  library="Cadru">
//    Copyright (C) 2001-2017 Scott Dorman.
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

namespace Cadru.TransientFaultHandling
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Cadru.TransientFaultHandling.DetectionStrategies;
    using Cadru.TransientFaultHandling.Resources;
    using Cadru.TransientFaultHandling.RetryStrategies;

    /// <summary>
    /// Provides the entry point to the retry functionality.
    /// </summary>
    public class RetryManager
    {
        private static RetryManager defaultRetryManager;
        private readonly IDictionary<string, RetryStrategy> defaultRetryStrategiesMap;
        private readonly IDictionary<string, RetryStrategy> retryStrategies;
        private string defaultRetryStrategyName;
        private RetryStrategy defaultStrategy;

        /// <summary>
        /// Initializes a new instance of the <see cref="RetryManager"/> class.
        /// </summary>
        /// <param name="retryStrategies">The complete set of retry strategies.</param>
        public RetryManager(IEnumerable<RetryStrategy> retryStrategies)
            : this(retryStrategies, null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RetryManager"/> class with the specified retry
        /// strategies and default retry strategy name.
        /// </summary>
        /// <param name="retryStrategies">The complete set of retry strategies.</param>
        /// <param name="defaultRetryStrategyName">The default retry strategy.</param>
        public RetryManager(IEnumerable<RetryStrategy> retryStrategies, string defaultRetryStrategyName)
            : this(retryStrategies, defaultRetryStrategyName, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RetryManager"/> class with the specified retry
        /// strategies and defaults.
        /// </summary>
        /// <param name="retryStrategies">The complete set of retry strategies.</param>
        /// <param name="defaultRetryStrategyName">The default retry strategy.</param>
        /// <param name="defaultRetryStrategyNamesMap">The names of the default strategies for different technologies.</param>
        public RetryManager(IEnumerable<RetryStrategy> retryStrategies, string defaultRetryStrategyName,
            IDictionary<string, string> defaultRetryStrategyNamesMap)
        {
            this.retryStrategies = retryStrategies.ToDictionary(p => p.Name);
            DefaultRetryStrategyName = defaultRetryStrategyName;

            defaultRetryStrategiesMap = new Dictionary<string, RetryStrategy>();
            if (defaultRetryStrategyNamesMap != null)
            {
                foreach (var map in defaultRetryStrategyNamesMap.Where(x => !string.IsNullOrWhiteSpace(x.Value)))
                {
                    if (!this.retryStrategies.TryGetValue(map.Value, out RetryStrategy strategy))
                    {
                        throw new ArgumentOutOfRangeException(nameof(defaultRetryStrategyNamesMap), string.Format(CultureInfo.CurrentCulture, Strings.DefaultRetryStrategyMappingNotFound, map.Key, map.Value));
                    }

                    defaultRetryStrategiesMap.Add(map.Key, strategy);
                }
            }
        }

        /// <summary>
        /// Gets the default <see cref="RetryManager"/> for the application.
        /// </summary>
        public static RetryManager Instance
        {
            get
            {
                var instance = defaultRetryManager;
                if (instance == null)
                {
                    throw new InvalidOperationException(Strings.ExceptionRetryManagerNotSet);
                }

                return instance;
            }
        }

        /// <summary>
        /// Gets or sets the default retry strategy name.
        /// </summary>
        public string DefaultRetryStrategyName
        {
            get { return defaultRetryStrategyName; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (retryStrategies.TryGetValue(value, out RetryStrategy strategy))
                    {
                        defaultRetryStrategyName = value;
                        defaultStrategy = strategy;
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException("value", string.Format(CultureInfo.CurrentCulture,
                            Strings.RetryStrategyNotFound, value));
                    }
                }
                else
                {
                    defaultRetryStrategyName = null;
                }
            }
        }

        /// <summary>
        /// Sets the specified retry manager as the default retry manager.
        /// Will throw an exception if the manager is already set.
        /// </summary>
        /// <param name="retryManager">The retry manager.</param>
        public static void SetDefault(RetryManager retryManager)
        {
            SetDefault(retryManager, true);
        }

        /// <summary>
        /// Sets the specified retry manager as the default retry manager.
        /// </summary>
        /// <param name="retryManager">The retry manager.</param>
        /// <param name="throwIfSet">true to throw an exception if the manager is already set; otherwise, false.</param>
        public static void SetDefault(RetryManager retryManager, bool throwIfSet)
        {
            if (defaultRetryManager != null && throwIfSet && retryManager != defaultRetryManager)
            {
                throw new InvalidOperationException(Strings.ExceptionRetryManagerAlreadySet);
            }

            defaultRetryManager = retryManager;
        }


        /// <summary>
        /// Returns a retry policy with the specified error detection strategy and the default retry strategy
        /// defined in the configuration.
        /// </summary>
        /// <typeparam name="T">The type that implements the <see cref="ITransientErrorDetectionStrategy"/>
        /// interface that is responsible for detecting transient conditions.</typeparam>
        /// <returns>A new retry policy with the specified error detection strategy and the default retry
        /// strategy defined in the configuration.</returns>
        public virtual RetryPolicy<T> GetRetryPolicy<T>()
            where T : ITransientErrorDetectionStrategy, new()
        {
            return new RetryPolicy<T>(GetRetryStrategy());
        }

        /// <summary>
        /// Returns a retry policy with the specified error detection strategy and retry strategy.
        /// </summary>
        /// <typeparam name="T">The type that implements the <see cref="ITransientErrorDetectionStrategy"/>
        /// interface that is responsible for detecting transient conditions.</typeparam>
        /// <param name="retryStrategyName">The retry strategy name, as defined in the configuration.</param>
        /// <returns>A new retry policy with the specified error detection strategy and the default retry
        /// strategy defined in the configuration.</returns>
        public virtual RetryPolicy<T> GetRetryPolicy<T>(string retryStrategyName)
            where T : ITransientErrorDetectionStrategy, new()
        {
            return new RetryPolicy<T>(GetRetryStrategy(retryStrategyName));
        }

        /// <summary>
        /// Returns the default retry strategy defined in the configuration.
        /// </summary>
        /// <returns>The retry strategy that matches the default strategy.</returns>
        public virtual RetryStrategy GetRetryStrategy()
        {
            return defaultStrategy;
        }

        /// <summary>
        /// Returns the retry strategy that matches the specified name.
        /// </summary>
        /// <param name="retryStrategyName">The retry strategy name.</param>
        /// <returns>The retry strategy that matches the specified name.</returns>
        public virtual RetryStrategy GetRetryStrategy(string retryStrategyName)
        {
            if (string.IsNullOrEmpty(retryStrategyName))
            {
                throw new ArgumentException(String.Format(CultureInfo.CurrentCulture, Strings.StringCannotBeEmpty, nameof(retryStrategyName)));
            }

            if (!retryStrategies.TryGetValue(retryStrategyName, out RetryStrategy retryStrategy))
            {
                throw new ArgumentOutOfRangeException(string.Format(CultureInfo.CurrentCulture,
                    Strings.RetryStrategyNotFound, retryStrategyName));
            }

            return retryStrategy;
        }

        /// <summary>
        /// Returns the retry strategy for the specified technology.
        /// </summary>
        /// <param name="technology">The technology to get the default retry strategy for.</param>
        /// <returns>The retry strategy for the specified technology.</returns>
        public virtual RetryStrategy GetDefaultRetryStrategy(string technology)
        {
            if (string.IsNullOrEmpty(technology))
            {
                throw new ArgumentException(String.Format(CultureInfo.CurrentCulture, Strings.StringCannotBeEmpty, nameof(technology)));
            }

            if (!defaultRetryStrategiesMap.TryGetValue(technology, out RetryStrategy retryStrategy))
            {
                retryStrategy = defaultStrategy;
            }

            if (retryStrategy == null)
            {
                throw new ArgumentOutOfRangeException(string.Format(CultureInfo.CurrentCulture,
                    Strings.DefaultRetryStrategyNotFound, technology));
            }

            return retryStrategy;
        }
    }
}