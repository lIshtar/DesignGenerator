using CommunityToolkit.Mvvm.Messaging;
using DesignGenerator.Application.Messages;

namespace DesignGenerator.Application.Settings
{
    /// <summary>
    /// Manages the configuration of directory paths, such as where generated images are saved.
    /// </summary>
    public class DirectoriesSettings
    {
        private const string defaultImageSaveDirectoryConfig = "Folders:DefaultImageFolder";

        private readonly AppConfiguration _config;
        private readonly IMessenger _messenger;

        private string _imageSaveDirectory = null!;

        /// <summary>
        /// Directory where generated images should be saved.
        /// Updates configuration and sends change notifications.
        /// </summary>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="DirectoriesSettings"/> class
        /// and loads the initial configuration.
        /// </summary>
        public DirectoriesSettings(AppConfiguration configuration, IMessenger messenger)
        {
            _config = configuration;
            _messenger = messenger;
            InitializeDirectories();
        }

        /// <summary>
        /// Loads directory paths from configuration.
        /// </summary>
        private void InitializeDirectories()
        {
            ImageSaveDirectory = _config.GetRequiredValue(defaultImageSaveDirectoryConfig);
        }
    }

}
