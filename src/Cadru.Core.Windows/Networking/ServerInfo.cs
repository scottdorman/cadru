//------------------------------------------------------------------------------
// <copyright file="ServerInfo.cs"
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

namespace Cadru.Networking
{
    using System;
    using System.Globalization;
    using System.Runtime.InteropServices;
    using Cadru.InteropServices;

    /// <summary>
    /// The Server structure contains information about the specified server,
    /// including name, platform, type of server, and associated software.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public partial struct ServerInfo : IEquatable<ServerInfo>
    {
        #region events

        #endregion

        #region fields
        private PlatformId platformId;
        private string name;
        private int majorVersion;
        private int minorVersion;
        private ServerTypes serverType;
        private string comment;
        #endregion

        #region constructors

        #region Server(SERVER_INFO_101 info)
        internal ServerInfo(SERVER_INFO_101 info)
        {
            this.platformId = (PlatformId)info.sv101_platform_id;
            this.name = info.sv101_name;
            this.majorVersion = (int)info.sv101_version_major;
            this.minorVersion = (int)info.sv101_version_minor;
            this.serverType = (ServerTypes)info.sv101_type;
            this.comment = info.sv101_comment;
        }
        #endregion

        #endregion

        #region properties

        /// <summary>
        /// Gets the information level used for platform-specific information.
        /// </summary>
        /// <value>One of the <see cref="PlatformId"/> values.</value>
        public PlatformId PlatformId
        {
            get
            {
                return this.platformId;
            }
        }

        /// <summary>
        /// Gets the name of the computer.
        /// </summary>
        /// <value>A <see cref="String"/> that represents the name of the
        /// computer.</value>
        public string Name
        {
            get
            {
                return this.name;
            }
        }

        /// <summary>
        /// Gets the Server comment.
        /// </summary>
        /// <value>A <see cref="String"/> that represents the comment
        /// associated with the server or an empty string if there is no
        /// comment.</value>
        public string Comment
        {
            get
            {
                return this.comment;
            }
        }

        /// <summary>
        /// Gets the operating system major version number.
        /// </summary>
        /// <value>An <see cref="Int32"></see> value representing the major
        /// version number of the operating system.</value>
        public int MajorVersion
        {
            get
            {
                return this.majorVersion;
            }
        }

        /// <summary>
        /// Gets the operating system minor version number.
        /// </summary>
        /// <value>An <see cref="Int32"></see> value representing the minor
        /// version number of the operating system.</value>
        public int MinorVersion
        {
            get
            {
                return this.minorVersion;
            }
        }

        /// <summary>
        /// Gets the operating system version number.
        /// </summary>
        /// <value>A <see cref="Version"/> representing the operating system
        /// version.</value>
        public Version Version
        {
            get
            {
                return new Version(this.majorVersion, this.minorVersion);
            }
        }

        /// <summary>
        /// Gets the type of software the computer is running.
        /// </summary>
        /// <value>A <see cref="ServerTypes"/> value that represents the
        /// operating system running on the computer.</value>
        public ServerTypes ServerType
        {
            get
            {
                return this.serverType;
            }
        }

        #endregion

        #region operators

        #region op_Equality
        /// <summary>
        /// Determines whether two specified instances of <see cref="ServerInfo"/> are equal.
        /// </summary>
        /// <param name="left">An <see cref="ServerInfo"/>.</param>
        /// <param name="right">An <see cref="ServerInfo"/>.</param>
        /// <returns><see langword="true"/> if s1 and s2 represent the same server; otherwise <see langword="false"/>.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1625:ElementDocumentationMustNotBeCopiedAndPasted", Justification = "Reviewed.")]
        public static bool operator ==(ServerInfo left, ServerInfo right)
        {
            return left.Equals(right);
        }
        #endregion

        #region op_Inequality
        /// <summary>
        /// Determines whether two specified instances of <see cref="ServerInfo"/> are not equal.
        /// </summary>
        /// <param name="left">An <see cref="ServerInfo"/>.</param>
        /// <param name="right">An <see cref="ServerInfo"/>.</param>
        /// <returns><see langword="true"/> if s1 and s2 do note represent the same server;
        /// otherwise <see langword="false"/>.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1625:ElementDocumentationMustNotBeCopiedAndPasted", Justification = "Reviewed.")]
        public static bool operator !=(ServerInfo left, ServerInfo right)
        {
            return !left.Equals(right);
        }
        #endregion

        #endregion

        #region methods

        #region Equals(Server left, Server right)
        /// <summary>
        /// Returns a value indicating whether two instances of <see cref="ServerInfo"/> are equal.
        /// </summary>
        /// <param name="left">The first <see cref="ServerInfo"/>. </param>
        /// <param name="right">The second <see cref="ServerInfo"/>.</param>
        /// <returns><see langword="true"/> if the two <see cref="ServerInfo"/> values are equal; otherwise, <see langword="false"/>. </returns>
        public static bool Equals(ServerInfo left, ServerInfo right)
        {
            return left == right;
        }
        #endregion

        #region Equals

        #region Equals(Object obj)
        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified object.
        /// </summary>
        /// <param name="obj">An object to compare to this instance.</param>
        /// <returns><see langword="true"/> if value is an instance of <see cref="ServerInfo"/>
        /// equals the value of this instance; otherwise, <see langword="false"/>. </returns>
        public override bool Equals(Object obj)
        {
            ServerInfo s;

            // Check that o is a Server first
            if (obj == null || !(obj is ServerInfo))
            {
                return false;
            }
            else
            {
                s = (ServerInfo)obj;
            }

            // Now compare each of the elements
            if (s.platformId != this.platformId)
            {
                return false;
            }

            if (s.name != this.name)
            {
                return false;
            }

            if (s.serverType != this.serverType)
            {
                return false;
            }

            if (s.majorVersion != this.majorVersion)
            {
                return false;
            }

            if (s.minorVersion != this.minorVersion)
            {
                return false;
            }

            if (s.comment != this.comment)
            {
                return false;
            }

            return true;
        }
        #endregion

        #region Equals(Server other)
        /// <summary>
        /// Returns a value indicating whether this instance is equal to the specified <see cref="ServerInfo"/> instance.
        /// </summary>
        /// <param name="other">An <see cref="ServerInfo"/> instance to compare to this instance.</param>
        /// <returns><see langword="true"/> if the other parameter equals the value of this instance; otherwise, <see langword="false"/>. </returns>
        /// <remarks>This method implements the <see cref="System.IEquatable{T}"/> interface and performs slightly
        /// better than the <see cref="ServerInfo.Equals(Object)"/> method because it does not have to convert
        /// the other parameter to an object.</remarks>
        public bool Equals(ServerInfo other)
        {
            // Now compare each of the elements
            if (other.platformId != this.platformId)
            {
                return false;
            }

            if (other.name != this.name)
            {
                return false;
            }

            if (other.serverType != this.serverType)
            {
                return false;
            }

            if (other.majorVersion != this.majorVersion)
            {
                return false;
            }

            if (other.minorVersion != this.minorVersion)
            {
                return false;
            }

            if (other.comment != this.comment)
            {
                return false;
            }

            return true;
        }
        #endregion

        #endregion

        #region GetHashCode
        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            return this.platformId.GetHashCode() ^
                    this.serverType.GetHashCode() ^
                    this.name.GetHashCode() ^
                    this.majorVersion.GetHashCode() ^
                    this.minorVersion.GetHashCode() ^
                    this.comment.GetHashCode();
        }
        #endregion

        #region ToString

        #region ToString()
        /// <summary>
        /// Converts the value of this instance to its equivalent string representation.
        /// </summary>
        /// <returns>The string representation of this instance.</returns>
        public override String ToString()
        {
            return this.ToString(CultureInfo.CurrentCulture);
        }
        #endregion

        #region ToString(IFormatProvider provider)
        /// <summary>
        /// Converts the value of the current Server object to its equivalent string representation using the specified culture-specific format information.
        /// </summary>
        /// <param name="provider">An object that supplies culture-specific formatting information. </param>
        /// <returns>The string representation of this instance as specified by <paramref name="provider"/>.</returns>
        public String ToString(IFormatProvider provider)
        {
            return String.Format(provider, "{0}, {1}", this.name, this.serverType.ToString());
        }
        #endregion

        #endregion

        #endregion
    }
}
