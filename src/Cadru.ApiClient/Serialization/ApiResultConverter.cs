//------------------------------------------------------------------------------
// <copyright file="ApiResultConverter.cs"
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
using System.Text.Json;
using System.Text.Json.Serialization;

using Cadru.ApiClient.Models;

namespace Cadru.ApiClient.Serialization
{
    internal class ApiResultConverterFactory : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            if (!typeToConvert.IsGenericType)
            {
                return false;
            }

            if (typeToConvert.GetGenericTypeDefinition() != typeof(ApiResult<>))
            {
                return false;
            }

            return true;
        }

        public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            var keyType = typeToConvert.GenericTypeArguments[0];
            var converterType = typeof(ApiResultConverter<>).MakeGenericType(keyType);
            return (JsonConverter?)Activator.CreateInstance(converterType);
        }

        class ApiResultConverter<TData> : JsonConverter<IApiResult<TData>>
            where TData : class
        {
            public override IApiResult<TData>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                var value = new ApiResult<TData>();

                if (reader.TokenType != JsonTokenType.StartObject)
                {
                    throw new JsonException();
                }

                while (reader.Read() && reader.TokenType == JsonTokenType.PropertyName)
                {
                    if (reader.TokenType == JsonTokenType.PropertyName)
                    {
                        var tokenName = reader.GetString();
                        reader.Read();

                        switch (tokenName)
                        {
                            case JsonSerializationPropertyNames.Data:
                                value.Data = JsonSerializer.Deserialize<TData>(ref reader, options);
                                break;

                            case JsonSerializationPropertyNames.Error:
                                value.Error = JsonSerializer.Deserialize<ApiError>(ref reader, options);
                                break;
                        }
                    }
                }

                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return value;
                }

                throw new JsonException();
            }

            /// <inheritdoc/>
            public override void Write(Utf8JsonWriter writer, IApiResult<TData> value, JsonSerializerOptions options)
            {
                writer.WriteStartObject();
                writer.WritePropertyName(JsonSerializationPropertyNames.Data);
                JsonSerializer.Serialize(writer, value.Data, options);
                writer.WritePropertyName(JsonSerializationPropertyNames.Error);
                JsonSerializer.Serialize(writer, value.Error, options);
                writer.WriteEndObject();
            }
        }
    }
}
