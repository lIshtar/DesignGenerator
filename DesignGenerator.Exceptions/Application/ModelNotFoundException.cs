using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Exceptions.Application
{
    public class ModelNotFoundException : AppConfigurationException
    {
        public string ModelName { get; }

        public ModelNotFoundException(string modelName)
            : base($"Model with name '{modelName}' was not found in the available model list.")
        {
            ModelName = modelName;
        }
    }
}
