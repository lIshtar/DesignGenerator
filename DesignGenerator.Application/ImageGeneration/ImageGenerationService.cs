using DesignGenerator.Application.Interfaces.ImageGeneration;
using DesignGenerator.Domain.Interfaces.ImageGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application.ImageGeneration
{
    /// <summary>
    /// Default implementation of the IImageGenerationService interface.
    /// Responsible for initiating image generation requests using the specified client
    /// and returning the unprocessed image data.
    /// </summary>
    public class ImageGenerationService : IImageGenerationService
    {
        /// <summary>
        /// Generates an image using the provided client and parameters.
        /// Returns the raw ImageData (may contain bytes or just a URL).
        /// </summary>
        /// <param name="client">The image generation client to use (e.g., Stable Diffusion).</param>
        /// <param name="parameters">The parameters used to guide image generation.</param>
        /// <returns>Raw ImageData which might contain image bytes or an external URL.</returns>
        public async Task<ImageData> GenerateAsync(IImageGenerationClient client, IImageGenerationParams parameters)
        {
            return await client.GenerateAsync(parameters);
        }
    }
}
