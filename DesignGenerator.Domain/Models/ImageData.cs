using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Domain.Interfaces.ImageGeneration
{
    /// <summary>
    /// Represents image data returned from the API.
    /// </summary>
    public class ImageData
    {
        /// <summary>
        /// Optional: A URL pointing to the image (used when response contains a URL).
        /// </summary>
        public string? Url { get; set; }

        /// <summary>
        /// Optional: The actual image bytes (used when response contains base64-encoded data).
        /// </summary>
        public byte[]? Bytes { get; set; }

        /// <summary>
        /// Image format (e.g., "png", "jpg").
        /// </summary>
        public string Format { get; set; } = "png";
    }
}
