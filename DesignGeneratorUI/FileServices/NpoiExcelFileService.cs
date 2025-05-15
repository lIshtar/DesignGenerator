using NPOI.SS.UserModel;
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
            IDictionary<string, object> dict = firstItem as IDictionary<string, object>;
            var creationHelper = workbook.GetCreationHelper();

            if (dict == null)
            {
                // Стандартная обработка обычных объектов
                var properties = firstItem.GetType().GetProperties();

                var headerRow = sheet.CreateRow(0);
                for (int i = 0; i < properties.Length; i++)
                {
                    headerRow.CreateCell(i).SetCellValue(properties[i].Name);
                }

                int rowIndex = 1;
                foreach (var item in data)
                {
                    var row = sheet.CreateRow(rowIndex++);
                    for (int i = 0; i < properties.Length; i++)
                    {
                        var propertyName = properties[i].Name;
                        var value = properties[i].GetValue(item);
                        if (propertyName.Equals("Path", StringComparison.OrdinalIgnoreCase) && value is string path && !string.IsNullOrEmpty(path))
                        {
                            var link = creationHelper.CreateHyperlink(HyperlinkType.File);
                            link.Address = path;
                            var cell = row.CreateCell(i);
                            cell.Hyperlink = link;
                            cell.SetCellValue("image");
                            var linkStyle = workbook.CreateCellStyle();
                            var font = workbook.CreateFont();
                            font.Underline = FontUnderlineType.Single;
                            font.Color = IndexedColors.Blue.Index;
                            linkStyle.SetFont(font);
                            cell.CellStyle = linkStyle;
                        }
                        else
                        {
                            row.CreateCell(i).SetCellValue(value?.ToString() ?? "");
                        }
                    }
                }

                for (int i = 0; i < properties.Length; i++)
                {
                    sheet.AutoSizeColumn(i);
                }
            }
            else
            {
                // Обработка dynamic-объектов
                var keys = dict.Keys.ToList();

                var headerRow = sheet.CreateRow(0);
                for (int i = 0; i < keys.Count; i++)
                {
                    headerRow.CreateCell(i).SetCellValue(keys[i]);
                }

                int rowIndex = 1;
                foreach (IDictionary<string, object> item in data)
                {
                    var row = sheet.CreateRow(rowIndex++);
                    for (int i = 0; i < keys.Count; i++)
                    {
                        var value = item[keys[i]];
                        var propertyName = keys[i];
                        if (propertyName.Equals("Path", StringComparison.OrdinalIgnoreCase) && value is string path && !string.IsNullOrEmpty(path))
                        {
                            var link = creationHelper.CreateHyperlink(HyperlinkType.File);
                            link.Address = path;
                            var cell = row.CreateCell(i);
                            cell.Hyperlink = link;
                            cell.SetCellValue("image");
                            var linkStyle = workbook.CreateCellStyle();
                            var font = workbook.CreateFont();
                            font.Underline = FontUnderlineType.Single;
                            font.Color = IndexedColors.Blue.Index;
                            linkStyle.SetFont(font);
                            cell.CellStyle = linkStyle;
                        }
                        else
                        {
                            row.CreateCell(i).SetCellValue(value?.ToString() ?? "");
                        }
                    }
                }

                for (int i = 0; i < keys.Count; i++)
                {
                    sheet.AutoSizeColumn(i);
                }
            }

            using (var fileStream = new FileStream(filename, FileMode.Create, FileAccess.Write))
            {
                workbook.Write(fileStream);
            }
        }
    }
}
