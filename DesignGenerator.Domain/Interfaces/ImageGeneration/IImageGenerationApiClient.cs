using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Domain.Interfaces.ImageGeneration
{
    /// <summary>
    /// Defines an abstraction for a low-level API client that communicates with an image generation service via HTTP.
    /// </summary>
    public interface IImageGenerationApiClient
    {
        /// <summary>
        /// Sends a payload to the image generation API and returns the generated result.
        /// </summary>
        /// <param name="payload">A dictionary containing key-value pairs that represent the request body.</param>
        /// <returns>A task that returns a result ImageData class  upon completion.</returns>
        Task<ImageData> GenerateImageAsync(Dictionary<string, object> payload);
    }
}
