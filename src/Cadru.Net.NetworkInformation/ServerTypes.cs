//------------------------------------------------------------------------------
// <copyright file="ServerTypes.cs"
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

namespace Cadru.Net.NetworkInformation
{
#if !(WP80 || WPA81)

    using System;

    /// <summary>
    /// Specifies the type of software the computer is running.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1028:EnumStorageShouldBeInt32", Justification = "The values in this enum are actually uint (UInt32) values. In order to be CLS compliant, the datatype must be Int64 (long).")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2217:DoNotMarkEnumsWithFlags", Justification = "The values in this enum map to values defined by the Windows API, which treats these values as flags.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1008:EnumsShouldHaveZeroValue", Justification = "The values in this enum map to values defined by the Windows API, which provides a cosntant 'Unknown' that maps to zero.")]
    [Flags]
    public enum ServerTypes : long
    {
        /// <summary>
        /// Unknown server type.
        /// </summary>
        Unknown = 0x00000000,

        /// <summary>
        /// All workstations.
        /// </summary>
        Workstation = 0x00000001,

        /// <summary>
        /// All computers that have the server service running.
        /// </summary>
        Server = 0x00000002,

        /// <summary>
        /// Any server running Microsoft SQL Server.
        /// </summary>
        SqlServer = 0x00000004,

        /// <summary>
        /// Primary domain controller.
        /// </summary>
        DomainController = 0x00000008,

        /// <summary>
        /// Backup domain controller.
        /// </summary>
        BackupDomainController = 0x00000010,

        /// <summary>
        /// Server running the Timesource service.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
        TimeSource = 0x00000020,

        /// <summary>
        /// Apple File Protocol servers.
        /// </summary>
        AppleFileProtocol = 0x00000040,

        /// <summary>
        /// Novell servers.
        /// </summary>
        Novell = 0x00000080,

        /// <summary>
        /// LAN Manager 2.x domain member.
        /// </summary>
        DomainMember = 0x00000100,

        /// <summary>
        /// Server sharing print queue.
        /// </summary>
        PrintQueue = 0x00000200,

        /// <summary>
        /// Server running dial-in service.
        /// </summary>
        DialIn = 0x00000400,

        /// <summary>
        /// Xenix server.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Xenix", Justification = "Reviewed.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
        Xenix = 0x00000800,

        /// <summary>
        /// Unix server.
        /// </summary>
        Unix = Xenix,

        /// <summary>
        /// Windows NT workstation or server.
        /// </summary>
        WindowsNT = 0x00001000,

        /// <summary>
        /// Server running Windows for Workgroups.
        /// </summary>
        WindowsForWorkgroups = 0x00002000,

        /// <summary>
        /// Microsoft File and Print for NetWare.
        /// </summary>
        MicrosoftFilePrintForNetware = 0x00004000,

        /// <summary>
        /// Server that is not a domain controller.
        /// </summary>
        WindowsNTServer = 0x00008000,

        /// <summary>
        /// Server that can run the browser service.
        /// </summary>
        PotentialBrowser = 0x00010000,

        /// <summary>
        /// Server running a browser service as backup.
        /// </summary>
        BackupBrowser = 0x00020000,

        /// <summary>
        /// Server running the master browser service.
        /// </summary>
        MasterBrowser = 0x00040000,

        /// <summary>
        /// Server running the domain master browser.
        /// </summary>
        DomainMaster = 0x00080000,

        /// <summary>
        /// OSF server.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "OSF", Justification = "Reviewed.")]
        OSF = 0x00100000,

        /// <summary>
        /// VAX VMS server.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "VMS", Justification = "Reviewed.")]
        VMS = 0x00200000,

        /// <summary>
        /// Windows 95 or later.
        /// </summary>
        Windows = 0x00400000,

        /// <summary>
        /// Root of a DFS tree.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "DFS", Justification = "Reviewed.")]
        DFS = 0x00800000,

        /// <summary>
        /// Server clusters available in the domain.
        /// </summary>
        Cluster = 0x01000000,

        /// <summary>
        /// Terminal Server.
        /// </summary>
        TerminalServer = 0x02000000,

        /// <summary>
        /// Cluster virtual servers available in the domain (Not supported for
        /// Windows 2000/NT).
        /// </summary>
        VirtualCluster = 0x04000000,

        /// <summary>
        /// IBM DSS (Directory and Security Services) or equivalent.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "DSS", Justification = "Reviewed.")]
        DSS = 0x10000000,

        /// <summary>
        /// Return list for alternate transport.
        /// </summary>
        AlternateTransport = 0x20000000,

        /// <summary>
        /// Return local list only.
        /// </summary>
        LocalListOnly = 0x40000000,

        /// <summary>
        /// Lists available domains.
        /// </summary>
        AllDomains = 0x80000000,

        /// <summary>
        /// All server types.
        /// </summary>
        All = 0xFFFFFFFF
    }

#endif
}