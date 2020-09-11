//------------------------------------------------------------------------------
// <copyright file="ExecutableType.cs"
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

using System.Diagnostics.CodeAnalysis;

namespace Cadru.IO
{
    /// <summary>
    /// Specifies the executable file type.
    /// </summary>
    public enum ExecutableType
    {
        /// <summary>
        /// The file executable type is not able to be determined.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// The file is an MS-DOS .exe, .com, or .bat file.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "DOS", Justification = "Reviewed.")]
        DOS,

        /// <summary>
        /// The file is a Microsoft Win32®-based console application.
        /// </summary>
        Win32Console,

        /// <summary>
        /// The file is a Windows application.
        /// </summary>
        Windows,
    }
}