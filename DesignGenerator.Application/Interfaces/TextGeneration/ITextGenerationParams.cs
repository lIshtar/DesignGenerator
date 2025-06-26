using DesignGenerator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application.Interfaces.TextGeneration
{
    public interface ITextGenerationParams
    {
        public IEnumerable<ParameterDescriptor> Parameters { get; }
    }
}
