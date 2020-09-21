//------------------------------------------------------------------------------
// <copyright file="ExtendedDirectoryInfo.cs"
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
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Security;
using System.Security.AccessControl;
using System.Security.Principal;

using Validation;

namespace Cadru.IO
{
    /// <summary>
    /// Provides an encapsulated implementation of the standard .NET
    /// <see cref="DirectoryInfo"/> and the
    /// <see href="http://msdn.microsoft.com/en-us/library/windows/desktop/bb762179(v=vs.85).aspx">SHGetFileInfo</see>
    /// API method.
    /// </summary>
    public sealed partial class ExtendedDirectoryInfo
    {
        private readonly DirectoryInfo directoryInfo;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="ExtendedDirectoryInfo"/> class, which acts as a wrapper
        /// for a file path.
        /// </summary>
        /// <param name="path">
        /// The fully qualified name of the new file, or the relative file name.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="path"/> is a <see langword="null"/>.
        /// </exception>
        /// <exception cref="SecurityException">
        /// The caller does not have the required permission.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="path"/> is empty, contains only white spaces, or
        /// contains invalid characters.
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        /// Access to fileName is denied.
        /// </exception>
        /// <exception cref="PathTooLongException">
        /// The specified path, file name, or both exceed the system-defined
        /// maximum length. For example, on Windows-based platforms, paths must
        /// be less than 248 characters, and file names must be less than 260 characters.
        /// </exception>
        /// <exception cref="NotSupportedException">
        /// <paramref name="path"/> contains a colon (:) in the middle of the string.
        /// </exception>
        /// <remarks>
        /// You can specify either the fully qualified or the relative file
        /// name, but the security check gets the fully qualified name.
        /// </remarks>
        public ExtendedDirectoryInfo(string path)
        {
            Requires.NotNullOrEmpty(path, "path");
            this.directoryInfo = new DirectoryInfo(path);
            if (this.directoryInfo.Exists)
            {
                var ds = this.directoryInfo.GetAccessControl(AccessControlSections.Owner);
                this.DirectoryOwner = ds.GetOwner(typeof(NTAccount)).ToString();
            }
        }

        /// <inheritdoc cref="FileSystemInfo.Attributes"/>
        public FileAttributes Attributes
        {
            get => this.directoryInfo.Attributes;
            set => this.directoryInfo.Attributes = value;
        }

        /// <inheritdoc cref="FileSystemInfo.CreationTimeUtc"/>
        public DateTime CreateTimeUtc => this.directoryInfo.CreationTimeUtc;

        /// <inheritdoc cref="FileSystemInfo.CreationTime"/>
        public DateTime CreationTime
        {
            get => this.directoryInfo.CreationTime;

            set => this.directoryInfo.CreationTime = value;
        }

        /// <summary>
        /// Gets the Windows owner associated with the directory.
        /// </summary>
        /// <value>
        /// A string representing the owner of the directory or
        /// <see langword="null"/> if the owner cannot be determined.
        /// </value>
        public string? DirectoryOwner { get; }

        /// <inheritdoc cref="DirectoryInfo.Exists"/>
        public bool Exists => this.directoryInfo.Exists;

        /// <inheritdoc cref="FileSystemInfo.Extension"/>
        public string Extension => this.directoryInfo.Extension;

        /// <inheritdoc cref="FileSystemInfo.FullName"/>
        public string FullName => this.directoryInfo.FullName;

        /// <inheritdoc cref="FileSystemInfo.LastAccessTime"/>
        public DateTime LastAccessTime => this.directoryInfo.LastAccessTime;

        /// <inheritdoc cref="FileSystemInfo.LastAccessTimeUtc"/>
        public DateTime LastAccessTimeUtc => this.directoryInfo.LastAccessTimeUtc;

        /// <inheritdoc cref="FileSystemInfo.LastWriteTime"/>
        public DateTime LastWriteTime => this.directoryInfo.LastWriteTime;

        /// <inheritdoc cref="FileSystemInfo.LastWriteTimeUtc"/>
        public DateTime LastWriteTimUtc => this.directoryInfo.LastWriteTimeUtc;

        /// <inheritdoc cref="DirectoryInfo.Name"/>
        public string Name => this.directoryInfo.Name;

        /// <inheritdoc cref="DirectoryInfo.Parent"/>
        public DirectoryInfo Parent => this.directoryInfo.Parent;

        /// <inheritdoc cref="DirectoryInfo.Root"/>
        public DirectoryInfo Root => this.directoryInfo.Root;

        /// <inheritdoc cref="DirectoryInfo.Create"/>
        public void Create()
        {
            this.directoryInfo.Create();
        }

        /// <inheritdoc cref="DirectoryInfo.CreateSubdirectory(String)"/>
        public DirectoryInfo CreateSubdirectory(string path)
        {
            return this.directoryInfo.CreateSubdirectory(path);
        }

