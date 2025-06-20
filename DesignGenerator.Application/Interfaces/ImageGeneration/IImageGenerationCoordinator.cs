using DesignGenerator.Domain.Interfaces.ImageGeneration;
using DesignGenerator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application.Interfaces.ImageGeneration
{
    /// <summary>
    /// Represents a service that coordinates the process of generating an image and saving it to storage.
    /// </summary>
    public interface IImageGenerationCoordinator
    {
        /// <summary>
        /// Generates an image using the specified image generation client and parameters,
        /// then saves the result using the configured image saver.
        /// </summary>
        /// <param name="client">The image generation client (e.g., Stable Diffusion) to use.</param>
        /// <param name="parameters">The parameters that define how the image should be generated.</param>
        /// <returns>The file path or reference to the saved image.</returns>
        Task<string> GenerateAndSaveAsync(IImageGenerationParams parameters);
        Task<string> GenerateAndSaveAsync(IEnumerable<ParameterDescriptor> parameters);
    }
}
