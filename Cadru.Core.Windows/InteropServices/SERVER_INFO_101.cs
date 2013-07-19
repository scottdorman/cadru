//------------------------------------------------------------------------------
// <copyright file="SERVER_INFO_101.cs" 
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

namespace Cadru.InteropServices
{
    using System;
    using System.Runtime.InteropServices;
    using Cadru.Networking;

    /// <summary>
    /// The SERVER_INFO_101 structure contains information about the
    /// specified server, including name, platform, type of server, 
    /// and associated software.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct SERVER_INFO_101
    {
        /// <summary>
        /// Specifies the information level to use for platform-specific information.
        /// </summary>
        [MarshalAs(UnmanagedType.U4)]
        public UInt32 sv101_platform_id;

        /// <summary>
        /// Pointer to a Unicode string specifying the name of a server.
        /// </summary>
        [MarshalAs(UnmanagedType.LPWStr)]
        public String sv101_name;

        /// <summary>
        /// Specifies, in the least significant 4 bits of the byte, the major 
        /// release version number of the operating system. The most significant 4 
        /// bits of the byte specifies the server type. The mask MAJOR_VERSION_MASK should
        /// be used to ensure correct results. 
        /// </summary>
        [MarshalAs(UnmanagedType.U4)]
        public UInt32 sv101_version_major;

        /// <summary>
        /// Specifies the minor release version number of the operating system.
        /// </summary>
        [MarshalAs(UnmanagedType.U4)]
        public UInt32 sv101_version_minor;

        /// <summary>
        /// Specifies the type of software the computer is running.
        /// </summary>
        [MarshalAs(UnmanagedType.U4)]
        public UInt32 sv101_type;

        /// <summary>
        /// Pointer to a Unicode string specifying a comment describing the server. 
        /// The comment can be null.
        /// </summary>
        [MarshalAs(UnmanagedType.LPWStr)]
        public String sv101_comment;
    }
}
