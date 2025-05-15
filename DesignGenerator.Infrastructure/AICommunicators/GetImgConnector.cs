using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft;
using Newtonsoft.Json;

namespace DesignGenerator.Infrastructure.AICommunicators
{
    public class GetImgConnector : IImageAICommunicator
    {
        private readonly string _authorizationKey = "key-MVag0BDI6z8f2lCufUYFkvsZmdmk8uBYHeeLq9pdsQlXDgFsxIF5M22KlVwDDmZMMtqbRXF1qQVlFZ7ASBCKqv8lvWJQgrp";
        private readonly HttpClient _httpClient = new HttpClient();
        public async Task<string> GetImageUrlAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return null;

            var url = "https://api.getimg.ai/v1/flux-schnell/text-to-image";

            var payload = new
            {
                prompt = query,
                response_format = "url"
            };
            var content = JsonConvert.SerializeObject(payload); 
            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(content, Encoding.UTF8, "application/json")
            };

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _authorizationKey);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(json);
            return doc.RootElement.TryGetProperty("url", out var urlElement) ? urlElement.GetString() : null;
        }
    }
}
