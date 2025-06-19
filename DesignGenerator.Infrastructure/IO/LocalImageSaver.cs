using DesignGenerator.Domain.Interfaces.ImageGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Infrastructure.IO
{
    //// <summary>
    /// Provides a local implementation of <see cref="IImageSaver"/> that saves image data as a file
    /// to the specified directory using the provided format.
    /// </summary>
    public class LocalImageSaver : IImageSaver
    {
        private readonly string _saveDirectory;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalImageSaver"/> class.
        /// </summary>
        /// <param name="saveDirectory">The directory where images will be saved.</param>
        public LocalImageSaver(string saveDirectory)
        {
            _saveDirectory = saveDirectory;
        }

        /// <summary>
        /// Saves the image bytes to disk in the configured directory.
        /// </summary>
        /// <param name="data">An <see cref="ImageData"/> object containing image bytes and format.</param>
        /// <returns>The full file path of the saved image.</returns>
        /// <exception cref="ArgumentException">Thrown if the image bytes are null.</exception>
        public async Task<string> SaveAsync(ImageData data)
        {
            if (data.Bytes == null)
                throw new ArgumentException("Image data is empty.");

            var fileName = $"image_{DateTime.Now:yyyyMMdd_HHmmss}.{data.Format}";

            var filePath = Path.Combine(_saveDirectory, fileName);

            await File.WriteAllBytesAsync(filePath, data.Bytes);

            return filePath;
        }
    }
}
