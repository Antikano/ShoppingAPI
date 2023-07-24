using ClosedXML.Excel;
using ShoppingAPI.Application.ViewModel.ClosedXML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Infrastructure.Services.ExportToDoc
{
    public class CreWorksheet : ICreWorksheet
    {
        public WorksheetWithWorkbook CreateWorksheet(string filePath, string wsName)
        {

            XLWorkbook workbook;
            if (File.Exists(filePath))
            {
                workbook = new XLWorkbook(filePath);
            }
            else
            {
                workbook = new XLWorkbook();
            }

            var worksheet = workbook.Worksheets.FirstOrDefault(ws => ws.Name == wsName);

            

            return new WorksheetWithWorkbook
            {
                Worksheet = worksheet,
                Workbook = workbook
            };
        }
    }
}
