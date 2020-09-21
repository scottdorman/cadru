//------------------------------------------------------------------------------
// <copyright file="ExtendedFileInfo.cs"
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
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.AccessControl;
using System.Security.Principal;

using Cadru.IO.Interop;

using Validation;

namespace Cadru.IO
{
    /// <summary>
    /// Provides an encapsulated implementation of the standard .NET
    /// <see cref="FileInfo"/>, <see cref="FileVersionInfo"/> and the
    /// <see href="http://msdn.microsoft.com/en-us/library/windows/desktop/bb762179(v=vs.85).aspx">SHGetFileInfo</see>
    /// API method.
    /// </summary>
    public sealed partial class ExtendedFileInfo
    {
        private readonly FileInfo fileInfo;
        private readonly FileVersionInfo? fileVersionInfo;
        private readonly string originalFileName;
        private SHFILEINFO shellFileInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedFileInfo"/>
        /// class, which acts as a wrapper for a file path.
        /// </summary>
        /// <param name="fileName">
        /// The fully qualified name of the new file, or the relative file name.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="fileName"/> is a <see langword="null"/>.
        /// </exception>
        /// <exception cref="SecurityException">
        /// The caller does not have the required permission.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The file name is empty, contains only white spaces, or contains
        /// invalid characters.
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        /// Access to <paramref name="fileName"/> is denied.
        /// </exception>
        /// <exception cref="PathTooLongException">
        /// The specified path, file name, or both exceed the system-defined
        /// maximum length. For example, on Windows-based platforms, paths must
        /// be less than 248 characters, and file names must be less than 260 characters.
        /// </exception>
        /// <exception cref="NotSupportedException">
        /// <paramref name="fileName"/> contains a colon (:) in the middle of
        /// the string.
        /// </exception>
        /// <remarks>
        /// You can specify either the fully qualified or the relative file
        /// name, but the security check gets the fully qualified name.
        /// </remarks>
        public ExtendedFileInfo(string fileName)
        {
            Requires.NotNull(fileName, "fileName");

            this.originalFileName = fileName;
            this.fileInfo = new FileInfo(fileName);

            this.ExecutableType = ExecutableType.Unknown;

            if (this.fileInfo.Exists)
            {
                this.fileVersionInfo = FileVersionInfo.GetVersionInfo(fileName);
                var fs = new FileSecurity(this.originalFileName, AccessControlSections.Owner);
                this.FileOwner = fs.GetOwner(typeof(NTAccount)).ToString();

                // Try to fill the SHFILEINFO struct for the file type, if the
                // returned pointer is 0 then an error occurred.
                var ptr = SafeNativeMethods.SHGetFileInfo(fileName, FileAttributes.Normal, ref this.shellFileInfo, Marshal.SizeOf<SHFILEINFO>(), SHGFI.TYPENAME);
                if (ptr == IntPtr.Zero)
                {
                    throw new IOException();
                }

                Marshal.FreeCoTaskMem(ptr);

                // Try to fill the same SHFILEINFO struct for the exe type. The
                // returned pointer contains the encoded executable type data.
                ptr = SafeNativeMethods.SHGetFileInfo(fileName, FileAttributes.Normal, ref this.shellFileInfo, Marshal.SizeOf<SHFILEINFO>(), SHGFI.EXETYPE);

                // We need to split the returned pointer up into the high and
                // low order words. These are important because they help
                // distinguish some of the types. The possible values are:
                //
                // Value Meaning
                // ----------------------------------------------------------------------------------------------
                // 0 Nonexecutable file or an error condition. LOWORD = NE or PE
                // and HIWORD = Windows version Microsoft Windows application.
                // LOWORD = MZ and HIWORD = 0 Windows 95, Windows 98: Microsoft
                // MS-DOS .exe, .com, or .bat file Microsoft Windows NT, Windows
                // 2000, Windows XP: MS-DOS .exe or .com file LOWORD = PE and
                // HIWORD = 0 Windows 95, Windows 98: Microsoft Win32 console
                // application Windows NT, Windows 2000, Windows XP: Win32
                // console application or .bat file MZ = 0x5A4D - DOS signature.
                // NE = 0x454E - OS/2 signature. LE = 0x454C - OS/2 LE or VXD
                // signature. PE = 0x4550 - Win32/NT signature.
                var wparam = ptr.ToInt32();
                var loWord = wparam & 0xffff;
                var hiWord = wparam >> 16;

                if (wparam == 0)
                {
                    this.ExecutableType = ExecutableType.Unknown;
                }
                else
                {
                    if (hiWord == 0x0000)
                    {
                        if (loWord == 0x5A4D)
                        {
                            // The file is an MS-DOS .exe, .com, or .bat
                            this.ExecutableType = ExecutableType.DOS;
                        }
                        else if (loWord == 0x4550)
                        {
                            this.ExecutableType = ExecutableType.Win32Console;
                        }
                    }
                    else
                    {
                        if (loWord == 0x454E || loWord == 0x4550)
                        {
                            this.ExecutableType = ExecutableType.Windows;
                        }
                        else if (loWord == 0x454C)
                        {
                            this.ExecutableType = ExecutableType.Windows;
                        }
                    }
                }
            }
        }

