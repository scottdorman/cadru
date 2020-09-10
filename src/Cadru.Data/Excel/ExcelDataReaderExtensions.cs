//------------------------------------------------------------------------------
// <copyright file="ExcelDataReaderExtensions.cs"
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
    using System.Linq;
    using Cadru.Extensions;

    /// <summary>
    /// Defines the <see cref=ExcelDataReaderExtensions />
    /// </summary>
    public static partial class ExcelDataReaderExtensions
    {
        public static T Field<T>(this ExcelDataReader reader, int columnIndex)
        {
            T fieldValue;
            try
            {
                fieldValue = UnboxT<T>.Unbox(reader[columnIndex]);
            }
            catch (InvalidCastException e)
            {
                e.Data.Add(nameof(columnIndex), columnIndex);
                throw;
            }

            return fieldValue;
        }

        public static T Field<T>(this ExcelDataReader reader, string columnName)
        {
            T fieldValue;
            try
            {
                fieldValue = UnboxT<T>.Unbox(reader[columnName]);
            }
            catch (InvalidCastException e)
            {
                e.Data.Add(nameof(columnName), columnName);
                throw;
            }

            return fieldValue;
        }

        public static T FieldOrDefault<T>(this ExcelDataReader reader, int columnIndex)
        {
            return FieldOrDefault(reader, columnIndex, default(T));
        }

        public static T FieldOrDefault<T>(this ExcelDataReader reader, string columnName)
        {
            return FieldOrDefault(reader, columnName, default(T));
        }

        public static T FieldOrDefault<T>(this ExcelDataReader reader, int columnIndex, T nullValue)
        {
            return reader.IsNullOrWhiteSpace(columnIndex) ? nullValue : reader.Field<T>(columnIndex);
        }

        public static T FieldOrDefault<T>(this ExcelDataReader reader, string columnName, T nullValue)
        {
            return reader.IsNullOrWhiteSpace(columnName) ? nullValue : reader.Field<T>(columnName);
        }

        public static bool IsNull(this ExcelDataReader reader, int columnIndex)
        {
            if (!columnIndex.Between(0, reader.FieldCount))
            {
                return true;
            }

            return reader[columnIndex] == null;
        }

        public static bool IsNull(this ExcelDataReader reader, string columnName)
        {
            if (String.IsNullOrWhiteSpace(columnName) || !reader.FieldNames.Contains(columnName))
            {
                return true;
            }

            return reader[columnName] == null;
        }

        public static bool IsNullOrWhiteSpace(this ExcelDataReader reader, int columnIndex)
        {
            if (!columnIndex.Between(0, reader.FieldCount))
            {
                return true;
            }

            return reader.IsNull(columnIndex) || String.IsNullOrWhiteSpace(reader[columnIndex].ToString());
        }

        public static bool IsNullOrWhiteSpace(this ExcelDataReader reader, string columnName)
        {
            if (String.IsNullOrWhiteSpace(columnName) || !reader.FieldNames.Contains(columnName))
            {
                return true;
            }

            return reader.IsNull(columnName) || String.IsNullOrWhiteSpace(reader[columnName].ToString());
        }

        public static string ToDelimitedString(this ExcelDataReader reader)
        {
            return reader.ToDelimitedString(includeHeaders: false);
        }

        public static string ToDelimitedString(this ExcelDataReader reader, bool includeHeaders)
        {
            var itemArray = new object[reader.FieldCount];
            var fieldNames = reader.FieldNames.ToArray();

            reader.GetValues(itemArray);
            return String.Join(",", itemArray.Select((x, i) => $"[{(includeHeaders ? $"{fieldNames[i]}: " : "")}{x}]"));
        }
    }
}