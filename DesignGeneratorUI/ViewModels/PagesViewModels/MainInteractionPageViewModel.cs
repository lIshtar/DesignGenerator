using DesignGeneratorUI.ViewModels.ElementsViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICommand = System.Windows.Input.ICommand;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Runtime;
using Microsoft.VisualBasic.ApplicationServices;
using DesignGeneratorUI.ViewModels.Navigation;
using DesignGeneratorUI.Views.Pages;
using DesignGenerator.Application.Parsers;
using DesignGenerator.Application.Interfaces;
using DesignGenerator.Application.Commands.CreateIllustration;
using DesignGenerator.Application.Commands.AddNewIllustration;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using DesignGenerator.Application.Queries.Communicate;

namespace DesignGeneratorUI.ViewModels.PagesViewModels
{
    // TODO: Подключить бд
    public class MainInteractionPageViewModel : BaseViewModel
    {
        private string _saveFolder;

        private string? _userInput;
        private bool _isSelectionMode;
        private string? _imageTitle;
        private string? _imageDescription;
        private string? _workingStatus;
        private bool _canGenerateImages;
        private string? _generatedImagePath;
        private string? _selectedText;

        public ObservableCollection<ChatMessageViewModel> Messages { get; set; } = new();

        public string? UserInput
        {
            get => _userInput;
            set
            {
                _userInput = value;
                OnPropertyChanged(nameof(UserInput));
            }
        }

        public bool IsSelectionMode
        {
            get => _isSelectionMode;
            set
            {
                _isSelectionMode = value;
                foreach (var message in Messages)
                    message.IsSelectable = _isSelectionMode;

                OnPropertyChanged(nameof(IsSelectionMode));
            }
        }

        public string? ImageTitle
        {
            get => _imageTitle;
            set
            {
                _imageTitle = value;
                OnPropertyChanged(nameof(ImageTitle));
            }
        }

        public string? ImageDescription
        {
            get => _imageDescription;
            set
            {
                _imageDescription = value;
                OnPropertyChanged(nameof(ImageDescription));
            }
        }

        public string? WorkingStatus
        {
            get => _workingStatus;
            set
            {
                _workingStatus = value;
                OnPropertyChanged(nameof(WorkingStatus));
            }
        }

        public bool CanGenerateImages
        {
            get => _canGenerateImages;
            set { _canGenerateImages = value; OnPropertyChanged(nameof(CanGenerateImages)); }
        }

        public string? GeneratedImagePath
        {
            get => _generatedImagePath;
            set { _generatedImagePath = value; OnPropertyChanged(nameof(GeneratedImagePath)); }
        }

        public string? SelectedText
        {
            get => _selectedText;
            set
            {
                _selectedText = value;
                OnTextSelected(SelectedText);
                OnPropertyChanged(nameof(SelectedText));
            }
        }

        
        public ICommand TextSelectedCommand { get; }
        public ICommand SendMessageCommand { get; }
        public ICommand ToggleSelectionModeCommand { get; }
        public ICommand GenerateImageCommand { get; }
        public ICommand GenerateMultipleImagesCommand { get; }

        private INavigationService _navigationService;
        private ICommandDispatcher _commandDispatcher;
        private IQueryDispatcher _queryDispatcher;

        public MainInteractionPageViewModel(
            ICommandDispatcher commandDispatcher,
            IQueryDispatcher queryDispatcher,
            IConfiguration configuration,
            INavigationService navigationService)
        {
            _navigationService = navigationService;
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;

            //Инициализация команд
            SendMessageCommand = new RelayCommand(SendMessage);
            ToggleSelectionModeCommand = new RelayCommand(ToggleSelectionMode);
            GenerateImageCommand = new RelayCommand(GenerateImage);
            GenerateMultipleImagesCommand = new RelayCommand(GenerateMultipleImages);
            TextSelectedCommand = new RelayCommand(OnTextSelected);

            _saveFolder = GetImageFolder(configuration);
            GeneratedImagePath = GetDefaultImagePath();
            SelectedText = "";

            // Тестовые сообщения
            Messages.Add(new ChatMessageViewModel { Text = "Привет! Как я могу помочь?", IsBotMessage = true });
        }

        private string GetDefaultImagePath() => Directory.GetCurrentDirectory() + "\\Images\\DefaultImage.png";

        private string GetImageFolder(IConfiguration configuration)
        {
            return configuration.GetRequiredSection("Folders")
                    .GetRequiredSection("DefaultImageFolder").Value
                    ?? throw new Exception("Unable to find DefaultImageFolder in appsettings.json");
        }

        private async void SendMessage(object parameter)
        {
            if (!string.IsNullOrWhiteSpace(UserInput))
            {
                Messages.Add(new ChatMessageViewModel { Text = UserInput, IsBotMessage = false });
                var processingMessage = new ChatMessageViewModel { Text = "Processing...", IsBotMessage = true };
                Messages.Add(processingMessage);

                var communicateQuery = new CommunicateQuery
                {
                    Query = UserInput
                };
                var result = _queryDispatcher.Send<CommunicateQuery, CommunicateQueryResponse>(communicateQuery).Result;

                Messages.Remove(processingMessage);
                Messages.Add(new ChatMessageViewModel { Text = result.Answer, IsBotMessage = true });
                UserInput = string.Empty;
            }
        }

        private void OnTextSelected(object? parameter)
        {
            if (parameter is string selectedText)
            {
                ImageDescription imageDescription = DescriptionParser.ParseOne(selectedText);
                if (imageDescription != null)
                {
                    this.ImageDescription = imageDescription.Description;
                    ImageTitle = imageDescription.Title;
                    WorkingStatus = "Successfully parsed image description";
                }
                else
                {
                    WorkingStatus = "Couldn't parse image description";
                }
            }
            else
            {
                WorkingStatus = "Selected text was not recognized";
            }
        }

        private void ToggleSelectionMode(object parameter)
        {
            foreach (var mes in Messages)
            {
                if (mes.IsBotMessage)
                    mes.IsSelectable = !mes.IsSelectable;
            }
        }

        private async void GenerateImage(object parameter)
        {
            WorkingStatus = "Image creating in progress";
            CanGenerateImages = false;
            string fullFolderPath = _saveFolder + "\\" + ImageTitle;

            var createCommand = new CreateIllustrationCommand
            {
                Prompt = ImageDescription,
                FolderPath = fullFolderPath
            };
            _commandDispatcher.Send<CreateIllustrationCommand>(createCommand);

            var addCommand = new AddNewIllustrationCommand
            {
                Title = ImageTitle,
                Description = ImageDescription,
                IllustrationFolder = fullFolderPath
            };
            _commandDispatcher.Send<AddNewIllustrationCommand>(addCommand);

            CanGenerateImages = true;
            WorkingStatus = "Image creating completed";
        }

        private void GenerateMultipleImages(object parameter)
        {
            _navigationService.NavigateTo<DescriptionsViewerPage>();
        }
    }
}
