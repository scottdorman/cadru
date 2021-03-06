﻿//------------------------------------------------------------------------------
// <copyright file="ExcelDataWriter.cs"
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

using System.Collections.Generic;
using System.Linq;

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Cadru.Data.Excel
{
    /// <summary>
    /// Represents a writer for Excel data
    /// </summary>
    public class ExcelDataWriter
    {
        private readonly SpreadsheetDocument document;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExcelDataWriter"/> class.
        /// </summary>
        /// <param name="path"></param>
        public ExcelDataWriter(string path)
        {
            this.document = SpreadsheetDocument.Create(path, SpreadsheetDocumentType.Workbook);
            this.document.AddWorkbookPart();
            this.document.WorkbookPart.Workbook = new Workbook();
        }

        /// <summary>
        /// Adds a new worksheet to the spreadsheet.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="headers"></param>
        /// <param name="data"></param>
        public void AddWorksheet<T>(string name, IEnumerable<string> headers, IEnumerable<T> data)
        {
            var sheet = this.AddWorksheet(name);
        }

        private Sheet AddWorksheet(string sheetName)
        {
            var newWorksheetPart = this.document.WorkbookPart.AddNewPart<WorksheetPart>();
            newWorksheetPart.Worksheet = new Worksheet(new SheetData());
            var sheets = this.document.WorkbookPart.Workbook.GetFirstChild<Sheets>();
            var relationshipId = this.document.WorkbookPart.GetIdOfPart(newWorksheetPart);
            var sheetId = this.GetUniqueSheetId(sheets);

            var sheet = new Sheet() { Id = relationshipId, SheetId = sheetId, Name = $"{sheetName}{sheetId}" };
            sheets.Append(sheet);
            return sheet;
        }

        private uint GetUniqueSheetId(Sheets sheets)
        {
            uint sheetId = 1;
            if (sheets.Elements<Sheet>().Count() > 0)
            {
                sheetId = sheets.Elements<Sheet>().Select(s => s.SheetId.Value).Max() + 1;
            }

            return sheetId;
        }
    }
}