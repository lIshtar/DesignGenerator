using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Exceptions.Application
{
    /// <summary>
    /// Exception thrown when image saving is attempted but no byte data is available.
    /// </summary>
    public class EmptyImageBytesException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyImageBytesException"/> class with a default message.
        /// </summary>
        public EmptyImageBytesException()
            : base("Cannot save image: resolved byte array is empty.") { }
    }
}
