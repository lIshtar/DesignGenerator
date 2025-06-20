using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Exceptions.Application
{
    // <summary>
    /// Exception thrown when an image generation client has not been selected.
    /// </summary>
    public class ClientNotSelectedException : Exception
    {
        public ClientNotSelectedException() { }

        public ClientNotSelectedException(string message) : base(message) { }

        public ClientNotSelectedException(string message, Exception innerException) : base(message, innerException) { }
    }
}
