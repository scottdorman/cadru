//------------------------------------------------------------------------------
// <copyright file="ServerInfo.cs"
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
using System.Globalization;
using System.Runtime.InteropServices;

using Cadru.Net.NetworkInformation.Interop;

namespace Cadru.Net.NetworkInformation
{
    /// <summary>
    /// The Server structure contains information about the specified server,
    /// including name, platform, type of server, and associated software.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public partial struct ServerInfo : IEquatable<ServerInfo>
    {
        internal ServerInfo(SERVER_INFO_101 info)
        {
            this.PlatformId = (PlatformId)info.sv101_platform_id;
            this.Name = info.sv101_name;
            this.MajorVersion = (int)info.sv101_version_major;
            this.MinorVersion = (int)info.sv101_version_minor;
            this.ServerType = (ServerTypes)info.sv101_type;
            this.Comment = info.sv101_comment;
        }

        /// <summary>
        /// Gets the information level used for platform-specific information.
        /// </summary>
        /// <value>One of the <see cref="PlatformId"/> values.</value>
        public PlatformId PlatformId { get; }

        /// <summary>
        /// Gets the name of the computer.
        /// </summary>
        /// <value>A <see cref="String"/> that represents the name of the computer.</value>
        public string Name { get; }

        /// <summary>
        /// Gets the Server comment.
        /// </summary>
        /// <value>
        /// A <see cref="String"/> that represents the comment associated with
        /// the server or an empty string if there is no comment.
        /// </value>
        public string Comment { get; }

        /// <summary>
        /// Gets the operating system major version number.
        /// </summary>
        /// <value>
        /// An <see cref="Int32"></see> value representing the major version
        /// number of the operating system.
        /// </value>
        public int MajorVersion { get; }

        /// <summary>
        /// Gets the operating system minor version number.
        /// </summary>
        /// <value>
        /// An <see cref="Int32"></see> value representing the minor version
        /// number of the operating system.
        /// </value>
        public int MinorVersion { get; }

        /// <summary>
        /// Gets the operating system version number.
        /// </summary>
        /// <value>
        /// A <see cref="Version"/> representing the operating system version.
        /// </value>
        public Version Version => new Version(this.MajorVersion, this.MinorVersion);

        /// <summary>
        /// Gets the type of software the computer is running.
        /// </summary>
        /// <value>
        /// A <see cref="ServerTypes"/> value that represents the operating
        /// system running on the computer.
        /// </value>
        public ServerTypes ServerType { get; }

        /// <inheritdoc/>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1625:ElementDocumentationMustNotBeCopiedAndPasted", Justification = "Reviewed.")]
        public static bool operator ==(ServerInfo left, ServerInfo right)
        {
            return left.Equals(right);
        }

        /// <inheritdoc/>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1625:ElementDocumentationMustNotBeCopiedAndPasted", Justification = "Reviewed.")]
        public static bool operator !=(ServerInfo left, ServerInfo right)
        {
            return !left.Equals(right);
        }

        /// <inheritdoc/>
        public static bool Equals(ServerInfo left, ServerInfo right)
        {
            return left == right;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is ServerInfo s)
            {
                if (s.PlatformId != this.PlatformId)
                {
                    return false;
                }

                if (s.Name != this.Name)
                {
                    return false;
                }

                if (s.ServerType != this.ServerType)
                {
                    return false;
                }

                if (s.MajorVersion != this.MajorVersion)
                {
                    return false;
                }

                if (s.MinorVersion != this.MinorVersion)
                {
                    return false;
                }

                if (s.Comment != this.Comment)
                {
                    return false;
                }

                return true;
            }

            return false;
        }

        /// <inheritdoc/>
        public bool Equals(ServerInfo other)
        {
            // Now compare each of the elements
            if (other.PlatformId != this.PlatformId)
            {
                return false;
            }

            if (other.Name != this.Name)
            {
                return false;
            }

            if (other.ServerType != this.ServerType)
            {
                return false;
            }

            if (other.MajorVersion != this.MajorVersion)
            {
                return false;
            }

            if (other.MinorVersion != this.MinorVersion)
            {
                return false;
            }

            if (other.Comment != this.Comment)
            {
                return false;
            }

            return true;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return this.PlatformId.GetHashCode() ^
                    this.ServerType.GetHashCode() ^
                    this.Name.GetHashCode() ^
                    this.MajorVersion.GetHashCode() ^
                    this.MinorVersion.GetHashCode() ^
                    this.Comment.GetHashCode();
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return this.ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Converts the value of the current Server object to its equivalent
        /// string representation using the specified culture-specific format information.
        /// </summary>
        /// <param name="provider">
        /// An object that supplies culture-specific formatting information.
        /// </param>
        /// <returns>
        /// The string representation of this instance as specified by <paramref name="provider"/>.
        /// </returns>
        public string ToString(IFormatProvider provider)
        {
            return String.Format(provider, "{0}, {1}", this.Name, this.ServerType.ToString());
        }
    }
}