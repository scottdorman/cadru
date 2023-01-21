//------------------------------------------------------------------------------
// <copyright file="StringEnumConverter.cs"
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
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Linq;
using Cadru.Extensions;
using Cadru.Json.DataAnnotations;

namespace Cadru.Json.Serialization.Converters
{
#if NETSTANDARD2_0
    public class StringEnumConverter<TEnum> : JsonConverter<TEnum> where TEnum : struct
#else
    public class StringEnumConverter<TEnum> : JsonConverter<TEnum> where TEnum : struct, System.Enum
#endif
    {

        private readonly Dictionary<TEnum, string> enumToString = new();
        private readonly Dictionary<string, TEnum> stringToEnum = new();

        public StringEnumConverter()
        {
            var type = typeof(TEnum);
#if NET5_0_OR_GREATER
            var values = System.Enum.GetValues<TEnum>();
#else
            var values = Enum.GetValues(typeof(TEnum)).Cast<TEnum>();
#endif

            foreach (var value in values)
            {
                var enumMember = type.GetMember(value.ToString())[0];
                var attr = enumMember.GetCustomAttributes(typeof(JsonStringEnumValueAttribute), false)
                  .Cast<JsonStringEnumValueAttribute>()
                  .FirstOrDefault();

                this.stringToEnum.Add(value.ToString(), value);

                if (attr?.Value != null)
                {
                    this.enumToString.Add(value, attr.Value);
                    this.stringToEnum.Add(attr.Value, value);
                }
                else
                {
                    this.enumToString.Add(value, value.ToString());
                }
            }
        }

        public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var stringValue = reader.GetString();

            if (!String.IsNullOrWhiteSpace(stringValue))
            {
                if (this.stringToEnum.TryGetValue(stringValue!, out var enumValue))
                {
                    return enumValue;
                }
            }

            return default;
        }

        public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(this.enumToString[value]);
        }
    }
}
