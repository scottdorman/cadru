//------------------------------------------------------------------------------
// <copyright file="DataValidation.cs" 
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
using System;
using System.Text;
using System.Collections;

namespace Cadru.Collections
{

	public class BitVector {

		#region ** Member Variables **
		private byte[] _data = new byte[0];
		private int _maxOffset = -1;
		#endregion

		public BitVector() {
		}

		public BitVector(byte[] data) {

			_data = new byte[data.Length];
			for (int i=0; i<data.Length; i++) {
				_data[i] = data[i];
			}
			_maxOffset = (_data.Length * 8) - 1;
		}

		public BitVector(long val, int length) {
			AddData(val, length);
		}


		public void AddString(string strValue) {

			for (int i = 0; i< strValue.Length; i++) {
				AddData((long) strValue[i], 8);
			}
		}

		public void AddData(long val, int length) {

			int offset = _maxOffset + 1;

			for (int i = 0; i < length; i++) {
				this[offset + i] = (val & (long)(1 << (length - i - 1))) != 0;
			}
			
		}


		public byte[] GetByteArray() {

			byte[] result = new byte[_data.Length];

			for(int i = 0; i < _data.Length; i++)
				result[i] = (byte) _data[i];
			
			return result;
		}


		public BitVector Range(int start, int length) {

			BitVector result = new BitVector();
			for (int i = start; i < (start + length); i++) {
				result[i - start] = this[i];
			}
			return result;
		}

		public int LongestCommonPrefix(BitVector other) {
			int i = 0;

			while ((i <= other._maxOffset) && (i <= this._maxOffset)) {
				if (other[i] != this[i]) {
					return i;
				}
				i++;
			}

			return i;
		}


		override public String ToString() {

			StringBuilder sb = new StringBuilder();

			for (int i = 0; i <= _maxOffset; i++) {

				sb.Append( this[i] ? "1" : "0" );
				if (i % 8 == 7)
					sb.Append(" ");
			}

			return sb.ToString();
		}


		public bool this[int index] {
			get {

				if (index > _maxOffset) {
					throw new ArgumentOutOfRangeException("index");
				}

				int byteOffset = index / 8;
				int bitOffset = index % 8;

				if (byteOffset >= _data.Length) {
					throw new InvalidOperationException("index out of bounds");
				}
				
				return (_data[byteOffset] & ( 1 << (7 - bitOffset))) != 0;
			}
			set {


				int byteOffset = index / 8;
				int bitOffset = index % 8;

				if (byteOffset >= _data.Length) {

					byte[] data = new byte[byteOffset + 1];

					for (int i = 0; i < _data.Length; i++) {
						data[i] = _data[i];
					}

					_data = data;
					_maxOffset = index;
				}
				else if (index > _maxOffset) {
					_maxOffset = index;
				}

				if (value)
					_data[byteOffset] |= (byte)(1 << (7 - bitOffset));
				else
					_data[byteOffset] &= (byte)(0xff - (1 << (7 - bitOffset)));
				
			}
		}

		public int Length {
			get { 
				return _maxOffset + 1; 
			}
		}
	}

}

