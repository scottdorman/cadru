//------------------------------------------------------------------------------
// <copyright file="ExtendedDirectoryInfo.cs"
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

namespace Cadru.IO
{
#if !DNXCORE50
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.InteropServices;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    /// <summary>
    /// Provides an encapsulated implementation of the standard .NET
    /// <see cref="DirectoryInfo"/> and the
    /// <see href="http://msdn.microsoft.com/en-us/library/windows/desktop/bb762179(v=vs.85).aspx">SHGetFileInfo</see>
    /// API method.
    /// </summary>
    [Serializable]
    public sealed partial class ExtendedDirectoryInfo : ISerializable
    {
        #region fields
        #endregion

        #region constructors
        private ExtendedDirectoryInfo(SerializationInfo info, StreamingContext context)
        {
            Contracts.Requires.NotNull(info, "info");

            this.Initialize(info.GetString("originalPath"));
        }

        #endregion

        #region events
        #endregion

        #region properties
        #endregion

        #region methods

        #region GetObjectData
        /// <summary>
        /// Sets the SerializationInfo object with the file name and additional exception information.
        /// </summary>
        /// <param name="info">The SerializationInfo that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The StreamingContext that contains contextual information about the source or destination.</param>
        [ComVisible(false)]
        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        [SuppressMessage("Microsoft.Security", "CA2103:ReviewImperativeSecurity", Justification = "This security demand cannot be declaritve as the path is not known until runtime. The value of originalFileName cannot change once the class is instantiated, so there is no risk that the value will change while the demand is in effect.")]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            Contracts.Requires.NotNull(info, "info");

            new FileIOPermission(FileIOPermissionAccess.PathDiscovery, this.originalPath).Demand();

            info.AddValue("originalPath", this.originalPath, typeof(String));
        }
        #endregion

        #endregion
    }
#endif
}
