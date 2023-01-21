//------------------------------------------------------------------------------
// <copyright file="BooleanConverter.cs"
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
using System.Text.Json;
using System.Text.Json.Serialization;

using Cadru.Extensions;
using Cadru.Json.Resources;

namespace Cadru.Json.Serialization.Converters
{
    /// <inheritdoc/>
    public class BooleanConverter : JsonConverter<bool>
    {
        /// <inheritdoc/>
        public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            static bool ParseString(ReadOnlySpan<byte> valueSpan)
            {
                var textValue = valueSpan.TranscodeText();
                if (textValue.TryParseAsBoolean(out var result))
                {
                    return result;
                }

                throw new JsonException(String.Format(Strings.JsonConverter_BooleanEnabled_ConversionFailure, textValue));
            }

            var result = reader.TokenType switch
            {
                JsonTokenType.String => ParseString(reader.ValueSpan),
                JsonTokenType.False => false,
                JsonTokenType.True => true,
                _ => JsonSerializer.Deserialize<bool>(ref reader, options)
            };

            return result;
        }

        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
