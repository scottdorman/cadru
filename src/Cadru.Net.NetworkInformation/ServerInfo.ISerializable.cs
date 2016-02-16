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

namespace Cadru.Net.NetworkInformation
{
#if NET40
    using System;
    using System.Globalization;
    using System.Runtime.InteropServices;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using Cadru.Net.NetworkInformation.Interop;

    /// <summary>
    /// The Server structure contains information about the specified server,
    /// including name, platform, type of server, and associated software.
    /// </summary>
    [Serializable]
    public partial struct ServerInfo : ISerializable
    {
    #region events
    #endregion

    #region fields
    #endregion

    #region constructors

    #region serialization constructor
        private ServerInfo(SerializationInfo info, StreamingContext context)
        {
            Contracts.Requires.NotNull(info, "info");

            this.platformId = (PlatformId)info.GetValue("platformId", typeof(PlatformId));
            this.name = info.GetString("name");
            this.serverType = (ServerTypes)info.GetValue("serverType", typeof(ServerTypes));
            this.majorVersion = info.GetInt32("majorVersion");
            this.minorVersion = info.GetInt32("minorVersion");
            this.comment = info.GetString("comment");
        }
    #endregion

    #endregion

    #region properties
    #endregion

    #region operators
    #endregion

    #region methods

    #region GetObjectData
        /// <summary>
        /// Populates a <see cref="SerializationInfo"/> object with the data needed
        /// to serialize the current <see cref="ServerInfo"/> object.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> object to populate with data.</param>
        /// <param name="context">The destination for this serialization.(This parameter is not used;
        /// specify a <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="info"/> is a <see langword="null"/>.</exception>
        //[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            Contracts.Requires.NotNull(info, "info");

            info.AddValue("platformId", this.platformId, typeof(PlatformId));
            info.AddValue("name", this.name);
            info.AddValue("serverType", this.serverType, typeof(ServerTypes));
            info.AddValue("majorVersion", this.majorVersion);
            info.AddValue("minorVersion", this.minorVersion);
            info.AddValue("comment", this.comment);
        }
    #endregion

    #endregion
    }
#endif
}
