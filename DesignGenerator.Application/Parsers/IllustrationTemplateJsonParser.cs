using DesignGenerator.Application.Interfaces;
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
            return illustrations;
        }

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
            return null;
        }
    }
}
