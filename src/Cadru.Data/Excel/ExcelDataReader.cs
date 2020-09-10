﻿//------------------------------------------------------------------------------
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

namespace Cadru.Data.Excel
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using DocumentFormat.OpenXml;
    using DocumentFormat.OpenXml.Packaging;
    using DocumentFormat.OpenXml.Spreadsheet;

    public partial class ExcelDataReader
    {
        private int currentIndex = 0;
        private int? currentRowIndex = null;
        private IEnumerable<Cell> currentRowData;
        private Sheet currentSheet;
        private readonly SpreadsheetDocument document;
        private IList<string> headers;
        private readonly IDictionary<int, string> sharedStrings;
        private readonly IList<Sheet> sheets;
        private OpenXmlReader reader;
        private bool firstRead = true;

        public ExcelDataReader(string path)
        {
            this.document = SpreadsheetDocument.Open(path, false);
            this.sheets = this.GetSheets(this.document);
            this.sharedStrings = GetSharedStrings(this.document);
        }

        public ExcelDataReader(Stream stream)
        {
            this.document = SpreadsheetDocument.Open(stream, false);
            this.sheets = this.GetSheets(this.document);
            this.sharedStrings = GetSharedStrings(this.document);
        }

        public object this[string name] => this[this.GetOrdinal(name)];

        public object this[int i] => this.GetValue(i);

        public IEnumerable<string> FieldNames => this.headers;

        public bool FirstRowAsHeader
        {
            get;
            set;
        }

        public bool IsClosed => this.document == null;

        public int ResultsCount => this.sheets?.Count() ?? -1;

        public int? CurrentRowIndex => this.currentRowIndex;

        public string CurrentSheetName => this.currentSheet?.Name ?? String.Empty;

        public string CurrentSheetId => this.currentSheet?.Id ?? String.Empty;

        public int CurrentSheetIndex => this.currentIndex;

        public IEnumerable<string> SheetNames => this.sheets?.Select(s => s.Name.Value) ?? Enumerable.Empty<string>();

        public bool NextResult(string sheetName, bool firstRowAsHeader)
        {
            this.FirstRowAsHeader = firstRowAsHeader;
            var sheet = this.GetSheetByName(sheetName);

            if (sheet == null)
            {
                return false;
            }

            this.Reset();
            this.currentIndex = this.sheets.IndexOf(sheet);
            this.FirstRead();
            return true;
        }

        public bool NextResult(string sheetName)
        {
            return this.NextResult(sheetName, false);
        }

        private static IDictionary<int, string> GetSharedStrings(SpreadsheetDocument document)
        {
            return document.WorkbookPart.SharedStringTablePart?.SharedStringTable.Select((x, i) => System.Tuple.Create(i, x.InnerText)).ToDictionary(x => x.Item1, x => x.Item2);
        }
    }
}
