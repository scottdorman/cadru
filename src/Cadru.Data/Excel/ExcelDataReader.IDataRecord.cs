//------------------------------------------------------------------------------
// <copyright file="ExcelDataReader.IDataRecord.cs"
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

namespace Cadru.Data.Excel
{
    using System;
    using System.Data;
    using System.Linq;

    public partial class ExcelDataReader : IDataRecord
    {
        /// <inheritdoc/>
        public int FieldCount => this.FieldNames?.Count ?? -1;

        /// <inheritdoc/>
        public bool GetBoolean(int i)
        {
            return this.Field<bool>(i);
        }

        /// <inheritdoc/>
        public byte GetByte(int i)
        {
            return this.Field<byte>(i);
        }

        /// <inheritdoc/>
        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public char GetChar(int i)
        {
            return this.Field<char>(i);
        }

        /// <inheritdoc/>
        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IDataReader GetData(int i)
        {
            return null;
        }

        /// <inheritdoc/>
        public string GetDataTypeName(int i)
        {
            return typeof(string).Name;
        }

        /// <inheritdoc/>
        public DateTime GetDateTime(int i)
        {
            return this.Field<DateTime>(i);
        }

        /// <inheritdoc/>
        public decimal GetDecimal(int i)
        {
            return this.Field<decimal>(i);
        }

        /// <inheritdoc/>
        public double GetDouble(int i)
        {
            return this.Field<double>(i);
        }

        /// <inheritdoc/>
        public Type GetFieldType(int i) => throw new NotSupportedException();

        /// <inheritdoc/>
        public float GetFloat(int i)
        {
            return this.Field<float>(i);
        }

        /// <inheritdoc/>
        public Guid GetGuid(int i)
        {
            return this.Field<Guid>(i);
        }

        /// <inheritdoc/>
        public short GetInt16(int i)
        {
            return this.Field<short>(i);
        }

        /// <inheritdoc/>
        public int GetInt32(int i)
        {
            return this.Field<int>(i);
        }

        /// <inheritdoc/>
        public long GetInt64(int i)
        {
            return this.Field<long>(i);
        }

        /// <inheritdoc/>
        public string GetName(int i)
        {
            return this.FieldNames[i] ?? String.Empty;
        }

        /// <inheritdoc/>
        public int GetOrdinal(string name)
        {
            for (var i = 0; i < this.FieldNames.Count; i++)
            {
                if (String.Equals(this.FieldNames[i], name, StringComparison.OrdinalIgnoreCase))
                {
                    return i;
                }
            }
            return -1;
        }

        /// <inheritdoc/>
        public string GetString(int i)
        {
            return this.Field<string>(i);
        }

        /// <inheritdoc/>
        public object GetValue(int i)
        {
            var cell = this.currentRowData.ElementAtOrDefault(i);
            return this.GetCellValue(cell);
        }

        /// <inheritdoc/>
        public int GetValues(object[] values)
        {
            var num = values.Length < this.FieldNames.Count ? values.Length : this.FieldNames.Count;
            if (this.currentRowData == null)
            {
                values = new object[num];
                for (var i = 0; i < num; i++)
                {
                    values[i] = DBNull.Value;
                }
            }
            else
            {
                var row = this.currentRowData.Select(this.GetCellValue).ToArray();
                for (var i = 0; i < num; i++)
                {
                    values[i] = row[i];
                }
            }

            return num;
        }

        /// <inheritdoc/>
        public bool IsDBNull(int i)
        {
            return DBNull.Value == this.GetValue(i);
        }
    }
}