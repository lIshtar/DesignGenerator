using DesignGenerator.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DesignGenerator.Application.Parsers
{
    public class IllustrationTemplateParser : ITemplateParser
    {
        public List<IllustrationTemplate> ParseMany(string input)
        {
            var items = new List<IllustrationTemplate>();
            var lines = input.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            IllustrationTemplate currentItem = null;
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
                    currentItem = new IllustrationTemplate();
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
                    currentItem.Prompt += (currentItem.Prompt == null ? "" : "\n") + trimmedLine;
                }
            }

            if (currentItem != null) // Добавляем последний элемент
            {
                items.Add(currentItem);
            }

            return items;
        }

        public IllustrationTemplate ParseOne(string input)
        {
            return new IllustrationTemplate();
            //throw new NotImplementedException();
        }

        private static bool IsEmpty(IllustrationTemplate imageDescription)
        {
            return string.IsNullOrEmpty(imageDescription.Title) || string.IsNullOrEmpty(imageDescription.Prompt);
        }
    }
}
