using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rise.Rabbitmq.Consumer.Helpers
{
    public class ExcelHelper
    {
        public bool Export<T>(List<T> list, string file, string sheetName)
        {
            bool exported = false;
            using (IXLWorkbook workbook = new XLWorkbook())
            {
                workbook.AddWorksheet(sheetName).FirstCell().InsertTable<T>(list, false);
                workbook.SaveAs(file);
                exported = true;
            }
            return exported;
        }
    }
}