        /// <inheritdoc cref="DirectoryInfo.Delete()"/>
        public void Delete()
        {
            this.directoryInfo.Delete();
        }

        /// <inheritdoc cref="DirectoryInfo.Delete(bool)"/>
        [SuppressMessage("Style", "IDE0049:Use framework type", Justification = "<Pending>")]
        public void Delete(bool recursive)
        {
            this.directoryInfo.Delete(recursive);
        }

        /// <summary>
        /// Gets a DirectorySecurity object that encapsulates the access control
        /// list (ACL) entries for the directory described by the current
        /// DirectoryInfo object.
        /// </summary>
        /// <returns>
        /// A DirectorySecurity object that encapsulates the access control
        /// rules for the directory.
        /// </returns>
        /// <exception cref="Exception">
        /// The directory could not be found or modified.
        /// </exception>
        /// <exception cref="PlatformNotSupportedException">
        /// The current operating systme is not Microsoft Windows 2000 or later.
        /// </exception>
        /// <exception cref="IOException">
        /// An I/O error occurred while opening the directory.
        /// </exception>
        /// <exception cref="Exception">The file could not be found.</exception>
        /// <exception cref="UnauthorizedAccessException">
        /// <para>The current process does not have access to open the directory.</para>
        /// <para>-or-</para>
        /// <para>This operation is not supported on the current platform.</para>
        /// <para>-or-</para>
        /// <para>The caller does not have the required permission.</para>
        /// </exception>
        /// <remarks>
        /// <para>
        /// Calling this method overload is equivalent to calling the
        /// GetAccessControl method overload and specifying the access control
        /// sections AccessControlSections.Access | AccessControlSections.Owner
        /// | AccessControlSections.Group
        /// (AccessControlSections.AccessOrAccessControlSections.OwnerOrAccessControlSections.Group
        /// in Visual Basic).
        /// </para>
        /// <para>
        /// Use the GetAccessControl method to retrieve the access control list
        /// (ACL) entries for the current file.
        /// </para>
        /// <para>
        /// An ACL describes individuals and/or groups who have, or do not have,
        /// rights to specific actions on the given file or directory. For more
        /// information, see ACL Technology Overview and How to: Add or Remove
        /// Access Control List Entries.
        /// </para>
        /// </remarks>
        public DirectorySecurity GetAccessControl()
        {
            return this.directoryInfo.GetAccessControl();
        }

        /// <summary>
        /// Gets a DirectorySecurity object that encapsulates the specified type
        /// of access control list (ACL) entries for the directory described by
        /// the current DirectoryInfo object.
        /// </summary>
        /// <param name="includeSections">
        /// One of the <see cref="AccessControlSections"/> values that specifies
        /// which group of access control entries to retrieve.
        /// </param>
        /// <returns>
        /// A DirectorySecurity object that encapsulates the access control
        /// rules for the file described by the path parameter.
        /// </returns>
        /// <exception cref="IOException">
        /// An I/O error occurred while opening the file.
        /// </exception>
        /// <exception cref="PlatformNotSupportedException">
        /// The current operating systme is not Microsoft Windows 2000 or later.
        /// </exception>
        /// <exception cref="PrivilegeNotHeldException">
        /// The current system account does not have administrative privileges.
        /// </exception>
        /// <exception cref="Exception">The file could not be found.</exception>
        /// <exception cref="UnauthorizedAccessException">
        /// <para>This operation is not supported on teh current platform.</para>
        /// <para>-or-</para>
        /// <para>The caller does not have the required permission.</para>
        /// </exception>
        /// <remarks>
        /// <para>
        /// Use the GetAccessControl method to retrieve the access control list
        /// (ACL) entries for the current file.
        /// </para>
        /// <para>
        /// An ACL describes individuals and/or groups who have, or do not have,
        /// rights to specific actions on the given file.
        /// </para>
        /// </remarks>
        public DirectorySecurity GetAccessControl(AccessControlSections includeSections)
        {
            return this.directoryInfo.GetAccessControl(includeSections);
        }

        /// <inheritdoc cref="DirectoryInfo.GetDirectories()"/>
        public DirectoryInfo[] GetDirectories()
        {
            return this.directoryInfo.GetDirectories();
        }

        /// <inheritdoc cref="DirectoryInfo.GetDirectories(String)"/>
        public DirectoryInfo[] GetDirectories(string searchPattern)
        {
            return this.directoryInfo.GetDirectories(searchPattern);
        }

        /// <inheritdoc cref="DirectoryInfo.GetDirectories(String, SearchOption)"/>
        public DirectoryInfo[] GetDirectories(string searchPattern, SearchOption searchOption)
        {
            return this.directoryInfo.GetDirectories(searchPattern, searchOption);
        }

