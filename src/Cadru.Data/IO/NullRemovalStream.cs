//------------------------------------------------------------------------------
// <copyright file="NullRemovalStream.cs"
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

using Cadru.Data.Resources;

namespace Cadru.Data.IO
{
    /// <summary>
    /// A stream implementation that removes consecutive null bytes above a
    /// threshold from source
    /// </summary>
    public class NullRemovalStream : Stream
    {
        private readonly bool _addMark;
        private readonly byte[] _buffer;
        private readonly Stream _source;
        private readonly byte[] _storage;
        private readonly int _threshold;
        private int _bufferIndex;
        private int _bufferSize;
        private int _storageIndex;
        private int _storageSize;

        /// <summary>
        /// Initializes a new instance of the <see cref="NullRemovalStream"/> class.
        /// </summary>
        /// <param name="source">
        /// A <see cref="T:Stream"/> pointing to the source data
        /// </param>
        /// <param name="addMark">
        /// Indicates if a mark ([removed x null bytes]) should be added to
        /// indicate removal
        /// </param>
        /// <param name="threshold">
        /// The number above which any consecutive null bytes above this
        /// threshold will be removed or replaced by a mark.
        /// </param>
        /// <param name="bufferSize">Size of buffer</param>
        public NullRemovalStream(Stream source, bool addMark = true, int threshold = 60, int bufferSize = 4096)
        {
            if (bufferSize <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(bufferSize), bufferSize, Strings.BufferSizeTooSmall);
            }

            this._source = source ?? throw new ArgumentNullException(nameof(source));
            this._addMark = addMark;
            this._buffer = new byte[bufferSize];
            this._threshold = threshold < 60 ? 60 : threshold;
            this.PopulateBuffer();

            this._storage = new byte[4096];
            this._storageIndex = 0;
            this._storageSize = 0;
        }

        /// <inheritdoc cref="Stream.CanRead"/>
        public override bool CanRead => this._source.CanRead;

        /// <inheritdoc cref="Stream.CanSeek"/>
        public override bool CanSeek => this._source.CanSeek;

        /// <inheritdoc cref="Stream.CanWrite"/>
        public override bool CanWrite => this._source.CanWrite;

        /// <inheritdoc cref="Stream.Length"/>
        public override long Length => this._source.Length;

        /// <inheritdoc cref="Stream.Position"/>
        public override long Position
        {
            get => this._source.Position;
            set => this._source.Position = value;
        }

        /// <inheritdoc cref="Stream.Close"/>
        public override void Close() => this._source.Close();

        /// <inheritdoc cref="Stream.Flush"/>
        public override void Flush() => this._source.Flush();

        /// <inheritdoc cref="Stream.Read(Byte[], Int32, Int32)"/>
        public override int Read(byte[] buffer, int offset, int count)
        {
            if (count > buffer.Length - offset)
            {
                return 0;
            }

            var targetIndex = offset;
            var dataRead = 0;
            var nullCount = 0;

            if (this._bufferSize == 0)
            {
                return 0;
            }

            var lastByteInBuffer = 1;
            while (dataRead < count)
            {
                lastByteInBuffer = 1;
                var readFromStorage = false;
                byte current;
                if (this._storageSize > 0)
                {
                    // read from temporary storage first
                    current = this._storage[this._storageIndex++];
                    this._storageSize--;
                    readFromStorage = true;
                }
                else
                {
                    // if last PopulateBuffer() exhausted the source stream,
                    // _bufferSize will be less than 4096
                    if (this._bufferIndex == this._bufferSize)
                    {
                        // save the current last byte in _buffer before
                        // populating _buffer again
                        lastByteInBuffer = this._bufferIndex > 0 ? this._buffer[this._bufferIndex - 1] : lastByteInBuffer;
                        this.PopulateBuffer();
                        if (this._bufferSize == 0)
                        {
                            break;
                        }
                    }
                    current = this._buffer[this._bufferIndex];
                }

                if (current == 0 && !readFromStorage)
                {
                    nullCount++;
                }
                else
                {
                    var processed = false;
                    if (this._storageSize == 0 && this._storageIndex > 0 && !readFromStorage)
                    {
                        // last iteration was reading from storage so no need to
                        // process again even the last byte was null; best place
                        // to reset storage index
                        this._storageIndex = 0;
                        processed = true;
                    }
                    var lastIsNull = !readFromStorage && !processed && (this._bufferIndex > 0 ? this._buffer[this._bufferIndex - 1] == 0 : lastByteInBuffer == 0);
                    var newTargetIndex = targetIndex;
                    if (lastIsNull)
                    {
                        // first non null byte
                        newTargetIndex = this.Process(buffer, targetIndex, nullCount);
                        if (newTargetIndex == buffer.Length)
                        {
                            return dataRead + newTargetIndex - targetIndex;
                        }
                        nullCount = 0;
                    }
                    buffer[newTargetIndex] = current;
                    dataRead += newTargetIndex - targetIndex + 1;
                    targetIndex = newTargetIndex + 1;
                }
                if (!readFromStorage)
                {
                    this._bufferIndex++;
                }
            }
            if (nullCount > 0 && dataRead == 0 || this._bufferSize == 0 && lastByteInBuffer == 0)
            {
                // the end of the source stream is a null byte so couldn't enter
                // the else block in the while loop above, do the needful
                return this.Process(buffer, targetIndex, nullCount);
            }
            return dataRead;
        }

        /// <inheritdoc cref="Stream.Seek(Int64, SeekOrigin)"/>
        public override long Seek(long offset, SeekOrigin origin) => this._source.Seek(offset, origin);

        /// <inheritdoc cref="Stream.SetLength(Int64)"/>
        public override void SetLength(long value) => this._source.SetLength(value);

        /// <inheritdoc cref="Stream.Write(Byte[], Int32, Int32)"/>
        public override void Write(byte[] buffer, int offset, int count) => this._source.Write(buffer, offset, count);

        private void PopulateBuffer()
        {
            this._bufferSize = this._source.Read(this._buffer, 0, this._buffer.Length);
            this._bufferIndex = 0;
        }

        private int Process(byte[] target, int targetIndex, int nullCount)
        {
            if (nullCount < this._threshold)
            {
                while (nullCount > 0 && targetIndex < target.Length)
                {
                    target[targetIndex] = 0;
                    targetIndex++;
                    nullCount--;
                }
                while (nullCount > 0)
                {
                    this._storage[this._storageSize++] = 0;
                    nullCount--;
                }
                return targetIndex;
            }

            if (!this._addMark)
            {
                return targetIndex;
            }

            var template = "[removed {0} null bytes]";
            var mark = String.Format(template, nullCount);
            foreach (var c in mark)
            {
                if (targetIndex < target.Length)
                {
                    target[targetIndex] = Convert.ToByte(c);
                    targetIndex++;
                }
                else
                {
                    this._storage[this._storageSize++] = Convert.ToByte(c);
                }
            }
            return targetIndex;
        }
    }
}