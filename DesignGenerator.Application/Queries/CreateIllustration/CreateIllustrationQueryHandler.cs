using DesignGenerator.Application.Interfaces;
using DesignGenerator.Infrastructure.AICommunicators;
using DesignGenerator.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application.Queries.CreateIllustration
{
    public class CreateIllustrationQueryHandler : IQueryHandler<CreateIllustrationQuery, CreateIllustrationQueryResponse>
    {
        private IImageAICommunicator _imageAICommunicator;
        private IImageDownloader _imageDownloader;

        public CreateIllustrationQueryHandler(IImageAICommunicator imageAICommunicator, IImageDownloader imageDownloader)
        {
            _imageAICommunicator = imageAICommunicator;
            _imageDownloader = imageDownloader;
        }
        public async Task<CreateIllustrationQueryResponse> Handle(CreateIllustrationQuery command)
        {
            string imageUrl = "";
            string folderPath = Path.GetDirectoryName(command.FolderPath);
            var directory = new DirectoryInfo(folderPath);

            imageUrl = await _imageAICommunicator.GetImageUrlAsync(command.Prompt);
            // TODO: Не загружается изображение. Исправить
            // TODO: Возможно переработать бд. Добавить данные об общении с сервисами
            string illustrationPath = await _imageDownloader.DownloadImageAsync(imageUrl, directory);

            return new CreateIllustrationQueryResponse { IllustrationPath = illustrationPath };
        }
    }
}
