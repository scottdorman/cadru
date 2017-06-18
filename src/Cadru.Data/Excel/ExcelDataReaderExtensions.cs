namespace Cadru.Data.Excel
{
    using Cadru.Extensions;
    using System;
    using System.Linq;

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