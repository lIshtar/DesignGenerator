using DesignGenerator.Application.Interfaces.TextGeneration;

namespace DesignGenerator.Application.TextGeneration
{
    public class TextGenerationService : ITextGenerationService
    {
        public async Task<string> GenerateAsync(ITextGenerationClient client, ITextGenerationParams parameters)
        {
            return await client.GenerateAsync(parameters);
        }
    }
}
