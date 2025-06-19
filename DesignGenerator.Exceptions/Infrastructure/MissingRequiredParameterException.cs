using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Exceptions.Infrastructure
{
    /// <summary>
    /// Exception thrown when a required parameter is missing.
    /// </summary>
    public class MissingRequiredParameterException : Exception
    {
        public MissingRequiredParameterException(string paramName)
            : base($"Required parameter '{paramName}' is missing.") { }
    }
}
