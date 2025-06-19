using DesignGenerator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Domain.Interfaces.ImageGeneration
{
    /// <summary>
    /// Represents a container for a collection of image generation parameters.
    /// </summary>
    public interface IImageGenerationParams
    {
        /// <summary>
        /// Gets a collection of parameter descriptors used for image generation.
        /// </summary>
        IEnumerable<ParameterDescriptor> Parameters { get; }
    }
}
