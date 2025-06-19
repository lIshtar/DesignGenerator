using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Domain.Interfaces.ImageGeneration
{
    /// <summary>
    /// Persists image data to the local file system.
    /// </summary>
    public interface IImageSaver
    {
        /// <summary>
        /// Saves the image to a predefined location and returns the file path.
        /// </summary>
        Task<string> SaveAsync(ImageData data);
    }
}
