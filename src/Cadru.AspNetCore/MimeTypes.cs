//------------------------------------------------------------------------------
// <copyright file="MimeTypes.cs"
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

namespace Cadru.AspNetCore
{
    /// <summary>
    /// Common MIME type constants
    /// </summary>
    public static class MimeTypes
    {
        /// <summary>
        /// JavaScript Object Notation (JSON)
        /// </summary>
        public const string Json = "application/json";

        /// <summary>
        /// This is the default for binary files. As it means unknown binary
        /// file, browsers usually don't execute it, or even ask if it should be
        /// executed.
        /// </summary>
        public const string OctetStream = "application/octet-stream";

        /// <summary>
        /// Microsoft Excel (OpenXML) Spreadsheet
        /// </summary>
        public const string OpenXML_Excel = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; //"application/vnd.ms-excel"

        /// <summary>
        /// This is the default for textual files. Even if it really means
        /// "unknown textual file," browsers assume they can display it.
        /// </summary>
        public const string PlainText = "text/plain";

        /// <summary>
        /// Microsoft Remote Desktop Protocol (RDP)
        /// </summary>
        public const string RemoteDesktopProtocol = "application/x-rdp";
    }
}