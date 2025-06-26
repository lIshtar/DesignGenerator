using DesignGenerator.Application;
using DesignGenerator.Application.Commands.AddIllustration;
using DesignGenerator.Application.Interfaces;
using DesignGeneratorUI.ViewModels.Navigation;
using DesignGeneratorUI.Views.Pages;
using Microsoft.Extensions.Configuration;
using ICommand = System.Windows.Input.ICommand;
using Application = System.Windows.Application;
using CommunityToolkit.Mvvm.Messaging;
using DesignGeneratorUI.Messages;
using DesignGenerator.Application.Interfaces.ImageGeneration;
using DesignGenerator.Application.Messages;
using DesignGeneratorUI.ViewModels.ElementsViewModel;

namespace DesignGeneratorUI.ViewModels.PagesViewModels
{
    public class GenerationProgressPageViewModel : BaseViewModel
    {
        private readonly CancellationTokenSource _cts;
        private readonly INavigationService _navigationService;
        private readonly ICommandDispatcher _commandDispatcher;
        private List<ImageGenerationRequestViewModel> _illustrationsTemplates;
        private IImageGenerationCoordinator _imageGenerationCoordinator;

        private double _progressValue;
        private string? _progressText;
        private int _numberOfImages;
        private bool _isGenerationComplete;

        public ICommand CancelCommand { get; }
        public ICommand GoToViewerCommand { get; }
        public ICommand StartGenerationCommand { get; }

        public double ProgressValue
        {
            get => _progressValue;
            set { _progressValue = value; OnPropertyChanged(nameof(ProgressValue)); }
        }

        public string? ProgressText
        {
            get => _progressText;
            set { _progressText = value; OnPropertyChanged(nameof(ProgressText)); }
        }

        public int NumberOfImages
        {
            get => _numberOfImages;
            set { _numberOfImages = value; OnPropertyChanged(nameof(NumberOfImages)); }
        }

        public bool IsGenerationComplete
        {
            get => _isGenerationComplete;
            set { _isGenerationComplete = value; OnPropertyChanged(nameof(IsGenerationComplete)); }
        }

        public GenerationProgressPageViewModel(
            INavigationService navigationService, 
            ICommandDispatcher commandDispatcher, 
            IConfiguration configuration, 
            IMessenger messenger,
            IImageGenerationCoordinator imageGenerationCoordinator)
        {
            _navigationService = navigationService;
            _commandDispatcher = commandDispatcher;
            _imageGenerationCoordinator = imageGenerationCoordinator;

            _illustrationsTemplates = new();

            ProgressValue = 0;
            ProgressText = "Ожидание начала...";

            _cts = new CancellationTokenSource();

            messenger.Register<TemplatesCreatedMessage>(this, (e, m) => ReloadTemplates(m.Value));
            messenger.Register<TemplatesModifiedMessage>(this, (e, m) => ReloadTemplates(m.Value));

            CancelCommand = new RelayCommand(CancelGeneration);
            GoToViewerCommand = new RelayCommand(GoToViewer);
            StartGenerationCommand = new RelayCommand(ExecuteStartGeneration);
        }

        private void ReloadTemplates(IEnumerable<ImageGenerationRequestViewModel> templates)
        {
            _illustrationsTemplates = [.. templates];
        }

        private void GoToViewer()
        {
            _navigationService.NavigateTo<ImageViewerPage>();
        }

        private void ExecuteStartGeneration()
        {
            var temp = _illustrationsTemplates.Count;
            Task.Run(() => StartGeneration(temp));
        }

        private async Task StartGeneration(int numberOfImages)
        {
            try
            {
                await SetProgressText("Генерация начата!");

                for (int i = 0; i < numberOfImages; i++)
                {
                    if (_cts.Token.IsCancellationRequested)
                        break;

                    var parametersVM = _illustrationsTemplates[i];
                    var parameters = parametersVM.ToParameterDescriptors();
                    var imagePath = await _imageGenerationCoordinator.GenerateAndSaveAsync(parameters);

                    await SetProgressValue(i + 1);

                    await SetProgressText($"Генерация... {(int)(ProgressValue / numberOfImages * 100)}%");


                    var addCommand = new AddIllustrationCommand
                    {
                        Title = _illustrationsTemplates[i].Title ?? "",
                        Prompt = _illustrationsTemplates[i].Prompt ?? throw new Exception("Could not find prompt of image"),
                        IllustrationPath = imagePath,
                        IsReviewed = false,
                    };

                    await Task.Run(() => _commandDispatcher.Send(addCommand));
                }

                await SetProgressText("Генерация завершена");
            }
            catch (OperationCanceledException)
            {
                //ProgressText = "Отменено";
            }
        }

        private async Task SetProgressText(string message)
        {
            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                ProgressText = message;
            });
        }

        private async Task SetProgressValue(int value)
        {
            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                ProgressValue = value;
            });
        }

        private void CancelGeneration()
        {
            _cts.Cancel();
            _navigationService.NavigateTo<MainInteractionPage>();
        }
    }
}
