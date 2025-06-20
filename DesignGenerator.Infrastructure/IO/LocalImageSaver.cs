using DesignGenerator.Application.Interfaces.ImageGeneration;
using DesignGenerator.Domain.Interfaces.ImageGeneration;
using DesignGenerator.Exceptions.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Infrastructure.IO
{
    /// <summary>
    /// Saves resolved image data to the local file system.
    /// Uses an image data resolver to get image bytes before saving.
    /// </summary>
    public class LocalImageSaver : IImageSaver
    {
        private readonly string _saveDirectory;
        private readonly IImageDataResolver _resolver;

        /// <summary>
        /// Initializes a new instance with a target save directory and a resolver.
        /// </summary>
        public LocalImageSaver(string saveDirectory, IImageDataResolver resolver)
        {
            _saveDirectory = saveDirectory;
            _resolver = resolver;
        }

        /// <summary>
        /// Resolves and saves the image to disk.
        /// Returns the path to the saved file.
        /// </summary>
        public async Task<string> SaveAsync(ImageData rawData)
        {
            var imageData = await _resolver.ResolveAsync(rawData);

            if (imageData.Bytes == null || imageData.Bytes.Length == 0)
                throw new EmptyImageBytesException();

            var fileName = $"image_{DateTime.Now:yyyyMMdd_HHmmss}.{imageData.Format}";
            var filePath = Path.Combine(_saveDirectory, fileName);

            Directory.CreateDirectory(_saveDirectory);
            await File.WriteAllBytesAsync(filePath, imageData.Bytes);

            return filePath;
        }
    }
}