using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Cadru.Data.Excel
{
    public class ExcelDataWriter
    {
        private SpreadsheetDocument document;

        public ExcelDataWriter(string path)
        {
            this.document = SpreadsheetDocument.Create(path, SpreadsheetDocumentType.Workbook);
            this.document.AddWorkbookPart();
            this.document.WorkbookPart.Workbook = new Workbook();
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

        private Sheet AddWorksheet(string sheetName)
        {
            var newWorksheetPart = this.document.WorkbookPart.AddNewPart<WorksheetPart>();
            newWorksheetPart.Worksheet = new Worksheet(new SheetData());
            var sheets = this.document.WorkbookPart.Workbook.GetFirstChild<Sheets>();
            var relationshipId = this.document.WorkbookPart.GetIdOfPart(newWorksheetPart);
            var sheetId = GetUniqueSheetId(sheets);

            var sheet = new Sheet() { Id = relationshipId, SheetId = sheetId, Name = $"{sheetName}{sheetId}" };
            sheets.Append(sheet);
            return sheet;
        }

        public void AddWorksheet<T>(string name, IEnumerable<string> headers, IEnumerable<T> data)
        {
            var sheet = AddWorksheet(name);

        }
    }
}
