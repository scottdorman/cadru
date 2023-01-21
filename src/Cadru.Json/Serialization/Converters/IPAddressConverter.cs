//------------------------------------------------------------------------------
// <copyright file="IPAddressConverter.cs"
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
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

using Cadru.Json.Resources;

namespace Cadru.Json.Serialization.Converters
{
    /// <inheritdoc/>
    public class IPAddressConverter : JsonConverter<IPAddress?>
    {
        /// <inheritdoc/>
        public override IPAddress? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.String)
            {
                throw new JsonException(String.Format(Strings.JsonConverter_IPAddress_ParseError, reader.ValueSpan.ToString()));
            }

            return !IPAddress.TryParse(reader.GetValueText(), out var value)
                ? throw new JsonException(String.Format(Strings.JsonConverter_IPAddress_ParseError, reader.ValueSpan.ToString()))
                : value;
        }

        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer, IPAddress? value, JsonSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNullValue();
            }
            else
            {
                writer.WriteStringValue(value.ToString());
            }
        }
    }
}