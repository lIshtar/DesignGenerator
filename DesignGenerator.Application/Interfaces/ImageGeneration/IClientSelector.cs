using DesignGenerator.Domain.Interfaces.ImageGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application.Interfaces.ImageGeneration
{
    /// <summary>
    /// Selects an appropriate image generation client based on the given parameters.
    /// </summary>
    public interface IClientSelector
    {
        IImageGenerationClient SelectClient();
    }
}
