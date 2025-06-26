using DesignGenerator.Application.Interfaces.TextGeneration;

namespace DesignGenerator.Application.TextGeneration
{
    public class TextGenerationCoordinator : ITextGenerationCoordinator
    {
        private readonly ITextGenerationService _generator;
        private readonly IClientSelector _clientSelector;

        /// <summary>
        /// Constructs the coordinator with required dependencies.
        /// </summary>
        public TextGenerationCoordinator(
            ITextGenerationService generator,
            IClientSelector clientSelector)
        {
            _generator = generator;
            _clientSelector = clientSelector;
        }

        public async Task<string> GenerateAsync(ITextGenerationParams parameters)
        {
            var client = _clientSelector.SelectClient();
            var textData = await _generator.GenerateAsync(client, parameters);
            return textData;
        }
    }
}
