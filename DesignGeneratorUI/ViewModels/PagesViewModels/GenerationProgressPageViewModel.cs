using DesignGenerator.Application;
using DesignGenerator.Application.Commands.AddNewIllustration;
using DesignGenerator.Application.Queries.CreateIllustration;
using DesignGenerator.Application.Interfaces;
using DesignGenerator.Application.Parsers;
using DesignGeneratorUI.ViewModels.ElementsViewModel;
using DesignGeneratorUI.ViewModels.Navigation;
using DesignGeneratorUI.Views.Pages;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICommand = System.Windows.Input.ICommand;

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

        private int _progressValue;
        private string? _progressText;
        private int _numberOfImages;
        private bool _isGenerationComplete;

        public ICommand CancelCommand { get; }
        public ICommand GoToViewerCommand { get; }
        public ICommand StartGenerationCommand { get; }

        public int ProgressValue
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
            StartGenerationCommand = new RelayCommand(StartGeneration);
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

        private async void StartGeneration(object argument)
        {
            try
            {
                string illustrationFolder;
                for (int i = 0; i < NumberOfImages; i++)
                {
                    if (_cts.Token.IsCancellationRequested)
                        break;

                    ProgressValue = i/NumberOfImages;
                    ProgressText = $"Генерация... {ProgressValue}%";

                    illustrationFolder = _saveFolder + "\\" + _illustrationsTemplates[i].Title;
                    var createCommand = new CreateIllustrationQuery
                    {
                        Prompt = _illustrationsTemplates[i].Prompt,
                        FolderPath = illustrationFolder
                    };
                    await _queryDispatcher.Send<CreateIllustrationQuery, CreateIllustrationQueryResponse>(createCommand);

                    var addCommand = new AddNewIllustrationCommand
                    {
                        Title = _illustrationsTemplates[i].Title,
                        Description = _illustrationsTemplates[i].Prompt,
                        IllustrationFolder = illustrationFolder
                    };
                    _commandDispatcher.Send<AddNewIllustrationCommand>(addCommand);
                }

                ProgressText = "Генерация завершена!";
            }
            catch (OperationCanceledException)
            {
                ProgressText = "Отменено";
            }
        }

        private void CancelGeneration(object argument)
        {
            _cts.Cancel();
            _navigationService.NavigateTo<MainInteractionPage>();
        }
    }
}
