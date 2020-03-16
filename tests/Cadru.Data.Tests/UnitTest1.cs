using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Cadru.Data.Excel;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cadru.Env.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod, Ignore]
        public void TestMethod1()
        {
            //var filePath = @"D:\Users\Scott\Source\github\cadru\tests\Cadru.Data.Tests\Plan Assignment DELETE File 20170615.xlsx";
            var filePath = @"D:\Users\Scott\Source\github\cadru\tests\Cadru.Data.Tests\Adjustments Table Upload 20170616.xlsx";

            using (var stream = File.OpenRead(filePath))
            {
                using (var reader = new ExcelDataReader(stream))
                {
                    reader.FirstRowAsHeader = true;
                    reader.Read();
                    var sheetName = reader.CurrentSheetName;

                    reader.Read();

                    var empty = IsRowEmpty(reader, 0, Enumerable.Empty<int>());

                    var fiscalYear = reader.Field<int>(0);
                    var empId = reader.Field<string>(1);
                    var startDate = reader.Field<DateTime>(2);
                    var status = reader.Field<int?>(6);
                    var monthlyRate = reader.Field<decimal?>(7);
                    var orgUnit = reader.Field<string>(10);

                    var s1 = reader.ToDelimitedString();
                    var s2 = reader.ToDelimitedString(true);

                    var action = "I";
                    var index = -1;
                    if (reader.FieldNames.Contains("ImportAction"))
                    {
                        index = reader.GetOrdinal("ImportAction");
                    }
                    else if (reader.FieldNames.Contains("Action"))
                    {
                        index = reader.GetOrdinal("Action");
                    }

                    if (index != -1)
                    {
                        action = reader.GetString(index) ?? "I";
                    }

                    reader.Close();
                }
            }
        }

        protected bool IsRowEmpty(ExcelDataReader reader, decimal count, IEnumerable<int> excludedColumns)
        {
            var itemArray = new object[reader.FieldCount];
            reader.GetValues(itemArray);
            var emptyRowFound = itemArray.Where((r, d) => excludedColumns.Contains(d)).All(v => v == DBNull.Value);
            if (emptyRowFound)
            {
            }

            return emptyRowFound;
        }
    }
}
