using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Exceptions.Application
{
    /// <summary>
    /// Exception thrown when neither image bytes nor URL are available in the data.
    /// </summary>
    public class ImageDataMissingException : Exception
    {
        public ImageDataMissingException()
            : base("No image bytes or URL provided in the image data.") { }
    }
}
