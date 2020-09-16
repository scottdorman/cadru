//------------------------------------------------------------------------------
// <copyright file="FileSystem.cs"
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

using Cadru.Data.IO;
using Cadru.Data.Resources;

namespace Cadru.Data
{
    // Install-Package Microsoft.VisualBasic

    internal static class FileSystem
    {
        /// <summary>
        /// Return an instance of a TextFieldParser for the given file.
        /// </summary>
        /// <param name="file">The path to the file to parse.</param>
        /// <returns>An instance of a TextFieldParser.</returns>
        public static TextFieldParser OpenTextFieldParser(string file)
        {
            return new TextFieldParser(file);
        }

        /// <summary>
        /// Return an instance of a TextFieldParser for the given file using the given delimiters.
        /// </summary>
        /// <param name="file">The path to the file to parse.</param>
        /// <param name="delimiters">A list of delimiters.</param>
        /// <returns>An instance of a TextFieldParser</returns>
        public static TextFieldParser OpenTextFieldParser(string file, params string[] delimiters)
        {
            var Result = new TextFieldParser(file);
            Result.SetDelimiters(delimiters);
            Result.TextFieldType = FieldType.Delimited;
            return Result;
        }

        /// <summary>
        /// Return an instance of a TextFieldParser for the given file using the given field widths.
        /// </summary>
        /// <param name="file">The path to the file to parse.</param>
        /// <param name="fieldWidths">A list of field widths.</param>
        /// <returns>An instance of a TextFieldParser</returns>
        public static TextFieldParser OpenTextFieldParser(string file, params int[] fieldWidths)
        {
            var Result = new TextFieldParser(file);
            Result.SetFieldWidths(fieldWidths);
            Result.TextFieldType = FieldType.FixedWidth;
            return Result;
        }

        /// <summary>
        /// Normalize the path, but throw exception if the path ends with separator.
        /// </summary>
        /// <param name="path">The input path.</param>
        /// <param name="paramName">The parameter name to include in the exception if one is raised.</param>
        /// <returns>The normalized path.</returns>
        internal static string NormalizeFilePath(string path, string paramName)
        {
            CheckFilePathTrailingSeparator(path, paramName);
            return NormalizePath(path);
        }

        /// <summary>
        /// Get full path, get long format, and remove any pending separator.
        /// </summary>
        /// <param name="path">The path to be normalized.</param>
        /// <returns>The normalized path.</returns>
        /// <remarks>Keep this function since we might change the implementation / behavior later.</remarks>
        internal static string NormalizePath(string path)
        {
            return Path.GetFullPath(RemoveEndingSeparator(Path.GetFullPath(path)));
        }

        /// <summary>
        /// Throw ArgumentException if the file path ends with a separator.
        /// </summary>
        /// <param name="path">The file path.</param>
        /// <param name="paramName">The parameter name to include in ArgumentException.</param>
        internal static void CheckFilePathTrailingSeparator(string path, string paramName)
        {
            if (String.IsNullOrEmpty(path)) // Check for argument null
            {
                throw new ArgumentNullException(nameof(paramName));
            }

#if NETSTANDARD2_0
            var length = path.Length;
            var lastPos = path.Length - 1;

            bool EndsWith(char value)
            {
                return ((uint)lastPos < (uint)length) && path[lastPos] == value;
            }

            if (EndsWith(Path.DirectorySeparatorChar) || EndsWith(Path.AltDirectorySeparatorChar))
            {
                throw new ArgumentException(Strings.IO_FilePathException, nameof(paramName));
            }
#else
            if (path.EndsWith(Path.DirectorySeparatorChar) || path.EndsWith(Path.AltDirectorySeparatorChar))
            {
                throw new ArgumentException(Strings.IO_FilePathException, nameof(paramName));
            }
#endif
        }

        /// <summary>
        /// Removes all directory separators at the end of a path.
        /// </summary>
        /// <param name="path">a full or relative path.</param>
        /// <returns>If Path is a root path, the same value. Otherwise, removes any directory separators at the end.</returns>
        /// <remarks>We decided not to return path with separators at the end.</remarks>
        private static string RemoveEndingSeparator(string path)
        {
            // If the path is rooted, attempt to check if it is a root path.
            // Note: IO.Path.GetPathRoot: C: -> C:, C:\ -> C:\, \\myshare\mydir -> \\myshare\mydir
            // BUT \\myshare\mydir\ -> \\myshare\mydir!!! This function will remove the ending separator of
            // \\myshare\mydir\ as well. Do not use IsRoot here.
            if (Path.IsPathRooted(path) && Path.Equals(Path.GetPathRoot(path), StringComparison.OrdinalIgnoreCase))
            {
                return path;
            }

            // Otherwise, remove all separators at the end.
            return path.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        }

    }
}