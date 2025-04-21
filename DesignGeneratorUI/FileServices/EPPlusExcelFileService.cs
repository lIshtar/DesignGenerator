using Microsoft.Win32;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace DesignGeneratorUI.FileServices
{
    public class EPPlusExcelFileService : IFileService
    {
        public void OpenFile(string filename)
        {
            throw new NotImplementedException();
        }

        public void SaveToFile(string filename, IEnumerable<object> data)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using var package = new ExcelPackage(new FileInfo(filename));
            package.Workbook.Worksheets.Delete("Sheet1");
            var workSheet = package.Workbook.Worksheets.Add("Sheet1");
            //var typedList = data.Cast<dynamic>().ToList();
            workSheet.Cells[1, 1].LoadFromCollection(data, false);
            package.Save();
        }
    }
}
