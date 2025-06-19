using CommunityToolkit.Mvvm.Messaging;
using DesignGenerator.Application.Messages;

namespace DesignGenerator.Application.Settings
{
    public class DirectoriesService
    {
        private const string defaultImageSaveDirectoryConfig = "Folders:DefaultImageFolder";

        private readonly AppConfiguration _config;
        private readonly IMessenger _messenger;

        private string _imageSaveDirectory;

        internal string ImageSaveDirectory
        {
            get => _imageSaveDirectory;
            set
            {
                _imageSaveDirectory = value;
                _messenger.Send(new ImageSaveDirectoryChangedMessage(value));
                _config.SetValue(defaultImageSaveDirectoryConfig, value);
            }
        }

        public DirectoriesService(AppConfiguration configuration, IMessenger messenger)
        {
            _config = configuration;
            _messenger = messenger;

            InitializeDirectories();
        }

        private void InitializeDirectories()
        {
            string imageSaveDirecxtory = _config.GetRequiredValue(defaultImageSaveDirectoryConfig);
        }
    }
}
