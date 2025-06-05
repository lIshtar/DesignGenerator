using DesignGenerator.Application;
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
        private string _imageSavePath;
        public string ImageSavePath
        {
            get
            {
                if (_imageSavePath == null)
                {
                    _imageSavePath = _config.GetValue("Folders:DefaultImageFolder") ?? String.Empty;
                }
                return _imageSavePath;
            }
            set
            {
                _config.SetValue("Folders:DefaultImageFolder", value);
                _imageSavePath = value;
                OnPropertyChanged(nameof(ImageSavePath));
            }
        }

        private string _visualapiKey;
        public string VisualApiKey
        {
            get
            {
                if (_visualapiKey == null)
                {
                    _visualapiKey = _config.GetValue("ApiKeys:Visual") ?? string.Empty;
                }
                return _visualapiKey;
            }
            set
            {
                _visualapiKey = value;
                _config.SetValue("ApiKeys:Visual", value);
                OnPropertyChanged(nameof(VisualApiKey));
            }
        }

        private string _textapiKey;
        public string TextApiKey
        {
            get
            {
                if (_textapiKey == null)
                {
                    _textapiKey = _config.GetValue("ApiKeys:Text") ?? string.Empty;
                }
                return _textapiKey;
            }
            set
            {
                _textapiKey = value;
                _config.SetValue("ApiKeys:Text", value);
                OnPropertyChanged(nameof(TextApiKey));
            }
        }

        public ObservableCollection<string> AvailableModels { get; } = new ObservableCollection<string>
{
    "flux-schnell",
    "essential-v2",
    "stable-diffusion-xl",
    "stable-diffusion"
};

        public string SelectedModel
        {
            get => _config.GetValue("Models:DefaultImageModel") ?? throw new Exception("Models:DefaultImageModel was not found in appconfig");
            set
            {
                _config.SetValue("Models:DefaultImageModel", value);
                OnPropertyChanged(nameof(SelectedModel));
            }
        }

        private AppConfiguration _config;
        public ICommand BrowsePathCommand { get; private set; }

        public SettingsPageViewModel(AppConfiguration configuration)
        {
            _config = configuration;
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
    }
}
