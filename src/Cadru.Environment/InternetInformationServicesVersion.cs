//------------------------------------------------------------------------------
// <copyright file="InternetInformationServicesVersion.cs"
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

namespace Cadru.Environment
{
    /// <summary>
    /// Specifies the Internet Information Services (IIS) versions.
    /// </summary>
    /// <remarks>
    /// <list type="table">
    /// <listheader>
    /// <term>Version</term>
    /// <term>Obtained from</term>
    /// <term>Operating system</term>
    /// </listheader>
    /// <item>
    /// <description>1.0</description>
    /// <description>
    /// Included with Windows NT 3.51 SP 3 (or as a self-contained download).
    /// </description>
    /// <description>Windows NT Server 3.51</description>
    /// </item>
    /// <item>
    /// <description>2.0</description>
    /// <description>Included with Windows NT Server 4.0.</description>
    /// <description>Windows NT Server 4.0</description>
    /// </item>
    /// <item>
    /// <description>3.0</description>
    /// <description>
    /// Included with Windows NT Server 4.0 Service Pack 3 (Internet Information
    /// Server 2.0 is automatically upgraded to Internet Information Server 3.0
    /// during the install of SP3).
    /// </description>
    /// <description>Windows NT Server 4.0</description>
    /// </item>
    /// <item>
    /// <description>4.0</description>
    /// <description>
    /// Self-contained download from www.microsoft.com or the Windows NT Option
    /// Pack compact disc.
    /// </description>
    /// <description>
    /// Windows NT Server 4.0 SP3 and Microsoft Internet Explorer 4.01
    /// </description>
    /// </item>
    /// <item>
    /// <description></description>
    /// <description>Built-in component of Windows 2000.</description>
    /// <description>Windows 2000</description>
    /// </item>
    /// <item>
    /// <description>5.1</description>
    /// <description>Built-in component of Windows XP Professional.</description>
    /// <description>Windows XP Professional</description>
    /// </item>
    /// <item>
    /// <description>6.0</description>
    /// <description>Built-in component of Windows Server 2003.</description>
    /// <description>Windows Server 2003</description>
    /// </item>
    /// <item>
    /// <description>7.0</description>
    /// <description>
    /// Built-in component of Windows Vista and Windows Server 2008.
    /// </description>
    /// <description>Windows Vista and Windows Server 2008</description>
    /// </item>
    /// <item>
    /// <description>7.5</description>
    /// <description>
    /// Built-in component of Windows 7 and Windows Server 2008 R2.
    /// </description>
    /// <description>Windows 7 and Windows Server 2008 R2</description>
    /// </item>
    /// <item>
    /// <description>8.0</description>
    /// <description>
    /// Built-in component of Windows 8 and Windows Server 2012.
    /// </description>
    /// <description>Windows 8 and Windows Server 2012</description>
    /// </item>
    /// </list>
    /// </remarks>
    public enum InternetInformationServicesVersion
    {
        /// <summary>
        /// Internet Information Services 1.0.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "IIS", Justification = "Reviewed.")]
        IIS1,

        /// <summary>
        /// Internet Information Services 2.0.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "IIS", Justification = "Reviewed.")]
        IIS2,

        /// <summary>
        /// Internet Information Services 3.0.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "IIS", Justification = "Reviewed.")]
        IIS3,

        /// <summary>
        /// Internet Information Services 4.0.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "IIS", Justification = "Reviewed.")]
        IIS4,

        /// <summary>
        /// Internet Information Services 5.0.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "IIS", Justification = "Reviewed.")]
        IIS5,

        /// <summary>
        /// Internet Information Services 5.1.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "IIS", Justification = "Reviewed.")]
        IIS51,

        /// <summary>
        /// Internet Information Services 6.0.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "IIS", Justification = "Reviewed.")]
        IIS6,

        /// <summary>
        /// Internet Information Services 7.0.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "IIS", Justification = "Reviewed.")]
        IIS7,

        /// <summary>
        /// Internet Information Services 7.5.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "IIS", Justification = "Reviewed.")]
        IIS75,

        /// <summary>
        /// Internet Information Services 8.0.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "IIS", Justification = "Reviewed.")]
        IIS8,
    }
}