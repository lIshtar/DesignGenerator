using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Domain.Interfaces.ImageGeneration
{
    public interface IImageGenerationParams
    {
        public IEnumerable<ParameterDescriptor> Parameters { get; }
    }
}
