using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DesignGenerator.Application;
using DesignGenerator.Application.Commands.AddIllustration;
using DesignGenerator.Application.Interfaces;
using DesignGenerator.Application.Queries.Communicate;
using DesignGenerator.Application.Queries.CreateIllustration;
using DesignGenerator.Application.Queries.GetAllPrompts;
using DesignGenerator.Domain;
using DesignGeneratorUI.ViewModels.ElementsViewModel;
using DesignGeneratorUI.ViewModels.Navigation;
using DesignGeneratorUI.Views.Pages;
using Microsoft.Extensions.Configuration;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.CompilerServices;
using static Yandex.Cloud.Serverless.Triggers.V1.Trigger.Types;
using ICommand = System.Windows.Input.ICommand;

namespace DesignGeneratorUI.ViewModels.PagesViewModels
{
    // TODO: Выделение текста не меняет значение промпта у _visualParametersVM. Функция выделения стала околобесполезной
    // TODO: Добавить генерацию нескольких изображений по промпту.
    // TODO: Добавить возможность отключения параметров
    public class MainInteractionPageViewModel : BaseViewModel
    {
        private string _saveFolder;
        private string? _userInput;
        private bool _isSelectionMode;
        private string? _imageTitle;
        private string? _imageDescription;
        private bool _canGenerateImages;
        private string? _generatedImagePath;
        private string? _selectedText;
        private ParametersSetViewModel _visualParametersVM;

        public ObservableCollection<ParameterViewModel> Parameters { get => _visualParametersVM.Parameters; } 
        public ObservableCollection<ChatMessageViewModel> Messages { get; set; } = new();
        public ObservableCollection<Prompt> SavedPrompts { get; set; } = new();

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

        public ICommand LoadedCommand { get; private set; }
        public ICommand TextSelectedCommand { get; private set;  }
        public ICommand SendMessageCommand { get; private set; }
        public ICommand ToggleSelectionModeCommand { get; private set; }
        public ICommand GenerateImageCommand { get; private set; }
        public ICommand GenerateMultipleImagesCommand { get; private set; }
        public ICommand InsertPromptCommand { get; private set; }
        public ICommand NavigateToPromptManagerCommand { get; private set; }

        private INavigationService _navigationService;
        private ICommandDispatcher _commandDispatcher;
        private IQueryDispatcher _queryDispatcher;
        private ITemplateParser _templateParser;
        private IMessenger _messenger;

        public MainInteractionPageViewModel(
            ICommandDispatcher commandDispatcher,
            IQueryDispatcher queryDispatcher,
            IConfiguration configuration,
            INavigationService navigationService,
            ITemplateParser templateParser,
            IMessenger messenger)
        {
            _navigationService = navigationService;
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
            _templateParser = templateParser;
            _messenger = messenger;

            Initialize(configuration);
        }

        private async Task Loaded()
        {
            SavedPrompts = await LoadPrompts();
        }

        private void Initialize(IConfiguration configuration)
        {
            InitializeProperties(configuration);
            InitializeCommands();
            InitializeVievModels();
        }

        private void InitializeVievModels()
        {
            _visualParametersVM = new ParametersSetViewModel(_messenger);
        }

        private void InitializeProperties(IConfiguration configuration)
        {
            _saveFolder = GetImageFolder(configuration);
            GeneratedImagePath = GetDefaultImagePath();
        }

        private void InitializeCommands()
        {
            //Инициализация команд
            LoadedCommand = new AsyncRelayCommand(Loaded);
            SendMessageCommand = new AsyncRelayCommand(SendMessage);
            ToggleSelectionModeCommand = new RelayCommand(ToggleSelectionMode);
            GenerateImageCommand = new AsyncRelayCommand(GenerateImage);
            GenerateMultipleImagesCommand = new RelayCommand(GenerateMultipleImages);
            TextSelectedCommand = new RelayCommand<string>(OnTextSelected);
            InsertPromptCommand = new RelayCommand<Prompt>(InsertPrompt);
            NavigateToPromptManagerCommand = new RelayCommand(NavigateToPromptManager);
        }

        private async Task<ObservableCollection<Prompt>> LoadPrompts()
        {
            var query = new GetAllPromptsQuery();
            var response = await _queryDispatcher.Send<GetAllPromptsQuery, GetAllPromptsResponse>(query);
            return [.. response.Prompts];
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
                PostMessage(UserInput, false);
                var processingMessage = PostMessage("Processing...", true);

                var communicateQuery = new CommunicateQuery
                {
                    Query = UserInput
                };

                var response = await Task.Run(() => 
                    _queryDispatcher.Send<CommunicateQuery, CommunicateQueryResponse>(communicateQuery));

                Messages.Remove(processingMessage);
                PostMessage(response.Message, true);

                ClearUserInput();
            }
        }

        private void ClearUserInput()
        {
            UserInput = string.Empty;
        }

        private ChatMessageViewModel PostMessage(string messageText, bool isBotMessage)
        {
            var message = new ChatMessageViewModel { Text = messageText, IsBotMessage = isBotMessage };
            Messages.Add(message);
            return message;
        }

        private void OnTextSelected(string? parameter)
        {
            if (parameter is string selectedText)
            {
                IllustrationTemplate imageDescription = _templateParser.ParseOne(selectedText);
                if (imageDescription != null)
                {
                    ImageDescription = imageDescription.Prompt;
                    ImageTitle = imageDescription.Title;
                }
            }
        }

        private void ToggleSelectionMode()
        {
            foreach (var mes in Messages)
            {
                if (mes.IsBotMessage)
                    mes.IsSelectable = !mes.IsSelectable;
            }
        }

        private async Task GenerateImage()
        {
            CanGenerateImages = false;
            string fullFolderPath = _saveFolder + "\\" + ImageTitle;

            var createCommand = new CreateIllustrationQuery
            {
                Prompt = ImageDescription,
                FolderPath = fullFolderPath
            };
            var response = await Task.Run(() => _queryDispatcher.Send<CreateIllustrationQuery, CreateIllustrationQueryResponse>(createCommand));
            GeneratedImagePath = response.IllustrationPath;

            var addCommand = new AddIllustrationCommand
            {
                Title = ImageTitle,
                Prompt = ImageDescription,
                IllustrationPath = response.IllustrationPath,
                IsReviewed = true
            };
            await Task.Run(() => _commandDispatcher.Send(addCommand));

            CanGenerateImages = true;
        }

        private void GenerateMultipleImages()
        {
            var selectedMessages = Messages.Where(x => x.IsSelected).ToList();
            var illustrationTemplates = IllustrationTemplatesContainer.GetInstance().IllustrastionsTemplates;
            illustrationTemplates.Clear();
            foreach (var message in selectedMessages)
            {
                  
                var templates = _templateParser.ParseMany(message.Text);
                foreach (var template in templates)
                {
                    illustrationTemplates.Add(template);
                }
            }

            IllustrationTemplatesContainer.GetInstance().IllustrastionsTemplates = illustrationTemplates;
            _navigationService.NavigateTo<DescriptionsViewerPage>();
        }

        private void InsertPrompt(Prompt prompt)
        {
            if (prompt == null) return;

            // Добавим текст промпта в начало строки ввода
            if (!string.IsNullOrWhiteSpace(UserInput))
                UserInput = prompt.Text + Environment.NewLine + UserInput;
            else
                UserInput = prompt.Text;
        }

        private void NavigateToPromptManager()
        {
            _navigationService.NavigateTo<PromptManagerPage>();
        }
    }
}
