using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using Cadru.ApiClient.Models;

namespace Cadru.ApiClient.Extensions
{
    /// <summary>
    /// Provides basic routines for serialization.
    /// </summary>
    public static class ModelSerializationExtensions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormUrlEncodedContent"/> class
        /// with the collection of key/value pairs representing the value.
        /// </summary>
        /// <param name="value">The value to represent.</param>
        /// <returns>A populated <see cref="FormUrlEncodedContent"/> instance
        /// representing all of the serializable properties in <paramref name="value"/>.</returns>
        public static FormUrlEncodedContent ToFormData(this object value)
        {
            return new FormUrlEncodedContent(value.ToKeyValuePairs());
        }

        /// <summary>
        /// Converts the value of a type to an <see cref="IDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="serializerOptions">The options to control serialization behavior.</param>
        /// <returns>An <see cref="IDictionary{TKey, TValue}"/> representation of the value</returns>
        public static IDictionary<string?, string?> ToDictionary(this object value, JsonSerializerOptions? serializerOptions = null)
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
        public static IEnumerable<KeyValuePair<string?, string?>> ToKeyValuePairs(this object value, JsonSerializerOptions? serializerOptions = null)
        {
            string? GetStringValue(JsonElement jsonElement)
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

            IEnumerable<KeyValuePair<string?, string?>> EnumerateElement(JsonElement jsonElement, string? parentProperty = null)
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
                            yield return new KeyValuePair<string?, string?>(propertyKey, GetStringValue(jsonProperty.Value));
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
                            yield return new KeyValuePair<string?, string?>(arrayElementParentProperty, GetStringValue(jsonArrayElement));
                        }
                    }
                }
            }

            var dictionary = Enumerable.Empty<KeyValuePair<string?, string?>>();
            if (value != null)
            {
                var document = JsonDocument.Parse(JsonSerializer.Serialize(value, serializerOptions));
                dictionary = EnumerateElement(document.RootElement).ToList();
            }

            return dictionary;
        }
    }
}
