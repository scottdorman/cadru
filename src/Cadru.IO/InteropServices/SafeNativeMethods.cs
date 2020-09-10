//------------------------------------------------------------------------------
// <copyright file="SafeNativeMethods.cs"
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

namespace Cadru.IO.Interop
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;

    internal static class SafeNativeMethods
    {
        [DllImport("netapi32.dll", SetLastError = true)]
        internal static extern int NetApiBufferFree(IntPtr buffer);

        [DllImport("netapi32.dll", SetLastError = false)]
        internal static extern int NetServerEnum(
            [MarshalAs(UnmanagedType.LPWStr)] string servername,
            int level,
            out IntPtr bufptr,
            int prefmaxlen,
            ref int entriesread,
            ref int totalentries,
            [MarshalAs(UnmanagedType.U4)] uint servertype,
            [MarshalAs(UnmanagedType.LPWStr)] string domain,
            IntPtr resume_handle);

        [DllImport("netapi32.dll", SetLastError = false)]
        internal static extern int NetServerGetInfo(
            [MarshalAs(UnmanagedType.LPWStr)] string servername,
            int level,
            out IntPtr bufptr);

        // Retrieves information about an object in the file system,
        // such as a file, a folder, a directory, or a drive root.
        [DllImport("shell32",
            EntryPoint = "SHGetFileInfo",
            ExactSpelling = false,
            CharSet = CharSet.Unicode,
            SetLastError = true)]
        internal static extern IntPtr SHGetFileInfo(
            string pszPath,
            FileAttributes dwFileAttributes,
            ref SHFILEINFO sfi,
            int cbFileInfo,
            SHGFI uFlags);
    }
}