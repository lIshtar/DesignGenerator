using DesignGenerator.Domain.Interfaces.ImageGeneration;
using DesignGenerator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application.ImageGeneration
{
    public class CustomGenerationParams : IImageGenerationParams
    {

        public CustomGenerationParams(IEnumerable<ParameterDescriptor> parameters)
        {
            Parameters = parameters;
        }

        public IEnumerable<ParameterDescriptor> Parameters { get; }
    }
}
