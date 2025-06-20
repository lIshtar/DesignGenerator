using DesignGenerator.Domain.Interfaces.ImageGeneration;
using System.Text.Json;

namespace DesignGenerator.Application.ImageGeneration
{
    /// <summary>
    /// Default implementation of IResponseHandler interface.
    /// Responsible for parsing the HTTP response from an image generation API
    /// and extracting image data as bytes or URL.
    /// </summary>
    public class JsonResponseHandler : IResponseHandler
    {
        /// <summary>
        /// Parses the HTTP response, expecting a JSON payload,
        /// and extracts image bytes or URL to populate an ImageData instance.
        /// </summary>
        /// <param name="response">The HTTP response message received from the image generation API.</param>
        /// <returns>
        /// An ImageData object containing either raw image bytes or a URL pointing to the generated image.
        /// </returns>
        public async Task<ImageData> ParseResponseAsync(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();

            using var jsonDoc = JsonDocument.Parse(jsonString);
            var root = jsonDoc.RootElement;

            var imageData = new ImageData();

            // Try to get "url" property (API returns a URL to the image)
            if (root.TryGetProperty("url", out var urlProperty))
            {
                imageData.Url = urlProperty.GetString();
            }
            // Alternatively, try to get "image" property (API returns base64 encoded image)
            else if (root.TryGetProperty("image", out var base64Property))
            {
                var base64String = base64Property.GetString();
                if (!string.IsNullOrWhiteSpace(base64String))
                {
                    imageData.Bytes = Convert.FromBase64String(base64String);
                }
            }
            else
            {
                throw new JsonException("Response JSON does not contain expected 'url' or 'image' properties.");
            }

            //TODO: надо понять, как засунуть в данный метод формат изображения. Пока что просто ставим png
            imageData.Format = "png";

            return imageData;
        }
    }
}
