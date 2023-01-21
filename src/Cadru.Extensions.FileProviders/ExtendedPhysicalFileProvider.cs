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
using System.Diagnostics.CodeAnalysis;
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
    /// This extends <see cref="PhysicalFileProvider"/> and adds support for
    /// working with files or folders returned as an <see cref="IFileInfo"/>.
    /// </para>
    /// <para>
    /// When the environment variable "DOTNET_USE_POLLING_FILE_WATCHER" is set
    /// to "1" or "true", calls to
    /// <see cref="PhysicalFileProvider.Watch(String)"/> will use <see cref="PollingFileChangeToken"/>.
    /// </para>
    /// </remarks>
    public class ExtendedPhysicalFileProvider : PhysicalFileProvider, IExtendedPhysicalFileProvider
    {
        private static readonly char[] _pathSeparators = new char[2]
        {
            Path.DirectorySeparatorChar,
            Path.AltDirectorySeparatorChar
        };

        private readonly ExclusionFilters _filters;

        /// <summary>
        /// Initializes a new instance of a PhysicalFileProvider at the given
        /// root directory.
        /// </summary>
        /// <param name="root">
        /// The root directory. This should be an absolute path.
        /// </param>
        public ExtendedPhysicalFileProvider(string root) : base(root)
        {
        }

        /// <summary>
        /// Initializes a new instance of a PhysicalFileProvider at the given
        /// root directory.
        /// </summary>
        /// <param name="root">
        /// The root directory. This should be an absolute path.
        /// </param>
        /// <param name="filters">
        /// Specifies which files or directories are excluded.
        /// </param>
        public ExtendedPhysicalFileProvider(string root, ExclusionFilters filters) : base(root, filters)
        {
            this._filters = filters;
        }

        /// <inheritdoc/>
        public IFileInfo CreateDirectory(string subpath)
        {
            PhysicalDirectoryInfo? physicalDirectoryInfo = null;
            var fileInfo = this.GetDirectoryInfo(subpath);
            if (fileInfo is not NotFoundFileInfo)
            {
                var fileSystemInfo = new DirectoryInfo(fileInfo.PhysicalPath ?? fileInfo.Name);
                if (!fileSystemInfo.Exists)
                {
                    fileSystemInfo.Create();
                    fileSystemInfo.Refresh();
                }

                physicalDirectoryInfo = new PhysicalDirectoryInfo(fileSystemInfo);
            }

            return physicalDirectoryInfo ?? fileInfo;
        }

        /// <inheritdoc/>
        public IFileInfo CreateFile(string subpath)
        {
            PhysicalFileInfo? physicalFileInfo = null;
            var fileInfo = this.GetFileInfo(subpath);
            if (fileInfo is not NotFoundFileInfo)
            {
                var fileSystemInfo = new FileInfo(fileInfo.PhysicalPath ?? fileInfo.Name);
                if (!fileSystemInfo.Exists)
                {
                    fileSystemInfo.Create();
                    fileSystemInfo.Refresh();
                }

                physicalFileInfo = new PhysicalFileInfo(fileSystemInfo);
            }

            return physicalFileInfo ?? fileInfo;
        }

        /// <inheritdoc/>
        public IFileInfo GetDirectoryInfo(string subpath)
        {
            if (string.IsNullOrEmpty(subpath) || PathUtils.HasInvalidPathChars(subpath))
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
            if (fullPath is null)
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

        private string? GetFullPath(string path)
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