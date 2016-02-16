//------------------------------------------------------------------------------
// <copyright file="PlatformId.cs"
//  company="Scott Dorman"
//  library="Cadru">
//    Copyright (C) 2001-2013 Scott Dorman.
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

namespace Cadru.Net.NetworkInformation
{
#if !(WP80 || WPA81)
    /// <summary>
    /// Specifies the information level to use for platform-specific information.
    /// </summary>
    public enum PlatformId
    {
        /// <summary>
        /// Retrieve platform-specific information for an unknown system.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Retrieve platform-specific information for a DOS system.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "DOS", Justification = "Reviewed.")]
        DOS = 300,

        /// <summary>
        /// Retrieve platform-specific information for an OS2 system.
        /// </summary>
        OS2 = 400,

        /// <summary>
        /// Retrieve platform-specific information for a Windows system.
        /// </summary>
        WindowsNT = 500,

        /// <summary>
        /// Retrieve platform-specific information for an OSF system.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "OSF", Justification = "Reviewed.")]
        OSF = 600,

        /// <summary>
        /// Retrieve platform-specific information for a VMS system.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "VMS", Justification = "Reviewed.")]
        VMS = 700
    }
#endif
}
