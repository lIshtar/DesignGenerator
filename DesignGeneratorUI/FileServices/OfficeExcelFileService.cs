using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGeneratorUI.FileServices
{
    internal class OfficeExcelFileService : IFileService
    {
        public void OpenFile(string filename)
        {
            throw new NotImplementedException();
        }

        public void SaveToFile(string filename, IEnumerable<object> data)
        {
            if (data == null || !data.Any())
                return;

            var excelApp = new Microsoft.Office.Interop.Excel.Application();
            var workbook = excelApp.Workbooks.Add();
            var worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[1];

            var type = data.First().GetType();
            var properties = type.GetProperties();

            // Заголовки
            for (int i = 0; i < properties.Length; i++)
            {
                worksheet.Cells[1, i + 1] = properties[i].Name;
            }

            // Данные
            int row = 2;
            foreach (var item in data)
            {
                for (int col = 0; col < properties.Length; col++)
                {
                    var value = properties[col].GetValue(item);
                    worksheet.Cells[row, col + 1] = value?.ToString() ?? "";
                }
                row++;
            }

            worksheet.Columns.AutoFit();

            try
            {
                workbook.SaveAs(filename);
            }
            finally
            {
                workbook.Close();
                excelApp.Quit();

                // Освобождаем COM-объекты
                System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
            }
        }

    }
}
