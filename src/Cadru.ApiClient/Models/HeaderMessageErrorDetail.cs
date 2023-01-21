//------------------------------------------------------------------------------
// <copyright file="ErrorDetail.cs"
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

using System.Diagnostics;
using System.Text.Json.Serialization;

using Cadru.ApiClient.Serialization;

namespace Cadru.ApiClient.Models
{
    /// <summary>
    /// Represents a display-friendly error message
    /// </summary>
    [JsonConverter(typeof(HeaderMessageErrorDetailConverter))]
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public sealed class HeaderMessageErrorDetail : IErrorDetail
    {
        internal HeaderMessageErrorDetail()
        {
        }

        internal HeaderMessageErrorDetail(string? header, string? message) : this()
        {
            this.Header = header;
            this.Message = message;
        }

        /// <summary>
        /// The error message header.
        /// </summary>
        [JsonPropertyName(JsonSerializationPropertyNames.MessageHeader)]
        [JsonInclude]
        public string? Header { get; internal set; }

        /// <summary>
        /// The error message content.
        /// </summary>
        [JsonPropertyName(JsonSerializationPropertyNames.Message)]
        [JsonInclude]
        public string? Message { get; internal set; }

        private string GetDebuggerDisplay()
        {
            return $"{JsonSerializationPropertyNames.MessageHeader}: {this.Header}; {JsonSerializationPropertyNames.Message}: {this.Message}";
        }
    }
}
