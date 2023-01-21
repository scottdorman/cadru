//------------------------------------------------------------------------------
// <copyright file="DefaultJsonSerializerOptions.cs"
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

using System.Text.Json;
using System.Text.Json.Serialization;

using Cadru.Json.Serialization.Converters;

namespace Cadru.ApiClient.Serialization
{
    /// <summary>
    /// Provides default options to be used with JSON serialization.
    /// </summary>
    public static class DefaultJsonSerializerOptions
    {
        private static readonly JsonSerializerOptions _defaultSerializerOptions = new(JsonSerializerDefaults.Web)
        {
            WriteIndented = true,
            DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
            Converters =
            {
                new JsonStringEnumConverter(JsonNamingPolicy.CamelCase),
                new ApiResultConverterFactory(),
                new BooleanConverter(),
            }
        };

        /// <summary>
        /// Creates the default options to be used with JSON serialization.
        /// </summary>
        public static JsonSerializerOptions Create() => _defaultSerializerOptions;
    }
}
