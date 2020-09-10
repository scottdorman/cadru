//------------------------------------------------------------------------------
// <copyright file="FileUtilities.cs"
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
using System.Runtime.CompilerServices;

using Cadru.Build.Tasks.Resources;

namespace Cadru.Build.Tasks.Internal
{
    /// <summary>
    /// This class contains utility methods for file IO. PERF\COVERAGE NOTE: Try
    /// to keep classes in 'shared' as granular as possible. All the methods in
    /// each class get pulled into the resulting assembly.
    /// </summary>
    internal static partial class FileUtilities
    {
        internal static void CopyDirectory(string source, string dest)
        {
            Directory.CreateDirectory(dest);

            var sourceInfo = new DirectoryInfo(source);
            foreach (var fileInfo in sourceInfo.GetFiles())
            {
                var destFile = Path.Combine(dest, fileInfo.Name);
                fileInfo.CopyTo(destFile);
            }
            foreach (var subdirInfo in sourceInfo.GetDirectories())
            {
                var destDir = Path.Combine(dest, subdirInfo.Name);
                CopyDirectory(subdirInfo.FullName, destDir);
            }
        }

        /// <summary>
        /// Generates a unique directory name in the temporary folder. Caller
        /// must delete when finished.
        /// </summary>
        /// <param name="createDirectory"></param>
        internal static string GetTemporaryDirectory(bool createDirectory = true)
        {
            var temporaryDirectory = Path.Combine(Path.GetTempPath(), "Temporary" + Guid.NewGuid().ToString("N"));

            if (createDirectory)
            {
                Directory.CreateDirectory(temporaryDirectory);
            }

            return temporaryDirectory;
        }

        /// <summary>
        /// Generates a unique temporary file name with a given extension in the
        /// temporary folder. If no extension is provided, uses ".tmp". File is
        /// guaranteed to be unique. Caller must delete it when finished.
        /// </summary>
        internal static string GetTemporaryFile()
        {
            return GetTemporaryFile(".tmp");
        }

        /// <summary>
        /// Generates a unique temporary file name with a given extension in the
        /// temporary folder. File is guaranteed to be unique. Extension may
        /// have an initial period. Caller must delete it when finished. May
        /// throw IOException.
        /// </summary>
        internal static string GetTemporaryFile(string extension)
        {
            return GetTemporaryFile(null, extension);
        }

        /// <summary>
        /// Creates a file with unique temporary file name with a given
        /// extension in the specified folder. File is guaranteed to be unique.
        /// Extension may have an initial period. If folder is null, the
        /// temporary folder will be used. Caller must delete it when finished.
        /// May throw IOException.
        /// </summary>
        internal static string GetTemporaryFile(string directory, string extension, bool createFile = true)
        {
            ErrorUtilities.VerifyThrowArgumentLengthIfNotNull(directory, nameof(directory));
            ErrorUtilities.VerifyThrowArgumentLength(extension, nameof(extension));

            if (extension[0] != '.')
            {
                extension = '.' + extension;
            }

            try
            {
                directory = directory ?? Path.GetTempPath();

                Directory.CreateDirectory(directory);

                var file = Path.Combine(directory, $"tmp{Guid.NewGuid():N}{extension}");

                ErrorUtilities.VerifyThrow(!File.Exists(file), "Guid should be unique");

                if (createFile)
                {
                    File.WriteAllText(file, String.Empty);
                }

                return file;
            }
            catch (Exception ex) when (ExceptionHandling.IsIoRelatedException(ex))
            {
                throw new IOException(String.Format(Strings.Shared_FailedCreatingTempFile, ex.Message), ex);
            }
        }

        /// <summary>
        /// Generates a unique temporary file name with a given extension in the
        /// temporary folder. File is guaranteed to be unique. Extension may
        /// have an initial period. File will NOT be created. May throw IOException.
        /// </summary>
        internal static string GetTemporaryFileName(string extension)
        {
            return GetTemporaryFile(null, extension, false);
        }

        public class TempWorkingDirectory : IDisposable
        {
            public TempWorkingDirectory(string sourcePath, [CallerMemberName] string name = null)
            {
                this.Path = name == null
                    ? GetTemporaryDirectory()
                    : System.IO.Path.Combine(System.IO.Path.GetTempPath(), name);

                if (Directory.Exists(this.Path))
                {
                    Directory.Delete(this.Path, true);
                }

                CopyDirectory(sourcePath, this.Path);
            }

            public string Path { get; }

            public void Dispose()
            {
                Directory.Delete(this.Path, true);
            }
        }
    }
}