using DesignGenerator.Application.Interfaces;
using DesignGenerator.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using JsonException = Newtonsoft.Json.JsonException;

namespace DesignGenerator.Application.Parsers
{
    public class IllustrationTemplateJsonParser : ITemplateParser
    {
        private static readonly Regex JsonRegex = new Regex("\\{.*?\\}|\\[.*?\\]", RegexOptions.Compiled | RegexOptions.Singleline);
        public List<IllustrationTemplate> ParseMany(string input)
        {
            var illustrations = new List<IllustrationTemplate>();
            foreach (Match match in JsonRegex.Matches(input))
            {
                try
                {
                    var parsedObjects = JsonConvert.DeserializeObject<List<IllustrationTemplate>>(match.Value);
                    if (parsedObjects != null)
                    {
                        illustrations.AddRange(parsedObjects);
                    }
                }
                catch (JsonException)
                {
                    try
                    {
                        var singleIllustration = JsonConvert.DeserializeObject<IllustrationTemplate>(match.Value);
                        if (singleIllustration != null && !string.IsNullOrEmpty(singleIllustration.Title) && !string.IsNullOrEmpty(singleIllustration.Prompt))
                        {
                            illustrations.Add(singleIllustration);
                        }
                    }
                    catch (JsonException)
                    {
                        // Пропускаем некорректные JSON-объекты
                    }
                }
            }
            if (illustrations.Count > 0)
                return illustrations;

            // Основной паттерн поиска блоков title ... prompt ...
            string pattern = @"(?i)\btitle\b\s*[:\-]*\s*(.*?)\s*(?=\bprompt\b)(?i)(?:prompt)\s*[:\-]*\s*(.*?)(?=(?i:\btitle\b)|\z)";

            var matches = Regex.Matches(input, pattern, RegexOptions.Singleline);

            foreach (Match match in matches)
            {
                if (match.Groups.Count >= 3)
                {
                    string rawTitle = match.Groups[1].Value.Trim();
                    string rawPrompt = match.Groups[2].Value.Trim();

                    string cleanTitle = TrimPunctuation(rawTitle);
                    string cleanPrompt = TrimPunctuation(rawPrompt);

                    illustrations.Add(new IllustrationTemplate
                    {
                        Title = cleanTitle,
                        Prompt = cleanPrompt
                    });
                }
            }

            if (illustrations.Count > 0)
                return illustrations;



            return illustrations;
        }

        private static string TrimEdgePunctuation(string input)
        {
            return Regex.Replace(input, @"^\W+|\W+$", "").Trim();
            // Удаляем пунктуацию только по краям строки
            //int start = 0, end = input.Length - 1;

            //while (start <= end && char.IsPunctuation(input[start])) start++;
            //while (end >= start && char.IsPunctuation(input[end])) end--;

            //return input.Substring(start, end - start + 1).Trim();
        }

        private static string TrimPunctuation(string input)
        {
            return TrimEdgePunctuation(input.Trim().Trim(PunctuationChars));
        }

        private static readonly char[] PunctuationChars = new char[]
        {
        '#', '*', '"', '\'', ':', '-', '_', '—', '–', ' '
        };

        public IllustrationTemplate ParseOne(string input)
        {
            foreach (Match match in JsonRegex.Matches(input))
            {
                try
                {
                    var parsedObjects = JsonConvert.DeserializeObject<List<IllustrationTemplate>>(match.Value);
                    if (parsedObjects != null && parsedObjects.Count > 0)
                    {
                        return parsedObjects[0];
                    }
                }
                catch (JsonException)
                {
                    try
                    {
                        var singleIllustration = JsonConvert.DeserializeObject<IllustrationTemplate>(match.Value);
                        if (singleIllustration != null && !string.IsNullOrEmpty(singleIllustration.Title) && !string.IsNullOrEmpty(singleIllustration.Prompt))
                        {
                            return singleIllustration;
                        }
                    }
                    catch (JsonException)
                    {
                        // Пропускаем некорректные JSON-объекты
                    }
                }
            }
            // Основной паттерн поиска блоков title ... prompt ...
            string pattern = @"(?i)\btitle\b\s*[:\-]*\s*(.*?)\s*(?=\bprompt\b)(?i)(?:prompt)\s*[:\-]*\s*(.*?)(?=(?i:\btitle\b)|\z)";

            var matches = Regex.Matches(input, pattern, RegexOptions.Singleline);

            foreach (Match match in matches)
            {
                if (match.Groups.Count >= 3)
                {
                    string rawTitle = match.Groups[1].Value.Trim();
                    string rawPrompt = match.Groups[2].Value.Trim();

                    string cleanTitle = TrimPunctuation(rawTitle);
                    string cleanPrompt = TrimPunctuation(rawPrompt);

                    return new IllustrationTemplate
                    {
                        Title = cleanTitle,
                        Prompt = cleanPrompt
                    };
                }
            }

            return null;
        }
    }
}
