using DesignGenerator.Domain.Interfaces.ImageGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Domain.Interfaces.ImageGeneration
{
    /// <summary>
    /// Provides a mechanism for transforming high-level image generation parameters into a raw API payload.
    /// </summary>
    public interface IRequestBuilder
    {
        /// <summary>
        /// Builds a dictionary of request parameters suitable for the image generation API.
        /// </summary>
        /// <param name="parameters">The high-level parameters provided by the user or UI layer.</param>
        /// <returns>A dictionary representing the final request payload to send to the API.</returns>
        Dictionary<string, object> Build(IImageGenerationParams parameters);
    }
}
