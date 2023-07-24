using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Application.ViewModel.ClosedXML
{
    public interface ICreWorksheet
    {
        public WorksheetWithWorkbook CreateWorksheet(string filePath, string wsName);
    }
}
