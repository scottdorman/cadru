//------------------------------------------------------------------------------
// <copyright file="InternetInformationServicesSubcomponent.cs" 
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

namespace Cadru
{
    /// <summary>
    /// Specifies the Internet Information Services (IIS) subcomponents.
    /// </summary>
    /// <remarks>Subcomponents only apply to IIS versions 6 and earlier.</remarks>
    public enum InternetInformationServicesSubcomponent
    {
        /// <summary>
        /// Internet Information Services Common Files.
        /// </summary>
        Common,

        /// <summary>
        /// Active Server Pages (ASP) for Internet Information Services.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ASP", Justification = "Reviewed.")]
        ASP,

        /// <summary>
        /// File Transfer Protocol (FTP) service.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "FTP", Justification = "Reviewed.")]
        FTP,

        /// <summary>
        /// Internet Information Services Manager (Microsoft Management Console [MMC] snap-in).
        /// </summary>
        ManagementConsole,

        /// <summary>
        /// Internet Data Connector.
        /// </summary>
        InternetDataConnector,

        /// <summary>
        /// Network News Transfer Protocol (NNTP) service.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "NNTP", Justification = "Reviewed.")]
        NNTP,

        /// <summary>
        /// Server-Side Includes.
        /// </summary>
        ServerSideIncludes,

        /// <summary>
        /// Simple Mail Transfer Protocol (SMTP) service.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SMTP", Justification = "Reviewed.")]
        SMTP,

        /// <summary>
        /// Web Distributed Authoring and Versioning (WebDAV) publishing.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "DAV", Justification = "Reviewed.")]
        WebDAV,

        /// <summary>
        /// World Wide Web (WWW) service.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "WWW", Justification = "Reviewed.")]
        WWW,

        /// <summary>
        /// Remote administration (HTML).
        /// </summary>
        RemoteAdmin,

        /// <summary>
        /// Internet Server Application Programming Interface (ISAPI) for
        /// Background Intelligent Transfer Service (BITS) server extensions.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ISAPI", Justification = "Reviewed.")]
        BitsISAPI,

        /// <summary>
        /// Background Intelligent Transfer Service (BITS) server extensions snap-in.
        /// </summary>
        Bits,

        /// <summary>
        /// FrontPage server extensions.
        /// </summary>
        FrontPageExtensions,

        /// <summary>
        /// Internet printing.
        /// </summary>
        InternetPrinting,

        /// <summary>
        /// ActiveX control and sample pages for hosting Terminal Services
        /// client connections over the web.
        /// </summary>
        TSWebClient,
    }
}
