using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Exceptions.Application
{
    /// <summary>
    /// Exception thrown when an invalid or empty image URL is provided.
    /// </summary>
    public class InvalidImageUrlException : ArgumentException
    {
        /// <summary>
        /// Creates a new instance of InvalidImageUrlException with a default message.
        /// </summary>
        public InvalidImageUrlException()
            : base("The image URL must not be null or empty.") { }

        /// <summary>
        /// Creates a new instance of InvalidImageUrlException with a custom message.
        /// </summary>
        /// <param name="message">The error message to include with the exception.</param>
        public InvalidImageUrlException(string message)
            : base(message) { }
    }
}
