using CommunityToolkit.Mvvm.Messaging;
using DesignGenerator.Application.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Infrastructure.ImageGeneration.StableDiffusion
{
    /// <summary>
    /// Holds configuration data for the Stable Diffusion API including URL and API key.
    /// Listens for key change messages using IMessenger.
    /// </summary>
    public class StableDiffusionConfig
    {
        /// <summary>
        /// Constructs configuration and registers listener for API key changes.
        /// </summary>
        public StableDiffusionConfig(IMessenger messenger)
        {
            messenger.Register<VisualApiKeyChangedMessage>(this, (r, m) => UpdateApiKey(m.Value));
        }

        /// <summary>
        /// Updates the internal API key value.
        /// </summary>
        private void UpdateApiKey(string apiKey)
        {
            ApiKey = apiKey;
        }

        /// <summary>
        /// Endpoint for image generation.
        /// </summary>
        public string EndpointUrl { get; init; } = "https://api.getimg.ai/v1/stable-diffusion/text-to-image";

        /// <summary>
        /// Current API key used for authorization.
        /// </summary>
        public string ApiKey { get; private set; } = string.Empty;
    }
}
