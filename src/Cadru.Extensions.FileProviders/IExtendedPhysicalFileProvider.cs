//------------------------------------------------------------------------------
// <copyright file="ExtendedPhysicalFileProvider.cs"
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

using Microsoft.Extensions.FileProviders;

namespace Cadru.Extensions.FileProviders
{
    public interface IExtendedPhysicalFileProvider
    {
        /// <summary>
        /// Creates a directory at the given path.
        /// </summary>
        /// <param name="subpath">A path under the root directory</param>
        /// <returns>
        /// The directory information. Caller must check
        /// <see cref="IFileInfo.Exists"/> property.
        /// </returns>
        IFileInfo CreateDirectory(string subpath);

        /// <summary>
        /// Creates a file at the given path.
        /// </summary>
        /// <param name="subpath">A path under the root directory</param>
        /// <returns>
        /// The file information. Caller must check
        /// <see cref="IFileInfo.Exists"/> property.
        /// </returns>
        IFileInfo CreateFile(string subpath);

        /// <summary>
        /// Locate a directory at the given path by directly mapping path
        /// segments to physical directories.
        /// </summary>
        /// <param name="subpath">A path under the root directory</param>
        /// <returns>
        /// The directory information. Caller must check
        /// <see cref="IFileInfo.Exists"/> property.
        /// </returns>
        IFileInfo GetDirectoryInfo(string subpath);
    }
}