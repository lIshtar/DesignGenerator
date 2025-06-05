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
using DesignGenerator.Domain;
using DesignGenerator.Application.Queries.GetAllPrompts;
using System.Runtime.CompilerServices;

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

        public ICommand LoadedCommand { get; }
        public ICommand TextSelectedCommand { get; }
        public ICommand SendMessageCommand { get; }
        public ICommand ToggleSelectionModeCommand { get; }
        public ICommand GenerateImageCommand { get; }
        public ICommand GenerateMultipleImagesCommand { get; }
        public ICommand InsertPromptCommand { get; }
        public ICommand NavigateToPromptManagerCommand { get; }

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
            LoadedCommand = new AsyncRelayCommand(Loaded);
            SendMessageCommand = new AsyncRelayCommand(SendMessage);
            ToggleSelectionModeCommand = new RelayCommand(ToggleSelectionMode);
            GenerateImageCommand = new AsyncRelayCommand(GenerateImage);
            GenerateMultipleImagesCommand = new RelayCommand(GenerateMultipleImages);
            TextSelectedCommand = new RelayCommand<string>(OnTextSelected);
            InsertPromptCommand = new RelayCommand<Prompt>(InsertPrompt);
            NavigateToPromptManagerCommand = new RelayCommand(NavigateToPromptManager);

            _saveFolder = GetImageFolder(configuration);
            GeneratedImagePath = GetDefaultImagePath();

            // Тестовые сообщения
            //Messages.Add(new ChatMessageViewModel { IsBotMessage = true, IsSelected = true, Text = "Sure! Here are five title and prompt objects for earring design inspirations:\r\n\r\n1. **Title:** Celestial Charm  \r\n   **Prompt:** Design a pair of earrings inspired by celestial bodies. Incorporate elements like stars, moons, and planets using materials such as silver, gold, and gemstones. Consider a mix of dangling elements and stud styles to capture the beauty of the night sky.\r\n\r\n2. **Title:** Oceanic Elegance  \r\n   **Prompt:** Create earrings that reflect the beauty of the ocean. Use materials like coral, shells, and aquatic-themed motifs. Incorporate shades of blue, green, and pearlescent finishes to invoke a sense of the sea, and think about designs that mimic waves or marine life.\r\n\r\n3. **Title:** Nature's Whimsy  \r\n   **Prompt:** Design earrings inspired by the elements of nature, focusing on flora and fauna. Incorporate shapes like leaves, flowers, or animal silhouettes while using sustainable materials. Explore various styles, from minimalistic to intricate details, to celebrate the beauty of the natural world.\r\n\r\n4. **Title:** Geometric Fusion  \r\n   **Prompt:** Create contemporary earrings that embrace geometric shapes and patterns. Experiment with a mix of materials like acrylic, metal, and resin to create eye-catching contrasts. Focus on symmetry and asymmetry to bring a modern twist to classic earring designs.\r\n\r\n5. **Title:** Cultural Heritage  \r\n   **Prompt:** Design earrings that celebrate a specific cultural tradition or heritage. Research traditional motifs, patterns, and colors that are significant to the culture you've chosen. Incorporate authentic materials or techniques that highlight the beauty and significance of that cultural background." });
            //Messages.Add(new ChatMessageViewModel { Text = "Sure! Here are two illustration prompts for you:\r\n\r\n### Title: \"Whimsical Ocean Adventure\"\r\n**Prompt:** Illustrate a vibrant underwater scene where a friendly octopus, wearing a tiny pirate hat, is guiding a group of colorful fish as they explore a sunken treasure chest surrounded by coral reefs. The background should feature rays of sunlight filtering through the water, illuminating the treasure and casting playful shadows, while curious sea turtles and playful dolphins peek from the distance.\r\n\r\n---\r\n\r\n### Title: \"Enchanted Forest Gathering\"\r\n**Prompt:** Create a magical forest clearing where various woodland creatures, such as rabbits, foxes, and deer, have gathered for a mystical moonlit celebration. The scene should be filled with glowing mushrooms and fireflies, with a large, ancient tree at the center adorned with twinkling fairy lights. In the foreground, a wise owl perches on a branch, observing the joyful festivities below, as the atmosphere radiates warmth and enchantment.", IsBotMessage = true });
            //Messages.Add(new ChatMessageViewModel { Text = "```json\r\n[\r\n    {\r\n        \"Title\": \"Natural Wood Drop Earrings\",\r\n        \"Prompt\": \"Elegant drop earrings made from polished wooden discs with engraved floral patterns.\"\r\n    },\r\n    {\r\n        \"Title\": \"Geometric Wood Studs\",\r\n        \"Prompt\": \"Minimalist square wooden studs featuring a smooth, unfinished texture and subtle grain.\"\r\n    },\r\n    {\r\n        \"Title\": \"Boho Wood Hoop Earrings\",\r\n        \"Prompt\": \"Lightweight wooden hoops adorned with vibrant tassels, showcasing a rustic yet trendy design.\"\r\n    }\r\n]\r\n```", IsBotMessage = true });
        }

        private async Task Loaded()
        {
            SavedPrompts = await LoadPrompts();
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
                Messages.Add(new ChatMessageViewModel { Text = UserInput, IsBotMessage = false });
                var processingMessage = new ChatMessageViewModel { Text = "Processing...", IsBotMessage = true };
                Messages.Add(processingMessage);

                var message = new ChatMessageViewModel { IsBotMessage = true };

                var imbeddedString = "Your response must be in the following format:\r\n\r\nTitle: [Title]\r\nPrompt: [Prompt]";
                var communicateQuery = new CommunicateQuery
                {
                    Query = UserInput + imbeddedString
                };

                var response = await Task.Run(() => 
                    _queryDispatcher.Send<CommunicateQuery, CommunicateQueryResponse>(communicateQuery));

                message.Text = response.Message;

                Messages.Remove(processingMessage);
                Messages.Add(message);
                UserInput = string.Empty;
            }
        }

        private void OnTextSelected(string? parameter)
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
            WorkingStatus = "Image creating in progress";
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
            WorkingStatus = "Image creating completed";
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
