using DesignGenerator.Application;
using DesignGenerator.Application.Exceptions;
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
    public class SettingsPageViewModel : BaseViewModel
    {
        private SettingsService _settingsService;

        public string ImageSaveDirectory
        {
            get => _settingsService.ImageSaveDirectory;
            set
            {
                _settingsService.ImageSaveDirectory = value;
                OnPropertyChanged(nameof(ImageSaveDirectory));
            }
        }

        public string TextApiKey
        {
            get => _settingsService.TextApiKey;
            set
            {
                _settingsService.TextApiKey = value;
                OnPropertyChanged(nameof(TextApiKey));
            }
        }

        public string VisualApiKey
        {
            get => _settingsService.VisualApiKey;
            set
            {
                _settingsService.VisualApiKey = value;
                OnPropertyChanged(nameof(VisualApiKey));
            }
        }

        public ObservableCollection<string> AvailableModels
        {
            get => [.. _settingsService.Models.Select(model => model.Name)];
        }

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

        public ICommand BrowsePathCommand { get; private set; }

        public SettingsPageViewModel(SettingsService settingsService)
        {
            _settingsService = settingsService;

            BrowsePathCommand = new RelayCommand(BrowsePath);
        }

        private void BrowsePath()
        {
            var dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                ImageSavePath = dialog.SelectedPath;
            }
        }

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
