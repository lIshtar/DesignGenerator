using DesignGenerator.Application;
using DesignGenerator.Application.Commands.AddIllustration;
using DesignGenerator.Application.Queries.CreateIllustration;
using DesignGenerator.Application.Interfaces;
using DesignGeneratorUI.ViewModels.Navigation;
using DesignGeneratorUI.Views.Pages;
using Microsoft.Extensions.Configuration;
using ICommand = System.Windows.Input.ICommand;
using CommunityToolkit.Mvvm.Input;
using Application = System.Windows.Application;

namespace DesignGeneratorUI.ViewModels.PagesViewModels
{
    public class GenerationProgressPageViewModel : BaseViewModel
    {
        private readonly CancellationTokenSource _cts;
        private readonly INavigationService _navigationService;
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;
        private List<IllustrationTemplate> _illustrationsTemplates;
        private string _saveFolder;

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

        public GenerationProgressPageViewModel(INavigationService navigationService, IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher, IConfiguration configuration)
        {
            _navigationService = navigationService;
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;

            ProgressValue = 0;
            ProgressText = "Ожидание начала...";

            _cts = new CancellationTokenSource();
            _saveFolder = GetImageFolder(configuration);

            _illustrationsTemplates = IllustrationTemplatesContainer.GetInstance().IllustrastionsTemplates.ToList();
            NumberOfImages = _illustrationsTemplates.Count;
            CancelCommand = new RelayCommand(CancelGeneration);
            GoToViewerCommand = new RelayCommand(GoToViewer);
            StartGenerationCommand = new RelayCommand(ExecuteStartGeneration);
        }

        private string GetImageFolder(IConfiguration configuration)
        {
            return configuration.GetRequiredSection("Folders")
                    .GetRequiredSection("DefaultImageFolder").Value
                    ?? throw new Exception("Unable to find DefaultImageFolder in appsettings.json");
        }

        private void GoToViewer(object argument)
        {
            _navigationService.NavigateTo<ImageViewerPage>();
        }

        private void ExecuteStartGeneration(object argument)
        {
            var temp = NumberOfImages;
            Task.Run(() => StartGeneration(temp));
        }

        private async Task StartGeneration(int numberOfImages)
        {
            try
            {
                await Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    ProgressText = "Генерация начата!";
                });

                string illustrationFolder;
                for (int i = 0; i < numberOfImages; i++)
                {
                    if (_cts.Token.IsCancellationRequested)
                        break;

                    illustrationFolder = _saveFolder + "\\" + _illustrationsTemplates[i].Title;
                    var createQuery = new CreateIllustrationQuery
                    {
                        Prompt = _illustrationsTemplates[i].Prompt,
                        FolderPath = illustrationFolder
                    };

                    // TODO: replace with real logic
                    await Task.Delay(1000);

                    var createIllustrationQueryResponse = await Task.Run(() =>
                        _queryDispatcher.Send<CreateIllustrationQuery, CreateIllustrationQueryResponse>(createQuery));

                    await Application.Current.Dispatcher.InvokeAsync(() =>
                    {
                        ProgressValue = (i + 1);
                    });

                    await Application.Current.Dispatcher.InvokeAsync(() =>
                    {
                        ProgressText = $"Генерация... {ProgressValue / numberOfImages * 100}%";
                    });
                    

                    //var addCommand = new AddIllustrationCommand
                    //{
                    //    Title = _illustrationsTemplates[i].Title,
                    //    Prompt = _illustrationsTemplates[i].Prompt,
                    //    IllustrationPath = createIllustrationQueryResponse.IllustrationPath,
                    //    IsReviewed = false,
                    //};

                    //await Task.Run(() => _commandDispatcher.Send<AddIllustrationCommand>(addCommand));
                }

                await Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    ProgressText = "Генерация завершена!";
                });
                //
            }
            catch (OperationCanceledException)
            {
                //ProgressText = "Отменено";
            }
        }

        private void CancelGeneration(object argument)
        {
            _cts.Cancel();
            _navigationService.NavigateTo<MainInteractionPage>();
        }
    }
}
