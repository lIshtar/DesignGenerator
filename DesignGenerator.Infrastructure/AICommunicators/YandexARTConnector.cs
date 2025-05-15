using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DesignGenerator.Infrastructure.AICommunicators
{
    //public class YandexARTConnector : IImageAICommunicator
    //{
    //    private readonly string _authorizationKey = "AQVN3KeParxh1AMzCRxddy1Osor_I5erS3o8zUlK";
    //    private readonly HttpClient _httpClient = new HttpClient();
    //    public async Task<string> GetImageUrlAsync(string query)
    //    {
    //        var url = "https://example.com/api/data";

    //        // Define the payload
    //        var payload = new
    //        {
    //            Name = "John Doe",
    //            Email = "john.doe@example.com"
    //        };

    //        // Serialize payload to JSON using Newtonsoft.Json
    //        var json = JsonConvert.SerializeObject(payload);

    //        var content = new StringContent(json, Encoding.UTF8, "application/json");

    //        // Add custom headers
    //        _httpClient.DefaultRequestHeaders.Clear();
    //        _httpClient.DefaultRequestHeaders.Add("X-Custom-Header", "MyHeaderValue");
    //        _httpClient.DefaultRequestHeaders.Add("X-Request-ID", Guid.NewGuid().ToString());

    //        try
    //        {
    //            var response = await _httpClient.PostAsync(url, content);
    //            response.EnsureSuccessStatusCode();

    //            var responseBody = await response.Content.ReadAsStringAsync();
    //            Console.WriteLine("Response: " + responseBody);
    //        }
    //        catch (HttpRequestException ex)
    //        {
    //            Console.WriteLine("Error sending POST request: " + ex.Message);
    //        }
    //    }
    //}
}
