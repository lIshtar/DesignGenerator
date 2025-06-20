using DesignGenerator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Domain.Interfaces.ImageGeneration
{
    /// <summary>
    /// Defines a client interface for interacting with a specific image generation model (e.g., Stable Diffusion).
    /// </summary>
    public interface IImageGenerationClient
    {
        /// <summary>
        /// Gets the display name of the image generation client or model.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the set of default parameters expected by this client.
        /// </summary>
        IImageGenerationParams DefaultParams { get; }

        /// <summary>
        /// Sends a request to generate an image using the given parameters.
        /// </summary>
        /// <param name="parameters">The input parameters describing the image generation prompt and settings.</param>
        /// <returns>A task that returns a result string (e.g., image URL or base64) upon completion.</returns>
        Task<ImageData> GenerateAsync(IImageGenerationParams parameters);
    }
}
