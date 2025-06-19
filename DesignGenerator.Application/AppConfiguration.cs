using DesignGenerator.Exceptions.Application;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace DesignGenerator.Application
{
    public class AppConfiguration
    {
        private readonly IConfiguration _configuration;
        private readonly string _configFilePath;
        private JsonObject _jsonRoot;

        public AppConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;

            // Путь к appsettings.json (предполагается, что файл лежит рядом с exe)
            _configFilePath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");

            // Загружаем JSON из файла в JsonObject для редактирования
            LoadJson();
        }

        private void LoadJson()
        {
            if (!File.Exists(_configFilePath))
                throw new FileNotFoundException("Конфигурационный файл не найден", _configFilePath);

            var jsonText = File.ReadAllText(_configFilePath);
            _jsonRoot = JsonNode.Parse(jsonText)?.AsObject() ?? new JsonObject();
        }

        /// <summary>
        /// Получить значение по ключу (формат ключа: "Section:Subsection:Key")
        /// </summary>
        public string? GetValue(string key)
        {
            return _configuration[key];
        }

        /// <summary>
        /// Установить новое значение и сохранить файл конфигурации
        /// </summary>
        public void SetValue(string key, string value)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            // Разбиваем ключ на части: "Section", "Subsection", ...
            var parts = key.Split(':');

            JsonObject current = _jsonRoot;

            // Проходим по вложенным объектам
            for (int i = 0; i < parts.Length - 1; i++)
            {
                var part = parts[i];
                if (current[part] == null || current[part].GetType() != typeof(JsonObject))
                {
                    var newObj = new JsonObject();
                    current[part] = newObj;
                    current = newObj;
                }
                else
                {
                    current = current[part]!.AsObject();
                }
            }

            // Устанавливаем конечное значение
            current[parts[^1]] = value;

            // Сохраняем в файл
            SaveJson();
        }

        public string GetRequiredValue(string key)
        {
            var value = GetValue(key);

            if (value == null || (value is string s && string.IsNullOrWhiteSpace(s)))
                throw new ConfigurationEntityNotFoundException<string>($"Configuration key '{key}'");

            return value;
        }

        private void SaveJson()
        {
            var options = new System.Text.Json.JsonSerializerOptions
            {
                WriteIndented = true
            };
            var jsonText = _jsonRoot.ToJsonString(options);
            File.WriteAllText(_configFilePath, jsonText);
        }
    }
}
