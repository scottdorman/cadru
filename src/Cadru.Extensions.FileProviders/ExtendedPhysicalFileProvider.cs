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

using System;
using System.IO;

using Cadru.Extensions.FileProviders.Internal;

using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Physical;

namespace Cadru.Extensions.FileProviders
{
    /// <summary>
    /// Looks up files or folders using the on-disk file system.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This extends  <see cref="PhysicalFileProvider" /> and adds support for
    /// working with files or folders returned as an <see cref="IFileInfo" />.
    /// </para>
    /// <para>
    /// When the environment variable "DOTNET_USE_POLLING_FILE_WATCHER" is set to "1" or "true", calls to
    /// <see cref="PhysicalFileProvider.Watch(String)" /> will use <see cref="PollingFileChangeToken" />.
    /// </para>
    /// </remarks>
    public class ExtendedPhysicalFileProvider : PhysicalFileProvider
    {
        private static readonly char[] _pathSeparators = new[]
            {Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar};

        private readonly ExclusionFilters _filters;

        /// <summary>
        /// Initializes a new instance of a PhysicalFileProvider at the given root directory.
        /// </summary>
        /// <param name="root">The root directory. This should be an absolute path.</param>
        public ExtendedPhysicalFileProvider(string root) : base(root)
        {
        }

        /// <summary>
        /// Initializes a new instance of a PhysicalFileProvider at the given root directory.
        /// </summary>
        /// <param name="root">The root directory. This should be an absolute path.</param>
        /// <param name="filters">Specifies which files or directories are excluded.</param>
        public ExtendedPhysicalFileProvider(string root, ExclusionFilters filters) : base(root, filters)
        {
            this._filters = filters;
        }

        /// <summary>
        /// Creates a directory at the given path.
        /// </summary>
        /// <param name="subpath">A path under the root directory</param>
        /// <returns>The directory information. Caller must check <see cref="IFileInfo.Exists" /> property. </returns>
        public IFileInfo CreateDirectory(string subpath)
        {
            PhysicalDirectoryInfo physicalDirectoryInfo = null;
            var fileInfo = this.GetDirectoryInfo(subpath);
            if (!(fileInfo is NotFoundFileInfo))
            {
                var fileSystemInfo = new DirectoryInfo(fileInfo.PhysicalPath);
                if (!fileSystemInfo.Exists)
                {
                    fileSystemInfo.Create();
                    fileSystemInfo.Refresh();
                }

                physicalDirectoryInfo = fileSystemInfo.Exists ? new PhysicalDirectoryInfo(fileSystemInfo) : null;
            }

            return physicalDirectoryInfo;
        }

        /// <summary>
        /// Creates a file at the given path.
        /// </summary>
        /// <param name="subpath">A path under the root directory</param>
        /// <returns>The file information. Caller must check <see cref="IFileInfo.Exists" /> property. </returns>
        public IFileInfo CreateFile(string subpath)
        {
            PhysicalFileInfo physicalFileInfo = null;
            var fileInfo = this.GetFileInfo(subpath);
            if (!(fileInfo is NotFoundFileInfo))
            {
                var fileSystemInfo = new FileInfo(fileInfo.PhysicalPath);
                if (!fileSystemInfo.Exists)
                {
                    fileSystemInfo.Create();
                    fileSystemInfo.Refresh();
                }

                physicalFileInfo = fileSystemInfo.Exists ? new PhysicalFileInfo(fileSystemInfo) : null;
            }

            return physicalFileInfo;
        }

        /// <summary>
        /// Locate a directory at the given path by directly mapping path segments to physical directories.
        /// </summary>
        /// <param name="subpath">A path under the root directory</param>
        /// <returns>The directory information. Caller must check <see cref="IFileInfo.Exists" /> property.</returns>
        public IFileInfo GetDirectoryInfo(string subpath)
        {
            if (String.IsNullOrEmpty(subpath) || PathUtils.HasInvalidPathChars(subpath))
            {
                return new NotFoundFileInfo(subpath);
            }

            // Relative paths starting with leading slashes are okay
            subpath = subpath.TrimStart(_pathSeparators);

            // Absolute paths not permitted.
            if (Path.IsPathRooted(subpath))
            {
                return new NotFoundFileInfo(subpath);
            }

            var fullPath = this.GetFullPath(subpath);
            if (fullPath == null)
            {
                return new NotFoundFileInfo(subpath);
            }

            var directoryInfo = new DirectoryInfo(fullPath);

            if (FileSystemInfoHelper.IsExcluded(directoryInfo, this._filters))
            {
                return new NotFoundFileInfo(subpath);
            }

            return new PhysicalDirectoryInfo(directoryInfo);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Pending>")]
        private string GetFullPath(string path)
        {
            if (PathUtils.PathNavigatesAboveRoot(path))
            {
                return null;
            }

            string fullPath;
            try
            {
                fullPath = Path.GetFullPath(Path.Combine(this.Root, path));
            }
            catch
            {
                return null;
            }

            if (!this.IsUnderneathRoot(fullPath))
            {
                return null;
            }

            return fullPath;
        }

        private bool IsUnderneathRoot(string fullPath)
        {
            return fullPath.StartsWith(this.Root, StringComparison.OrdinalIgnoreCase);
        }
    }
}