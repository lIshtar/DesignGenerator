using DesignGenerator.Application.Interfaces.ImageGeneration;
using DesignGenerator.Exceptions.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application.ImageGeneration
{
    /// <summary>
    /// Default implementation of IUrlToBytesFetcher that uses HttpClient to download image data.
    /// </summary>
    public class UrlToBytesFetcher : IUrlToBytesFetcher
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Constructs the fetcher with an injected HttpClient.
        /// </summary>
        public UrlToBytesFetcher(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Downloads the byte array of an image from the provided URL.
        /// </summary>
        /// <param name="url">The image URL to fetch data from.</param>
        /// <returns>The image data as a byte array.</returns>
        public async Task<byte[]> FetchAsync(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new InvalidImageUrlException();

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode(); 

            return await response.Content.ReadAsByteArrayAsync();
        }
    }
}
