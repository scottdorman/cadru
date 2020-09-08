//------------------------------------------------------------------------------
// <copyright file="ContextExtensions.cs"
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

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using Cadru.Contracts;

using Microsoft.Extensions.Logging;

using Polly;

namespace Cadru.Polly
{
    /// <summary>
    /// Extension methods for working with a <see cref="Context"/>.
    /// </summary>
    public static class ContextExtensions
    {
        /// <summary>
        /// Gets an <see cref="ILogger"/> instance from the <see
        /// cref="Context"/>.
        /// </summary>
        /// <param name="context">The <see cref="Context" /> to retrieve items
        /// from.</param>
        /// <returns>An <see cref="ILogger"/> instance if one is found in the
        /// <paramref name="context"/>; otherwise, <see
        /// langword="null"/>.</returns>
        public static ILogger? GetLogger(this Context context)
        {
            context.TryGetLogger(out var logger);
            return logger;
        }

        /// <summary>
        /// Gets an <see cref="ILogger"/> instance from the <see
        /// cref="Context"/>.
        /// </summary>
        /// <param name="context">The <see cref="Context" /> to retrieve items
        /// from.</param>
        /// <param name="logger">When this method returns, contains the <see
        /// cref="ILogger"/> instance if one is found in the <paramref
        /// name="context"/>; otherwise, <see langword="null"/>. This parameter
        /// is passed uninitialized.</param>
        /// <returns><see langword="true"/> if the <paramref name="context"/>
        /// contains an <see cref="ILogger"/> instance; otherwise, <see
        /// langword="false"/>.</returns>
        public static bool TryGetLogger(this Context context, [NotNullWhen(true)] out ILogger? logger)
        {
            if (context.TryGetValue(PolicyContextItems.Logger, out ILogger contextLogger))
            {
                logger = contextLogger;
                return true;
            }
            else
            {
                logger = null;
                return false;
            }
        }

        /// <summary>
        /// Gets the value associated with the specified key from the <see
        /// cref="Context"/>.
        /// </summary>
        /// <param name="context">The <see cref="Context" /> to retrieve items
        /// from.</param>
        /// <param name="key">The key of the value to get.</param>
        /// <param name="value">When this method returns, contains the value
        /// associated with the specified key, if the key is found in the
        /// <paramref name="context"/>; otherwise, the default value for the
        /// type of the <paramref name="value"/> parameter. This parameter is
        /// passed uninitialized.</param>
        /// <returns><see langword="true"/> if the <paramref name="context"/>
        /// contains an element with the specified key; otherwise, <see
        /// langword="false"/>.</returns>
        public static bool TryGetValue<T>(this Context context, string key, out T value)
        {
            if (context.TryGetValue(key, out var objectValue) && objectValue is T convertedValue)
            {
                value = convertedValue;
                return true;
            }
            else
            {
                value = default!;
                return false;
            }
        }

        /// <summary>
        /// Adds additional items to the <see cref="Context"/>.
        /// </summary>
        /// <param name="context">The <see cref="Context" /> to add items
        /// to.</param>
        /// <param name="contextData">The <see cref="IDictionary{TKey,
        /// TValue}"/> whose elements are copied into the <paramref
        /// name="context"/>.</param>
        /// <returns>The <see cref="Context"/> so that additional calls can be
        /// chained.</returns>
        public static Context WithContextData(this Context context, IDictionary<string, object> contextData)
        {
            Requires.NotNull(contextData, nameof(contextData));
            foreach (var item in contextData)
            {
                context.Add(item.Key, item.Value);
            }

            return context;
        }

        /// <summary>
        /// Adds additional items to the <see cref="Context"/>.
        /// </summary>
        /// <param name="context">The <see cref="Context" /> to add items
        /// to.</param>
        /// <param name="contextData">The <see
        /// cref="IEnumerable{T}"/> whose elements are
        /// copied into the <paramref name="context"/>.</param>
        /// <returns>The <see cref="Context"/> so that additional calls can be
        /// chained.</returns>
        public static Context WithContextData(this Context context, IEnumerable<KeyValuePair<string, object>> contextData)
        {
            Requires.NotNull(contextData, nameof(contextData));
            foreach (var item in contextData)
            {
                context.Add(item.Key, item.Value);
            }

            return context;
        }

        /// <summary>
        /// Adds the specified <see cref="ILogger"/> instance to <see
        /// cref="Context"/>.
        /// </summary>
        /// <param name="context">The <see cref="Context" /> to add the
        /// <paramref name="logger"/> to.</param>
        /// <param name="logger">The <see cref="ILogger"/> instance to be added.</param>
        /// <returns>The <see cref="Context"/> so that additional calls can be
        /// chained.</returns>
        public static Context WithLogger(this Context context, ILogger logger)
        {
            context[PolicyContextItems.Logger] = logger;
            return context;
        }
    }
}
