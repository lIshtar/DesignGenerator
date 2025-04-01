using DesignGenerator.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Infrastructure
{
    public class ImageDownloader : IImageDownloader
    {

        private readonly HttpClient client = new HttpClient();

        public async Task DownloadImageAsync(string imageUrl, DirectoryInfo saveFolder)
        {
            try
            {
                // Убедимся, что папка существует
                if (!saveFolder.Exists)
                {
                    saveFolder.Create();
                }

                // Получаем расширение файла
                string extension = Path.GetExtension(new Uri(imageUrl).AbsolutePath);
                if (string.IsNullOrEmpty(extension))
                {
                    extension = ".jpg"; // Подставляем расширение по умолчанию, если его нет в URL
                }

                // Генерируем имя файла на основе GUID, чтобы избежать конфликтов
                string fileName = $"{Guid.NewGuid()}{extension}";
                string savePath = Path.Combine(saveFolder.FullName, fileName);

                // Загружаем изображение
                byte[] imageBytes = await client.GetByteArrayAsync(imageUrl);

                // Сохраняем файл
                await File.WriteAllBytesAsync(savePath, imageBytes);

                Console.WriteLine($"Изображение сохранено: {savePath}");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Ошибка HTTP: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке изображения: {ex.Message}");
            }
        }
    }
}
