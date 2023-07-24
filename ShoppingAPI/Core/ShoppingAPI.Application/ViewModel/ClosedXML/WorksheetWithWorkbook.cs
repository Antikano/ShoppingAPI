using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Application.ViewModel.ClosedXML
{
    public class WorksheetWithWorkbook
    {
        public IXLWorksheet Worksheet { get; set; }
        public XLWorkbook Workbook { get; set; }
    }
}
