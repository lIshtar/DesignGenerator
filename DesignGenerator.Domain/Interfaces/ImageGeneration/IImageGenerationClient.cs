using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Domain.Interfaces.ImageGeneration
{
    public interface IImageGenerationClient 
    {
        public string Name { get; }
        public IImageGenerationParams DefaultParams { get; }
        Task<string> GenerateAsync(IImageGenerationParams parameters);
    }
}
