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

    public partial class ExcelDataReader
    {
        public int FieldCount => this.headers?.Count() ?? -1;

        public bool GetBoolean(int i)
        {
            return this.Field<bool>(i);
            //return SafeConverter.Convert<bool>(GetValue(i));
        }

        public byte GetByte(int i)
        {
            return this.Field<byte>(i);
            //return SafeConverter.Convert<byte>(GetValue(i));
        }

        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public char GetChar(int i)
        {
            return this.Field<char>(i);
            //return SafeConverter.Convert<char>(GetValue(i));
        }

        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public IDataReader GetData(int i)
        {
            return null;
        }

        public string GetDataTypeName(int i)
        {
            return typeof(string).Name;
        }

        public DateTime GetDateTime(int i)
        {
            return this.Field<DateTime>(i);
            //return DateTime.FromBinary(GetInt64(i));
        }

        public decimal GetDecimal(int i)
        {
            return this.Field<decimal>(i);

            //var value = GetValue(i);
            //if (value != null)
            //{
            //    if (Decimal.TryParse(value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture.NumberFormat, out decimal num))
            //    {
            //        return num;
            //    }
            //}

            //return SafeConverter.Convert<decimal>(value);
        }

        public double GetDouble(int i)
        {
            return this.Field<double>(i);

            //var value = GetValue(i);
            //if (value != null)
            //{
            //    if (Double.TryParse(value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture.NumberFormat, out double num))
            //    {
            //        return num;
            //    }
            //}

            //return SafeConverter.Convert<double>(value);
        }

        public Type GetFieldType(int i) => throw new NotSupportedException();

        public float GetFloat(int i)
        {
            return this.Field<float>(i);

            //var value = GetValue(i);
            //if (value != null)
            //{
            //    if (Single.TryParse(value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture.NumberFormat, out float num))
            //    {
            //        return num;
            //    }
            //}

            //return SafeConverter.Convert<float>(value);
        }

        public Guid GetGuid(int i)
        {
            return this.Field<Guid>(i);
            //return SafeConverter.Convert<Guid>(GetValue(i));
        }

        public short GetInt16(int i)
        {
            return this.Field<short>(i);
            //return SafeConverter.Convert<short>(GetValue(i));
        }

        public int GetInt32(int i)
        {
            return this.Field<int>(i);
            //return SafeConverter.Convert<int>(GetValue(i));
        }

        public long GetInt64(int i)
        {
            return this.Field<long>(i);
            //return SafeConverter.Convert<long>(GetValue(i));
        }

        public string GetName(int i)
        {
            return this.headers[i];
        }

        public int GetOrdinal(string name)
        {
            for (var i = 0; i < this.headers.Count; i++)
            {
                if (String.Equals(this.headers[i], name, StringComparison.OrdinalIgnoreCase))
                {
                    return i;
                }
            }
            return -1;
        }

        public string GetString(int i)
        {
            return this.Field<string>(i);
            //return SafeConverter.Convert<string>(GetValue(i));
        }

        public object GetValue(int i)
        {
            var cell = this.currentRowData.ElementAtOrDefault(i);
            return this.GetCellValue(cell);
        }

        public int GetValues(object[] values)
        {
            var num = values.Length < this.headers.Count ? values.Length : this.headers.Count;
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

        public bool IsDBNull(int i)
        {
            return DBNull.Value == this.GetValue(i);
        }
    }
}
