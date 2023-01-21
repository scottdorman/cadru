//------------------------------------------------------------------------------
// <copyright file="EnumerableExtensions.cs"
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
using System.Linq;
using System.Text.Json;

namespace Cadru.Json
{
    public static class EnumerableExtensions
    {
        public static JsonElement? Get(this JsonElement element, string name) =>
        element.ValueKind != JsonValueKind.Null && element.ValueKind != JsonValueKind.Undefined && element.TryGetProperty(name, out var value)
            ? value : null;

        public static JsonElement? Get(this JsonElement element, int index)
        {
            if (element.ValueKind == JsonValueKind.Null || element.ValueKind == JsonValueKind.Undefined)
            {
                return null;
            }

            var value = element.EnumerateArray().ElementAtOrDefault(index);
            return value.ValueKind != JsonValueKind.Undefined ? value : null;
        }

        public static string? SelectToken(this JsonElement jsonElement) => jsonElement.ValueKind != JsonValueKind.Null &&
                                                                                   jsonElement.ValueKind != JsonValueKind.Undefined
            ? jsonElement.ToString()
            : default;

        public static JsonElement? SelectToken(this JsonElement jsonElement, string path)
        {
            if (jsonElement.ValueKind is JsonValueKind.Null or JsonValueKind.Undefined)
            {
                return null;
            }

            var segments = path.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var segment in segments)
            {
                if (Int32.TryParse(segment, out var index) && jsonElement.ValueKind == JsonValueKind.Array)
                {
                    jsonElement = jsonElement.EnumerateArray().ElementAtOrDefault(index);
                    if (jsonElement.ValueKind is JsonValueKind.Null or JsonValueKind.Undefined)
                    {
                        return null;
                    }

                    continue;
                }

                jsonElement = jsonElement.TryGetProperty(segment, out var value) ? value : default;

                if (jsonElement.ValueKind is JsonValueKind.Null or JsonValueKind.Undefined)
                {
                    return null;
                }
            }

            return jsonElement;
        }
    }
}