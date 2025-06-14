﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application.Interfaces
{
    public interface IImageDownloader
    {
        Task<string> DownloadImageAsync(string imageUrl, DirectoryInfo saveFolder);
    }
}
