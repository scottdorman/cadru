//------------------------------------------------------------------------------
// <copyright file="NullRemovalStreamReader.cs"
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

using System.IO;
using System.Text;

namespace Cadru.Data.IO
{
    /// <summary>
    /// Implements a <see cref="TextReader"/> that reads characters from a byte
    /// stream in a particular encoding.
    /// </summary>
    public class NullRemovalStreamReader : StreamReader
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="NullRemovalStreamReader"/> class for the specified stream
        /// based on the specified character encoding, byte order mark detection
        /// option, and buffer size, and optionally leaves the stream open.
        /// </summary>
        /// <param name="stream">The stream to read.</param>
        /// <param name="addMark">
        /// Indicates if a mark ([removed x null bytes]) should be added to
        /// indicate removal
        /// </param>
        /// <param name="threshold">
        /// The number above which any consecutive null bytes above this
        /// threshold will be removed or replaced by a mark.
        /// </param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <param name="bufferSize">The minimum buffer size.</param>
        /// <param name="detectEncoding">
        /// <see langword="true"/> to look for byte order marks at the beginning
        /// of the file; otherwise, <see langword="false"/>.
        /// </param>
        public NullRemovalStreamReader(Stream stream, bool addMark, int threshold, Encoding encoding, int bufferSize = 4096, bool detectEncoding = false)
            : base(new NullRemovalStream(stream, addMark, threshold, bufferSize), encoding, detectEncoding, bufferSize)
        {
        }
    }
}