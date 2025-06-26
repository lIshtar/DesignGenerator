using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Domain.Interfaces.ImageGeneration
{
    /// <summary>
    /// Handles parsing of image generation API responses into usable image data.
    /// </summary>
    public interface IResponseHandler
    {
        /// <summary>
        /// Extracts image bytes or URL from the raw JSON response.
        /// </summary>
        Task<ImageData> ParseResponseAsync(HttpResponseMessage response);
    }
}
