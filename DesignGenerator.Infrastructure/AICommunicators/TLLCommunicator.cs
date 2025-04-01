using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Security.Cryptography.X509Certificates;


namespace DesignGenerator.Infrastructure.AICommunicators
{
    public class TLLCommunicator : ITextAICommunicator
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _url;

        public TLLCommunicator()
        {
            _url = "http://localhost:8080/query";

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddHttpClient();
            var serviceProvider = serviceCollection.BuildServiceProvider();

            _httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
        }

        public async Task<string> GetTextAnswerAsync(string query)
        {
            try
            {
                using var httpClient = _httpClientFactory.CreateClient();

                var data = new Request(query);

                // Красиво и правильно, но не работает

                //using var resp = await httpClient.PostAsJsonAsync(requestUri: _url, data);
                //.ConfigureAwait(false);
                // exception will be thrown here if not OK code
                //_ = resp.EnsureSuccessStatusCode();
                //var responseString = await resp.Content.ReadAsStringAsync().ConfigureAwait(false);
                //return responseString;

                // некрасиво и неправильно, но работает

                var response = Task.Run(() => httpClient.PostAsJsonAsync(_url, data));
                response.Wait();
                string jsonResponse = await response.Result.Content.ReadAsStringAsync();
                Dictionary<string, string> responseDictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonResponse);
                return responseDictionary["text_answer"];
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private record Request(string Query);
    }
}
