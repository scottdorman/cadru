//------------------------------------------------------------------------------
// <copyright file="ExcelDataReader.cs"
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
using System.Collections.Generic;
using System.IO;
using System.Linq;

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Cadru.Data.Excel
{
    /// <summary>
    /// Represents a reader that provides fast, non-cached, forward-only access
    /// to data contained in an Excel spreadsheet (.xlsx) file.
    /// </summary>
    public sealed partial class ExcelDataReader
    {
        private readonly SpreadsheetDocument document;
        private readonly IDictionary<int, string>? sharedStrings;
        private readonly IList<Sheet> sheets;
        private IEnumerable<Cell>? currentRowData;
        private Sheet? currentSheet;
        private bool firstRead = true;
        private OpenXmlReader? openXmlReader;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="DataReaderExtensions"/> class.
        /// </summary>
        /// <param name="path">The path to the Excel spreadsheet file.</param>
        public ExcelDataReader(string path) : this(SpreadsheetDocument.Open(path, false))
        {
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="DataReaderExtensions"/> class.
        /// </summary>
        /// <param name="stream">
        /// An unopened stream to the Excel spreadsheet file.
        /// </param>
        public ExcelDataReader(Stream stream) : this(SpreadsheetDocument.Open(stream, false))
        {
        }

        private ExcelDataReader(SpreadsheetDocument document)
        {
            this.document = document;
            this.sheets = this.GetSheets(this.document);
            this.sharedStrings = GetSharedStrings(this.document);
            this.FieldNames = new List<string>();
        }

        /// <summary>
        /// Gets the index of the current row in the spreadsheet.
        /// </summary>
        public int? CurrentRowIndex { get; private set; } = null;

        /// <summary>
        /// Gets the current sheet Id.
        /// </summary>
        public string CurrentSheetId => this.currentSheet?.Id ?? String.Empty;

        /// <summary>
        /// Gets the index of the current sheet in the spreadsheet.
        /// </summary>
        public int CurrentSheetIndex { get; private set; } = 0;

        /// <summary>
        /// Gets the current sheet name.
        /// </summary>
        public string CurrentSheetName => this.currentSheet?.Name ?? String.Empty;

        /// <summary>
        /// Gets the column header names.
        /// </summary>
        public IList<string> FieldNames { get; }

        /// <summary>
        /// Gets or sets a value indicating whether or not the first row
        /// contains column header names.
        /// </summary>
        public bool FirstRowAsHeader
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a value indicating if the reader is closed.
        /// </summary>
        public bool IsClosed => this.document == null;

        /// <summary>
        /// Gets a value indicating the number of sheets read.
        /// </summary>
        public int ResultsCount => this.sheets?.Count() ?? -1;

        /// <summary>
        /// Gets the sheet names.
        /// </summary>
        public IEnumerable<string> SheetNames => this.sheets?.Select(s => s.Name.Value) ?? Enumerable.Empty<string>();

        /// <summary>
        /// Gets a value from the current sheet by name.
        /// </summary>
        /// <param name="name">The column header name of the value to get.</param>
        /// <returns>The value associated with the column header.</returns>
        public object this[string name] => this[this.GetOrdinal(name)];

        /// <summary>
        /// Gets a value from the current sheet by index.
        /// </summary>
        /// <param name="i">The column index of the value to get.</param>
        /// <returns>The value at the column index.</returns>
        public object this[int i] => this.GetValue(i);

        /// <summary>
        /// Advances the reader to the named sheet.
        /// </summary>
        /// <param name="sheetName">The name of the sheet to advance to.</param>
        /// <param name="firstRowAsHeader">
        /// Indicates whether or not the first row contains column header names.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the reader was able to advance; otherwise, <see langword="false"/>.
        /// </returns>
        public bool NextResult(string sheetName, bool firstRowAsHeader)
        {
            this.FirstRowAsHeader = firstRowAsHeader;
            var sheet = this.GetSheetByName(sheetName);

            if (sheet == null)
            {
                return false;
            }

            this.Reset();
            this.CurrentSheetIndex = this.sheets.IndexOf(sheet);
            this.FirstRead();
            return true;
        }

        /// <summary>
        /// Advances the reader to the named sheet.
        /// </summary>
        /// <param name="sheetName">The name of the sheet to advance to.</param>
        /// <returns>
        /// <see langword="true"/> if the reader was able to advance; otherwise, <see langword="false"/>.
        /// </returns>
        public bool NextResult(string sheetName)
        {
            return this.NextResult(sheetName, false);
        }

        private static IDictionary<int, string>? GetSharedStrings(SpreadsheetDocument document)
        {
            return document.WorkbookPart.SharedStringTablePart?.SharedStringTable.Select((x, i) => System.Tuple.Create(i, x.InnerText)).ToDictionary(x => x.Item1, x => x.Item2);
        }
    }
}