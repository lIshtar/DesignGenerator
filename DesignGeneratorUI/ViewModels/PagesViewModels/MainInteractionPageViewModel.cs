using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DesignGenerator.Application;
using DesignGenerator.Application.Commands.AddIllustration;
using DesignGenerator.Application.Interfaces;
using DesignGenerator.Application.Interfaces.ImageGeneration;
using DesignGenerator.Application.Messages;
using DesignGenerator.Application.Parsers;
using DesignGenerator.Application.Queries.Communicate;
using DesignGenerator.Application.Queries.GetAllPrompts;
using DesignGenerator.Domain;
using DesignGenerator.Domain.Interfaces.ImageGeneration;
using DesignGenerator.Domain.Models;
using DesignGeneratorUI.Messages;
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
    // TODO: Добавить генерацию нескольких изображений по промпту.
    // TODO: Изменить подход к взаимодействию с текстовым ИИ. Взять за основу то, что описано для визуальной
    // TODO: Изменить подход к получению модели генерации изображений. Надо внедрять в классы сервис настроек и в нужный момент извлекать из него вабранную модель генерации изображений.
    public class MainInteractionPageViewModel : BaseViewModel
    {
        private string _userInput = "";
        private bool _isSelectionMode;
        private bool _canGenerateImages;
        private string _generatedImagePath = null!;
        private string _selectedText = "";
        private ImageGenerationRequestViewModel _parametersVM = null!;
        public ImageGenerationRequestViewModel ParametersVM
        {
            get => _parametersVM;
            set
            {
                _parametersVM = value;
                OnPropertyChanged(nameof(ParametersVM));
            }
        }

        public ObservableCollection<ChatMessageViewModel> Messages { get; set; } = new();
        public ObservableCollection<Prompt> SavedPrompts { get; set; } = new();

        public string UserInput
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

        public bool CanGenerateImages
        {
            get => _canGenerateImages;
            set { _canGenerateImages = value; OnPropertyChanged(nameof(CanGenerateImages)); }
        }

        public string GeneratedImagePath
        {
            get => _generatedImagePath;
            set { _generatedImagePath = value; OnPropertyChanged(nameof(GeneratedImagePath)); }
        }

        public string SelectedText
        {
            get => _selectedText;
            set
            {
                _selectedText = value;
                OnTextSelected(SelectedText);
                OnPropertyChanged(nameof(SelectedText));
            }
        }

        public ICommand LoadedCommand { get; private set; } = null!;
        public ICommand TextSelectedCommand { get; private set; } = null!;
        public ICommand SendMessageCommand { get; private set; } = null!;
        public ICommand ToggleSelectionModeCommand { get; private set; } = null!;
        public ICommand GenerateImageCommand { get; private set; } = null!;
        public ICommand GenerateMultipleImagesCommand { get; private set; } = null!;
        public ICommand InsertPromptCommand { get; private set; } = null!;
        public ICommand NavigateToPromptManagerCommand { get; private set; } = null!;

        private INavigationService _navigationService;
        private ICommandDispatcher _commandDispatcher;
        private IQueryDispatcher _queryDispatcher;
        private IllustrationTemplateParser _templateParser;
        private IMessenger _messenger;
        private IImageGenerationCoordinator _imageGenerationCoordinator;

        public MainInteractionPageViewModel(
            ICommandDispatcher commandDispatcher,
            IQueryDispatcher queryDispatcher,
            IConfiguration configuration,
            INavigationService navigationService,
            IllustrationTemplateParser templateParser,
            IMessenger messenger,
            IImageGenerationCoordinator imageGenerationCoordinator)
        {
            _navigationService = navigationService;
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
            _templateParser = templateParser;
            _messenger = messenger;
            _imageGenerationCoordinator = imageGenerationCoordinator;

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
            ParametersVM = new();
            _messenger.Register<ModelSelectionChangedMessage>(this, (r, m) => ReloadParameters(m.Value));
        }

        private void InitializeProperties(IConfiguration configuration)
        {
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

        private void ReloadParameters(IImageGenerationClient generationClient)
        {
            ParametersVM.ReloadParameters(generationClient);
        }

        private async Task<ObservableCollection<Prompt>> LoadPrompts()
        {
            var query = new GetAllPromptsQuery();
            var response = await _queryDispatcher.Send<GetAllPromptsQuery, GetAllPromptsResponse>(query);
            return [.. response.Prompts];
        }
        private string GetDefaultImagePath() => Directory.GetCurrentDirectory() + "\\Images\\DefaultImage.png";

        private async Task SendMessage()
        {
            if (string.IsNullOrWhiteSpace(UserInput))
                return;

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
                var parsedData = _templateParser.ParseOne(selectedText);
                ModifyParameter(ParametersVM, parsedData);
            }
        }

        private void ModifyParameter(ImageGenerationRequestViewModel requestViewModel, IllustrationTemplate? template)
        {
            if (template != null)
            {
                requestViewModel.Title = template.Title;
                var prompt = requestViewModel.Params.FirstOrDefault(p => p.DisplayName == "Prompt");
                if (prompt != null)
                {
                    prompt.Value = template.Prompt;
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

            var parametersDescriptors = ParametersVM.Params.Select(p => p.CreateParameterDescriptor());
            string imagePath = await _imageGenerationCoordinator.GenerateAndSaveAsync(parametersDescriptors);
            GeneratedImagePath = imagePath;

            var prompt = ParametersVM.GetParameterValueByDisplayName("Prompt");
            var addCommand = new AddIllustrationCommand
            {
                Title = ParametersVM.Title,
                Prompt = prompt ?? throw new Exception("Could not find prompt of image"),
                IllustrationPath = imagePath,
                IsReviewed = true
            };
            await Task.Run(() => _commandDispatcher.Send(addCommand));

            CanGenerateImages = true;
        }

        // TODO: Переработать логику получения illustrationTemplates с учетом ввода ParametersVM
        private void GenerateMultipleImages()
        {
            var selectedMessages = Messages.Where(x => x.IsSelected);

            var illustrationTemplates = ParseMessages(selectedMessages);

            _messenger.Send(new TemplatesCreatedMessage(illustrationTemplates));

            _navigationService.NavigateTo<DescriptionsViewerPage>();
        }

        private IEnumerable<ImageGenerationRequestViewModel> ParseMessages(IEnumerable<ChatMessageViewModel> messages)
        {
            var illustrationTemplates = new List<ImageGenerationRequestViewModel>();
            foreach (var message in messages)
            {
                var templates = _templateParser.ParseMany(message.Text);
                foreach (var template in templates)
                {
                    //illustrationTemplates.Add(new ImageGenerationRequestViewModel
                    //{
                    //    Title = template.Title,
                    //    Params = new ObservableCollection<ParameterViewModel>
                    //    {
                    //        new ParameterViewModel(new ParameterDescriptor
                    //        {
                    //            DisplayName = "Prompt",
                    //            Value = template.Prompt
                    //        })
                    //    }
                    //});
                    //illustrationTemplates.Add(template);
                }
            }
            return illustrationTemplates;
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
