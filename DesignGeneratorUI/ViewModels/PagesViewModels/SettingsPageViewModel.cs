using DesignGenerator.Application;
using DesignGenerator.Exceptions.Application;
using DesignGenerator.Application.Settings;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DesignGeneratorUI.ViewModels.PagesViewModels
{
    /// <summary>
    /// ViewModel for the Settings Page of the application.
    /// Provides data binding for settings such as API keys, selected model,
    /// and image save directory. Also handles user interactions like browsing for folders.
    /// </summary>
    public class SettingsPageViewModel : BaseViewModel
    {
        // Service responsible for managing and persisting application settings.
        private SettingsService _settingsService;

        /// <summary>
        /// Gets or sets the path to the directory where images are saved.
        /// Updates the settings service and notifies the UI of changes.
        /// </summary>
        public string ImageSaveDirectory
        {
            get => _settingsService.ImageSaveDirectory;
            set
            {
                _settingsService.ImageSaveDirectory = value;
                OnPropertyChanged(nameof(ImageSaveDirectory));
            }
        }

        /// <summary>
        /// Gets or sets the API key used for text-based AI services.
        /// </summary>
        public string TextApiKey
        {
            get => _settingsService.TextApiKey;
            set
            {
                _settingsService.TextApiKey = value;
                OnPropertyChanged(nameof(TextApiKey));
            }
        }

        /// <summary>
        /// Gets or sets the API key used for visual (image generation) AI services.
        /// </summary>
        public string VisualApiKey
        {
            get => _settingsService.VisualApiKey;
            set
            {
                _settingsService.VisualApiKey = value;
                OnPropertyChanged(nameof(VisualApiKey));
            }
        }

        /// <summary>
        /// A list of available model names for selection, bound to a dropdown or similar UI element.
        /// </summary>
        public ObservableCollection<string> AvailableModels
        {
            get => [.. _settingsService.Models.Select(model => model.Name)];
        }

        /// <summary>
        /// Gets or sets the name of the currently selected model.
        /// Automatically updates the underlying selected model in the settings service.
        /// </summary>
        public string SelectedModel
        {
            get => _settingsService.SelectedModel.Name;
            set
            {
                if (string.Equals(value, SelectedModel, StringComparison.OrdinalIgnoreCase))
                    return;

                SetSelectedModelByName(value);
                OnPropertyChanged(nameof(SelectedModel));
            }
        }

        /// <summary>
        /// Command used to open a folder browser dialog and update the image save directory.
        /// </summary>
        public ICommand BrowsePathCommand { get; private set; }

        /// <summary>
        /// Initializes a new instance of the SettingsPageViewModel class with the specified settings service.
        /// </summary>
        /// <param name="settingsService">The service used to manage application settings.</param>
        public SettingsPageViewModel(SettingsService settingsService)
        {
            _settingsService = settingsService;

            BrowsePathCommand = new RelayCommand(BrowsePath);
        }

        /// <summary>
        /// Opens a folder browser dialog and sets the selected path as the image save directory.
        /// </summary>
        private void BrowsePath()
        {
            var dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                ImageSaveDirectory = dialog.SelectedPath;
            }
        }

        /// <summary>
        /// Updates the selected model in the settings service based on its name.
        /// Throws an exception if the specified model is not found.
        /// </summary>
        /// <param name="name">The name of the model to select.</param>
        private void SetSelectedModelByName(string name)
        {
            var model = _settingsService.Models
                .FirstOrDefault(m => m.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (model == null)
                throw new ModelNotFoundException(name);

            _settingsService.SelectedModel = model;
        }
    }
}
