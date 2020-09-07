//------------------------------------------------------------------------------
// <copyright file="SqlStrategyConfigurationExtensions.cs"
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

using System;
using System.Diagnostics.CodeAnalysis;

using Cadru.Polly.Sql;

namespace Cadru.Polly.Utilities
{
    /// <summary>
    /// Helper methods for getting strongly typed values from an <see cref="ISqlStrategyConfiguration"/>.
    /// </summary>
    public static class SqlStrategyConfigurationExtensions
    {
        /// <summary>
        /// Gets the value of the specified element as a <see cref="Boolean"/>.
        /// </summary>
        /// <param name="sqlStrategyConfiguration"></param>
        /// <param name="key">The key of the element.</param>
        /// <param name="value">When this method returns, contains the value
        /// associated with the specified key, if the key is found in the
        /// <paramref name="sqlStrategyConfiguration"/>; otherwise, <see
        /// langword="null"/>. This parameter is passed uninitialized.</param>
        /// <returns><see langword="true"/> if the <paramref
        /// name="sqlStrategyConfiguration"/> contains an element with the
        /// specified key; otherwise, <see langword="false"/>.</returns>
        public static bool TryGetBoolean(this ISqlStrategyConfiguration sqlStrategyConfiguration, string key, [NotNullWhen(true)] out bool? value)
        {
            if (sqlStrategyConfiguration.TryGetValue(key, out var objectValue) && Boolean.TryParse(objectValue.ToString(), out var parsedValue))
            {
                value = parsedValue;
                return true;
            }

            value = null;
            return false;
        }

        /// <summary>
        /// Gets the value of the specified element as a <see cref="Decimal"/>.
        /// </summary>
        /// <param name="sqlStrategyConfiguration"></param>
        /// <param name="key">The key of the element.</param>
        /// <param name="value">When this method returns, contains the value
        /// associated with the specified key, if the key is found in the
        /// <paramref name="sqlStrategyConfiguration"/>; otherwise, <see
        /// langword="null"/>. This parameter is passed uninitialized.</param>
        /// <returns><see langword="true"/> if the <paramref
        /// name="sqlStrategyConfiguration"/> contains an element with the
        /// specified key; otherwise, <see langword="false"/>.</returns>
        public static bool TryGetDecimal(this ISqlStrategyConfiguration sqlStrategyConfiguration, string key, [NotNullWhen(true)] out decimal? value)
        {
            if (sqlStrategyConfiguration.TryGetValue(key, out var objectValue) && Decimal.TryParse(objectValue.ToString(), out var parsedValue))
            {
                value = parsedValue;
                return true;
            }

            value = null;
            return false;
        }

        /// <summary>
        /// Gets the value of the specified element as a <see cref="Double"/>.
        /// </summary>
        /// <param name="sqlStrategyConfiguration"></param>
        /// <param name="key">The key of the element.</param>
        /// <param name="value">When this method returns, contains the value
        /// associated with the specified key, if the key is found in the
        /// <paramref name="sqlStrategyConfiguration"/>; otherwise, <see
        /// langword="null"/>. This parameter is passed uninitialized.</param>
        /// <returns><see langword="true"/> if the <paramref
        /// name="sqlStrategyConfiguration"/> contains an element with the
        /// specified key; otherwise, <see langword="false"/>.</returns>
        public static bool TryGetDouble(this ISqlStrategyConfiguration sqlStrategyConfiguration, string key, [NotNullWhen(true)] out double? value)
        {
            if (sqlStrategyConfiguration.TryGetValue(key, out var objectValue) && Double.TryParse(objectValue.ToString(), out var parsedValue))
            {
                value = parsedValue;
                return true;
            }

            value = null;
            return false;
        }

        /// <summary>
        /// Gets the value of the specified element as a <see cref="Single"/>.
        /// </summary>
        /// <param name="sqlStrategyConfiguration"></param>
        /// <param name="key">The key of the element.</param>
        /// <param name="value">When this method returns, contains the value
        /// associated with the specified key, if the key is found in the
        /// <paramref name="sqlStrategyConfiguration"/>; otherwise, <see
        /// langword="null"/>. This parameter is passed uninitialized.</param>
        /// <returns><see langword="true"/> if the <paramref
        /// name="sqlStrategyConfiguration"/> contains an element with the
        /// specified key; otherwise, <see langword="false"/>.</returns>
        public static bool TryGetFloat(this ISqlStrategyConfiguration sqlStrategyConfiguration, string key, [NotNullWhen(true)] out float? value)
        {
            if (sqlStrategyConfiguration.TryGetValue(key, out var objectValue) && Single.TryParse(objectValue.ToString(), out var parsedValue))
            {
                value = parsedValue;
                return true;
            }

            value = null;
            return false;
        }

