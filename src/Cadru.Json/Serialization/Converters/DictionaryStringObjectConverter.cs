//------------------------------------------------------------------------------
// <copyright file="DictionaryStringObjectConverter.cs"
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
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cadru.Json.Serialization.Converters
{
    /// <inheritdoc/>
    public class DictionaryStringObjectJsonConverter : JsonConverter<Dictionary<string, object?>>
    {
        /// <inheritdoc/>
        public override Dictionary<string, object?> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException($"JsonTokenType was of type {reader.TokenType}, only objects are supported");
            }

            var dictionary = new Dictionary<string, object?>();
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return dictionary;
                }

                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new JsonException("JsonTokenType was not PropertyName");
                }

                var propertyName = reader.GetString();

                if (String.IsNullOrWhiteSpace(propertyName))
                {
                    throw new JsonException("Failed to get property name");
                }

                reader.Read();

                dictionary.Add(propertyName!, this.ExtractValue(ref reader, options));
            }

            return dictionary;
        }

        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer, Dictionary<string, object?> value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, options);
        }

        private object? ExtractValue(ref Utf8JsonReader reader, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.String:
                    if (reader.TryGetDateTime(out var date))
                    {
                        return date;
                    }
                    return reader.GetString();
                case JsonTokenType.False:
                    return false;
                case JsonTokenType.True:
                    return true;
                case JsonTokenType.Null:
                    return null;
                case JsonTokenType.Number:
                    if (reader.TryGetInt64(out var result))
                    {
                        return result;
                    }
                    return reader.GetDecimal();
                case JsonTokenType.StartObject:
                    return this.Read(ref reader, typeof(object), options);
                case JsonTokenType.StartArray:
                    var list = new List<object?>();
                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        list.Add(this.ExtractValue(ref reader, options));
                    }
                    return list;
                default:
                    throw new JsonException($"'{reader.TokenType}' is not supported");
            }
        }
    }
}
