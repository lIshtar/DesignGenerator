using DesignGenerator.Domain.Interfaces.ImageGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application.Interfaces.ImageGeneration
{
    /// <summary>
    /// Handles raw image generation using a specified client and parameters.
    /// Returns an intermediate result which must be resolved before saving.
    /// </summary>
    public interface IImageGenerationService
    {
        /// <summary>
        /// Sends a request to the given image generation client and returns raw image response data.
        /// </summary>
        Task<ImageData> GenerateAsync(IImageGenerationClient client, IImageGenerationParams parameters);
    }
}