        /// <summary>
        /// Gets the value of the specified element as a <see cref="Int16"/>.
        /// </summary>
        /// <param name="sqlStrategyConfiguration"></param>
        /// <param name="key">The key of the element.</param>
        /// <param name="value">When this method returns, contains the value
        /// associated with the specified key, if the key is found in the
        /// <paramref name="sqlStrategyConfiguration"/>; otherwise, <see
        /// langword="null"/>. This parameter is passed uninitialized.</param>
        /// <returns><see langword="true"/> if the <paramref
        /// name="sqlStrategyConfiguration"/> contains an element with the
        /// specified key; otherwise, <see langword="false"/>.</returns>
        public static bool TryGetInt16(this ISqlStrategyConfiguration sqlStrategyConfiguration, string key, [NotNullWhen(true)] out short? value)
        {
            if (sqlStrategyConfiguration.TryGetValue(key, out var objectValue) && Int16.TryParse(objectValue.ToString(), out var parsedValue))
            {
                value = parsedValue;
                return true;
            }

            value = null;
            return false;
        }

        /// <summary>
        /// Gets the value of the specified element as a <see cref="Int32"/>.
        /// </summary>
        /// <param name="sqlStrategyConfiguration"></param>
        /// <param name="key">The key of the element.</param>
        /// <param name="value">When this method returns, contains the value
        /// associated with the specified key, if the key is found in the
        /// <paramref name="sqlStrategyConfiguration"/>; otherwise, <see
        /// langword="null"/>. This parameter is passed uninitialized.</param>
        /// <returns><see langword="true"/> if the <paramref
        /// name="sqlStrategyConfiguration"/> contains an element with the
        /// specified key; otherwise, <see langword="false"/>.</returns>
        public static bool TryGetInt32(this ISqlStrategyConfiguration sqlStrategyConfiguration, string key, [NotNullWhen(true)] out int? value)
        {
            if (sqlStrategyConfiguration.TryGetValue(key, out var objectValue) && Int32.TryParse(objectValue.ToString(), out var parsedValue))
            {
                value = parsedValue;
                return true;
            }

            value = null;
            return false;
        }

        /// <summary>
        /// Gets the value of the specified element as a <see cref="Int64"/>.
        /// </summary>
        /// <param name="sqlStrategyConfiguration"></param>
        /// <param name="key">The key of the element.</param>
        /// <param name="value">When this method returns, contains the value
        /// associated with the specified key, if the key is found in the
        /// <paramref name="sqlStrategyConfiguration"/>; otherwise, <see
        /// langword="null"/>. This parameter is passed uninitialized.</param>
        /// <returns><see langword="true"/> if the <paramref
        /// name="sqlStrategyConfiguration"/> contains an element with the
        /// specified key; otherwise, <see langword="false"/>.</returns>
        public static bool TryGetInt64(this ISqlStrategyConfiguration sqlStrategyConfiguration, string key, [NotNullWhen(true)] out long? value)
        {
            if (sqlStrategyConfiguration.TryGetValue(key, out var objectValue) && Int64.TryParse(objectValue.ToString(), out var parsedValue))
            {
                value = parsedValue;
                return true;
            }

            value = null;
            return false;
        }

        /// <summary>
        /// Gets the value of the specified element as a <see cref="String"/>.
        /// </summary>
        /// <param name="sqlStrategyConfiguration"></param>
        /// <param name="key">The key of the element.</param>
        /// <param name="value">When this method returns, contains the value
        /// associated with the specified key, if the key is found in the
        /// <paramref name="sqlStrategyConfiguration"/>; otherwise, <see
        /// langword="null"/>. This parameter is passed uninitialized.</param>
        /// <returns><see langword="true"/> if the <paramref
        /// name="sqlStrategyConfiguration"/> contains an element with the
        /// specified key; otherwise, <see langword="false"/>.</returns>
        public static bool TryGetString(this ISqlStrategyConfiguration sqlStrategyConfiguration, string key, [NotNullWhen(true)] out string? value)
        {
            if (sqlStrategyConfiguration.TryGetValue(key, out var objectValue))
            {
                value = objectValue?.ToString();
                return value != null;
            }

            value = null;
            return false;
        }

        /// <summary>
        /// Gets the value of the specified element as a <see cref="TimeSpan"/>.
        /// </summary>
        /// <param name="sqlStrategyConfiguration"></param>
        /// <param name="key">The key of the element.</param>
        /// <param name="value">When this method returns, contains the value
        /// associated with the specified key, if the key is found in the
        /// <paramref name="sqlStrategyConfiguration"/>; otherwise, <see
        /// langword="null"/>. This parameter is passed uninitialized.</param>
        /// <returns><see langword="true"/> if the <paramref
        /// name="sqlStrategyConfiguration"/> contains an element with the
        /// specified key; otherwise, <see langword="false"/>.</returns>
        public static bool TryGetTimeSpan(this ISqlStrategyConfiguration sqlStrategyConfiguration, string key, [NotNullWhen(true)] out TimeSpan? value)
        {
            if (sqlStrategyConfiguration.TryGetValue(key, out var objectValue) && TimeSpan.TryParse(objectValue.ToString(), out var parsedValue))
            {
                value = parsedValue;
                return true;
            }

            value = null;
            return false;
        }
    }
}