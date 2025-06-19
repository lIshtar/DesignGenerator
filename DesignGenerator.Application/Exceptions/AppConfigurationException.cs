using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application.Exceptions
{
    public class AppConfigurationException : Exception
    {
        public AppConfigurationException(string message) : base(message) { }

        public AppConfigurationException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