        /// <inheritdoc cref="DirectoryInfo.GetFiles()"/>
        public FileInfo[] GetFiles()
        {
            return this.directoryInfo.GetFiles();
        }

        /// <inheritdoc cref="DirectoryInfo.GetFiles(String)"/>
        public FileInfo[] GetFiles(string searchPattern)
        {
            return this.directoryInfo.GetFiles(searchPattern);
        }

        /// <inheritdoc cref="DirectoryInfo.GetFiles(String, SearchOption)"/>
        public FileInfo[] GetFiles(string searchPattern, SearchOption searchOption)
        {
            return this.directoryInfo.GetFiles(searchPattern, searchOption);
        }

        /// <inheritdoc cref="DirectoryInfo.GetFileSystemInfos()"/>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Infos", Justification = "This is the spelling used in the underlying DirectoryInfo object and is preserved here for consistency.")]
        public FileSystemInfo[] GetFileSystemInfos()
        {
            return this.directoryInfo.GetFileSystemInfos();
        }

        /// <inheritdoc cref="DirectoryInfo.GetFileSystemInfos(String)"/>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Infos", Justification = "This is the spelling used in the underlying DirectoryInfo object and is preserved here for consistency.")]
        public FileSystemInfo[] GetFileSystemInfos(string searchPattern)
        {
            return this.directoryInfo.GetFileSystemInfos(searchPattern);
        }

        /// <inheritdoc cref="DirectoryInfo.MoveTo(String)"/>
        public void MoveTo(string destinationDirectoryName)
        {
            this.directoryInfo.MoveTo(destinationDirectoryName);
        }

        /// <inheritdoc cref="FileSystemInfo.Refresh"/>
        public void Refresh()
        {
            this.directoryInfo.Refresh();
        }

        /// <summary>
        /// Applies access control list (ACL) entries described by a
        /// DirectorySecurity object to the directory described by the current
        /// DirectoryInfo object.
        /// </summary>
        /// <param name="directorySecurity">
        /// A DirectorySecurity object that describes an ACL entry to apply to
        /// the directory described by the path parameter.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="directorySecurity"/> parameter is <see langword="null"/>.
        /// </exception>
        /// <exception cref="Exception">
        /// The file could not be found or modified.
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        /// The current process does not have access to open the file.
        /// </exception>
        /// <exception cref="PlatformNotSupportedException">
        /// The current operating system is not Microsoft Windows 2000 or later.
        /// </exception>
        /// <remarks>
        /// <para>
        /// The SetAccessControl method applies access control list (ACL)
        /// entries to the current file that represents the noninherited ACL list.
        /// </para>
        /// <para>
        /// Use the SetAccessControl method whenever you need to add or remove
        /// ACL entries from a file.
        /// </para>
        /// <para type="caution">
        /// The ACL specified for the directorySecurity parameter replaces the
        /// existing ACL for the file. To add permissions for a new user, use
        /// the GetAccessControl method to obtain the existing ACL, modify it,
        /// and then use SetAccessControl to apply it back to the file.
        /// </para>
        /// <para>
        /// An ACL describes individuals and/or groups who have, or do not have,
        /// rights to specific actions on the given file.
        /// </para>
        /// <para>
        /// The SetAccessControl method persists only DirectorySecurity objects
        /// that have been modified after object creation. If a
        /// DirectorySecurity object has not been modified, it will not be
        /// persisted to a file. Therefore, it is not possible to retrieve a
        /// DirectorySecurity object from one file and reapply the same object
        /// to another file.
        /// </para>
        /// <para>To copy ACL information from one file to another:</para>
        /// <list type="numbered">
        /// <item>
        /// Use the GetAccessControl method to retrieve the DirectorySecurity
        /// object from the source file.
        /// </item>
        /// <item>
        /// Create a new DirectorySecurity object for the destination file.
        /// </item>
        /// <item>
        /// Use the GetSecurityDescriptorBinaryForm or
        /// GetSecurityDescriptorSddlForm method of the source FileSecurity
        /// object to retrieve the ACL information.
        /// </item>
        /// <item>
        /// Use the SetSecurityDescriptorBinaryForm or
        /// SetSecurityDescriptorSddlForm method to copy the information
        /// retrieved in step 3 to the destination DirectorySecurity object.
        /// </item>
        /// <item>
        /// Set the destination DirectorySecurity object to the destination file
        /// using the SetAccessControl method.
        /// </item>
        /// </list>
        /// </remarks>
        public void SetAccessControl(DirectorySecurity directorySecurity)
        {
            this.directoryInfo.SetAccessControl(directorySecurity);
        }

        /// <inheritdoc cref="DirectoryInfo.ToString"/>
        public override string ToString()
        {
            return this.directoryInfo.ToString();
        }
    }
}