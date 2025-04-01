using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using DesignGenerator.Application.Parsers;
using DesignGenerator.Infrastructure.AICommunicators;
using DesignGeneratorUI.FileServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Win32;

namespace DesignGeneratorUI.ViewModels.PagesViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        private int _elementCount;
        private string? _generatedScript;
        private string? _statusMessage;
        private string? _selectedTemplate;
        private bool _canGenerateImages;
        private string? _selectedFolderPath;

        public string? SelectedFolderPath
        {
            get => _selectedFolderPath;
            set
            {
                _selectedFolderPath = value;
                OnPropertyChanged(nameof(SelectedFolderPath));
            }
        }

        public int ElementCount
        {
            get => _elementCount;
            set { _elementCount = value; OnPropertyChanged(nameof(ElementCount)); }
        }

        public string? GeneratedScript
        {
            get => _generatedScript;
            set { _generatedScript = value; OnPropertyChanged(nameof(GeneratedScript)); }
        }

        public string? StatusMessage
        {
            get => _statusMessage;
            set { _statusMessage = value; OnPropertyChanged(nameof(StatusMessage)); }
        }

        public string? SelectedTemplate
        {
            get => _selectedTemplate;
            set { _selectedTemplate = value; OnPropertyChanged(nameof(SelectedTemplate)); }
        }

        public bool CanGenerateImages
        {
            get => _canGenerateImages;
            set { _canGenerateImages = value; OnPropertyChanged(nameof(CanGenerateImages)); }
        }


        public ObservableCollection<string> Templates { get; set; }
        public ICommand GenerateCommand { get; }
        public ICommand GenerateImagesCommand { get; }
        public ICommand BrowseFolderCommand { get; }

        private ITextAICommunicator _textAICommunicator;
        private IImageAICommunicator _imageAICommunicator;
        private IConfiguration _config;
        private IOpenDialogService _dialogService;
        //ApplicationDbContext _dbContext;
        private List<ImageDescription>? _imageDescriptions;

        public HomePageViewModel(
            ITextAICommunicator textAICommunicator, 
            IImageAICommunicator imageAICommunicator, 
            //ApplicationDbContext dbContext, 
            IConfiguration config, 
            IOpenDialogService dialogService)
        {
            _imageAICommunicator = imageAICommunicator;
            _textAICommunicator = textAICommunicator;
            //_dbContext = dbContext;
            _config = config;
            _dialogService = dialogService;

            // Значения по умолчанию
            ElementCount = 2;
            GeneratedScript = "книги";
            StatusMessage = "Введите параметры и нажмите 'Генерировать'";
            Templates = new ObservableCollection<string> { "Шаблон 1", "Шаблон 2", "Шаблон 3" };
            SelectedTemplate = Templates[0];

            GenerateCommand = new RelayCommand(GenerateScript);
            GenerateImagesCommand = new RelayCommand(GenerateImages, CanExecuteGenerateImages);
            BrowseFolderCommand = new RelayCommand(BrowseFolder);
        }

        private async void GenerateImages(object parameter)
        {
            StatusMessage = "Генерация картинок началась...";
            CanGenerateImages = false;

            // Здесь логика генерации картинок
            string imageUrl = "";
            int imageCounter = 2;

            string folder = _config.GetRequiredSection("Folders")
                .GetRequiredSection("DefaultImageFolder").Value
                ?? throw new Exception("Unable to find DefaultImageFolder in appsettings.json");
            if (_imageDescriptions is not null)
                foreach (var imageDescription in _imageDescriptions)
                {
                    var directory = new DirectoryInfo(Path.Combine(folder, imageDescription.Title));
                    for (int i = 0; i < imageCounter; i++)
                    {
                        imageUrl = await _imageAICommunicator.GetImageUrlAsync(imageDescription.Description);
                        //await ImageDownloader.DownloadImageAsync(imageUrl, directory);
                    }

                    //var illustration = new Illustration
                    //{
                    //    Prompt = imageDescription.Description,
                    //    Path = directory.FullName,
                    //    IllustrationText = "",
                    //    Item = 1
                    //};
                    //_dbContext.Illustrations.Add(illustration);
                    //_dbContext.SaveChanges();
                }
            
            StatusMessage = "Генерация картинок завершена.";
        }

        private bool CanExecuteGenerateImages(object parameter)
        {
            return CanGenerateImages;
        }

        

        private void GenerateScript(object parameter)
        {
            StatusMessage = $"Генерация началась";
            // Пример логики генерации скрипта
            string prompt = $"Не повторяйся. Придумай {ElementCount} промптов для миджоурней. По теме {GeneratedScript} " + // ElementCount - количество генерируемых промтов
                $"Каждый промпт пиши в следующем формате: первая строка - разделитель вида '-----', вторая строка - Название:   " +
                $"Третья строка - Промпт: ";

            var answer = _textAICommunicator.GetTextAnswerAsync(prompt);
            GeneratedScript = answer.Result;
            CanGenerateImages = true;

            _imageDescriptions = DescriptionParser.ParseMany(GeneratedScript);

            StatusMessage = $"Скрипт сгенерирован для {ElementCount} элементов.";
        }

        //private void GenerateScript(object parameter)
        //{
        //    StatusMessage = $"Генерация началась";
        //    // Пример логики генерации скрипта
        //    string prompt = $"Не повторяйся. Придумай {ElementCount} промптов для миджоурней. По теме {GeneratedScript}" + // ElementCount - количество генерируемых промтов
        //        $"Каждый промпт пиши в формате json: \n" +
        //        "{" +
        //        "\"Title\" : \"\"" +
        //        "\"Description\" : \"\"" +
        //        "}";

        //    var answer = _textAICommunicator.GetTextAnswerAsync(prompt);
        //    GeneratedScript = answer.Result;
        //    CanGenerateImages = true;

        //    _imageDescriptions = JsonSerializer.Deserialize<ImageDescription>(GeneratedScript);

        //    StatusMessage = $"Скрипт сгенерирован для {ElementCount} элементов.";
        //}

        private void BrowseFolder(object obj)
        {
            try
            {
                if (_dialogService.OpenDialog() == true)
                {
                    SelectedFolderPath = _dialogService.FilePath;
                }
            }
            catch (Exception ex)
            {
                _dialogService.ShowMessage(ex.Message);
            }
        }
    }
}
