using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application.Exceptions
{
    public class MissingModelConfigurationException : AppConfigurationException
    {
        public string ModelKey { get; }

        public MissingModelConfigurationException(string modelKey)
            : base($"Required model '{modelKey}' was not found in the configuration.")
        {
            ModelKey = modelKey;
        }
    }
}
