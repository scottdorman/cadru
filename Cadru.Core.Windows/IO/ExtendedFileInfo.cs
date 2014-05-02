//------------------------------------------------------------------------------
// <copyright file="ExtendedFileInfo.cs" 
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
    using System.Diagnostics;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Runtime.Serialization;
    using System.Security;
    using System.Security.AccessControl;
    using System.Security.Permissions;
    using System.Security.Principal;
    using Cadru.InteropServices;

    /// <summary>
    /// Provides an encapsulated implementation of the standard .NET 
    /// <see cref="FileInfo"/>, <see cref="FileVersionInfo"/> and the
    /// <see href="http://msdn.microsoft.com/en-us/library/windows/desktop/bb762179(v=vs.85).aspx">SHGetFileInfo</see>
    /// API method.
    /// </summary>
    [Serializable]
    public sealed class ExtendedFileInfo : MarshalByRefObject, ISerializable
    {
        #region fields
        private ExecutableType executableType;

        private FileInfo fileInfo;
        private string fileOwner;
        private FileVersionInfo fileVersionInfo;
        private string originalFileName;
        private SHFILEINFO shellFileInfo;

        #endregion

        #region constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedFileInfo"/> class, which acts as a wrapper for a file path.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the new file, or the relative file name.</param>
        /// <exception cref="ArgumentNullException"><paramref name="fileName"/> is a <see langword="null" />. </exception>
        /// <exception cref="SecurityException">The caller does not have the required permission.</exception>
        /// <exception cref="ArgumentException">The file name is empty, contains only white spaces, or contains invalid characters. </exception>
        /// <exception cref="UnauthorizedAccessException">Access to <paramref name="fileName"/> is denied.</exception>
        /// <exception cref="PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
        /// <exception cref="NotSupportedException"><paramref name="fileName"/> contains a colon (:) in the middle of the string.</exception>
        /// <remarks>You can specify either the fully qualified or the relative file name, but the security check gets the fully qualified name.</remarks>
        public ExtendedFileInfo(string fileName)
        {
            this.Initialize(fileName);
        } 

        private ExtendedFileInfo(SerializationInfo info, StreamingContext context)
        {
            Contracts.Requires.NotNull(info, "info");

            this.Initialize(info.GetString("originalFileName"));
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
                return this.fileInfo.Attributes;
            }

            set
            {
                this.fileInfo.Attributes = value;
            }
        } 
        #endregion

        #region Comments
        /// <summary>
        /// Gets the comments associated with the file. 
        /// </summary>
        /// <value>The comments associated with the file or a <see langword="null" /> if the file did not contain version information.</value>
        /// <remarks>This property contains additional information that can be displayed for diagnostic purposes.</remarks>
        public string Comments
        {
            get
            {
                return this.fileVersionInfo.Comments;
            }
        } 
        #endregion

        #region CompanyName
        /// <summary>
        /// Gets the name of the company that produced the file. 
        /// </summary>
        /// <value>The name of the company that produced the file or a <see langword="null" /> if the file did not contain version information.</value>
        public string CompanyName
        {
            get
            {
                return this.fileVersionInfo.CompanyName;
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
                return this.fileInfo.CreationTime;
            }

            set
            {
                this.fileInfo.CreationTime = value;
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
                return this.fileInfo.CreationTimeUtc;
            }
        } 
        #endregion

        #region Directory
        /// <summary>
        /// Gets an instance of the parent directory.
        /// </summary>
        /// <value>A <see cref="DirectoryInfo"/> object representing the
        /// parent directory of this file.</value>
        /// <exception cref="DirectoryNotFoundException">The specified path is
        /// invalid, such as being on an unmapped drive.</exception>
        /// <exception cref="SecurityException">The caller does not have the
        /// required permission.</exception>
        /// <remarks><para>To get the parent directory as a string, use the
        /// <see cref="DirectoryName"/> property.</para></remarks>
        public DirectoryInfo Directory
        {
            get
            {
                return this.fileInfo.Directory;
            }
        } 
        #endregion

        #region DirectoryName
        /// <summary>
        /// Gets a string representing the directory's full path.
        /// </summary>
        /// <value>A string representing the directory's full path.</value>
        /// <exception cref="SecurityException">The caller does not have the
        /// required permission.</exception>
        /// <exception cref="ArgumentNullException">A <see langword="null"/>
        /// was passed in for the directory name.</exception>
        /// <remarks><para>To get the parent directory as a 
        /// <see cref="DirectoryInfo"/> object, use the 
        /// <see cref="Directory"/> property.</para>
        /// <para>When first called, <see cref="ExtendedFileInfo"/> calls
        /// <see cref="Refresh"/> and caches information on the file. On
        /// subsequent calls, you must call <see cref="Refresh"/> to get the
        /// latest copy of the information.</para></remarks>
        public string DirectoryName
        {
            get
            {
                return this.fileInfo.DirectoryName;
            }
        } 
        #endregion

        #region ExecutableType
        /// <summary>
        /// Gets the type of executable that this instance of FileVersionInfo describes.
        /// </summary>
        /// <value>The type of executable of the file described by this instance of FileVersionInfo.</value>
        public ExecutableType ExecutableType
        {
            get
            {
                return this.executableType;
            }
        } 
        #endregion

        #region Exists
        /// <summary>
        /// Gets a value indicating whether a file exists.
        /// </summary>
        /// <value><see langword="true"/> if the file exists;
        /// <see langword="false"/> if the file does not exist or the file
        /// is a directory.</value>
        /// <remarks><para>When first called, <see cref="ExtendedFileInfo"/>
        /// calls <see cref="Refresh"/> and caches information on the file. On
        /// subsequent calls, you must call <see cref="Refresh"/> to get the
        /// latest copy of the information.</para></remarks>
        public bool Exists
        {
            get
            {
                return this.fileInfo.Exists;
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
                return this.fileInfo.Extension;
            }
        } 
        #endregion

        #region FileBuildPart
        /// <summary>
        /// Gets the build number of the file. 
        /// </summary>
        /// <value>A value representing the build number of the file or a <see langword="null" /> if the file did not contain version information.</value>
        /// <remarks><para>Typically, a version number is displayed as "major number.minor number.build number.private part number". A file version number is a 64-bit number that holds the version number for a file as follows:</para>
        /// <list type="bullet">
        /// <item>The first 16 bits are the FileMajorPart number.</item>
        /// <item>The next 16 bits are the FileMinorPart number.</item>
        /// <item>The third set of 16 bits are the FileBuildPart number.</item>
        /// <item>The last 16 bits are the FilePrivatePart number.</item>
        /// </list>
        /// <para>This property gets the third set of 16 bits.</para>
        /// </remarks>
        public int FileBuildPart
        {
            get
            {
                return this.fileVersionInfo.FileBuildPart;
            }
        }
        #endregion

        #region FileDescription
        /// <summary>
        /// Gets the description of the file.
        /// </summary>
        /// <value>The description of the file or a <see langword="null" /> if the file did not contain version information.</value>
        public string FileDescription
        {
            get
            {
                return this.fileVersionInfo.FileDescription;
            }
        } 
        #endregion

        #region FileMajorPart
        /// <summary>
        /// Gets the major part of the version number. 
        /// </summary>
        /// <value>A value representing the major part of the version number or a <see langword="null" /> if the file did not contain version information.</value>
        /// <remarks><para>Typically, a version number is displayed as "major number.minor number.build number.private part number". A file version number is a 64-bit number that holds the version number for a file as follows:</para>
        /// <list type="bullet">
        /// <item>The first 16 bits are the FileMajorPart number.</item>
        /// <item>The next 16 bits are the FileMinorPart number.</item>
        /// <item>The third set of 16 bits are the FileBuildPart number.</item>
        /// <item>The last 16 bits are the FilePrivatePart number.</item>
        /// </list>
        /// <para>This property gets the first set of 16 bits.</para>
        /// </remarks>
        public int FileMajorPart
        {
            get
            {
                return this.fileVersionInfo.FileMajorPart;
            }
        } 
        #endregion

        #region FileMinorPart
        /// <summary>
        /// Gets the minor part of the version number. 
        /// </summary>
        /// <value>A value representing the minor part of the version number or a <see langword="null" /> if the file did not contain version information.</value>
        /// <remarks><para>Typically, a version number is displayed as "major number.minor number.build number.private part number". A file version number is a 64-bit number that holds the version number for a file as follows:</para>
        /// <list type="bullet">
        /// <item>The first 16 bits are the FileMajorPart number.</item>
        /// <item>The next 16 bits are the FileMinorPart number.</item>
        /// <item>The third set of 16 bits are the FileBuildPart number.</item>
        /// <item>The last 16 bits are the FilePrivatePart number.</item>
        /// </list>
        /// <para>This property gets the second set of 16 bits.</para>
        /// </remarks>
        public int FileMinorPart
        {
            get
            {
                return this.fileVersionInfo.FileMinorPart;
            }
        } 
        #endregion

        #region FileName
        /// <summary>
        /// Gets the name of the file that this instance of FileVersionInfo describes.
        /// </summary>
        /// <value>The name of the file described by this instance of FileVersionInfo.</value>
        public string FileName
        {
            get
            {
                return this.fileVersionInfo.FileName;
            }
        } 
        #endregion

        #region FileOwner
        /// <summary>
        /// Gets the Windows owner associated with the file.
        /// </summary>
        /// <value>A string representing the owner of the file or <see langword="null"/> if the owner cannot be determined.</value>
        public string FileOwner
        {
            get
            {
                return this.fileOwner;
            }
        } 
        #endregion

        #region FilePrivatePart
        /// <summary>
        /// Gets the private part number. 
        /// </summary>
        /// <value>A value representing the file private part number or a <see langword="null" /> if the file did not contain version information.</value>
        /// <remarks><para>Typically, a version number is displayed as "major number.minor number.build number.private part number". A file version number is a 64-bit number that holds the version number for a file as follows:</para>
        /// <list type="bullet">
        /// <item>The first 16 bits are the FileMajorPart number.</item>
        /// <item>The next 16 bits are the FileMinorPart number.</item>
        /// <item>The third set of 16 bits are the FileBuildPart number.</item>
        /// <item>The last 16 bits are the FilePrivatePart number.</item>
        /// </list>
        /// <para>This property gets the last set of 16 bits.</para>
        /// </remarks>
        public int FilePrivatePart
        {
            get
            {
                return this.fileVersionInfo.FilePrivatePart;
            }
        } 
        #endregion

        #region FileType
        /// <summary>
        /// Gets the type of file.
        /// </summary>
        /// <value>The type of the file as displayed by the Shell.</value>
        public string FileType
        {
            get
            {
                return this.shellFileInfo.szTypeName;
            }
        } 
        #endregion
        
        #region FileVersion
        /// <summary>
        /// Gets the file version number.
        /// </summary>
        /// <value>The version number of the file or a <see langword="null" /> if the file did not contain version information.</value>
        /// <remarks><para>Typically, a version number is displayed as "major number.minor number.build number.private part number". A file version number is a 64-bit number that holds the version number for a file as follows:</para>
        /// <list type="bullet">
        /// <item>The first 16 bits are the FileMajorPart number.</item>
        /// <item>The next 16 bits are the FileMinorPart number.</item>
        /// <item>The third set of 16 bits are the FileBuildPart number.</item>
        /// <item>The last 16 bits are the FilePrivatePart number.</item>
        /// </list>
        /// </remarks>
        public string FileVersion
        {
            get
            {
                return this.fileVersionInfo.FileVersion;
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
                return this.fileInfo.FullName;
            }
        } 
        #endregion

        #region InternalName
        /// <summary>
        /// Gets the internal name of the file, if one exists. 
        /// </summary>
        /// <value>The internal name of the file. If none exists, this property will contain the original name of the file without the extension.</value>
        public string InternalName
        {
            get
            {
                return this.fileVersionInfo.InternalName;
            }
        } 
        #endregion

        #region IsDebug
        /// <summary>
        /// Gets a value indicating whether the file contains debugging information or is compiled with debugging features enabled.
        /// </summary>
        /// <value><see langword="true"/> if the file contains debugging information or is compiled with debugging features enabled; otherwise, <see langword="false"/>.</value>
        /// <remarks>
        /// <para>The FileVersionInfo properties are based on version resource information built into the file. Version resources are often built into binary files such as .exe or .dll files; text files do not have version resource information.</para>
        /// <para>Version resources are typically specified in a Win32 resource file, or in assembly attributes. The IsDebug property reflects the VS_FF_DEBUG flag value in the file's VS_FIXEDFILEINFO block, which is built from the VERSIONINFO resource in a Win32 resource file. For more information about specifying version resources in a Win32 resource file, see the Platform SDK About Resource Files topic and VERSIONINFO Resource topic topics.</para>
        /// </remarks>
        public bool IsDebug
        {
            get
            {
                return this.fileVersionInfo.IsDebug;
            }
        } 
        #endregion

        #region IsPatched
        /// <summary>
        /// Gets a value indicating whether the file has been modified and is not identical to the original shipping file of the same version number.
        /// </summary>
        /// <value><see langword="true"/> if the file is patched; otherwise, <see langword="false"/>.</value>
        public bool IsPatched
        {
            get
            {
                return this.fileVersionInfo.IsPatched;
            }
        } 
        #endregion

        #region IsPreRelease
        /// <summary>
        /// Gets a value indicating whether the file is a development version, rather than a commercially released product.
        /// </summary>
        /// <value><see langword="true"/> if the file is prerelease; otherwise, <see langword="false"/>.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "PreRelease", Justification = "This property follows the same naming convention as the underlying property in the FileVersionInfo class.")]
        public bool IsPreRelease
        {
            get
            {
                return this.fileVersionInfo.IsPreRelease;
            }
        } 
        #endregion

        #region IsPrivateBuild
        /// <summary>
        /// Gets a value indicating whether the file was built using standard release procedures.
        /// </summary>
        /// <value><see langword="true"/> if the file is a private build; <see langword="false"/> if the file was built using standard release procedures or if the file did not contain version information.</value>
        /// <remarks>If this value is true, PrivateBuild will describe how this version of the file differs from the standard version.</remarks>
        public bool IsPrivateBuild
        {
            get
            {
                return this.fileVersionInfo.IsPrivateBuild;
            }
        } 
        #endregion

        #region IsReadOnly
        /// <summary>
        /// Gets or sets a value indicating whether the file is read only.
        /// </summary>
        /// <value><see langword="true"/> if the current file is read only;
        /// otherwise <see langword="false"/>.</value>
        /// <exception cref="FileNotFoundException">The file described by the
        /// current <see cref="ExtendedFileInfo"/> object could not be found.
        /// </exception>
        /// <exception cref="IOException">An I/O error occurred while opening
        /// the file.</exception>
        /// <exception cref="UnauthorizedAccessException">
        /// <para>The file described by the current 
        /// <see cref="ExtendedFileInfo"/> object could not be found.</para>
        /// <para>-or-</para>
        /// <para>This operation is not supported on the current paltform.
        /// </para>
        /// <para>-or-</para>
        /// <para>THe called does not have the required permissions.</para>
        /// </exception>
        /// <remarsk><para>Use the <see cref="IsReadOnly"/> property to quickly
        /// determine or change whether the current file is read only.</para>
        /// <para>When first called, <see cref="ExtendedFileInfo"/>
        /// calls <see cref="Refresh"/> and caches information on the file. On
        /// subsequent calls, you must call <see cref="Refresh"/> to get the
        /// latest copy of the information.</para>
        /// </remarsk>
        public bool IsReadOnly
        {
            get
            {
                return this.fileInfo.IsReadOnly;
            }

            set
            {
                this.fileInfo.IsReadOnly = value;
            }
        } 
        #endregion

        #region IsSpecialBuild
        /// <summary>
        /// Gets a value indicating whether the file is a special build. 
        /// </summary>
        /// <value><see langword="true"/> if the file is a special build; otherwise, <see langword="false"/>.</value>
        /// <remarks>A file that is a special build was built using standard release procedures, but the file differs from a standard file of the same version number. If this value is true, the SpecialBuild property must specify how this file differs from the standard version.</remarks>
        public bool IsSpecialBuild
        {
            get
            {
                return this.fileVersionInfo.IsSpecialBuild;
            }
        } 
        #endregion

        #region Language
        /// <summary>
        /// Gets the default language string for the version info block.
        /// </summary>
        /// <value>The description string for the Microsoft Language Identifier in the version resource or a <see langword="null" /> if the file did not contain version information.</value>
        public string Language
        {
            get
            {
                return this.fileVersionInfo.Language;
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
                return this.fileInfo.LastAccessTime;
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
                return this.fileInfo.LastAccessTimeUtc;
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
                return this.fileInfo.LastWriteTime;
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
                return this.fileInfo.LastWriteTimeUtc;
            }
        } 
        #endregion

        #region LegalCopyright
        /// <summary>
        /// Gets all copyright notices that apply to the specified file. 
        /// </summary>
        /// <value>The copyright notices that apply to the specified file.</value>
        /// <remarks>This should include the full text of all notices, legal symbols, copyright dates, and so on or a <see langword="null" /> if the file did not contain version information.</remarks>
        public string LegalCopyright
        {
            get
            {
                return this.fileVersionInfo.LegalCopyright;
            }
        } 
        #endregion

        #region LegalTrademarks
        /// <summary>
        /// Gets the trademarks and registered trademarks that apply to the file. 
        /// </summary>
        /// <value>The trademarks and registered trademarks that apply to the file or a <see langword="null" /> if the file did not contain version information.</value>
        /// <remarks>The legal trademarks include the full text of all notices, legal symbols, and trademark numbers.</remarks>
        public string LegalTrademarks
        {
            get
            {
                return this.fileVersionInfo.LegalTrademarks;
            }
        } 
        #endregion

        #region Length
        /// <summary>
        /// Gets the size of the current file.
        /// </summary>
        /// <value>The size of the current file.</value>
        /// <exception cref="IOException"><see cref="Refresh"/> cannot update
        /// the state of the file or directory.</exception>
        /// <exception cref="FileNotFoundException"><para>The file does not
        /// exist.</para>
        /// <para>-or-</para>
        /// <para>The <see cref="Length"/> property is called for a 
        /// directory.</para>
        /// </exception>
        /// <remarsk><para>This property value is a <see langword="null"/>
        /// if the file system containing the file does not support this
        /// information.</para>
        /// <para>When first called, <see cref="ExtendedFileInfo"/>
        /// calls <see cref="Refresh"/> and caches information on the file. On
        /// subsequent calls, you must call <see cref="Refresh"/> to get the
        /// latest copy of the information.</para>
        /// </remarsk>
        public long Length
        {
            get
            {
                return this.fileInfo.Length;
            }
        } 
        #endregion

        #region Name
        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        /// <value>The name of the file.</value>
        /// <remarsk>
        /// <para>When first called, <see cref="ExtendedFileInfo"/>
        /// calls <see cref="Refresh"/> and caches information on the file. On
        /// subsequent calls, you must call <see cref="Refresh"/> to get the
        /// latest copy of the information.</para>
        /// </remarsk>
        public string Name
        {
            get
            {
                return this.fileInfo.Name;
            }
        } 
        #endregion

        #region OriginalFilename
        /// <summary>
        /// Gets the name the file was created with.
        /// </summary>
        /// <value>The name the file was created with or a <see langword="null" /> if the file did not contain version information.</value>
        /// <remarks>This property enables an application to determine whether a file has been renamed.</remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "Filename", Justification = "This property follows the same naming convention as the underlying property in the FileVersionInfo class.")]
        public string OriginalFilename
        {
            get
            {
                return this.fileVersionInfo.OriginalFilename;
            }
        } 
        #endregion

        #region PrivateBuild
        /// <summary>
        /// Gets information about a private version of the file.
        /// </summary>
        /// <value>Information about a private version of the file or a <see langword="null" /> if the file did not contain version information.</value>
        /// <remarks>This information is present when IsPrivateBuild is true.</remarks>
        public string PrivateBuild
        {
            get
            {
                return this.fileVersionInfo.PrivateBuild;
            }
        } 
        #endregion

        #region ProductBuildPart
        /// <summary>
        /// Gets the build number of the product this file is associated with.
        /// </summary>
        /// <value>A value representing the build number of the product this file is associated with or a <see langword="null" /> if the file did not contain version information.</value>
        /// <remarks><para>Typically, a version number is displayed as "major number.minor number.build number.private part number". A product version number is a 64-bit number that holds the version number for a file as follows:</para>
        /// <list type="bullet">
        /// <item>The first 16 bits are the ProductMajorPart number.</item>
        /// <item>The next 16 bits are the ProductMinorPart number.</item>
        /// <item>The third set of 16 bits are the ProductBuildPart number.</item>
        /// <item>The last 16 bits are the ProductPrivatePart number.</item>
        /// </list>
        /// <para>This property gets the third set of 16 bits.</para>
        /// </remarks>
        public int ProductBuildPart
        {
            get
            {
                return this.fileVersionInfo.ProductBuildPart;
            }
        } 
        #endregion

        #region ProductMajorPart
        /// <summary>
        /// Gets the major part of the version number for the product this file is associated with.
        /// </summary>
        /// <value>A value representing the major part of the product version number or a <see langword="null" /> if the file did not contain version information.</value>
        /// <remarks><para>Typically, a version number is displayed as "major number.minor number.build number.private part number". A product version number is a 64-bit number that holds the version number for a file as follows:</para>
        /// <list type="bullet">
        /// <item>The first 16 bits are the ProductMajorPart number.</item>
        /// <item>The next 16 bits are the ProductMinorPart number.</item>
        /// <item>The third set of 16 bits are the ProductBuildPart number.</item>
        /// <item>The last 16 bits are the ProductPrivatePart number.</item>
        /// </list>
        /// <para>This property gets the first set of 16 bits.</para>
        /// </remarks>
        public int ProductMajorPart
        {
            get
            {
                return this.fileVersionInfo.ProductMajorPart;
            }
        } 
        #endregion

        #region ProductMinorPart
        /// <summary>
        /// Gets the minor part of the version number for the product this file is associated with.
        /// </summary>
        /// <value>A value representing the minor part of the product version number or a <see langword="null" /> if the file did not contain version information.</value>
        /// <remarks><para>Typically, a version number is displayed as "major number.minor number.build number.private part number". A product version number is a 64-bit number that holds the version number for a file as follows:</para>
        /// <list type="bullet">
        /// <item>The first 16 bits are the ProductMajorPart number.</item>
        /// <item>The next 16 bits are the ProductMinorPart number.</item>
        /// <item>The third set of 16 bits are the ProductBuildPart number.</item>
        /// <item>The last 16 bits are the ProductPrivatePart number.</item>
        /// </list>
        /// <para>This property gets the second set of 16 bits.</para>
        /// </remarks>
        public int ProductMinorPart
        {
            get
            {
                return this.fileVersionInfo.ProductMinorPart;
            }
        } 
        #endregion

        #region ProductName
        /// <summary>
        /// Gets the name of the product this file is distributed with.
        /// </summary>
        /// <value>The name of the product this file is distributed with or a <see langword="null" /> if the file did not contain version information.</value>
        public string ProductName
        {
            get
            {
                return this.fileVersionInfo.ProductName;
            }
        } 
        #endregion

        #region ProductPrivatePart
        /// <summary>
        /// Gets the private part number of the product this file is associated with..
        /// </summary>
        /// <value>A value representing the private part number of the product this file is associated with or a <see langword="null" /> if the file did not contain version information.</value>
        /// <remarks><para>Typically, a version number is displayed as "major number.minor number.build number.private part number". A product version number is a 64-bit number that holds the version number for a file as follows:</para>
        /// <list type="bullet">
        /// <item>The first 16 bits are the ProductMajorPart number.</item>
        /// <item>The next 16 bits are the ProductMinorPart number.</item>
        /// <item>The third set of 16 bits are the ProductBuildPart number.</item>
        /// <item>The last 16 bits are the ProductPrivatePart number.</item>
        /// </list>
        /// <para>This property gets the last set of 16 bits.</para>
        /// </remarks>
        public int ProductPrivatePart
        {
            get
            {
                return this.fileVersionInfo.ProductPrivatePart;
            }
        } 
        #endregion

        #region ProductVersion
        /// <summary>
        /// Gets the version of the product this file is distributed with.
        /// </summary>
        /// <value>The version of the product this file is distributed with or a <see langword="null" /> if the file did not contain version information.</value>
        /// <remarks><para>Typically, a version number is displayed as "major number.minor number.build number.private part number". A product version number is a 64-bit number that holds the version number for a file as follows:</para>
        /// <list type="bullet">
        /// <item>The first 16 bits are the ProductMajorPart number.</item>
        /// <item>The next 16 bits are the ProductMinorPart number.</item>
        /// <item>The third set of 16 bits are the ProductBuildPart number.</item>
        /// <item>The last 16 bits are the ProductPrivatePart number.</item>
        /// </list>
        /// </remarks>
        public string ProductVersion
        {
            get
            {
                return this.fileVersionInfo.ProductVersion;
            }
        } 
        #endregion

        #region SpecialBuild
        /// <summary>
        /// Gets the special build information for the file. 
        /// </summary>
        /// <value>The special build information for the file or a <see langword="null" /> if the file did not contain version information.</value>
        /// <remarks>If IsSpecialBuild is true, SpecialBuild must specify how this file differs from the standard version of the file.</remarks>
        public string SpecialBuild
        {
            get
            {
                return this.fileVersionInfo.SpecialBuild;
            }
        } 
        #endregion

        #endregion

        #region methods

        #region AppendText
        /// <summary>
        /// Creates a <see cref="StreamWriter"/> that appends text to the file
        /// represented by this instance of the <see cref="ExtendedFileInfo"/>.
        /// </summary>
        /// <returns>A new <see cref="StreamWriter"/>.</returns>
        public StreamWriter AppendText()
        {
            return this.fileInfo.AppendText();
        } 
        #endregion

        #region CopyTo

        #region CopyTo(string destinationFileName)
        /// <summary>
        /// Copies an existing file to a new file, disallowing the overwriting
        /// of an existing file.
        /// </summary>
        /// <param name="destinationFileName">The name of the new file to copy to.
        /// </param>
        /// <returns>A new file with a fully qualified path.</returns>
        /// <exception cref="ArgumentException"><paramref name="destinationFileName"/>
        /// is empty, contains only white space, or contains invalid
        /// characters.</exception>
        /// <exception cref="IOException">An error occurs, or the destination
        /// file already exists.</exception>
        /// <exception cref="SecurityException">The caller does not have the
        /// required permission.</exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="destinationFileName"/> is a <see langword="null"/>.
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">A directory path is
        /// passed in, or the file is being moved to a differnt drive.
        /// </exception>
        /// <exception cref="DirectoryNotFoundException">The directory
        /// specified in <paramref name="destinationFileName"/> does not exist.
        /// </exception>
        /// <exception cref="PathTooLongException">The specified path, file
        /// name, or both exceeded the system-defined maximum length. For 
        /// example, on Windows-based platforms, paths must be less than 248
        /// characters, and file names must be less than 260 characters.
        /// </exception>
        /// <remarks><para>Use the <see cref="CopyTo(string, bool)"/> method to
        /// allow overwriting of an existing file.</para>
        /// <para type="caution">Whenever possible, avoid using short file
        /// names (such as XXXXXX~1.XXX) with this method. If two files have 
        /// equivalent short file names then this method may fail and raise an
        /// exception and/or result in undesirable behavior.</para>
        /// </remarks>
        public FileInfo CopyTo(string destinationFileName)
        {
            return this.fileInfo.CopyTo(destinationFileName);
        } 
        #endregion

        #region CopyTo(string destinationFileName, bool overwrite)
        /// <summary>
        /// Copies an existing file to a new file, allowing the overwriting
        /// of an existing file.
        /// </summary>
        /// <param name="destinationFileName">The name of the new file to copy to.
        /// </param>
        /// <param name="overwrite"><see langword="true"/> to allow an
        /// existing file to be overwritten; otherwise <see langword="false"/>.
        /// </param>
        /// <returns>A new file, or an overwrite of an existing file if
        /// <paramref name="overwrite"/> is <see langword="true"/>. If the file
        /// exists and <paramref name="overwrite"/> is <see langword="false"/>,
        /// an <see cref="IOException"/> is thrown.</returns>
        /// <exception cref="ArgumentException"><paramref name="destinationFileName"/>
        /// is empty, contains only white space, or contains invalid
        /// characters.</exception>
        /// <exception cref="IOException">An error occurs, or the destination
        /// file already exists and <paramref name="overwrite"/> is 
        /// <see langword="false"/>.</exception>
        /// <exception cref="SecurityException">The caller does not have the
        /// required permission.</exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="destinationFileName"/> is a <see langword="null"/>.
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">A directory path is
        /// passed in, or the file is being moved to a differnt drive.
        /// </exception>
        /// <exception cref="DirectoryNotFoundException">The directory
        /// specified in <paramref name="destinationFileName"/> does not exist.
        /// </exception>
        /// <exception cref="PathTooLongException">The specified path, file
        /// name, or both exceeded the system-defined maximum length. For 
        /// example, on Windows-based platforms, paths must be less than 248
        /// characters, and file names must be less than 260 characters.
        /// </exception>
        /// <exception cref="NotSupportedException">
        /// <paramref name="overwrite"/> contains a colon (:) in the middle
        /// of the string.</exception>
        /// <remarks><para>Use this method to allow or prevent overwriting of
        /// an existing file. Use the <see cref="CopyTo(string)"/> method to
        /// prevent overwriting of an existing file by default.</para>
        /// <para type="caution">Whenever possible, avoid using short file
        /// names (such as XXXXXX~1.XXX) with this method. If two files have 
        /// equivalent short file names then this method may fail and raise an
        /// exception and/or result in undesirable behavior.</para>
        /// </remarks>
        public FileInfo CopyTo(string destinationFileName, bool overwrite)
        {
            return this.fileInfo.CopyTo(destinationFileName, overwrite);
        }
        #endregion

        #endregion

        #region Create
        /// <summary>
        /// Creates a file.
        /// </summary>
        /// <returns>A new file.</returns>
        /// <remarks><para>By default, full read/write access to new files is
        /// granted to all users.</para>
        /// <para>This method is a wrapper for the functionality provided by
        /// <see cref="File.Create(string)"/>.</para></remarks>
        public FileStream Create()
        {
            return this.fileInfo.Create();
        } 
        #endregion

        #region Decrypt
        /// <summary>
        /// Decrypts a file that was encrypted by the current account using
        /// the <see cref="Encrypt"/> method.
        /// </summary>
        /// <exception cref="DriveNotFoundException">An invalid drive was
        /// specified.</exception>
        /// <exception cref="FileNotFoundException">THe file described by the
        /// current <see cref="ExtendedFileInfo"/> object could not be 
        /// found.</exception>
        /// <exception cref="IOException">An I/O error occurred while opening
        /// the file.</exception>
        /// <exception cref="NotSupportedException">The file system is not
        /// NTFS.</exception>
        /// <exception cref="PlatformNotSupportedException">The current
        /// operating system is not Microsoft Windows NT or later.</exception>
        /// <exception cref="UnauthorizedAccessException">
        /// <para>The file described by the current 
        /// <see cref="ExtendedFileInfo"/> object is read-only.</para>
        /// <para>-or-</para>
        /// <para>This operation is not supported on the current 
        /// platform.</para>
        /// <para>-or-</para>
        /// <para>The caller does not have the required permission.</para>
        /// </exception>
        /// <remarks><para>The <see cref="Decrypt"/> method allows you to
        /// decrypt a file that was encrypted using the <see cref="Encrypt"/>
        /// method. THe <see cref="Decrypt"/> method can decrypt only files
        /// that were encrypted using the current user account.</para>
        /// <para>Both the <see cref="Encrypt"/> and <see cref="Decrypt"/>
        /// method use the cryptographic service provider (CSP) installed on
        /// the computer and the file encryption keys of the process calling
        /// the method.</para>
        /// <para>The current file system must be formatted as NTFS and the
        /// current operating system must be Microsoft Windows NT or later.
        /// </para></remarks>
        public void Decrypt()
        {
            this.fileInfo.Decrypt();
        } 
        #endregion

        #region Delete
        /// <summary>
        /// Permanently deletes a file. 
        /// </summary>
        /// <exception cref="IOException">The target file is open or
        /// memory-mapped on a computer running Microsoft Windows NT.
        /// </exception>
        /// <exception cref="SecurityException">The caller does not have
        /// the required permission.</exception>
        /// <exception cref="UnauthorizedAccessException">The path is a
        /// directory.</exception>
        /// <remarks><para>If the file does not exist, this method does 
        /// nothing.</para>
        /// <para><b>Windows NT 4.0 Platform Note:</b><see cref="Delete"/>
        /// does not delete a file that is open for normal I/O or a file
        /// that is memory-mapped.</para></remarks>
        public void Delete()
        {
            this.fileInfo.Delete();
        } 
        #endregion

        #region Encrypt
        /// <summary>
        /// Encrypts a file so that only the account used to encrypt the file
        /// can decrypt it.
        /// </summary>
        /// <exception cref="DriveNotFoundException">An invalid drive was
        /// specified.</exception>
        /// <exception cref="FileNotFoundException">The file described by the
        /// current <see cref="ExtendedFileInfo"/> object could not be 
        /// found.</exception>
        /// <exception cref="IOException">An I/O error occurred while opening
        /// the file.</exception>
        /// <exception cref="NotSupportedException">The file system is not
        /// NTFS.</exception>
        /// <exception cref="PlatformNotSupportedException">The current
        /// operating system is not Microsoft Windows NT or later.</exception>
        /// <exception cref="UnauthorizedAccessException">
        /// <para>The file described by the current 
        /// <see cref="ExtendedFileInfo"/> object is read-only.</para>
        /// <para>-or-</para>
        /// <para>This operation is not supported on the current 
        /// platform.</para>
        /// <para>-or-</para>
        /// <para>The caller does not have the required permission.</para>
        /// </exception>
        /// <remarks><para>The <see cref="Encrypt"/> method allows you to
        /// encrypt a file so that only the current account used to call this
        /// method can decrypt it. Use the <see cref="Decrypt"/> method to
        /// decrypt a file encrypted by the <see cref="Encrypt"/> method.</para>
        /// <para>Both the <see cref="Encrypt"/> and <see cref="Decrypt"/>
        /// method use the cryptographic service provider (CSP) installed on
        /// the computer and the file encryption keys of the process calling
        /// the method.</para>
        /// <para>The current file system must be formatted as NTFS and the
        /// current operating system must be Microsoft Windows NT or later.
        /// </para></remarks>
        public void Encrypt()
        {
            this.fileInfo.Encrypt();
        } 
        #endregion

        #region GetAccessControl

        #region GetAccessControl()
        /// <summary>
        /// Gets a <see cref="FileSecurity"/> object that encapsulates the
        /// access control list (ACL) entries for the file described by the
        /// current <see cref="ExtendedFileInfo"/> object.
        /// </summary>
        /// <returns>A <see cref="FileSecurity"/> object that encapsulates the
        /// access control rules for the current file.</returns>
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
        /// <remarks><para>Use the <see cref="GetAccessControl()"/> method to
        /// retrieve the access control list (ACL) entries for the current
        /// file.</para>
        /// <para>An ACL describes individuals and/or groups who have, or do
        /// not have, rights to specific actions on the given file.</para>
        /// </remarks>
        public FileSecurity GetAccessControl()
        {
            return this.fileInfo.GetAccessControl();
        } 
        #endregion

        #region GetAccessControl(AccessControlSections includeSections)
        /// <summary>
        /// Gets a <see cref="FileSecurity"/> object that encapsulates the
        /// access control list (ACL) entries for the file described by the
        /// current <see cref="ExtendedFileInfo"/> object.
        /// </summary>
        /// <param name="includeSections">One of the 
        /// <see cref="AccessControlSections"/> values that specifies which
        /// group of access control entries to retrieve.</param>
        /// <returns>A <see cref="FileSecurity"/> object that encapsulates the
        /// access control rules for the current file.</returns>
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
        /// <remarks><para>Use the 
        /// <see cref="GetAccessControl(AccessControlSections)"/> method to
        /// retrieve the access control list (ACL) entries for the current
        /// file.</para>
        /// <para>An ACL describes individuals and/or groups who have, or do
        /// not have, rights to specific actions on the given file.</para>
        /// </remarks>
        public FileSecurity GetAccessControl(AccessControlSections includeSections)
        {
            return this.fileInfo.GetAccessControl(includeSections);
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2103:ReviewImperativeSecurity", Justification = "This security demand cannot be declaritve as the path is not known until runtime. The value of originalFileName cannot change once the class is instantiated, so there is no risk that the value will change while the demand is in effect.")]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            Contracts.Requires.NotNull(info, "info");

            new FileIOPermission(FileIOPermissionAccess.PathDiscovery, this.originalFileName).Demand();

            info.AddValue("originalFileName", this.originalFileName, typeof(String));
        } 
        #endregion

        #region MoveTo
        /// <summary>
        /// Moves a specified file to a new location, providing the option to 
        /// specify a new file name. 
        /// </summary>
        /// <param name="destinationFileName">The path to move the file to, which can
        /// specify a different file name.</param>
        /// <exception cref="IOException">An I/O error occurs, such as the
        /// destination file already exists or the destination device is not
        /// ready.</exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="destinationFileName"/> is a <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException"><paramref name="destinationFileName"/>
        /// is empty, contains only white space, or contains invalid
        /// characters.</exception>
        /// <exception cref="SecurityException">The caller does not have the
        /// required permission.</exception>
        /// <exception cref="UnauthorizedAccessException">
        /// <paramref name="destinationFileName"/> is read-only or is a directory.
        /// </exception>
        /// <exception cref="FileNotFoundException">The file is not 
        /// found.</exception>
        /// <exception cref="DirectoryNotFoundException">The specified path is
        /// invalid, such as being on an unmapped drive.</exception>
        /// <exception cref="PathTooLongException">The specified path, file
        /// name, or both exceeded the system-defined maximum length. For 
        /// example, on Windows-based platforms, paths must be less than 248
        /// characters, and file names must be less than 260 characters.
        /// </exception>
        /// <exception cref="NotSupportedException">
        /// <paramref name="destinationFileName"/> contains a colon (:) in the middle
        /// of the string.</exception>
        /// <remarks><para>This method works across disk volumes. For example,
        /// the file C:\MyFile.txt can be moved to D:\public and renamed
        /// NewFile.txt.</para>
        /// <para><b>Windows Mobile for Pocket PC, Windows Mobile for
        /// Smartphone, Windows CE Platform Note:</b> Some device file systems
        /// do not support relative paths. Specify absolute path information.
        /// </para></remarks>
        public void MoveTo(string destinationFileName)
        {
            this.fileInfo.MoveTo(destinationFileName);
        } 
        #endregion

        #region Open

        #region Open(FileMode mode)
        /// <summary>
        /// Opens a file in the specified mode.
        /// </summary>
        /// <param name="mode">A FileMode constant specifying the mode (for example, Open or Append) in which to open the file.</param>
        /// <returns>A file opened in the specified mode, with read/write access and unshared.</returns>
        /// <exception cref="FileNotFoundException">The file is not found.</exception>
        /// <exception cref="UnauthorizedAccessException">The file is read-only or is a directory.</exception>
        /// <exception cref="DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
        /// <exception cref="IOException">The file is already open.</exception>
        public FileStream Open(FileMode mode)
        {
            return this.fileInfo.Open(mode);
        } 
        #endregion

        #region Open(FileMode mode, FileAccess access)
        /// <summary>
        /// Opens a file in the specified mode with read, write, or read/write access.
        /// </summary>
        /// <param name="mode">A FileMode constant specifying the mode (for example, Open or Append) in which to open the file.</param>
        /// <param name="access">A FileAccess constant specifying whether to open the file with Read, Write, or ReadWrite file access.</param>
        /// <returns>A FileStream object opened in the specified mode and access, and unshared.</returns>
        /// <exception cref="SecurityException">The caller does not have the required permission.</exception>
        /// <exception cref="ArgumentException">path is empty or contains only white spaces.</exception>
        /// <exception cref="FileNotFoundException">The file is not found.</exception>
        /// <exception cref="ArgumentNullException">One or more arguments is a <see langword="null"/>.</exception>
        /// <exception cref="UnauthorizedAccessException">path is read-only or is a directory.</exception>
        /// <exception cref="DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
        /// <exception cref="IOException">The file is already open.</exception>
        public FileStream Open(FileMode mode, FileAccess access)
        {
            return this.fileInfo.Open(mode, access);
        } 
        #endregion

        #region Open(FileMode mode, FileAccess access, FileShare share)
        /// <summary>
        /// Opens a file in the specified mode with read, write, or read/write access and the specified sharing option.
        /// </summary>
        /// <param name="mode">A FileMode constant specifying the mode (for example, Open or Append) in which to open the file.</param>
        /// <param name="access">A FileAccess constant specifying whether to open the file with Read, Write, or ReadWrite file access.</param>
        /// <param name="share">A FileShare constant specifying the type of access other FileStream objects have to this file. </param>
        /// <returns>A FileStream object opened with the specified mode, access, and sharing options.</returns>
        /// <exception cref="SecurityException">The caller does not have the required permission.</exception>
        /// <exception cref="ArgumentException">path is empty or contains only white spaces.</exception>
        /// <exception cref="FileNotFoundException">The file is not found.</exception>
        /// <exception cref="ArgumentNullException">One or more arguments is a <see langword="null"/>.</exception>
        /// <exception cref="UnauthorizedAccessException">path is read-only or is a directory.</exception>
        /// <exception cref="DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
        /// <exception cref="IOException">The file is already open.</exception>
        public FileStream Open(FileMode mode, FileAccess access, FileShare share)
        {
            return this.fileInfo.Open(mode, access, share);
        } 
        #endregion

        #endregion

        #region OpenRead
        /// <summary>
        /// Creates a read-only FileStream.
        /// </summary>
        /// <returns>A new read-only FileStream object.</returns>
        /// <exception cref="UnauthorizedAccessException">The path used to construct this <see cref="ExtendedFileInfo"/> instance is read-only or is a directory.</exception>
        /// <exception cref="DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
        /// <exception cref="IOException">The file is already open.</exception>
        /// <remarks>This method returns a read-only FileStream object with the FileShare mode set to Read.</remarks>
        public FileStream OpenRead()
        {
            return this.fileInfo.OpenRead();
        } 
        #endregion

        #region OpenText
        /// <summary>
        /// Creates a StreamReader with UTF8 encoding that reads from an existing text file.
        /// </summary>
        /// <returns>A new StreamReader with UTF8 encoding.</returns>
        /// <exception cref="SecurityException">The caller does not have the required permission.</exception>
        /// <exception cref="FileNotFoundException">The file is not found.</exception>
        /// <exception cref="UnauthorizedAccessException">path is read-only or is a directory.</exception>
        /// <exception cref="DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
        public StreamReader OpenText()
        {
            return this.fileInfo.OpenText();
        } 
        #endregion

        #region OpenWrite
        /// <summary>
        /// Creates a write-only FileStream. 
        /// </summary>
        /// <returns>A new write-only unshared FileStream object.</returns>
        /// <exception cref="UnauthorizedAccessException">The path used to construct this <see cref="ExtendedFileInfo"/> instance is read-only or is a directory.</exception>
        /// <exception cref="DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
        public FileStream OpenWrite()
        {
            return this.fileInfo.OpenWrite();
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
            this.fileInfo.Refresh();
        } 
        #endregion

        #region Replace

        #region Replace(string destinationFileName, string destinationBackupFileName)
        /// <summary>
        /// Replaces the contents of a specified file with the file described
        /// by the current FileInfo object, deleting the original file, and
        /// creating a backup of the replaced file.
        /// </summary>
        /// <param name="destinationFileName">The name of a file to replace with the current file.</param>
        /// <param name="destinationBackupFileName">The name of a file with which to create a backup of the file described by the <paramref name="destinationFileName"/> parameter.</param>
        /// <returns>A FileInfo object that encapsulates information about the file described by the <paramref name="destinationFileName"/> parameter.</returns>
        /// <exception cref="ArgumentException">
        /// <para>The path described by the <paramref name="destinationFileName"/> parameter was not of a legal form.</para>
        /// <para>-or-</para>
        /// <para>The path described by the <paramref name="destinationBackupFileName"/> parameter was not of a legal form.</para>
        /// </exception>
        /// <exception cref="ArgumentNullException">The <paramref name="destinationFileName"/> parameter is a <see langword="null" />.</exception>
        /// <exception cref="FileNotFoundException">
        /// <para>The file described by the current FileInfo object could not be found.</para>
        /// <para>-or-</para>
        /// <para>The file described by the <paramref name="destinationFileName"/> parameter could not be found.</para>
        /// </exception>
        /// <exception cref="PlatformNotSupportedException">The current operating system is not Microsoft Windows NT or later.</exception>
        /// <remarks>
        /// <para>The Replace method replaces the contents of a specified file
        /// with the contents of the file described by the current FileInfo 
        /// object. It also creates a backup of the file that was replaced. 
        /// Finally, it returns a new FileInfo object that describes the
        /// overwritten file.</para>
        /// <para type="caution">This method will succeed in Windows 2000
        /// environments if the <paramref name="destinationFileName"/>
        /// is read-only and will not raise an exception. Use the 
        /// IsReadOnly property to check if the 
        /// destination file is read-only before attempting to replace it.</para>
        /// <para>Pass a <see langword="null"/> to the <paramref name="destinationBackupFileName"/>
        /// parameter if you do not want to create a backup of the file being replaced.</para>
        /// </remarks>
        public FileInfo Replace(string destinationFileName, string destinationBackupFileName)
        {
            return this.fileInfo.Replace(destinationFileName, destinationBackupFileName);
        } 
        #endregion

        #region Replace(string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors)
        /// <summary>
        /// Replaces the contents of a specified file with the file described by the current FileInfo object, deleting the original file, and creating a backup of the replaced file. Also specifies whether to ignore merge errors.
        /// </summary>
        /// <param name="destinationFileName">The name of a file to replace with the current file.</param>
        /// <param name="destinationBackupFileName">The name of a file with which to create a backup of the file described by the <paramref name="destinationFileName"/> parameter.</param>
        /// <param name="ignoreMetadataErrors"><see langword="true"/> to ignore merge errors (such as attributes and ACLs) from the replaced file to the replacement file; otherwise <see langword="false"/>.</param>
        /// <returns>A FileInfo object that encapsulates information about the file described by the <paramref name="destinationFileName"/> parameter.</returns>
        /// <exception cref="ArgumentException">
        /// <para>The path described by the <paramref name="destinationFileName"/> parameter was not of a legal form.</para>
        /// <para>-or-</para>
        /// <para>The path described by the <paramref name="destinationBackupFileName"/> parameter was not of a legal form.</para>
        /// </exception>
        /// <exception cref="ArgumentNullException">The <paramref name="destinationFileName"/> parameter is a <see langword="null" />.</exception>
        /// <exception cref="FileNotFoundException">
        /// <para>The file described by the current FileInfo object could not be found.</para>
        /// <para>-or-</para>
        /// <para>The file described by the <paramref name="destinationFileName"/> parameter could not be found.</para>
        /// </exception>
        /// <exception cref="PlatformNotSupportedException">The current operating system is not Microsoft Windows NT or later.</exception>
        /// <remarks>
        /// <para>The Replace method replaces the contents of a specified file with the contents of the file described by the current FileInfo object. It also creates a backup of the file that was replaced. Finally, it returns a new FileInfo object that describes the overwritten file.</para>
        /// <para type="caution">This method will succeed in Windows 2000 environments if the <paramref name="destinationFileName"/> is read-only and will not raise an exception. Use the IsReadOnly property to check if the destination file is read-only before attempting to replace it.</para>
        /// <para>Pass a <see langword="null" /> to the <paramref name="destinationBackupFileName"/> parameter if you do not want to create a backup of the file being replaced.</para>
        /// </remarks>
        public FileInfo Replace(string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors)
        {
            return this.fileInfo.Replace(destinationFileName, destinationBackupFileName, ignoreMetadataErrors);
        } 
        #endregion

        #endregion

        #region SetAccessControl
        /// <summary>
        /// Applies access control list (ACL) entries described by a FileSecurity object to the file described by the current FileInfo object. 
        /// </summary>
        /// <param name="fileSecurity">A FileSecurity object that describes an access control list (ACL) entry to apply to the current file.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="fileSecurity"/> parameter is <see langword="null"/>.</exception>
        /// <exception cref="SystemException">The file could not be found or modified.</exception>
        /// <exception cref="UnauthorizedAccessException">The current process does not have access to open the file.</exception>
        /// <exception cref="PlatformNotSupportedException">The current operating system is not Microsoft Windows 2000 or later.</exception>
        /// <remarks>
        /// <para>The SetAccessControl method applies access control list (ACL) entries to the current file that represents the noninherited ACL list.</para>
        /// <para>Use the SetAccessControl method whenever you need to add or remove ACL entries from a file.</para>
        /// <para type="caution">The ACL specified for the fileSecurity parameter replaces the existing ACL for the file. To add permissions for a new user, use the GetAccessControl method to obtain the existing ACL, modify it, and then use SetAccessControl to apply it back to the file.</para>
        /// <para>An ACL describes individuals and/or groups who have, or do not have, rights to specific actions on the given file.</para>
        /// <para>The SetAccessControl method persists only FileSecurity objects that have been modified after object creation.  If a FileSecurity object has not been modified, it will not be persisted to a file.  Therefore, it is not possible to retrieve a FileSecurity object from one file and reapply the same object to another file.</para>
        /// <para>To copy ACL information from one file to another:</para>
        /// <list type="numbered">
        /// <item>Use the GetAccessControl method to retrieve the FileSecurity object from the source file.</item>
        /// <item>Create a new FileSecurity object for the destination file.</item>
        /// <item>Use the GetSecurityDescriptorBinaryForm or GetSecurityDescriptorSddlForm method of the source FileSecurity object to retrieve the ACL information.</item>
        /// <item>Use the SetSecurityDescriptorBinaryForm or SetSecurityDescriptorSddlForm method to copy the information retrieved in step 3 to the destination FileSecurity object.</item>
        /// <item>Set the destination FileSecurity object to the destination file using the SetAccessControl method.</item>
        /// </list>
        /// </remarks>
        public void SetAccessControl(FileSecurity fileSecurity)
        {
            this.fileInfo.SetAccessControl(fileSecurity);
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
            return this.fileInfo.ToString();
        } 
        #endregion

        #region Initialize
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Reviewed.")]
        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        private void Initialize(string fileName)
        {
            Contracts.Requires.NotNull(fileName, "fileName");

            this.originalFileName = fileName;
            this.fileInfo = new FileInfo(fileName);

            string owner = null;
            this.executableType = ExecutableType.Unknown;

            if (this.fileInfo.Exists)
            {
                this.fileVersionInfo = FileVersionInfo.GetVersionInfo(fileName);

                // Try to fill the SHFILEINFO struct for the file type, if the returned pointer is 0 then an error occurred.
                IntPtr ptr = SafeNativeMethods.SHGetFileInfo(fileName, FileAttributes.Normal, ref this.shellFileInfo, Marshal.SizeOf(typeof(SHFILEINFO)), SHGFI.TYPENAME);
                if (ptr == IntPtr.Zero)
                {
                    throw new IOException();
                }

                Marshal.FreeCoTaskMem(ptr);

                // Try to fill the same SHFILEINFO struct for the exe type. The returned pointer contains the encoded 
                // executable type data.
                ptr = IntPtr.Zero;
                ptr = SafeNativeMethods.SHGetFileInfo(fileName, FileAttributes.Normal, ref this.shellFileInfo, Marshal.SizeOf(typeof(SHFILEINFO)), SHGFI.EXETYPE);

                // We need to split the returned pointer up into the high and low order words. These are important
                // because they help distinguish some of the types. The possible values are:
                //
                // Value                                            Meaning
                // ----------------------------------------------------------------------------------------------
                // 0                                                Nonexecutable file or an error condition. 
                // LOWORD = NE or PE and HIWORD = Windows version   Microsoft Windows application.
                // LOWORD = MZ and HIWORD = 0                       Windows 95, Windows 98: Microsoft MS-DOS .exe, .com, or .bat file
                //                                                  Microsoft Windows NT, Windows 2000, Windows XP: MS-DOS .exe or .com file 
                // LOWORD = PE and HIWORD = 0                       Windows 95, Windows 98: Microsoft Win32 console application 
                //                                                  Windows NT, Windows 2000, Windows XP: Win32 console application or .bat file 
                // MZ = 0x5A4D - DOS signature.
                // NE = 0x454E - OS/2 signature.
                // LE = 0x454C - OS/2 LE or VXD signature.
                // PE = 0x4550 - Win32/NT signature.
                int wparam = ptr.ToInt32();
                int loWord = wparam & 0xffff;
                int hiWord = wparam >> 16;

                if (wparam == 0)
                {
                    this.executableType = ExecutableType.Unknown;
                }
                else
                {
                    if (hiWord == 0x0000)
                    {
                        if (loWord == 0x5A4D)
                        {
                            // The file is an MS-DOS .exe, .com, or .bat
                            this.executableType = ExecutableType.DOS;
                        }
                        else if (loWord == 0x4550)
                        {
                            this.executableType = ExecutableType.Win32Console;
                        }
                    }
                    else
                    {
                        if (loWord == 0x454E || loWord == 0x4550)
                        {
                            this.executableType = ExecutableType.Windows;
                        }
                        else if (loWord == 0x454C)
                        {
                            this.executableType = ExecutableType.Windows;
                        }
                    }
                }

                FileSecurity fs = File.GetAccessControl(this.originalFileName, AccessControlSections.Owner);
                owner = fs.GetOwner(typeof(NTAccount)).ToString();
            }

            this.fileOwner = owner;
        }
        #endregion

        #endregion
    }
}
