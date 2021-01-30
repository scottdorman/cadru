//------------------------------------------------------------------------------
// <copyright file="ExcelDataReader.IDataReader.cs"
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
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;

    using Cadru.Extensions;

    using DocumentFormat.OpenXml;
    using DocumentFormat.OpenXml.Packaging;
    using DocumentFormat.OpenXml.Spreadsheet;

    public partial class ExcelDataReader : IDataReader
    {
        /// <inheritdoc/>
        public int Depth => 0;

        /// <inheritdoc/>
        public int RecordsAffected => -1;

        /// <inheritdoc/>
        public static int GetColumnIndexByName(string colName)
        {
            var name = GetStartingLettersOnly(colName);
            int number = 0, pow = 1;
            for (var i = name.Length - 1; i >= 0; i--)
            {
                number += (name[i] - 'A' + 1) * pow;
                pow *= 26;
            }

            return number - 1;
        }

        /// <inheritdoc/>
        public void Close() => this.Dispose();

        /// <inheritdoc/>
        public void Dispose()
        {
            if (this.openXmlReader != null)
            {
                this.openXmlReader.Dispose();
            }

            if (this.document != null)
            {
                this.document.Dispose();
            }
        }

        /// <inheritdoc/>
        public DataTable GetSchemaTable() => throw new NotSupportedException();

        /// <summary>
        /// Gets a value indicating if the current row is empty.
        /// </summary>
        /// <returns><see langword="true"/> if the current row is empty; otherwise, <see langword="false"/>.</returns>
        public bool IsCurrentRowEmpty()
        {
            return this.currentRowData == null || !this.currentRowData.Any();
        }

        /// <inheritdoc/>
        public bool NextResult()
        {
            if (this.CurrentSheetIndex >= this.ResultsCount - 1)
            {
                return false;
            }

            this.Reset();
            this.CurrentSheetIndex++;
            return true;
        }

        /// <inheritdoc/>
        public bool Read()
        {
            this.FirstRead();

            OpenXmlElement? currentRow = null;

            while (this.openXmlReader!.Read())
            {
                if (this.openXmlReader.ElementType == typeof(Row))
                {
                    currentRow = this.openXmlReader.LoadCurrentElement();
                    if (Int32.TryParse(currentRow.GetAttribute("r", String.Empty).Value, out var rowIndex))
                    {
                        this.CurrentRowIndex = rowIndex;
                    }

                    if (this.IsRowEmpty(currentRow))
                    {
                        continue;
                    }

                    this.currentRowData = AdjustRow(currentRow, this.FieldNames.Count);
                    break;
                }
            }

            return currentRow != null && !this.openXmlReader.EOF;
        }

        private static IEnumerable<Cell> AdjustRow(OpenXmlElement row, int capacity)
        {
            var currentCount = 0;
            foreach (var cell in row.Descendants<Cell>())
            {
                var currentColumnIndex = GetColumnIndexByName(cell.CellReference);

                for (; currentCount < currentColumnIndex; currentCount++)
                {
                    yield return new Cell();
                }

                yield return cell;
                currentCount++;
            }

            for (; currentCount < capacity; currentCount++)
            {
                yield return new Cell();
            }
        }

        private static IList<string> GetRangeHeaders(OpenXmlPart worksheetPart)
        {
            var count = 0;
            using (var reader = OpenXmlReader.Create(worksheetPart))
            {
                while (reader.Read())
                {
                    if (reader.ElementType == typeof(Row))
                    {
                        count = reader.LoadCurrentElement().Elements<Cell>().Count();
                        break;
                    }
                }
            }

            return Enumerable.Range(0, count).Select(x => "col" + x).ToList();
        }

        private static string GetStartingLettersOnly(string colName)
        {
            var result = new StringBuilder();
            foreach (var ch in colName)
            {
                if (Char.IsLetter(ch))
                {
                    result.Append(ch);
                }
                else
                {
                    break;
                }
            }

            return result.ToString();
        }

        private void FirstRead()
        {
            if (this.firstRead)
            {
                this.currentSheet = this.GetSheetByIndex(this.CurrentSheetIndex);
                var currentWorksheetPart = this.document.WorkbookPart.GetPartById(this.CurrentSheetId);
                this.openXmlReader = OpenXmlReader.Create(currentWorksheetPart);
                this.SkipRows(this.GetEmptyRowsCount(currentWorksheetPart));
                this.FieldNames.AddRange(this.FirstRowAsHeader ? this.GetFirstRowAsHeaders(currentWorksheetPart) : GetRangeHeaders(currentWorksheetPart));
                this.firstRead = false;
            }
        }

        private string GetCellValue(CellType cell)
        {
            var value = cell?.CellValue?.InnerXml;
            if (value == null)
            {
                return String.Empty;
            }

            if (Int32.TryParse(value, out var index) && cell?.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                return this.sharedStrings?[index] ?? String.Empty;
            }

            return value;
        }

        private int GetEmptyRowsCount(OpenXmlPart worksheetPart)
        {
            var emptyRowsCount = 0;
            using (var reader = OpenXmlReader.Create(worksheetPart))
            {
                while (reader.Read())
                {
                    if (reader.ElementType == typeof(Row))
                    {
                        var row = reader.LoadCurrentElement();
                        if (!this.IsRowEmpty(row))
                        {
                            break;
                        }

                        emptyRowsCount++;
                    }
                }
            }

            return emptyRowsCount;
        }

        private IList<string> GetFirstRowAsHeaders(OpenXmlPart worksheetPart)
        {
            var result = new List<string>();
            using (var reader = OpenXmlReader.Create(worksheetPart))
            {
                while (reader.Read())
                {
                    if (reader.ElementType == typeof(Row))
                    {
                        result = AdjustRow(reader.LoadCurrentElement(), -1).Select(this.GetCellValue).ToList();
                        break;
                    }
                }
            }

            this.SkipRow();
            return result;
        }

        private Sheet? GetSheetByIndex(int sheetIndex)
        {
            return this.sheets.ElementAtOrDefault(sheetIndex);
        }

        private Sheet? GetSheetByName(string sheetName)
        {
            return this.sheets.FirstOrDefault(x => x.Name == sheetName);
        }

        private IList<Sheet> GetSheets(SpreadsheetDocument document)
        {
            return document.WorkbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>().ToList();
        }

        private bool IsRowEmpty(OpenXmlElement row)
        {
            return String.IsNullOrEmpty(row.InnerText);
        }

        private void Reset(bool resetCurrentIndex = false)
        {
            if (resetCurrentIndex)
            {
                this.CurrentSheetIndex = 0;
            }

            this.CurrentRowIndex = null;
            this.currentRowData = null;
            this.currentSheet = null;
            this.FieldNames.Clear();
            this.firstRead = true;
        }

        private void SkipRow()
        {
            while (this.openXmlReader!.Read())
            {
                if (this.openXmlReader.ElementType == typeof(Row) && this.openXmlReader.IsEndElement)
                {
                    break;
                }
            }
        }

        private void SkipRows(int count)
        {
            for (var i = 0; i < count; i++)
            {
                this.SkipRow();
            }
        }
    }
}