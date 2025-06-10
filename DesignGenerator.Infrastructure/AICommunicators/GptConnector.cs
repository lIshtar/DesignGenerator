using Microsoft.Extensions.Configuration;
using OpenAI;
using OpenAI.Chat;
using System.ClientModel;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Yandex.Cloud.Loadtesting.Api.V1.Config;

namespace DesignGenerator.Infrastructure.AICommunicators
{
    public class GptConnector : ITextAICommunicator
    {
        public GptConnector(IConfiguration configuration)
        {
            _config = configuration;
        }

        private readonly IConfiguration _config;
        private readonly string _baseUrl = "https://models.inference.ai.azure.com";

        public async Task<string> GetTextAnswerAsync(string query)
        {
            var _key = _config["[ApiKeys:Text"];
            var options = new OpenAIClientOptions()
            {
                Endpoint = new Uri(_baseUrl)
            };
            var credentials = new ApiKeyCredential(_key);

            var client = new OpenAIClient(credentials, options);

            if (string.IsNullOrWhiteSpace(query))
                return "Query parameter is missing";

            var chatClient = client.GetChatClient("gpt-4o-mini");
            ChatCompletion completion = await chatClient.CompleteChatAsync(query);

            return completion.Content[0].Text ?? "No response content available";
        }
    }
}
