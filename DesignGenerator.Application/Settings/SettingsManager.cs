using DesignGenerator.Domain.Interfaces.ImageGeneration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application.Settings
{
    /// <summary>
    /// Aggregates access to various user-configurable settings such as API keys,
    /// selected models, and directory paths. Acts as a central point of access for the UI.
    /// </summary>
    public class SettingsManager
    {
        private ModelSelectionSettings _modelSelectionService;
        private ApiKeysSettings _apiKeysService;
        private DirectoriesSettings _directoriesService;

        /// <summary>
        /// Currently selected model for image generation.
        /// </summary>
        public IImageGenerationClient SelectedModel
        {
            get => _modelSelectionService.SelectedModel;
            set => _modelSelectionService.SelectedModel = value;
        }

        /// <summary>
        /// All available models that can be selected for image generation.
        /// </summary>
        public ObservableCollection<IImageGenerationClient> Models
        {
            get => [.. _modelSelectionService.Models];
        }

        /// <summary>
        /// Directory where generated images are saved.
        /// </summary>
        public string ImageSaveDirectory
        {
            get => _directoriesService.ImageSaveDirectory;
            set => _directoriesService.ImageSaveDirectory = value;
        }

        /// <summary>
        /// API key used for image generation services.
        /// </summary>
        public string VisualApiKey
        {
            get => _apiKeysService.VisualApiKey;
            set => _apiKeysService.VisualApiKey = value;
        }

        /// <summary>
        /// API key used for text-based services.
        /// </summary>
        public string TextApiKey
        {
            get => _apiKeysService.TextApiKey;
            set => _apiKeysService.TextApiKey = value;
        }

        /// <summary>
        /// Constructs a new <see cref="SettingsManager"/> that aggregates sub-services.
        /// </summary>
        public SettingsManager(
            ModelSelectionSettings modelSelectionService,
            ApiKeysSettings apiKeysService,
            DirectoriesSettings directoriesService)
        {
            _apiKeysService = apiKeysService;
            _modelSelectionService = modelSelectionService;
            _directoriesService = directoriesService;
        }
    }
}
