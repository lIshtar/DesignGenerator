using DesignGenerator.Domain.Interfaces.ImageGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application.Interfaces.TextGeneration
{
    public interface ITextGenerationApiClient
    {
        Task<string> GenerateTextAsync(Dictionary<string, object> payload);
    }
}
