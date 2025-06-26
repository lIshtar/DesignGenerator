using DesignGenerator.Domain.Interfaces.ImageGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application.Interfaces.TextGeneration
{
    public interface ITextGenerationService
    {
        Task<string> GenerateAsync(ITextGenerationClient client, ITextGenerationParams parameters);
    }
}
