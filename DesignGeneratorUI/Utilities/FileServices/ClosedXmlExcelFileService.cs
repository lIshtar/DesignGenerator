using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGeneratorUI.FileServices
{
    internal class ClosedXmlExcelFileService : IFileService
    {
        public void OpenFile(string filename)
        {
            throw new NotImplementedException();
        }

        public void SaveToFile(string filename, IEnumerable<object> data)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Sheet1");

            if (!data.Any())
                throw new InvalidOperationException("Коллекция пуста");

            var firstItem = data.First();
            var properties = firstItem.GetType().GetProperties();

            // Заголовки
            for (int i = 0; i < properties.Length; i++)
            {
                worksheet.Cell(1, i + 1).Value = properties[i].Name;
            }

            // Данные
            int row = 2;
            foreach (var item in data)
            {
                for (int col = 0; col < properties.Length; col++)
                {
                    var value = properties[col].GetValue(item);
                    worksheet.Cell(row, col + 1).Value = value?.ToString() ?? string.Empty;
                }
                row++;
            }

            // Автоширина
            worksheet.Columns().AdjustToContents();

            workbook.SaveAs(filename);
        }
    }
}
