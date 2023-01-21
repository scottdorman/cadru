//------------------------------------------------------------------------------
// <copyright file="SerializationExtensions.cs"
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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using Cadru.Json;

namespace Cadru.Json
{
    /// <summary>
    /// Extension methods to help serialize an object.
    /// </summary>
    public static class SerializationExtensions
    {
#pragma warning disable IDE0060 // Remove unused parameter
        public static T? DeserializeAnonymousType<T>(string json, T anonymousTypeObject, JsonSerializerOptions? options = default)
           => JsonSerializer.Deserialize<T>(json, options);

        public static ValueTask<TValue?> DeserializeAnonymousTypeAsync<TValue>(Stream stream, TValue anonymousTypeObject, JsonSerializerOptions? options = default, CancellationToken cancellationToken = default)
            => JsonSerializer.DeserializeAsync<TValue?>(stream, options, cancellationToken);
#pragma warning restore IDE0060 // Remove unused parameter

        /// <summary>
        /// Converts the value of a type specified by a generic type parameter into a JSON
        /// string.
        /// </summary>
        /// <typeparam name="TValue">The type of the value to serialize.</typeparam>
        /// <param name="value">The value to convert.</param>
        /// <param name="options">Options to control the conversion behavior.</param>
        /// <returns>The JSON string representation of the value.</returns>
        /// <exception cref="NotSupportedException">
        /// There is no compatible <see cref="System.Text.Json.Serialization.JsonConverter"/>
        /// for <typeparamref name="TValue"/> or its serializable members.
        /// </exception>
        public static string ToJson<TValue>(this TValue value, JsonSerializerOptions? options = null)
            where TValue : class
        {
            return JsonSerializer.Serialize(value, options);
        }

        /// <summary>
        /// Converts the value of a specified type into a JSON string.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="inputType">The type of the value to serialize.</param>
        /// <param name="options">Options to control the conversion behavior.</param>
        /// <returns>The JSON string representation of the value.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="inputType"/> is not compatible with value.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="inputType"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="NotSupportedException">
        /// There is no compatible <see cref="System.Text.Json.Serialization.JsonConverter"/>
        /// for <paramref name="inputType"/> or its serializable members.
        /// </exception>
        public static string ToJson(this object? value, Type inputType, JsonSerializerOptions? options = null)
        {
            return JsonSerializer.Serialize(value, inputType, options);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormUrlEncodedContent"/> class
        /// with the collection of key/value pairs representing the value.
        /// </summary>
        /// <param name="value">The value to represent.</param>
        /// <returns>A populated <see cref="FormUrlEncodedContent"/> instance
        /// representing all of the serializable properties in <paramref name="value"/>.</returns>
        public static FormUrlEncodedContent ToFormData(this object value)
        {
#if NET5_0
#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
#endif
            return new FormUrlEncodedContent(value.ToKeyValuePairs());
#if NET5_0
#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
#endif
        }

        /// <summary>
        /// Converts the value of a type to an <see cref="IDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="serializerOptions">The options to control serialization behavior.</param>
        /// <returns>An <see cref="IDictionary{TKey, TValue}"/> representation of the value</returns>
        public static IDictionary<string, string> ToDictionary(this object value, JsonSerializerOptions? serializerOptions = null)
        {
            return value.ToKeyValuePairs(serializerOptions).ToDictionary(keySelector => keySelector.Key, elementSelector => elementSelector.Value)!;
        }

        /// <summary>
        /// Converts the value of a type to a collection of <see cref="KeyValuePair{TKey, TValue}"/>.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="serializerOptions">The options to control serialization behavior.</param>
        /// <returns>A collection of <see cref="KeyValuePair{TKey, TValue}"/> instances which represent the value.</returns>
        /// <remarks>
        /// <para>The value is first serialized to a JSON string, which is then recursively parsed to
        /// create the key/value pairs.
        /// </para>
        /// <para>
        /// Nested objects are defined using keys which show their hierarchy in dot (".") notation, and
        /// collections are shown using an indexed notation.
        /// </para>
        /// </remarks>
        public static IEnumerable<KeyValuePair<string, string>> ToKeyValuePairs(this object value, JsonSerializerOptions? serializerOptions = null)
        {
            string GetStringValue(JsonElement jsonElement)
            {
                string? rawText = null;

                switch (jsonElement.ValueKind)
                {
                    case JsonValueKind.Number:
                        rawText = jsonElement.GetRawText();
                        break;

                    case JsonValueKind.String:
                        rawText = jsonElement.GetString();
                        break;
                }

                return rawText ?? jsonElement.GetRawText();
            }

            IEnumerable<KeyValuePair<string, string>> EnumerateElement(JsonElement jsonElement, string? parentProperty = null)
            {
                if (jsonElement.ValueKind == JsonValueKind.Object)
                {
                    foreach (var jsonProperty in jsonElement.EnumerateObject())
                    {
                        var propertyKey = String.IsNullOrWhiteSpace(parentProperty) ? jsonProperty.Name : $"{parentProperty}.{jsonProperty.Name}";

                        if (jsonProperty.Value.ValueKind == JsonValueKind.Object || jsonProperty.Value.ValueKind == JsonValueKind.Array)
                        {
                            foreach (var childElement in EnumerateElement(jsonProperty.Value, propertyKey))
                            {
                                yield return childElement;
                            }
                        }
                        else
                        {
                            yield return new KeyValuePair<string, string>(propertyKey, GetStringValue(jsonProperty.Value));
                        }
                    }
                }
                else if (jsonElement.ValueKind == JsonValueKind.Array)
                {
                    var index = 0;

                    foreach (var jsonArrayElement in jsonElement.EnumerateArray())
                    {
                        var arrayElementParentProperty = $"{parentProperty}[{index++}]";
                        if (jsonArrayElement.ValueKind == JsonValueKind.Object || jsonArrayElement.ValueKind == JsonValueKind.Array)
                        {
                            foreach (var childElement in EnumerateElement(jsonArrayElement, arrayElementParentProperty))
                            {
                                yield return childElement;
                            }
                        }
                        else
                        {
                            yield return new KeyValuePair<string, string>(arrayElementParentProperty, GetStringValue(jsonArrayElement));
                        }
                    }
                }
            }

            var dictionary = Enumerable.Empty<KeyValuePair<string, string>>();
            if (value != null)
            {
                var document = JsonDocument.Parse(JsonSerializer.Serialize(value, serializerOptions));
                dictionary = EnumerateElement(document.RootElement).ToList();
            }

            return dictionary;
        }
    }
}
