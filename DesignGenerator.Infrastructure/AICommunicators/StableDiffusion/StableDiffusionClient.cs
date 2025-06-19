using DesignGenerator.Domain.Interfaces.ImageGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Infrastructure.AICommunicators.StableDiffusion
{
    public class StableDiffusionClient : IImageGenerationClient
    {
        public string Name => "Stable Diffusion";
        public IImageGenerationParams DefaultParams { get; }

        public StableDiffusionClient()
        {
            DefaultParams = new StableDiffusionParams();
        }

        public Task<string> GenerateAsync(IImageGenerationParams parameters)
        {
            throw new NotImplementedException();
        }
    }
}
