using DesignGenerator.Domain.Interfaces.ImageGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DesignGenerator.Infrastructure.ImageGeneration.StableDiffusion
{
    /// <summary>
    /// Handles HTTP communication with the Stable Diffusion API.
    /// </summary>
    public class StableDiffusionApiClient : IImageGenerationApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly StableDiffusionConfig _config;
        private readonly IResponseHandler _responseHandler;

        /// <summary>
        /// Constructor with dependency injection for HttpClient and configuration.
        /// </summary>
        public StableDiffusionApiClient(HttpClient httpClient, StableDiffusionConfig config, IResponseHandler responseHandler)
        {
            _httpClient = httpClient;
            _config = config;
            _responseHandler = responseHandler;
        }

        /// <summary>
        /// Sends an image generation request to the API with the provided payload.
        /// </summary>
        public async Task<ImageData> GenerateImageAsync(Dictionary<string, object> payload)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, _config.EndpointUrl);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _config.ApiKey);
            request.Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return await _responseHandler.ParseResponseAsync(response);
        }
    }
}
