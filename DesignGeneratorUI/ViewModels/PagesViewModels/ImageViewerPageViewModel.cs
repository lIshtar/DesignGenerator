using DesignGenerator.Application.Commands.AddIllustration;
using DesignGenerator.Application.Interfaces;
using ICommand = System.Windows.Input.ICommand;
using CommunityToolkit.Mvvm.Input;
using DesignGenerator.Domain;
using DesignGenerator.Application.Queries.GetUnreviewedIllustrations;
using DesignGenerator.Application.Commands.UpdateIllustration;
using DesignGeneratorUI.ViewModels.ElementsViewModel;

namespace DesignGeneratorUI.ViewModels.PagesViewModels
{
    public partial class ImageViewerPageViewModel : BaseViewModel
    {
        private CreatedIllustrationViewModel _selectedIllustration;
        private bool _isRegenerateEnabled;

        public CreatedIllustrationViewModel SelectedIllustration
        {
            get => _selectedIllustration;
            set { _selectedIllustration = value; OnPropertyChanged(nameof(SelectedIllustration)); }
        }

        public ICommand NextImageCommand { get; private set; } = null!;
        public ICommand RegenerateCommand { get; private set; } = null!;
        public ICommand MarkAsReviewedCommand { get; private set; } = null!;
        public ICommand LoadedCommand { get; private set; } = null!;

        private List<Illustration>? _illustrations;
        private int _illustrationIndex = 0;

        private IQueryDispatcher _queryDispatcher;
        private ICommandDispatcher _commandDispatcher;

        public ImageViewerPageViewModel(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;

            InitializeCommands();
        }

        private void InitializeCommands()
        {
            NextImageCommand = new AsyncRelayCommand(NextImage);
            RegenerateCommand = new AsyncRelayCommand(RegenerateImage);
            MarkAsReviewedCommand = new AsyncRelayCommand(MarkAsReviewed);
            LoadedCommand = new AsyncRelayCommand(Loaded);
        }

        private async Task Loaded()
        {
            await InitializeIllustrations();
        }

        private async Task InitializeIllustrations()
        {
            var command = new GetUnreviewedIllustrationsQuery();
            var response = await _queryDispatcher.Send<GetUnreviewedIllustrationsQuery, GetUnreviewedIllustrationsQueryResponse>(command);
            _illustrations = response.Illustrations;

            if (IsIndexLegal())
            {
                var current = _illustrations[_illustrationIndex];
                SelectNewIllustration(current);
            }
            else
            {
                SelectDefaultIlustration();
            }
        }

        private bool IsIndexLegal() => 
            _illustrations is not null && 
            _illustrations.Count > 0 && 
            _illustrationIndex < _illustrations.Count;

        private void SelectNewIllustration(Illustration illustration)
        {
            SelectedIllustration = new CreatedIllustrationViewModel
            {
                Title = illustration.Title,
                Prompt = illustration.Prompt,
                IllustrationPath = illustration.IllustrationPath,
            };
        }
        private void SelectDefaultIlustration()
        {
            SelectedIllustration = new CreatedIllustrationViewModel
            {
                Title = "Больше нечего проверять",
                Prompt = "",
                IllustrationPath = "C:\\Users\\Ishtar\\Институт\\Диплом\\DesignGeneratorUI\\DesignGeneratorUI\\Images\\DefaultImage.png",
            };
        }

        private async Task NextImage()
        {
            if (_illustrations is null || 
                _illustrationIndex == _illustrations.Count - 1)
            {
                await InitializeIllustrations();
                _illustrationIndex = 0;
            }
            else
            {
                _illustrationIndex++;
            }

            if (_illustrations is null || !_illustrations.Any())
                return;

            var current = _illustrations[_illustrationIndex];

            SelectNewIllustration(current);
        }

        private async Task RegenerateImage()
        {
            if (SelectedIllustration.Title == "Больше нечего проверять")
                return;

            //var createCommand = new CreateIllustrationQuery
            //{
            //    Prompt = SelectedIllustration.Prompt,
            //    FolderPath = SelectedIllustration.IllustrationPath
            //};
            //var response = await _queryDispatcher.Send<CreateIllustrationQuery, CreateIllustrationQueryResponse>(createCommand);
            //SelectedIllustration.IllustrationPath = response.IllustrationPath;

            var addCommand = new AddIllustrationCommand
            {
                Title = SelectedIllustration.Title,
                Prompt = SelectedIllustration.Prompt,
                IllustrationPath = SelectedIllustration.IllustrationPath
            };
            await _commandDispatcher.Send<AddIllustrationCommand>(addCommand);
        }

        private async Task MarkAsReviewed()
        {
            if (_illustrations is not null &&
                _illustrations.Count > _illustrationIndex)
            {
                var current = _illustrations[_illustrationIndex];
                current.IsReviewed = true;
                var updateCommand = new UpdateIllustrationCommand
                { 
                    Id = current.Id,
                    Title = current .Title,
                    Prompt = current.Prompt,
                    IllustrationPath = current.IllustrationPath,
                    IsReviewed = current.IsReviewed,
                };


                await _commandDispatcher.Send<UpdateIllustrationCommand>(updateCommand);
            }

            await NextImage();
        }
    }
}
