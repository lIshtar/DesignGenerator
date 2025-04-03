using DesignGeneratorUI.ViewModels.ElementsViewModel;
using System.Collections.ObjectModel;
using ICommand = System.Windows.Input.ICommand;
using Microsoft.Extensions.Configuration;
using System.IO;
using DesignGeneratorUI.ViewModels.Navigation;
using DesignGeneratorUI.Views.Pages;
using DesignGenerator.Application.Interfaces;
using DesignGenerator.Application.Queries.CreateIllustration;
using DesignGenerator.Application.Commands.AddIllustration;
using DesignGenerator.Application.Queries.Communicate;
using DesignGenerator.Application;
using CommunityToolkit.Mvvm.Input;
namespace DesignGeneratorUI.ViewModels.PagesViewModels
{
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
        private ITemplateParser _templateParser;

        public MainInteractionPageViewModel(
            ICommandDispatcher commandDispatcher,
            IQueryDispatcher queryDispatcher,
            IConfiguration configuration,
            INavigationService navigationService,
            ITemplateParser templateParser)
        {
            _navigationService = navigationService;
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
            _templateParser = templateParser;

            //Инициализация команд
            SendMessageCommand = new AsyncRelayCommand(SendMessage);
            ToggleSelectionModeCommand = new RelayCommand(ToggleSelectionMode);
            GenerateImageCommand = new AsyncRelayCommand(GenerateImage);
            GenerateMultipleImagesCommand = new RelayCommand(GenerateMultipleImages);
            TextSelectedCommand = new RelayCommand(OnTextSelected);

            _saveFolder = GetImageFolder(configuration);
            GeneratedImagePath = GetDefaultImagePath();
            SelectedText = "";

            // Тестовые сообщения
            Messages.Add(new ChatMessageViewModel { Text = "Вот пример двух объектов в формате JSON с полями `Title` и `Prompt`, которые могут использоваться для иллюстрации:\r\n\r\n```json\r\n[\r\n    {\r\n        \"Title\": \"Тайный сад\",\r\n        \"Prompt\": \"Изображение волшебного сада, полного ярких цветов и таинственных существ, с солнечными лучами, пробивающимися сквозь листву.\"\r\n    },\r\n    {\r\n        \"Title\": \"Космическое путешествие\",\r\n        \"Prompt\": \"Иллюстрация космического корабля, плывущего сквозь галактику, окруженного звездами и планетами, с яркими цветными туманностями на фоне.\"\r\n    }\r\n]\r\n```\r\n\r\nЭти объекты содержат названия иллюстраций и описания, которые могут быть использованы для их создания.", IsBotMessage = true });
            _templateParser = templateParser;
        }

        private string GetDefaultImagePath() => Directory.GetCurrentDirectory() + "\\Images\\DefaultImage.png";

        private string GetImageFolder(IConfiguration configuration)
        {
            return configuration.GetRequiredSection("Folders")
                    .GetRequiredSection("DefaultImageFolder").Value
                    ?? throw new Exception("Unable to find DefaultImageFolder in appsettings.json");
        }

        private async Task SendMessage()
        {
            
            if (!string.IsNullOrWhiteSpace(UserInput))
            {
                Messages.Add(new ChatMessageViewModel { Text = UserInput, IsBotMessage = false });
                var processingMessage = new ChatMessageViewModel { Text = "Processing...", IsBotMessage = true };
                Messages.Add(processingMessage);

                var response = new CommunicateQueryResponse();
                var message = new ChatMessageViewModel { IsBotMessage = true };

                await Task.Run(async () => {
                    var communicateQuery = new CommunicateQuery
                    {
                        Query = UserInput
                    };
                    response = await _queryDispatcher.Send<CommunicateQuery, CommunicateQueryResponse>(communicateQuery);
                    message.Text = response.Message;
                });
                

                Messages.Remove(processingMessage);
                Messages.Add(message);
                UserInput = string.Empty;
            }
        }

        private void OnTextSelected(object? parameter)
        {
            if (parameter is string selectedText)
            {
                IllustrationTemplate imageDescription = _templateParser.ParseOne(selectedText);
                if (imageDescription != null)
                {
                    this.ImageDescription = imageDescription.Prompt;
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

        private async Task GenerateImage()
        {
            WorkingStatus = "Image creating in progress";
            CanGenerateImages = false;
            string fullFolderPath = _saveFolder + "\\" + ImageTitle;

            var createCommand = new CreateIllustrationQuery
            {
                Prompt = ImageDescription,
                FolderPath = fullFolderPath
            };
            var response = await _queryDispatcher.Send<CreateIllustrationQuery, CreateIllustrationQueryResponse>(createCommand);
            GeneratedImagePath = response.IllustrationPath;

            var addCommand = new AddIllustrationCommand
            {
                Title = ImageTitle,
                Prompt = ImageDescription,
                IllustrationPath = GeneratedImagePath,
                IsReviewed = true
            };
            await _commandDispatcher.Send<AddIllustrationCommand>(addCommand);

            CanGenerateImages = true;
            WorkingStatus = "Image creating completed";
        }

        private void GenerateMultipleImages(object parameter)
        {
            var selectedMessages = Messages.Where(x => x.IsSelected).ToList();
            var illustrationTemplates = IllustrationTemplatesContainer.GetInstance().IllustrastionsTemplates;
            foreach (var message in selectedMessages)
            {
                var templates = _templateParser.ParseMany(message.Text);
                foreach (var template in templates)
                {
                    if (illustrationTemplates.Where(x => x.Title == template.Title && x.Prompt == template.Prompt).Count() == 0)
                        illustrationTemplates.Add(template);
                }
            }
            IllustrationTemplatesContainer.GetInstance().IllustrastionsTemplates = illustrationTemplates;
            _navigationService.NavigateTo<DescriptionsViewerPage>();
        }
    }
}
