using DesignGenerator.Domain.Interfaces.ImageGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application.Interfaces.ImageGeneration
{
    /// <summary>
    /// Resolves raw image response (e.g., URL or base64 JSON) into actual binary image data.
    /// </summary>
    public interface IImageDataResolver
    {
        /// <summary>
        /// Converts raw data into a standardized image data structure containing bytes or URL.
        /// </summary>
        Task<ImageData> ResolveAsync(ImageData rawData);
    }
}
