using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGeneratorUI.FileServices
{
    internal class NpoiExcelFileService : IFileService
    {
        public void OpenFile(string filename)
        {
            throw new NotImplementedException();
        }

        public void SaveToFile(string filename, IEnumerable<object> data)
        {
            if (!data.Any())
                return;

            var workbook = new XSSFWorkbook();
            var sheet = workbook.CreateSheet("Sheet1");

            var firstItem = data.First();
            var properties = firstItem.GetType().GetProperties();

            // Заголовки
            var headerRow = sheet.CreateRow(0);
            for (int i = 0; i < properties.Length; i++)
            {
                headerRow.CreateCell(i).SetCellValue(properties[i].Name);
            }

            // Данные
            int rowIndex = 1;
            foreach (var item in data)
            {
                var row = sheet.CreateRow(rowIndex++);
                for (int i = 0; i < properties.Length; i++)
                {
                    var value = properties[i].GetValue(item);
                    row.CreateCell(i).SetCellValue(value?.ToString() ?? "");
                }
            }

            // Автоширина колонок
            for (int i = 0; i < properties.Length; i++)
            {
                sheet.AutoSizeColumn(i);
            }

            // Сохраняем файл
            using (var fileStream = new FileStream(filename, FileMode.Create, FileAccess.Write))
            {
                workbook.Write(fileStream);
            }
        }
    }
}
