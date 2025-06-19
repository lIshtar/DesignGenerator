using DesignGenerator.Domain.Interfaces.ImageGeneration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application.Settings
{
    public class SettingsService
    {
        private ModelSelectionService _modelSelectionService;
        private ApiKeysService _apiKeysService;
        private DirectoriesService _directoriesService;

        public IImageGenerationClient SelectedModel
        {
            get => _modelSelectionService.SelectedModel;
            set => _modelSelectionService.SelectedModel = value;
        }

        public ObservableCollection<IImageGenerationClient> Models 
        {
            get => [.. _modelSelectionService.Models];
        }

        public string ImageSaveDirectory
        {
            get => _directoriesService.ImageSaveDirectory;
            set => _directoriesService.ImageSaveDirectory = value;
        }

        public string VisualApiKey
        {
            get => _apiKeysService.VisualApiKey;
            set => _apiKeysService.VisualApiKey = value;
        }

        public string TextApiKey
        {
            get => _apiKeysService.TextApiKey;
            set => _apiKeysService.TextApiKey = value;
        }

        public SettingsService(
            ModelSelectionService modelSelectionService, 
            ApiKeysService apiKeysService,
            DirectoriesService directoriesService)
        {
            _apiKeysService = apiKeysService;
            _modelSelectionService = modelSelectionService;
            _directoriesService = directoriesService;
        }
    }
}
