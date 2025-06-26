using DesignGenerator.Domain.Interfaces.ImageGeneration;
using DesignGenerator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Infrastructure.ImageGeneration.StableDiffusion
{
    /// <summary>
    /// Implements the IImageGenerationClient interface for Stable Diffusion.
    /// Delegates request construction and API communication to injected components.
    /// </summary>
    public class StableDiffusionClient : IImageGenerationClient
    {
        /// <summary>
        /// The display name of this model.
        /// </summary>
        public string Name => "Stable Diffusion";

        /// </summary>
        /// The list of default parameter descriptors supported by Stable Diffusion.
        /// </summary>
        public IImageGenerationParams DefaultParams { get; set; }

        private readonly StableDiffusionRequestBuilder _requestBuilder;
        private readonly StableDiffusionApiClient _apiClient;

        /// <summary>
        /// Constructor that injects dependencies.
        /// </summary>
        public StableDiffusionClient(StableDiffusionRequestBuilder requestBuilder, StableDiffusionApiClient apiClient)
        {
            _requestBuilder = requestBuilder;
            _apiClient = apiClient;
            DefaultParams = new StableDiffusionParams();
        }

        /// <summary>
        /// Generates an image from the given parameters.
        /// </summary>
        public async Task<ImageData> GenerateAsync(IImageGenerationParams parameters)
        {
            var payload = _requestBuilder.Build(parameters);
            return await _apiClient.GenerateImageAsync(payload);
        }
    }
}
