using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application.Interfaces.ImageGeneration
{
    /// <summary>
    /// Provides a method for downloading image bytes from a URL.
    /// </summary>
    public interface IUrlToBytesFetcher
    {
        /// <summary>
        /// Downloads the binary data (bytes) of an image from the specified URL.
        /// </summary>
        Task<byte[]> FetchAsync(string url);
    }
}
