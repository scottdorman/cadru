//------------------------------------------------------------------------------
// <copyright file="ErrorDetailConverter.cs"
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
    /// <summary>
    ///  Converts an <see cref="HeaderMessageErrorDetail"/> to or from JSON.
    /// </summary>
    internal class HeaderMessageErrorDetailConverter : JsonConverter<HeaderMessageErrorDetail>
    {
        /// <inheritdoc/>
        public override HeaderMessageErrorDetail? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = new HeaderMessageErrorDetail();

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
                        case JsonSerializationPropertyNames.MessageHeader:
                            value.Header = reader.GetString();
                            break;

                        case JsonSerializationPropertyNames.Message:
                            value.Message = reader.GetString();
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
        public override void Write(Utf8JsonWriter writer, HeaderMessageErrorDetail value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString(JsonSerializationPropertyNames.MessageHeader, value.Header);
            writer.WriteString(JsonSerializationPropertyNames.Message, value.Message);
            writer.WriteEndObject();
        }
    }
}
