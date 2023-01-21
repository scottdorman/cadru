//------------------------------------------------------------------------------
// <copyright file="UnixDateTimeConverter.cs"
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
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

using Cadru.Extensions;
using Cadru.Json.Resources;

namespace Cadru.Json.Serialization.Converters
{
    /// <inheritdoc/>
    public class UnixDateTimeConverter : JsonConverter<DateTime>
    {
        static readonly DateTime s_epoch = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <inheritdoc/>
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var formatted = reader.GetString();
            return !Int64.TryParse(formatted, NumberStyles.Integer, CultureInfo.InvariantCulture, out var unixTime)
                ? throw new JsonException(Strings.JsonConverter_UnixDateTimeConverter_ParseError)
                : s_epoch.AddMilliseconds(unixTime);
        }

        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            var unixTime = (long)(value.ToUniversalTime() - s_epoch).TotalSeconds;
            if (unixTime < 0)
            {
                throw new JsonException(Strings.JsonConverter_UnixDateTimeConverter_ConversionFailure);
            }

            writer.WriteStringValue(unixTime.ToString());
        }
    }
}