        /// <inheritdoc cref="FileSystemInfo.Attributes"/>
        public FileAttributes Attributes
        {
            get => this.fileInfo.Attributes;
            set => this.fileInfo.Attributes = value;
        }

        /// <inheritdoc cref="FileVersionInfo.Comments"/>
        public string? Comments => this.fileVersionInfo?.Comments;

        /// <inheritdoc cref="FileVersionInfo.CompanyName"/>
        public string? CompanyName => this.fileVersionInfo?.CompanyName;

        /// <inheritdoc cref="FileSystemInfo.CreationTimeUtc"/>
        public DateTime CreateTimeUtc => this.fileInfo.CreationTimeUtc;

        /// <inheritdoc cref="FileSystemInfo.CreationTime"/>
        public DateTime CreationTime
        {
            get => this.fileInfo.CreationTime;
            set => this.fileInfo.CreationTime = value;
        }

        /// <inheritdoc cref="FileInfo.Directory"/>
        public DirectoryInfo Directory => this.fileInfo.Directory;

        /// <inheritdoc cref="FileInfo.DirectoryName"/>
        public string DirectoryName => this.fileInfo.DirectoryName;

        /// <summary>
        /// Gets the type of executable that this instance of FileVersionInfo describes.
        /// </summary>
        /// <value>
        /// The type of executable of the file described by this instance of FileVersionInfo.
        /// </value>
        public ExecutableType ExecutableType { get; }

        /// <inheritdoc cref="FileInfo.Exists"/>
        public bool Exists => this.fileInfo.Exists;

        /// <inheritdoc cref="FileSystemInfo.Extension"/>
        public string Extension => this.fileInfo.Extension;

        /// <inheritdoc cref="FileVersionInfo.FileBuildPart"/>
        public int? FileBuildPart => this.fileVersionInfo?.FileBuildPart;

        /// <inheritdoc cref="FileVersionInfo.FileDescription"/>
        public string? FileDescription => this.fileVersionInfo?.FileDescription;

        /// <inheritdoc cref="FileVersionInfo.FileMajorPart"/>
        public int? FileMajorPart => this.fileVersionInfo?.FileMajorPart;

        /// <inheritdoc cref="FileVersionInfo.FileMinorPart"/>
        public int? FileMinorPart => this.fileVersionInfo?.FileMinorPart;

        /// <inheritdoc cref="FileVersionInfo.FileName"/>
        public string? FileName => this.fileVersionInfo?.FileName;

        /// <summary>
        /// Gets the Windows owner associated with the file.
        /// </summary>
        /// <value>
        /// A string representing the owner of the file or
        /// <see langword="null"/> if the owner cannot be determined.
        /// </value>
        public string? FileOwner { get; private set; }

        /// <inheritdoc cref="FileVersionInfo.FilePrivatePart"/>
        public int? FilePrivatePart => this.fileVersionInfo?.FilePrivatePart;

