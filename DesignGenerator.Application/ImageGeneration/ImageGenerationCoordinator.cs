using CommunityToolkit.Mvvm.Messaging;
using DesignGenerator.Application.Interfaces.ImageGeneration;
using DesignGenerator.Domain.Interfaces.ImageGeneration;
using DesignGenerator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application.ImageGeneration
{
    /// <summary>
    /// Coordinates the full workflow of image generation and saving.
    /// Delegates image generation to IImageGenerationService and saving to IImageSaver.
    /// </summary>
    public class ImageGenerationCoordinator : IImageGenerationCoordinator
    {
        private readonly IImageGenerationService _generator;
        private readonly IImageSaver _saver;
        private readonly IClientSelector _clientSelector;

        /// <summary>
        /// Constructs the coordinator with required dependencies.
        /// </summary>
        public ImageGenerationCoordinator(
            IImageGenerationService generator,
            IImageSaver saver,
            IClientSelector clientSelector)
        {
            _generator = generator;
            _saver = saver;
            _clientSelector = clientSelector;
        }


        /// <summary>
        /// Generates an image using the provided client and parameters,
        /// and then saves the image using the configured saver.
        /// </summary>
        /// <param name="parameters">The parameters to use for generation (prompt, resolution, etc.).</param>
        /// <returns>File path or reference to the saved image.</returns>
        /// 
        // TODO: в будущем возможна реализация генерации по одному промпту от нескольких нейросетей. Возможно, стоит добавить другую версию метода, которая принимает список клиентов и возвращает несколько изображений.
        public async Task<string> GenerateAndSaveAsync(IImageGenerationParams parameters)
        {
            var client = _clientSelector.SelectClient();
            var imageData = await _generator.GenerateAsync(client, parameters);
            var savedPath = await _saver.SaveAsync(imageData);
            return savedPath;
        }

        public async Task<string> GenerateAndSaveAsync(IEnumerable<ParameterDescriptor> parameters)
        {
            // TODO: Может, избавиться от CustomGenerationParams?
            var customPrameters = new CustomGenerationParams(parameters);
            var client = _clientSelector.SelectClient();
            var imageData = await _generator.GenerateAsync(client, customPrameters);
            var savedPath = await _saver.SaveAsync(imageData);
            return savedPath;
        }
    }
}
