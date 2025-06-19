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
        public string? Url { get; set; }
        public byte[]? Bytes { get; set; }
        public string Format { get; set; } = "png";
    }
}