        /// <summary>
        /// Gets the type of file.
        /// </summary>
        /// <value>The type of the file as displayed by the Shell.</value>
        public string FileType => this.shellFileInfo.szTypeName;

        /// <inheritdoc cref="FileVersionInfo.FileVersion"/>
        public string? FileVersion => this.fileVersionInfo?.FileVersion;

        /// <inheritdoc cref="FileSystemInfo.FullName"/>
        public string? FullName => this.fileInfo?.FullName;

        /// <inheritdoc cref="FileVersionInfo.InternalName"/>
        public string? InternalName => this.fileVersionInfo?.InternalName;

        /// <inheritdoc cref="FileVersionInfo.IsDebug"/>
        public bool? IsDebug => this.fileVersionInfo?.IsDebug;

        /// <inheritdoc cref="FileVersionInfo.IsPatched"/>
        public bool? IsPatched => this.fileVersionInfo?.IsPatched;

        /// <inheritdoc cref="FileVersionInfo.IsPreRelease"/>
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "PreRelease", Justification = "This property follows the same naming convention as the underlying property in the FileVersionInfo class.")]
        public bool? IsPreRelease => this.fileVersionInfo?.IsPreRelease;

        /// <inheritdoc cref="FileVersionInfo.IsPrivateBuild"/>
        public bool? IsPrivateBuild => this.fileVersionInfo?.IsPrivateBuild;

        /// <inheritdoc cref="FileInfo.IsReadOnly"/>

        public bool IsReadOnly
        {
            get => this.fileInfo.IsReadOnly;
            set => this.fileInfo.IsReadOnly = value;
        }

        /// <inheritdoc cref="FileVersionInfo.IsSpecialBuild"/>
        public bool? IsSpecialBuild => this.fileVersionInfo?.IsSpecialBuild;

        /// <inheritdoc cref="FileVersionInfo.Language"/>
        public string? Language => this.fileVersionInfo?.Language;

        /// <inheritdoc cref="FileSystemInfo.LastAccessTime"/>
        public DateTime LastAccessTime => this.fileInfo.LastAccessTime;

        /// <inheritdoc cref="FileSystemInfo.LastAccessTimeUtc"/>
        public DateTime LastAccessTimeUtc => this.fileInfo.LastAccessTimeUtc;

        /// <inheritdoc cref="FileSystemInfo.LastWriteTime"/>
        public DateTime LastWriteTime => this.fileInfo.LastWriteTime;

        /// <inheritdoc cref="FileSystemInfo.LastWriteTimeUtc"/>
        public DateTime LastWriteTimUtc => this.fileInfo.LastWriteTimeUtc;

        /// <inheritdoc cref="FileVersionInfo.LegalCopyright"/>
        public string? LegalCopyright => this.fileVersionInfo?.LegalCopyright;

        /// <inheritdoc cref="FileVersionInfo.LegalTrademarks"/>
        public string? LegalTrademarks => this.fileVersionInfo?.LegalTrademarks;

        /// <inheritdoc cref="FileInfo.Length"/>
        public long Length => this.fileInfo.Length;

        /// <inheritdoc cref="FileInfo.Name"/>
        public string Name => this.fileInfo.Name;

        /// <inheritdoc cref="FileVersionInfo.OriginalFilename"/>
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "Filename", Justification = "This property follows the same naming convention as the underlying property in the FileVersionInfo class.")]
        public string? OriginalFilename => this.fileVersionInfo?.OriginalFilename;

        /// <inheritdoc cref="FileVersionInfo.PrivateBuild"/>
        public string? PrivateBuild => this.fileVersionInfo?.PrivateBuild;

        /// <inheritdoc cref="FileVersionInfo.ProductBuildPart"/>
        public int? ProductBuildPart => this.fileVersionInfo?.ProductBuildPart;

        /// <inheritdoc cref="FileVersionInfo.ProductMajorPart"/>
        public int? ProductMajorPart => this.fileVersionInfo?.ProductMajorPart;

        /// <inheritdoc cref="FileVersionInfo.ProductMinorPart"/>
        public int? ProductMinorPart => this.fileVersionInfo?.ProductMinorPart;

        /// <inheritdoc cref="FileVersionInfo.PrivateBuild"/>
        public string? ProductName => this.fileVersionInfo?.ProductName;

        /// <inheritdoc cref="FileVersionInfo.ProductPrivatePart"/>
        public int? ProductPrivatePart => this.fileVersionInfo?.ProductPrivatePart;

        /// <inheritdoc cref="FileVersionInfo.ProductVersion"/>
        public string? ProductVersion => this.fileVersionInfo?.ProductVersion;

        /// <inheritdoc cref="FileVersionInfo.SpecialBuild"/>
        public string? SpecialBuild => this.fileVersionInfo?.SpecialBuild;

        /// <inheritdoc cref="FileInfo.AppendText"/>
        public StreamWriter AppendText()
        {
            return this.fileInfo.AppendText();
        }

        /// <inheritdoc cref="FileInfo.CopyTo(String)"/>
        public FileInfo CopyTo(string destinationFileName)
        {
            return this.fileInfo.CopyTo(destinationFileName);
        }

        /// <inheritdoc cref="FileInfo.CopyTo(String, Boolean)"/>
        public FileInfo CopyTo(string destinationFileName, bool overwrite)
        {
            return this.fileInfo.CopyTo(destinationFileName, overwrite);
        }

        /// <inheritdoc cref="FileInfo.Create"/>
        public FileStream Create()
        {
            return this.fileInfo.Create();
        }

        /// <inheritdoc cref="FileInfo.Delete"/>
        public void Delete()
        {
            this.fileInfo.Delete();
        }

        /// <summary>
        /// Gets a <see cref="FileSecurity"/> object that encapsulates the
        /// access control list (ACL) entries for the file described by the
        /// current <see cref="ExtendedFileInfo"/> object.
        /// </summary>
        /// <returns>
        /// A <see cref="FileSecurity"/> object that encapsulates the access
        /// control rules for the current file.
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
        /// Use the <see cref="GetAccessControl()"/> method to retrieve the
        /// access control list (ACL) entries for the current file.
        /// </para>
        /// <para>
        /// An ACL describes individuals and/or groups who have, or do not have,
        /// rights to specific actions on the given file.
        /// </para>
        /// </remarks>
        public FileSecurity GetAccessControl()
        {
            return this.fileInfo.GetAccessControl();
        }

        /// <summary>
        /// Gets a <see cref="FileSecurity"/> object that encapsulates the
        /// access control list (ACL) entries for the file described by the
        /// current <see cref="ExtendedFileInfo"/> object.
        /// </summary>
        /// <param name="includeSections">
        /// One of the <see cref="AccessControlSections"/> values that specifies
        /// which group of access control entries to retrieve.
        /// </param>
        /// <returns>
        /// A <see cref="FileSecurity"/> object that encapsulates the access
        /// control rules for the current file.
        /// </returns>
        /// <exception cref="IOException">
        /// An I/O error occurred while opening the file.
        /// </exception>
        /// <exception cref="PlatformNotSupportedException">
        /// The current operating system is not Microsoft Windows 2000 or later.
        /// </exception>
        /// <exception cref="PrivilegeNotHeldException">
        /// The current system account does not have administrative privileges.
        /// </exception>
        /// <exception cref="Exception">The file could not be found.</exception>
        /// <exception cref="UnauthorizedAccessException">
        /// <para>This operation is not supported on the current platform.</para>
        /// <para>-or-</para>
        /// <para>The caller does not have the required permission.</para>
        /// </exception>
        /// <remarks>
        /// <para>
        /// Use the <see cref="GetAccessControl(AccessControlSections)"/> method
        /// to retrieve the access control list (ACL) entries for the current file.
        /// </para>
        /// <para>
        /// An ACL describes individuals and/or groups who have, or do not have,
        /// rights to specific actions on the given file.
        /// </para>
        /// </remarks>
        public FileSecurity GetAccessControl(AccessControlSections includeSections)
        {
            return this.fileInfo.GetAccessControl(includeSections);
        }

        /// <inheritdoc cref="FileInfo.MoveTo(String)"/>
        public void MoveTo(string destinationFileName)
        {
            this.fileInfo.MoveTo(destinationFileName);
        }

        /// <inheritdoc cref="FileInfo.Open(FileMode)"/>
        public FileStream Open(FileMode mode)
        {
            return this.fileInfo.Open(mode);
        }

        /// <inheritdoc cref="FileInfo.Open(FileMode, FileAccess)"/>
        public FileStream Open(FileMode mode, FileAccess access)
        {
            return this.fileInfo.Open(mode, access);
        }

        /// <inheritdoc cref="FileInfo.Open(FileMode, FileAccess, FileShare)"/>
        public FileStream Open(FileMode mode, FileAccess access, FileShare share)
        {
            return this.fileInfo.Open(mode, access, share);
        }

        /// <inheritdoc cref="FileInfo.OpenRead"/>
        public FileStream OpenRead()
        {
            return this.fileInfo.OpenRead();
        }

        /// <inheritdoc cref="FileInfo.OpenText"/>
        public StreamReader OpenText()
        {
            return this.fileInfo.OpenText();
        }

        /// <inheritdoc cref="FileInfo.OpenWrite"/>
        public FileStream OpenWrite()
        {
            return this.fileInfo.OpenWrite();
        }

        /// <inheritdoc cref="FileSystemInfo.Refresh"/>
        public void Refresh()
        {
            this.fileInfo.Refresh();
        }

        /// <summary>
        /// Applies access control list (ACL) entries described by a
        /// FileSecurity object to the file described by the current FileInfo object.
        /// </summary>
        /// <param name="fileSecurity">
        /// A FileSecurity object that describes an access control list (ACL)
        /// entry to apply to the current file.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="fileSecurity"/> parameter is <see langword="null"/>.
        /// </exception>
        /// <exception cref="System.Exception">
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
        /// The ACL specified for the fileSecurity parameter replaces the
        /// existing ACL for the file. To add permissions for a new user, use
        /// the GetAccessControl method to obtain the existing ACL, modify it,
        /// and then use SetAccessControl to apply it back to the file.
        /// </para>
        /// <para>
        /// An ACL describes individuals and/or groups who have, or do not have,
        /// rights to specific actions on the given file.
        /// </para>
        /// <para>
        /// The SetAccessControl method persists only FileSecurity objects that
        /// have been modified after object creation. If a FileSecurity object
        /// has not been modified, it will not be persisted to a file.
        /// Therefore, it is not possible to retrieve a FileSecurity object from
        /// one file and reapply the same object to another file.
        /// </para>
        /// <para>To copy ACL information from one file to another:</para>
        /// <list type="numbered">
        /// <item>
        /// Use the GetAccessControl method to retrieve the FileSecurity object
        /// from the source file.
        /// </item>
        /// <item>Create a new FileSecurity object for the destination file.</item>
        /// <item>
        /// Use the GetSecurityDescriptorBinaryForm or
        /// GetSecurityDescriptorSddlForm method of the source FileSecurity
        /// object to retrieve the ACL information.
        /// </item>
        /// <item>
        /// Use the SetSecurityDescriptorBinaryForm or
        /// SetSecurityDescriptorSddlForm method to copy the information
        /// retrieved in step 3 to the destination FileSecurity object.
        /// </item>
        /// <item>
        /// Set the destination FileSecurity object to the destination file
        /// using the SetAccessControl method.
        /// </item>
        /// </list>
        /// </remarks>
        public void SetAccessControl(FileSecurity fileSecurity)
        {
            this.fileInfo.SetAccessControl(fileSecurity);
        }

        /// <inheritdoc cref="FileInfo.ToString"/>
        public override string ToString()
        {
            return this.fileInfo.ToString();
        }
    }
}