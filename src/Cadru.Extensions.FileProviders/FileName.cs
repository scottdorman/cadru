//------------------------------------------------------------------------------
// <copyright file="FileName.cs"
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

using Cadru.Extensions.FileProviders.Resources;

namespace Cadru.Extensions.FileProviders
{
    /// <summary>
    /// Provides static methods for working with file names on disk.
    /// </summary>
    public static class FileName
    {
        /// <summary>
        /// Gets a unique folder name or file name, based on the given path.
        /// </summary>
        /// <param name="path">The initial folder or file name.</param>
        /// <returns>A unique folder or file name.</returns>
        /// <remarks><see cref="NextAvailableFilename(String)"/> does not create a file.</remarks>
        public static string NextAvailableFilename(string path)
        {
            // Short-cut if already available
            if (!File.Exists(path))
            {
                return path;
            }

            // If path has extension then insert the number pattern just before the extension and return next filename
            if (Path.HasExtension(path))
            {
                return GetNextFilename(path.Insert(path.LastIndexOf(Path.GetExtension(path)), Strings.FileName_NumberPattern));
            }

            // Otherwise just append the pattern to the path and return next filename
            return GetNextFilename(path + Strings.FileName_NumberPattern);
        }

        private static string GetNextFilename(string pattern)
        {
            var tmp = String.Format(pattern, 1);
            if (tmp == pattern)
            {
                throw new ArgumentException(Strings.FileName_ArgumentException_InvalidPattern, nameof(pattern));
            }

            if (!File.Exists(tmp))
            {
                return tmp; // short-circuit if no matches
            }

            int min = 1, max = 2; // min is inclusive, max is exclusive/untested

            while (File.Exists(String.Format(pattern, max)))
            {
                min = max;
                max *= 2;
            }

            while (max != min + 1)
            {
                var pivot = (max + min) / 2;
                if (File.Exists(String.Format(pattern, pivot)))
                {
                    min = pivot;
                }
                else
                {
                    max = pivot;
                }
            }

            return String.Format(pattern, max);
        }
    }
}