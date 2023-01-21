//------------------------------------------------------------------------------
// <copyright file="JsonExtensions.cs"
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
using System.Buffers;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace Cadru.Json.Serialization
{
    public static class JsonExtensions
    {
        private static readonly UTF8Encoding s_utf8Encoding = new(encoderShouldEmitUTF8Identifier: false, throwOnInvalidBytes: true);

        public static ReadOnlySpan<byte> GetValueTextSpan(this ref Utf8JsonReader reader)
        {
            return reader.HasValueSequence ? reader.ValueSequence.ToArray() : reader.ValueSpan;
        }

        public static string TranscodeText(this ReadOnlySpan<byte> span)
        {
            return s_utf8Encoding.GetString(span)?.ToLower()?.Trim() ?? String.Empty;
        }

        public static string GetValueText(this ref Utf8JsonReader reader)
        {
            return reader.GetValueTextSpan().TranscodeText();
        }
    }
}