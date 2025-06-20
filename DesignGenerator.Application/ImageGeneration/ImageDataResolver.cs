using DesignGenerator.Application.Interfaces;
using DesignGenerator.Application.Interfaces.ImageGeneration;
using DesignGenerator.Domain.Interfaces.ImageGeneration;
using DesignGenerator.Exceptions.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application.ImageGeneration
{
    /// <summary>
    /// Resolves raw image data by ensuring it contains image bytes.
    /// If only a URL is provided, it attempts to download the image bytes.
    /// </summary>
    public class ImageDataResolver : IImageDataResolver
    {
        private readonly IUrlToBytesFetcher _fetcher;

        /// <summary>
        /// Constructs the resolver with a component that can download images from URLs.
        /// </summary>
        public ImageDataResolver(IUrlToBytesFetcher fetcher)
        {
            _fetcher = fetcher;
        }

        /// <summary>
        /// Ensures that the image data includes bytes. If only a URL is present, downloads the image.
        /// </summary>
        /// <param name="rawData">The image data object containing either bytes or a URL.</param>
        /// <returns>Resolved ImageData with image bytes populated.</returns>
        /// <exception cref="ImageDataMissingException">Thrown if neither bytes nor URL are available.</exception>
        public async Task<ImageData> ResolveAsync(ImageData rawData)
        {
            if (rawData.Bytes != null)
                return rawData;

            if (!string.IsNullOrWhiteSpace(rawData.Url))
            {
                rawData.Bytes = await _fetcher.FetchAsync(rawData.Url);
                return rawData;
            }

            throw new ImageDataMissingException();
        }
    }
}
