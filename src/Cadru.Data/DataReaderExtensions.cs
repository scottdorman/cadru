//------------------------------------------------------------------------------
// <copyright file="IDataReaderExtensions.cs"
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
using System.Data;

using Cadru.Extensions;

namespace Cadru.Data
{
    /// <summary>
    /// Extension methods for working with an <see cref="IDataReader"/>.
    /// </summary>
    public static partial class DataReaderExtensions
    {
        /// <summary>
        /// Provides strongly-typed access to each of the column values in the <see cref="IDataReader"/>.
        /// </summary>
        /// <typeparam name="T">The return type of the column.</typeparam>
        /// <param name="reader">
        /// The input <see cref="IDataReader"/>, which acts as the this
        /// instance for the extension method.
        /// </param>
        /// <param name="columnIndex">The column index.</param>
        /// <returns>
        /// The value, of type <typeparamref name="T"/>, of the column specified
        /// by <paramref name="columnIndex"/>.
        /// </returns>
        public static T Field<T>(this IDataReader reader, int columnIndex)
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

        /// <summary>
        /// Provides strongly-typed access to each of the column values in the <see cref="IDataReader"/>.
        /// </summary>
        /// <typeparam name="T">The return type of the column.</typeparam>
        /// <param name="reader">
        /// The input <see cref="IDataReader"/>, which acts as the this
        /// instance for the extension method.
        /// </param>
        /// <param name="columnName">The column name.</param>
        /// <returns>
        /// The value, of type <typeparamref name="T"/>, of the column specified
        /// by <paramref name="columnName"/>.
        /// </returns>
        public static T Field<T>(this IDataReader reader, string columnName)
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

        /// <summary>
        /// Provides strongly-typed access to each of the column values in the <see cref="IDataReader"/>.
        /// </summary>
        /// <typeparam name="T">The return type of the column.</typeparam>
        /// <param name="reader">
        /// The input <see cref="IDataReader"/>, which acts as the this
        /// instance for the extension method.
        /// </param>
        /// <param name="columnIndex">The column index.</param>
        /// <returns>
        /// The value, of type <typeparamref name="T"/>, of the column specified
        /// by <paramref name="columnIndex"/> or the default for
        /// <typeparamref name="T"/> if the field is not found.
        /// </returns>
        public static T FieldOrDefault<T>(this IDataReader reader, int columnIndex)
        {
            return FieldOrDefault(reader, columnIndex, default(T)!);
        }

        /// <summary>
        /// Provides strongly-typed access to each of the column values in the <see cref="IDataReader"/>.
        /// </summary>
        /// <typeparam name="T">The return type of the column.</typeparam>
        /// <param name="reader">
        /// The input <see cref="IDataReader"/>, which acts as the this
        /// instance for the extension method.
        /// </param>
        /// <param name="columnIndex">The column index.</param>
        /// <param name="nullValue">The value to use as the default.</param>
        /// <returns>
        /// The value, of type <typeparamref name="T"/>, of the column specified
        /// by <paramref name="columnIndex"/> or <paramref name="nullValue"/> if
        /// the field is not found.
        /// </returns>
        public static T FieldOrDefault<T>(this IDataReader reader, int columnIndex, T nullValue)
        {
            return reader.IsNullOrWhiteSpace(columnIndex) ? nullValue : reader.Field<T>(columnIndex);
        }

        /// <summary>
        /// Indicates if the value of the specified column in the
        /// <see cref="IDataReader"/> is <see langword="null"/>.
        /// </summary>
        /// <param name="reader">
        /// The input <see cref="IDataReader"/>, which acts as the this
        /// instance for the extension method.
        /// </param>
        /// <param name="columnIndex">The column index.</param>
        /// <returns>
        /// <see langword="true"/> if the value is <see langword="null"/>;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsNull(this IDataReader reader, int columnIndex)
        {
            if (!columnIndex.Between(0, reader.FieldCount))
            {
                return true;
            }

            return reader[columnIndex] == null;
        }

        /// <summary>
        /// Indicates if the value of the specified column in the
        /// <see cref="IDataReader"/> is <see langword="null"/> or <see cref="String.Empty"/>.
        /// </summary>
        /// <param name="reader">
        /// The input <see cref="IDataReader"/>, which acts as the this
        /// instance for the extension method.
        /// </param>
        /// <param name="columnIndex">The column index.</param>
        /// <returns>
        /// <see langword="true"/> if the value is <see langword="null"/> or
        /// <see cref="String.Empty"/>; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool IsNullOrWhiteSpace(this IDataReader reader, int columnIndex)
        {
            if (!columnIndex.Between(0, reader.FieldCount))
            {
                return true;
            }

            return reader.IsNull(columnIndex) || String.IsNullOrWhiteSpace(reader[columnIndex].ToString());
        }
    }
}