using DesignGenerator.Domain.Interfaces.ImageGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application.Interfaces.TextGeneration
{
    public interface ITextGenerationClient
    {
        string Name { get; }
        ITextGenerationParams DefaultParams { get; set; }
        Task<string> GenerateAsync(ITextGenerationParams parameters);
    }
}
