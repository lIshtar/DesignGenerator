using DesignGenerator.Application.Interfaces;
using DesignGenerator.Infrastructure.AICommunicators;
using DesignGenerator.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application.Commands.CreateIllustration
{
    public class CreateIllustrationCommandHandler : ICommandHandler<CreateIllustrationCommand>
    {
        private IImageAICommunicator _imageAICommunicator;
        private IImageDownloader _imageDownloader;

        public CreateIllustrationCommandHandler(IImageAICommunicator imageAICommunicator, IImageDownloader imageDownloader)
        {
            _imageAICommunicator = imageAICommunicator;
            _imageDownloader = imageDownloader;
        }
        public async Task Handle(CreateIllustrationCommand command)
        {
            string imageUrl = "";
            var directory = new DirectoryInfo(command.FolderPath);

            imageUrl = await _imageAICommunicator.GetImageUrlAsync(command.Prompt);
            await _imageDownloader.DownloadImageAsync(imageUrl, directory);

            return;
        }
    }
}
