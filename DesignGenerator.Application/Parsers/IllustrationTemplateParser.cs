using DesignGenerator.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DesignGenerator.Application.Parsers
{
    //TODO: Добавить парсер от пользователя

    /// <summary>
    /// Represents a parsed image generation template with a user-defined title and prompt text.
    /// Used for feeding structured data to the image generation pipeline.
    /// </summary>
    public record IllustrationTemplate(string Title, string Prompt);

    /// <summary>
    /// Attempts to parse raw text into a collection of illustration templates (title + prompt).
    /// Supports multiple formats including JSON, key-value patterns, and fallback heuristics.
    /// </summary>
    public class IllustrationTemplateParser
    {
        public IllustrationTemplateParser()
        {
            strategies = new()
            {
                TryParseJson,
                TryParseKeyValueFormat,
                TryParseDelimitedLines
            };
        }

        private List<Func<string, IEnumerable<IllustrationTemplate>>> strategies;
        /// <summary>
        /// Parses multiple templates from a block of text, using available strategies.
        /// </summary>
        /// <param name="text">The input text from the LLM or user.</param>
        /// <returns>A collection of extracted templates.</returns>
        public IEnumerable<IllustrationTemplate> ParseMany(string text)
        {
            foreach (var strategy in strategies)
            {
                var result = strategy(text)?.ToList();
                if (result != null && result.Any())
                    return result;
            }

            return Enumerable.Empty<IllustrationTemplate>();
        }

        /// <summary>
        /// Parses one template from a block of text, using available strategies.
        /// </summary>
        /// <param name="text">The input text from the LLM or user.</param>
        /// <returns>A collection of extracted templates.</returns>
        public IllustrationTemplate? ParseOne(string text)
        {
            var allTemplates = ParseMany(text);
            return allTemplates.FirstOrDefault();
        }

        /// <summary>
        /// Attempts to parse JSON format like: [{ "title": "...", "prompt": "..." }, ...]
        /// </summary>
        private IEnumerable<IllustrationTemplate> TryParseJson(string rawText)
        {
            try
            {
                var doc = JsonDocument.Parse(rawText);
                var root = doc.RootElement;

                if (root.ValueKind == JsonValueKind.Array)
                {
                    foreach (var element in root.EnumerateArray())
                    {
                        if (element.TryGetProperty("title", out var titleProp) &&
                            element.TryGetProperty("prompt", out var promptProp))
                        {
                            var title = titleProp.GetString() ?? "Untitled";
                            var prompt = promptProp.GetString() ?? "";
                            yield return new IllustrationTemplate(title, prompt);
                        }
                    }
                }
            }
            finally
            {

            }
        }

        /// <summary>
        /// Attempts to parse lines with clear key markers like:
        /// "Title: Space Cowboy Prompt: A man in a cowboy hat floating in space"
        /// </summary>
        private IEnumerable<IllustrationTemplate> TryParseKeyValueFormat(string rawText)
        {
            var matches = Regex.Matches(rawText,
                @"Title\s*[:\-]\s*(?<title>.+?)\s+Prompt\s*[:\-]\s*(?<prompt>.+?)(?=(\n|$))",
                RegexOptions.IgnoreCase | RegexOptions.Singleline);

            foreach (Match match in matches)
            {
                var title = match.Groups["title"].Value.Trim();
                var prompt = match.Groups["prompt"].Value.Trim();

                if (!string.IsNullOrEmpty(prompt))
                    yield return new IllustrationTemplate(title, prompt);
            }
        }

        /// <summary>
        /// Attempts to parse lines like:
        /// "Space Cowboy - A man in a cowboy hat floating in space"
        /// or
        /// "Cyber Cat: A glowing neon feline running on wires"
        /// </summary>
        private IEnumerable<IllustrationTemplate> TryParseDelimitedLines(string rawText)
        {
            var lines = rawText.Split('\n');

            foreach (var line in lines)
            {
                var trimmed = line.Trim();
                if (string.IsNullOrWhiteSpace(trimmed)) continue;

                // Match "Title - Prompt" or "Title: Prompt"
                var match = Regex.Match(trimmed, @"^(?<title>.+?)[\-\:]\s*(?<prompt>.+)$");

                if (match.Success)
                {
                    var title = match.Groups["title"].Value.Trim();
                    var prompt = match.Groups["prompt"].Value.Trim();

                    yield return new IllustrationTemplate(title, prompt);
                }
            }
        }
    }
}

