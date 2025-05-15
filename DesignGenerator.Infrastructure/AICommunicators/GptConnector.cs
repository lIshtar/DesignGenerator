using OpenAI;
using OpenAI.Chat;
using System.ClientModel;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace DesignGenerator.Infrastructure.AICommunicators
{
    public class GptConnector : ITextAICommunicator
    {
        private readonly string _baseUrl = "https://models.inference.ai.azure.com";
        private string _key = "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";
        public async Task<string> GetTextAnswerAsync(string query)
        {
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
