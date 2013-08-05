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
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Runtime.Serialization;
    using System.Security;
    using System.Security.AccessControl;
    using System.Security.Permissions;
    using System.Security.Principal;
    using Cadru.Properties;

    /// <summary>
    /// Provides an encapsulated implementation of the standard .NET 
    /// <see cref="DirectoryInfo"/> and the
    /// <see href="http://msdn.microsoft.com/en-us/library/windows/desktop/bb762179(v=vs.85).aspx">SHGetFileInfo</see>
    /// API method.
    /// </summary>
    [Serializable]
    public sealed class ExtendedDirectoryInfo : MarshalByRefObject, ISerializable
    {
        #region fields

        private DirectoryInfo directoryInfo;
        private string directoryOwner;
        private string originalPath;

        #endregion

        #region constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedDirectoryInfo"/> class, which acts as a wrapper for a file path.
        /// </summary>
        /// <param name="path">The fully qualified name of the new file, or the relative file name.</param>
        /// <exception cref="ArgumentNullException"><paramref name="path"/> is a <see langword="null"/>.</exception>
        /// <exception cref="SecurityException">The caller does not have the required permission.</exception>
        /// <exception cref="ArgumentException"><paramref name="path"/> is empty, contains only white spaces, or contains invalid characters.</exception>
        /// <exception cref="UnauthorizedAccessException">Access to fileName is denied.</exception>
        /// <exception cref="PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
        /// <exception cref="NotSupportedException"><paramref name="path"/> contains a colon (:) in the middle of the string.</exception>
        /// <remarks>You can specify either the fully qualified or the relative file name, but the security check gets the fully qualified name.</remarks>
        public ExtendedDirectoryInfo(string path)
        {
            this.Initialize(path);
        } 

        private ExtendedDirectoryInfo(SerializationInfo info, StreamingContext context)
        {
            Contracts.Requires.NotNull(info, "info");

            this.Initialize(info.GetString("originalPath"));
        } 

        #endregion

        #region events
        #endregion

        #region properties

        #region Attributes
        /// <summary>
        /// Gets or sets the FileAttributes of the current FileSystemInfo. 
        /// </summary>
        /// <value>FileAttributes of the current FileSystemInfo.</value>
        /// <exception cref="FileNotFoundException">The specified file does not exist.</exception>
        /// <exception cref="DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
        /// <exception cref="SecurityException">The caller does not have the required permission.</exception>
        /// <exception cref="ArgumentException">The caller attempts to set an invalid file attribute.</exception>
        /// <exception cref="IOException">Refresh cannot initialize the data.</exception>
        /// <remarks>
        /// <para>When first called, FileSystemInfo calls Refresh and returns the cached information on APIs to get attributes and so on. On subsequent calls, you must call Refresh to get the latest copy of the information.</para>
        /// <para>The value of this property is a combination of the archive, compressed, directory, hidden, offline, read-only, system, and temporary file attribute flags.</para>
        /// </remarks>
        public FileAttributes Attributes
        {
            get
            {
                return this.directoryInfo.Attributes;
            }

            set
            {
                this.directoryInfo.Attributes = value;
            }
        }
        #endregion

        #region CreationTime
        /// <summary>
        /// Gets or sets the creation time of the current FileSystemInfo object.
        /// </summary>
        /// <value>The creation date and time of the current FileSystemInfo object.</value>
        /// <exception cref="IOException">Refresh cannot initialize the data.</exception>
        /// <exception cref="DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
        /// <exception cref="PlatformNotSupportedException">The current operating system is not Microsoft Windows NT or later.</exception>
        /// <remarks>
        /// <para>When first called, FileSystemInfo calls Refresh and returns the cached information on APIs to get attributes and so on. On subsequent calls, you must call Refresh to get the latest copy of the information.</para>
        /// <para>If the file described in the FileSystemInfo object does not exist, this property will return 12:00 midnight, January 1, 1601 A.D. (C.E.) Coordinated Universal Time (UTC), adjusted to local time.</para>
        /// <para>NTFS-formatted drives may cache file meta-info, such as file creation time, for a short period of time. This process is known as file tunneling. As a result, it may be necessary to explicitly set the creation time of a file if you are overwriting or replacing an existing file.</para>
        /// <para>This property value is a <see langword="null" /> if the file system containing the FileSystemInfo object does not support this information.</para>
        /// <para>Windows 95, Windows 98, Windows 98 Second Edition Platform Note: These operating systems do not support this property, and DirectoryInfo implementations of this property are not supported.</para>
        /// <para>Windows Mobile for Pocket PC, Windows Mobile for Smartphone, Windows CE Platform Note: This property is read-only.</para>
        /// </remarks>
        public DateTime CreationTime
        {
            get
            {
                return this.directoryInfo.CreationTime;
            }

            set
            {
                this.directoryInfo.CreationTime = value;
            }
        }
        #endregion

        #region CreationTimeUtc
        /// <summary>
        /// Gets the creation time, in coordinated universal time (UTC), of the current FileSystemInfo object.
        /// </summary>
        /// <value>The creation date and time in UTC format of the current FileSystemInfo object.</value>
        /// <exception cref="IOException">Refresh cannot initialize the data.</exception>
        /// <exception cref="DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
        /// <exception cref="PlatformNotSupportedException">The current operating system is not Microsoft Windows NT or later.</exception>
        /// <remarks>
        /// <para>When first called, FileSystemInfo calls Refresh and returns the cached information on APIs to get attributes and so on. On subsequent calls, you must call Refresh to get the latest copy of the information.</para>
        /// <para>If the file described in the FileSystemInfo object does not exist, this property will return 12:00 midnight, January 1, 1601 A.D. (C.E.) Coordinated Universal Time (UTC), adjusted to local time.</para>
        /// <para>NTFS-formatted drives may cache file meta-info, such as file creation time, for a short period of time. This process is known as file tunneling. As a result, it may be necessary to explicitly set the creation time of a file if you are overwriting or replacing an existing file.</para>
        /// <para>This property value is a <see langword="null" /> if the file system containing the FileSystemInfo object does not support this information.</para>
        /// <para>Windows 95, Windows 98, Windows 98 Second Edition Platform Note: These operating systems do not support this property, and DirectoryInfo implementations of this property are not supported.</para>
        /// </remarks>
        public DateTime CreateTimeUtc
        {
            get
            {
                return this.directoryInfo.CreationTimeUtc;
            }
        }
        #endregion

        #region Exists
        /// <summary>
        /// Gets a value indicating whether the directory exists.
        /// </summary>
        /// <value><see langword="true"/> if the directory exists;
        /// otherwise, <see langword="false"/>.</value>
        public bool Exists
        {
            get
            {
                return this.directoryInfo.Exists;
            }
        } 
        #endregion

        #region Extension
        /// <summary>
        /// Gets the string representing the extension part of the file.
        /// </summary>
        /// <value>A string containing the FileSystemInfo extension.</value>
        /// <remarks>The Extension property returns the FileSystemInfo extension, including the period (.). For example, for a file C:\NewFile.txt, this property returns ".txt".</remarks>
        public string Extension
        {
            get
            {
                return this.directoryInfo.Extension;
            }
        }
        #endregion

        #region DirectoryOwner
        /// <summary>
        /// Gets the Windows owner associated with the directory.
        /// </summary>
        /// <value>A string representing the owner of the directory or <see langword="null"/> if the owner cannot be determined.</value>
        public string DirectoryOwner
        {
            get
            {
                return this.directoryOwner;
            }
        }
        #endregion

        #region FullName
        /// <summary>
        /// Gets the full path of the directory or file.
        /// </summary>
        /// <value>A string containing the full path.</value>
        /// <exception cref="SecurityException">The caller does not have the required permission.</exception>
        /// <remarks>For example, for a file C:\NewFile.txt, this property returns "C:\NewFile.txt".</remarks>
        public string FullName
        {
            get
            {
                return this.directoryInfo.FullName;
            }
        }
        #endregion

         #region LastAccessTime
        /// <summary>
        /// Gets the time the current file or directory was last accessed.
        /// </summary>
        /// <value>The time that the current file or directory was last accessed.</value>
        /// <exception cref="IOException">Refresh cannot initialize the data.</exception>
        /// <exception cref="PlatformNotSupportedException">The current operating system is not Microsoft Windows NT or later.</exception>
        /// <remarks>
        /// <para>When first called, FileSystemInfo calls Refresh and returns the cached information on APIs to get attributes and so on. On subsequent calls, you must call Refresh to get the latest copy of the information.</para>
        /// <para>If the file described in the FileSystemInfo object does not exist, this property will return 12:00 midnight, January 1, 1601 A.D. (C.E.) Coordinated Universal Time (UTC), adjusted to local time.</para>
        /// <para>NTFS-formatted drives may cache file meta-info, such as file creation time, for a short period of time. This process is known as file tunneling. As a result, it may be necessary to explicitly set the creation time of a file if you are overwriting or replacing an existing file.</para>
        /// <para>This property value is a <see langword="null" /> if the file system containing the FileSystemInfo object does not support this information.</para>
        /// <para>Windows 95, Windows 98, Windows 98 Second Edition Platform Note: These operating systems do not support this property, and DirectoryInfo implementations of this property are not supported.</para>
        /// <para>Windows Mobile for Pocket PC, Windows Mobile for Smartphone, Windows CE Platform Note: This property is read-only.</para>
        /// </remarks>
        public DateTime LastAccessTime
        {
            get
            {
                return this.directoryInfo.LastAccessTime;
            }
        }
        #endregion

        #region LastAccessTimeUtc
        /// <summary>
        /// Gets the time, in coordinated universal time (UTC), that the current file or directory was last accessed. 
        /// </summary>
        /// <value>The UTC time that the current file or directory was last accessed.</value>
        /// <exception cref="IOException">Refresh cannot initialize the data.</exception>
        /// <exception cref="PlatformNotSupportedException">The current operating system is not Microsoft Windows NT or later.</exception>
        /// <remarks>
        /// <para>When first called, FileSystemInfo calls Refresh and returns the cached information on APIs to get attributes and so on. On subsequent calls, you must call Refresh to get the latest copy of the information.</para>
        /// <para>If the file described in the FileSystemInfo object does not exist, this property will return 12:00 midnight, January 1, 1601 A.D. (C.E.) Coordinated Universal Time (UTC), adjusted to local time.</para>
        /// <para>NTFS-formatted drives may cache file meta-info, such as file creation time, for a short period of time. This process is known as file tunneling. As a result, it may be necessary to explicitly set the creation time of a file if you are overwriting or replacing an existing file.</para>
        /// <para>This property value is a <see langword="null" /> if the file system containing the FileSystemInfo object does not support this information.</para>
        /// <para>Windows 95, Windows 98, Windows 98 Second Edition Platform Note: These operating systems do not support this property, and DirectoryInfo implementations of this property are not supported.</para>
        /// </remarks>
        public DateTime LastAccessTimeUtc
        {
            get
            {
                return this.directoryInfo.LastAccessTimeUtc;
            }
        }
        #endregion

        #region LastWriteTime
        /// <summary>
        /// Gets the time when the current file or directory was last written to.
        /// </summary>
        /// <value>The time the current file was last written.</value>
        /// <exception cref="IOException">Refresh cannot initialize the data.</exception>
        /// <exception cref="PlatformNotSupportedException">The current operating system is not Microsoft Windows NT or later.</exception>
        /// <remarks>
        /// <para>When first called, FileSystemInfo calls Refresh and returns the cached information on APIs to get attributes and so on. On subsequent calls, you must call Refresh to get the latest copy of the information.</para>
        /// <para>If the file described in the FileSystemInfo object does not exist, this property will return 12:00 midnight, January 1, 1601 A.D. (C.E.) Coordinated Universal Time (UTC), adjusted to local time.</para>
        /// <para>NTFS-formatted drives may cache file meta-info, such as file creation time, for a short period of time. This process is known as file tunneling. As a result, it may be necessary to explicitly set the creation time of a file if you are overwriting or replacing an existing file.</para>
        /// <para>This property value is a <see langword="null" /> if the file system containing the FileSystemInfo object does not support this information.</para>
        /// <para>Windows 95, Windows 98, Windows 98 Second Edition Platform Note: These operating systems do not support this property, and DirectoryInfo implementations of this property are not supported.</para>
        /// <para>Windows Mobile for Pocket PC, Windows Mobile for Smartphone, Windows CE Platform Note: This property is read-only.</para>
        /// </remarks>
        public DateTime LastWriteTime
        {
            get
            {
                return this.directoryInfo.LastWriteTime;
            }
        }
        #endregion

        #region LastWriteTimeUtc
        /// <summary>
        /// Gets the time, in coordinated universal time (UTC), when the current file or directory was last written to. 
        /// </summary>
        /// <value>The UTC time when the current file was last written to.</value>
        /// <exception cref="IOException">Refresh cannot initialize the data.</exception>
        /// <exception cref="PlatformNotSupportedException">The current operating system is not Microsoft Windows NT or later.</exception>
        /// <remarks>
        /// <para>When first called, FileSystemInfo calls Refresh and returns the cached information on APIs to get attributes and so on. On subsequent calls, you must call Refresh to get the latest copy of the information.</para>
        /// <para>If the file described in the FileSystemInfo object does not exist, this property will return 12:00 midnight, January 1, 1601 A.D. (C.E.) Coordinated Universal Time (UTC), adjusted to local time.</para>
        /// <para>NTFS-formatted drives may cache file meta-info, such as file creation time, for a short period of time. This process is known as file tunneling. As a result, it may be necessary to explicitly set the creation time of a file if you are overwriting or replacing an existing file.</para>
        /// <para>This property value is a <see langword="null" /> if the file system containing the FileSystemInfo object does not support this information.</para>
        /// <para>Windows 95, Windows 98, Windows 98 Second Edition Platform Note: These operating systems do not support this property, and DirectoryInfo implementations of this property are not supported.</para>
        /// </remarks>
        public DateTime LastWriteTimUtc
        {
            get
            {
                return this.directoryInfo.LastWriteTimeUtc;
            }
        }
        #endregion

        #region Name
        /// <summary>
        /// Gets the name of this DirectoryInfo instance.
        /// </summary>
        /// <value>The directory name.</value>
        /// <remarsk>
        /// <para>This Name property returns only the name of the directory, such as "Bin". To get the full path, such as "c:\public\Bin", use the FullName property.</para>
        /// <para>The Name property of a DirectoryInfo requires no permission (beyond the read permission to the directory necessary to construct the Exists) but can give out the directory name. If it is necessary to hand out a DirectoryInfo to a protected directory with a cryptographically secure name, create a dummy directory for the untrusted code’s use.</para>
        /// </remarsk>
        public string Name
        {
            get
            {
                return this.directoryInfo.Name;
            }
        } 
        #endregion

        #region Parent
        /// <summary>
        /// Gets the parent directory of a specified subdirectory.
        /// </summary>
        /// <value>The parent directory, or a <see langword="null" /> if the path is null or if the file path denotes a root (such as "\", "C:", or * "\\server\share").</value>
        /// <exception cref="SecurityException">The caller does not have the required permission.</exception>
        public DirectoryInfo Parent
        {
            get
            {
                return this.directoryInfo.Parent;
            }
        }
        #endregion

        #region Root
        /// <summary>
        /// Gets the root portion of a path.
        /// </summary>
        /// <value>A DirectoryInfo object representing the root of a path.</value>
        /// <exception cref="SecurityException">The caller does not have the required permission.</exception>
        public DirectoryInfo Root
        {
            get
            {
                return this.directoryInfo.Root;
            }
        }
        #endregion

        #endregion

        #region methods

        #region Create

        #region Create()
        /// <summary>
        /// Creates a directory.
        /// </summary>
        /// <remarks>If the directory already exists, this method does nothing.</remarks>
        /// <exception cref="IOException">The directory cannot be created.</exception>
        public void Create()
        {
            this.directoryInfo.Create();
        }
        #endregion

        #region Create(DirectorySecurity directorySecurity)
        /// <summary>
        /// Creates a directory using a DirectorySecurity object.
        /// </summary>
        /// <param name="directorySecurity">The access control to apply to the directory.</param>
        /// <remarks>
        /// <para>Use this method overload to create a directory with access control, so there is no chance the directory can be accessed before security is applied.</para>
        /// <para>If the directory already exists, this method does nothing.</para></remarks>
        /// <exception cref="IOException">The directory specified by path is read-only or is not empty.</exception>
        /// <exception cref="UnauthorizedAccessException">The caller does not have the required permission.</exception>
        /// <exception cref="ArgumentException">path is a zero-length string, contains only white space, or contains one or more invalid characters as defined by InvalidPathChars.</exception>
        /// <exception cref="ArgumentNullException">path is a <see langword="null" />.</exception>
        /// <exception cref="PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters.</exception>
        /// <exception cref="DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
        /// <exception cref="NotSupportedException">Creating a directory with only the colon (:) character was attempted.</exception>
        public void Create(DirectorySecurity directorySecurity)
        {
            this.directoryInfo.Create(directorySecurity);
        }
        #endregion

        #endregion

        #region CreateSubdirectory

        #region CreateSubdirectory(string path)
        /// <summary>
        /// Creates a subdirectory or subdirectories on the specified path. The specified path can be relative to this instance of the DirectoryInfo class.
        /// </summary>
        /// <param name="path">The specified path. This cannot be a different disk volume or Universal Naming Convention (UNC) name.</param>
        /// <returns>The last directory specified in path.</returns>
        /// <remarks>
        /// <para>Any and all directories specified in path are created, unless some part of path is invalid. The path parameter specifies a directory path, not a file path. If the subdirectory already exists, this method does nothing.</para>
        /// <para type="note">Path names are limited to 248 characters.</para></remarks>
        /// <exception cref="ArgumentException"><paramref name="path"/> does not specify a valid file path or contains invalid DirectoryInfo characters.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="path"/> is a <see langword="null" />.</exception>
        /// <exception cref="DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
        /// <exception cref="IOException"><para>The subdirectory cannot be created.</para>
        /// <para>-or-</para>
        /// <para>A file or directory already has the name specified by path.</para></exception>
        /// <exception cref="PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters.</exception>
        /// <exception cref="SecurityException"><para>The caller does not have code access permission to create the directory.</para>
        /// <para>-or-</para>
        /// <para>The caller does not have code access permission to read the directory described by the returned DirectoryInfo object. This can occur when the path parameter describes an existing directory.</para></exception>
        public DirectoryInfo CreateSubdirectory(string path)
        {
            return this.directoryInfo.CreateSubdirectory(path);
        }
        #endregion

        #region Create(string path, DirectorySecurity directorySecurity)
        /// <summary>
        /// Creates a subdirectory or subdirectories on the specified path with the specified security. The specified path can be relative to this instance of the DirectoryInfo class. 
        /// </summary>
        /// <param name="path">The specified path. This cannot be a different disk volume or Universal Naming Convention (UNC) name.</param>
        /// <param name="directorySecurity">The security to apply.</param>
        /// <returns>The last directory specified in path.</returns>
        /// <remarks>
        /// <para>Any and all directories specified in path are created, unless some part of path is invalid. The path parameter specifies a directory path, not a file path. If the subdirectory already exists, this method does nothing.</para>
        /// <para type="note">Path names are limited to 248 characters.</para></remarks>
        /// <exception cref="ArgumentException"><paramref name="path"/> does not specify a valid file path or contains invalid DirectoryInfo characters.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="path"/> is a <see langword="null"/>.</exception>
        /// <exception cref="DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
        /// <exception cref="IOException"><para>The subdirectory cannot be created.</para>
        /// <para>-or-</para>
        /// <para>A file or directory already has the name specified by path.</para></exception>
        /// <exception cref="PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters.</exception>
        /// <exception cref="SecurityException"><para>The caller does not have code access permission to create the directory.</para>
        /// <para>-or-</para>
        /// <para>The caller does not have code access permission to read the directory described by the returned DirectoryInfo object. This can occur when the path parameter describes an existing directory.</para></exception>
        public DirectoryInfo CreateSubdirectory(string path, DirectorySecurity directorySecurity)
        {
            return this.directoryInfo.CreateSubdirectory(path, directorySecurity);
        }
        #endregion

        #endregion

        #region Delete

        #region Delete()
        /// <summary>
        /// Deletes this DirectoryInfo if it is empty.
        /// </summary>
        /// <exception cref="SecurityException">The caller does not have
        /// the required permission.</exception>
        /// <exception cref="IOException"><para>The directory is not empty.</para>
        /// <para>-or-</para>
        /// <para>The directory is the application's current working directory.</para></exception>
        public void Delete()
        {
            this.directoryInfo.Delete();
        } 
        #endregion

        #region Delete(bool recursive)
        /// <summary>
        /// Deletes this instance of a DirectoryInfo, specifying whether to delete subdirectories and files. 
        /// </summary>
        /// <param name="recursive"><see langword="true"/> to delete this directory, its subdirectories, and all files;
        /// otherwise, <see langword="false"/>.</param>
        /// <exception cref="SecurityException">The caller does not have
        /// the required permission.</exception>
        /// <exception cref="IOException"><para>The directory is read-only.</para>
        /// <para>-or-</para>
        /// <para>The directory contains one or more files or subdirectories and 
        /// <paramref name="recursive"/> is <see langword="false"/>.</para>
        /// <para>-or-</para>
        /// <para>The directory is the application's current working directory.</para></exception>
        /// <remarks>If the DirectoryInfo has no files or subdirectories, this method deletes the DirectoryInfo even if recursive is false. Attempting to delete a DirectoryInfo that is not empty when recursive is false throws an IOException.</remarks>
        public void Delete(bool recursive)
        {
            this.directoryInfo.Delete(recursive);
        } 
        #endregion

        #endregion

        #region GetAccessControl

        #region GetAccessControl()
        /// <summary>
        /// Gets a DirectorySecurity object that encapsulates the access control list (ACL) entries for the directory described by the current DirectoryInfo object.
        /// </summary>
        /// <returns>A DirectorySecurity object that encapsulates the access control rules for the directory.</returns>
        /// <exception cref="SystemException">The directory could not be found or modified.</exception>
        /// <exception cref="PlatformNotSupportedException">The current
        /// operating systme is not Microsoft Windows 2000 or later.
        /// </exception>
        /// <exception cref="IOException">An I/O error occurred while opening the directory.</exception>
        /// <exception cref="SystemException">The file could not be 
        /// found.</exception>
        /// <exception cref="UnauthorizedAccessException">
        /// <para>The current process does not have access to open the directory.</para>
        /// <para>-or-</para>
        /// <para>This operation
        /// is not supported on the current platform.</para>
        /// <para>-or-</para>
        /// <para>The caller does not have the required permission.</para>
        /// </exception>
        /// <remarks><para>Calling this method overload is equivalent to calling the GetAccessControl method overload and specifying the access control sections AccessControlSections.Access | AccessControlSections.Owner | AccessControlSections.Group (AccessControlSections.AccessOrAccessControlSections.OwnerOrAccessControlSections.Group in Visual Basic).</para>
        /// <para>Use the GetAccessControl method to retrieve the access control list (ACL) entries for the current file.</para>
        /// <para>An ACL describes individuals and/or groups who have, or do not have, rights to specific actions on the given file or directory. For more information, see ACL Technology Overview and How to: Add or Remove Access Control List Entries.</para>
        /// </remarks>
        public DirectorySecurity GetAccessControl()
        {
            return this.directoryInfo.GetAccessControl();
        } 
        #endregion

        #region GetAccessControl(AccessControlSections includeSections)
        /// <summary>
        /// Gets a DirectorySecurity object that encapsulates the specified type of access control list (ACL) entries for the directory described by the current DirectoryInfo object.
        /// </summary>
        /// <param name="includeSections">One of the 
        /// <see cref="AccessControlSections"/> values that specifies which
        /// group of access control entries to retrieve.</param>
        /// <returns>A DirectorySecurity object that encapsulates the access control rules for the file described by the path parameter.</returns>
        /// <exception cref="IOException">An I/O error occurred while opening
        /// the file.</exception>
        /// <exception cref="PlatformNotSupportedException">The current
        /// operating systme is not Microsoft Windows 2000 or later.
        /// </exception>
        /// <exception cref="PrivilegeNotHeldException">The current system
        /// account does not have administrative privileges.</exception>
        /// <exception cref="SystemException">The file could not be 
        /// found.</exception>
        /// <exception cref="UnauthorizedAccessException"><para>This operation
        /// is not supported on teh current platform.</para>
        /// <para>-or-</para>
        /// <para>The caller does not have the required permission.</para>
        /// </exception>
        /// <remarks><para>Use the GetAccessControl method to retrieve the access control list (ACL) entries for the current file.</para>
        /// <para>An ACL describes individuals and/or groups who have, or do
        /// not have, rights to specific actions on the given file.</para>
        /// </remarks>
        public DirectorySecurity GetAccessControl(AccessControlSections includeSections)
        {
            return this.directoryInfo.GetAccessControl(includeSections);
        }
        #endregion

        #endregion

        #region GetDirectories

        #region GetDirectories()
        /// <summary>
        /// Returns the subdirectories of the current directory.
        /// </summary>
        /// <returns>An array of DirectoryInfo objects.</returns>
        /// <remarks>If there are no subdirectories, this method returns an empty array. This method is not recursive.</remarks>
        /// <exception cref="DirectoryNotFoundException">The path encapsulated in the DirectoryInfo object is invalid, such as being on an unmapped drive.</exception>
        public DirectoryInfo[] GetDirectories()
        {
            return this.directoryInfo.GetDirectories();
        }
        #endregion

        #region GetDirectories(String searchPattern)
        /// <summary>
        /// Returns an array of directories in the current DirectoryInfo matching the given search criteria. 
        /// </summary>
        /// <param name="searchPattern">The search string, such as "System*", used to search for all directories beginning with the word "System". </param>
        /// <returns>An array of type DirectoryInfo matching searchPattern.</returns>
        /// <remarks><para>Wildcards are permitted. For example, the searchPattern string "*t" searches for all directory names in path ending with the letter "t". The searchPattern string "s*" searches for all directory names in path beginning with the letter "s".</para>
        /// <para>The string ".." can only be used in searchPattern if it is specified as a part of a valid directory name, such as in the directory name "a..b". It cannot be used to move up the directory hierarchy.</para>
        /// <para>If there are no subdirectories, or no subdirectories match the searchPattern parameter, this method returns an empty array.</para>
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="searchPattern"/> is a <see langword="null"/>.</exception>
        /// <exception cref="DirectoryNotFoundException">The path encapsulated in the DirectoryInfo object is invalid, such as being on an unmapped drive.</exception>
        /// <exception cref="SecurityException">The caller does not have the required permission.</exception>
        public DirectoryInfo[] GetDirectories(String searchPattern)
        {
            return this.directoryInfo.GetDirectories(searchPattern);
        }
        #endregion

        #region GetDirectories(String searchPattern, SearchOption searchOption)
        /// <summary>
        /// Returns an array of directories in the current DirectoryInfo matching the given search criteria and using a value to determine whether to search subdirectories.
        /// </summary>
        /// <param name="searchPattern">The search string, such as "System*", used to search for all directories beginning with the word "System".</param>
        /// <param name="searchOption">One of the values of the SearchOption enumeration that specifies whether the search operation should include only the current directory or should include all subdirectories.</param>
        /// <returns>An array of type DirectoryInfo matching <paramref name="searchPattern"/>.</returns>
        /// <remarks><para>Wildcards are permitted. For example, the searchPattern string "*t" searches for all directory names in path ending with the letter "t". The searchPattern string "s*" searches for all directory names in path beginning with the letter "s".</para>
        /// <para>The string ".." can only be used in <paramref name="searchPattern"/> if it is specified as a part of a valid directory name, such as in the directory name "a..b". It cannot be used to move up the directory hierarchy.</para>
        /// <para>If there are no subdirectories, or no subdirectories match the searchPattern parameter, this method returns an empty array.</para>
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="searchPattern"/> is a <see langword="null"/>.</exception>
        /// <exception cref="DirectoryNotFoundException">The path encapsulated in the DirectoryInfo object is invalid, such as being on an unmapped drive.</exception>
        /// <exception cref="SecurityException">The caller does not have the required permission.</exception>
        public DirectoryInfo[] GetDirectories(String searchPattern, SearchOption searchOption)
        {
            return this.directoryInfo.GetDirectories(searchPattern, searchOption);
        }
        #endregion

        #endregion

        #region GetFiles

        #region GetFiles()
        /// <summary>
        /// Returns a file list from the current directory.
        /// </summary>
        /// <returns>An array of type FileInfo.</returns>
        /// <remarks><para>If there are no files in the DirectoryInfo, this method returns an empty array.</para>
        /// <para>The order of the returned file names is not guaranteed; use the Sort method if a specific sort order is required.</para></remarks>
        /// <exception cref="DirectoryNotFoundException">The path is invalid, such as being on an unmapped drive.</exception>
        public FileInfo[] GetFiles()
        {
            return this.directoryInfo.GetFiles();
        } 
        #endregion

        #region GetFiles(String searchPattern)
        /// <summary>
        /// Returns a file list from the current directory matching the given searchPattern.
        /// </summary>
        /// <param name="searchPattern">The search string, such as "*.txt".</param>
        /// <returns>An array of type FileInfo.</returns>
        /// <remarks>
        /// <para>The following wildcard specifiers are permitted in the searchPattern parameter.</para>
        /// <list type="table">
        /// <listheader>
        /// <term>Wildcard character</term>
        /// <term>Description</term>
        /// </listheader>
        /// <item><term>*</term><description>Zero or more characters.</description></item>
        /// <item><term>?</term><description>Exactly one character.</description></item>
        /// </list>
        /// <para>The order of the returned file names is not guaranteed; use the Sort method if a specific sort order is required.</para>
        /// <para>Wildcards are permitted. For example, the searchPattern string "*.txt" searches for all file names having an extension of "txt". The searchPattern string "s*" searches for all file names beginning with the letter "s". If there are no files, or no files that match the searchPattern string in the DirectoryInfo, this method returns an empty array.</para>
        /// <para type="note">When using the asterisk wildcard character in a searchPattern (for example, "*.txt"), the matching behavior varies depending on the length of the specified file extension. A searchPattern with a file extension of exactly three characters returns files with an extension of three or more characters, where the first three characters match the file extension specified in the searchPattern. A searchPattern with a file extension of one, two, or more than three characters returns only files with extensions of exactly that length that match the file extension specified in the searchPattern. When using the question mark wildcard character, this method returns only files that match the specified file extension. For example, given two files in a directory, "file1.txt" and "file1.txtother", a search pattern of "file?.txt" returns only the first file, while a search pattern of "file*.txt" returns both files.</para>
        /// <para>The following list shows the behavior of different lengths for the searchPattern parameter:</para>
        /// <list type="bulleted">
        /// <item>"*.abc" returns files having an extension of.abc,.abcd,.abcde,.abcdef, and so on.</item>
        /// <item>"*.abcd" returns only files having an extension of.abcd.</item>
        /// <item>"*.abcde" returns only files having an extension of.abcde.</item>
        /// <item>"*.abcdef" returns only files having an extension of.abcdef.</item>
        /// </list>
        /// <para type="note">Because this method checks against file names with both the 8.3 file name format and the long file name format, a search pattern similar to "*1*.txt" may return unexpected file names. For example, using a search pattern of "*1*.txt" will return "longfilename.txt" because the equivalent 8.3 file name format would be "longf~1.txt".</para>
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="searchPattern"/> is a <see langword="null" />.</exception>
        /// <exception cref="DirectoryNotFoundException">The path is invalid, such as being on an unmapped drive.</exception>
        /// <exception cref="SecurityException">The caller does not have the required permission.</exception>
        public FileInfo[] GetFiles(String searchPattern)
        {
            return this.directoryInfo.GetFiles(searchPattern);
        } 
        #endregion

        #region GetFiles(String searchPattern, SearchOption searchOption)
        /// <summary>
        /// Returns a file list from the current directory matching the given searchPattern and using a value to determine whether to search subdirectories.
        /// </summary>
        /// <param name="searchPattern">The search string, such as "*.txt".</param>
        /// <returns>An array of type FileInfo.</returns>
        /// <param name="searchOption">One of the values of the SearchOption enumeration that specifies whether the search operation should include only the current directory or should include all subdirectories.</param>
        /// <remarks>
        /// <para>The following wildcard specifiers are permitted in the searchPattern parameter.</para>
        /// <list type="table">
        /// <listheader>
        /// <term>Wildcard character</term>
        /// <term>Description</term>
        /// </listheader>
        /// <item><term>*</term><description>Zero or more characters.</description></item>
        /// <item><term>?</term><description>Exactly one character.</description></item>
        /// </list>
        /// <para>The order of the returned file names is not guaranteed; use the Sort method if a specific sort order is required.</para>
        /// <para>Wildcards are permitted. For example, the searchPattern string "*.txt" searches for all file names having an extension of "txt". The searchPattern string "s*" searches for all file names beginning with the letter "s". If there are no files, or no files that match the searchPattern string in the DirectoryInfo, this method returns an empty array.</para>
        /// <para type="note">When using the asterisk wildcard character in a searchPattern (for example, "*.txt"), the matching behavior varies depending on the length of the specified file extension. A searchPattern with a file extension of exactly three characters returns files with an extension of three or more characters, where the first three characters match the file extension specified in the searchPattern. A searchPattern with a file extension of one, two, or more than three characters returns only files with extensions of exactly that length that match the file extension specified in the searchPattern. When using the question mark wildcard character, this method returns only files that match the specified file extension. For example, given two files in a directory, "file1.txt" and "file1.txtother", a search pattern of "file?.txt" returns only the first file, while a search pattern of "file*.txt" returns both files.</para>
        /// <para>The following list shows the behavior of different lengths for the searchPattern parameter:</para>
        /// <list type="bulleted">
        /// <item>"*.abc" returns files having an extension of.abc,.abcd,.abcde,.abcdef, and so on.</item>
        /// <item>"*.abcd" returns only files having an extension of.abcd.</item>
        /// <item>"*.abcde" returns only files having an extension of.abcde.</item>
        /// <item>"*.abcdef" returns only files having an extension of.abcdef.</item>
        /// </list>
        /// <para type="note">Because this method checks against file names with both the 8.3 file name format and the long file name format, a search pattern similar to "*1*.txt" may return unexpected file names. For example, using a search pattern of "*1*.txt" will return "longfilename.txt" because the equivalent 8.3 file name format would be "longf~1.txt".</para>
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="searchPattern"/> is a <see langword="null" />.</exception>
        /// <exception cref="DirectoryNotFoundException">The path is invalid, such as being on an unmapped drive.</exception>
        /// <exception cref="SecurityException">The caller does not have the required permission.</exception>
        public FileInfo[] GetFiles(String searchPattern, SearchOption searchOption)
        {
            return this.directoryInfo.GetFiles(searchPattern, searchOption);
        } 
        #endregion

        #endregion

        #region GetFileSystemInfos

        #region GetFileSystemInfos()
        /// <summary>
        /// Returns an array of strongly typed FileSystemInfo entries representing all the files and subdirectories in a directory.
        /// </summary>
        /// <returns>An array of strongly typed FileSystemInfo entries.</returns>
        /// <remarks><para>If there are no files or directories in the DirectoryInfo, this method returns an empty array. This method is not recursive.</para>
        /// <para>For subdirectories, the FileSystemInfo objects returned by this method can be cast to the derived class DirectoryInfo. Use the FileAttributes value returned by the FileSystemInfo.Attributes property to determine whether the FileSystemInfo represents a file or a directory.</para>
        /// </remarks>
        /// <exception cref="DirectoryNotFoundException">The path is invalid, such as being on an unmapped drive.</exception>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Infos", Justification = "This is the spelling used in the underlying DirectoryInfo object and is preserved here for consistency.")]
        public FileSystemInfo[] GetFileSystemInfos()
        {
            return this.directoryInfo.GetFileSystemInfos();
        } 
        #endregion

        #region GetFileSystemInfos(String searchPattern)
        /// <summary>
        /// Retrieves an array of strongly typed FileSystemInfo objects representing the files and subdirectories matching the specified search criteria.
        /// </summary>
        /// <param name="searchPattern">The search string, such as "System*", used to search for all directories beginning with the word "System".</param>
        /// <returns>An array of strongly typed FileSystemInfo objects matching the search criteria.</returns>
        /// <remarks><para>This method is not recursive.</para>
        /// <para>For subdirectories, the FileSystemInfo objects returned by this method can be cast to the derived class DirectoryInfo. Use the FileAttributes value returned by the FileSystemInfo.Attributes property to determine whether the FileSystemInfo represents a file or a directory.</para>
        /// <para>Wild cards are permitted. For example, the searchPattern string "*t" searches for all directory names in path ending with the letter "t". The searchPattern string "s*" searches for all directory names in path beginning with the letter "s".</para>
        /// <para>The string ".." can only be used in searchPattern if it is specified as a part of a valid directory name, such as in the directory name "a..b". It cannot be used to move up the directory hierarchy. If there are no files or directories, or no files or directories that match the searchPattern string in the DirectoryInfo, this method returns an empty array.</para>
        /// </remarks>
        /// <exception cref="DirectoryNotFoundException">The path is invalid, such as being on an unmapped drive.</exception>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Infos", Justification = "This is the spelling used in the underlying DirectoryInfo object and is preserved here for consistency.")]
        public FileSystemInfo[] GetFileSystemInfos(String searchPattern)
        {
            return this.directoryInfo.GetFileSystemInfos(searchPattern);
        } 
        #endregion

        #endregion

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

        #region MoveTo
        /// <summary>
        /// Moves a DirectoryInfo instance and its contents to a new path. 
        /// </summary>
        /// <param name="destinationDirectoryName">The name and path to which to move this directory. The destination cannot be another disk volume or a directory with the identical name. It can be an existing directory to which you want to add this directory as a subdirectory.</param>
        /// <remarks><para>This method throws an IOException if, for example, you try to move c:\mydir to c:\public, and c:\public already exists. You must specify "c:\\public\\mydir" as the <paramref name="destinationDirectoryName"/> parameter, or specify a new directory name such as "c:\\newdir".</para>
        /// <para>This method permits moving a directory to a read-only directory. The read/write attribute of neither directory is affected.
        /// </para></remarks>
        /// <exception cref="ArgumentNullException">
        /// <para><paramref name="destinationDirectoryName"/> is a <see langword="null" />.</para>
        /// <para>-or-</para>
        /// <para>The directory being moved and the destination directory have the same name.</para>
        /// </exception>
        /// <exception cref="ArgumentException"><paramref name="destinationDirectoryName"/> is an empty string ("").</exception>
        /// <exception cref="IOException">
        /// <para>An attempt was made to move a directory to a different volume.</para>
        /// <para>-or-</para>
        /// <para><paramref name="destinationDirectoryName"/> already exists.</para>
        /// <para>-or-</para>
        /// <para>You are not authorized to access this path.</para>
        /// </exception>
        /// <exception cref="SecurityException">The caller does not have the required permission.</exception>
        /// <exception cref="DirectoryNotFoundException">The destination directory cannot be found.</exception>
        public void MoveTo(string destinationDirectoryName)
        {
            this.directoryInfo.MoveTo(destinationDirectoryName);
        } 
        #endregion

        #region Refresh
        /// <summary>
        /// Refreshes the state of the object.
        /// </summary>
        /// <exception cref="IOException">A device such as a disk drive is not ready.</exception>
        /// <remarks><para>FileSystemInfo.Refresh takes a snapshot of the file from the current file system. Refresh cannot correct the underlying file system even if the file system returns incorrect or outdated information. This can happen on platforms such as Windows 98.
        /// </para>
        /// <para>Calls must be made to Refresh before attempting to get the attribute information, or the information will be outdated.</para>
        /// </remarks>
        public void Refresh()
        {
            this.directoryInfo.Refresh();
        } 
        #endregion

        #region SetAccessControl
        /// <summary>
        /// Applies access control list (ACL) entries described by a DirectorySecurity object to the directory described by the current DirectoryInfo object.  
        /// </summary>
        /// <param name="directorySecurity">A DirectorySecurity object that describes an ACL entry to apply to the directory described by the path parameter.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="directorySecurity"/> parameter is <see langword="null"/>.</exception>
        /// <exception cref="SystemException">The file could not be found or modified.</exception>
        /// <exception cref="UnauthorizedAccessException">The current process does not have access to open the file.</exception>
        /// <exception cref="PlatformNotSupportedException">The current operating system is not Microsoft Windows 2000 or later.</exception>
        /// <remarks>
        /// <para>The SetAccessControl method applies access control list (ACL) entries to the current file that represents the noninherited ACL list.</para>
        /// <para>Use the SetAccessControl method whenever you need to add or remove ACL entries from a file.</para>
        /// <para type="caution">The ACL specified for the directorySecurity parameter replaces the existing ACL for the file. To add permissions for a new user, use the GetAccessControl method to obtain the existing ACL, modify it, and then use SetAccessControl to apply it back to the file.</para>
        /// <para>An ACL describes individuals and/or groups who have, or do not have, rights to specific actions on the given file.</para>
        /// <para>The SetAccessControl method persists only DirectorySecurity objects that have been modified after object creation.  If a DirectorySecurity object has not been modified, it will not be persisted to a file.  Therefore, it is not possible to retrieve a DirectorySecurity object from one file and reapply the same object to another file.</para>
        /// <para>To copy ACL information from one file to another:</para>
        /// <list type="numbered">
        /// <item>Use the GetAccessControl method to retrieve the DirectorySecurity object from the source file.</item>
        /// <item>Create a new DirectorySecurity object for the destination file.</item>
        /// <item>Use the GetSecurityDescriptorBinaryForm or GetSecurityDescriptorSddlForm method of the source FileSecurity object to retrieve the ACL information.</item>
        /// <item>Use the SetSecurityDescriptorBinaryForm or SetSecurityDescriptorSddlForm method to copy the information retrieved in step 3 to the destination DirectorySecurity object.</item>
        /// <item>Set the destination DirectorySecurity object to the destination file using the SetAccessControl method.</item>
        /// </list>
        /// </remarks>
        public void SetAccessControl(DirectorySecurity directorySecurity)
        {
            this.directoryInfo.SetAccessControl(directorySecurity);
        } 
        #endregion

        #region ToString
        /// <summary>
        /// Returns the path as a string. 
        /// </summary>
        /// <returns>A string representing the path.</returns>
        /// <remarks>The string returned by the ToString method represents path that was passed to the constructor. When you create a FileInfo object using the constructors, the ToString method returns the fully qualified path. However, there are cases where the string returned by the ToString method does not represent the fully qualified path. For example, when you create a FileInfo object using the GetFiles method, the ToString method does not represent the fully qualified path.</remarks>
        public override string ToString()
        {
            return this.directoryInfo.ToString();
        } 
        #endregion

        #region Initialize
        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        private void Initialize(string path)
        {
            Contracts.Requires.NotNullOrEmpty(path, "path");

            this.originalPath = path;
            this.directoryInfo = new DirectoryInfo(path);

            string owner = null;
            if (this.directoryInfo.Exists)
            {
                DirectorySecurity ds = this.directoryInfo.GetAccessControl(AccessControlSections.Owner);
                owner = ds.GetOwner(typeof(NTAccount)).ToString();
            }

            this.directoryOwner = owner;
        }
        #endregion

        #endregion
    }
}
