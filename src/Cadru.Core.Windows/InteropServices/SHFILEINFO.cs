//------------------------------------------------------------------------------
// <copyright file="SHFILEINFO.cs" 
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

    /// <summary>
    /// This structure contains information about a file object.
    /// </summary>
    /// <remarks>
    /// This structure is used with the SHGetFileInfo function.
    /// </remarks>
    [type: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1307:AccessibleFieldsMustBeginWithUpperCaseLetter", Justification = "Reviewed.")]
    [type: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Reviewed.")]
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct SHFILEINFO
    {
        /// <summary>
        /// Handle to the icon that represents the file. 
        /// </summary>
        internal IntPtr hIcon;
        
        /// <summary>
        /// Index of the icon image within the system image list.
        /// </summary>
        internal int iIcon;
        
        /// <summary>
        /// Specifies the attributes of the file object.
        /// </summary>
        internal SFGAO dwAttributes;
        
        /// <summary>
        /// Null-terminated string that contains the name of the file as it 
        /// appears in the Windows shell, or the path and name of the file that
        /// contains the icon representing the file.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.MAX_PATH)]
        internal string szDisplayName;
        
        /// <summary>
        /// Null-terminated string that describes the type of file. 
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
        internal string szTypeName;        
    }
}
