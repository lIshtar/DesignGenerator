using DesignGeneratorCore.Database.DBEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DesignGeneratorCore.Parsers
{
    public static class DescriptionParser
    {
        public static List<ImageDescription> ParseMany(string input)
        {
            var items = new List<ImageDescription>();
            var lines = input.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            ImageDescription currentItem = null;
            bool isReadingDescription = false;

            foreach (var line in lines)
            {
                string trimmedLine = line.Trim();

                if (trimmedLine == "-----") // Начало нового элемента
                {
                    if (currentItem != null)
                    {
                        items.Add(currentItem);
                    }
                    currentItem = new ImageDescription();
                    isReadingDescription = false;
                }
                else if (trimmedLine.StartsWith("Название:")) // Заголовок
                {
                    if (currentItem != null)
                    {
                        currentItem.Title = trimmedLine.Substring("Название:".Length).Trim();
                        isReadingDescription = true;
                    }
                }
                else if (isReadingDescription && currentItem != null) // Описание
                {
                    currentItem.Description += (currentItem.Description == null ? "" : "\n") + trimmedLine;
                }
            }

            if (currentItem != null) // Добавляем последний элемент
            {
                items.Add(currentItem);
            }

            foreach (var item in items)
            {
                if (IsEmpty(item)) throw new Exception($"Unable to parse incoming string. String:{input}");
            }

            return items;
        }

        public static ImageDescription ParseOne(string input)
        {
            return new ImageDescription { Description=input, Title=""};
            throw new NotImplementedException();
            //var item = new ImageDescription();
            //var lines = input.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            //bool isReadingDescription = false;

            //foreach (var line in lines)
            //{
            //    string trimmedLine = line.Trim();

            //    if (trimmedLine == "-----") // Начало нового элемента
            //    {
            //        if (currentItem != null)
            //        {
            //            items.Add(currentItem);
            //        }
            //        currentItem = new ImageDescription();
            //        isReadingDescription = false;
            //    }
            //    else if (trimmedLine.StartsWith("Название:")) // Заголовок
            //    {
            //        if (currentItem != null)
            //        {
            //            currentItem.Title = trimmedLine.Substring("Название:".Length).Trim();
            //            isReadingDescription = true;
            //        }
            //    }
            //    else if (isReadingDescription && currentItem != null) // Описание
            //    {
            //        currentItem.Description += (currentItem.Description == null ? "" : "\n") + trimmedLine;
            //    }
            //}

            //if (currentItem != null) // Добавляем последний элемент
            //{
            //    items.Add(currentItem);
            //}

            //foreach (var item in items)
            //{
            //    if (IsEmpty(item)) throw new Exception($"Unable to parse incoming string. String:{input}");
            //}

            //return items;
        }

        private static bool IsEmpty(ImageDescription imageDescription)
        {
            return string.IsNullOrEmpty(imageDescription.Title) || string.IsNullOrEmpty(imageDescription.Description);
        }
    }
}
