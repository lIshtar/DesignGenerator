using DesignGenerator.Application.Commands.AddIllustration;
using DesignGenerator.Application.Queries.CreateIllustration;
using DesignGenerator.Application.Interfaces;
using DesignGenerator.Application.Parsers;
using DesignGeneratorUI.ViewModels.ElementsViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICommand = System.Windows.Input.ICommand;
using CommunityToolkit.Mvvm.Input;
using DesignGenerator.Domain;
using DesignGenerator.Application.Queries.GetUnreviewedIllustrations;

namespace DesignGeneratorUI.ViewModels.PagesViewModels
{
    // TODO: Пришить БД
    public class ImageViewerPageViewModel : BaseViewModel
    {
        private string? _title;
        private string? _prompt;
        private string? _imageSource;
        private bool _isRegenerateEnabled;

        public string? Title
        {
            get => _title;
            set { _title = value; OnPropertyChanged(nameof(Title)); }
        }

        public string? Prompt
        {
            get => _prompt;
            set { _prompt = value; OnPropertyChanged(nameof(Prompt)); }
        }

        public string? ImageSource
        {
            get => _imageSource;
            set { _imageSource = value; OnPropertyChanged(nameof(ImageSource)); }
        }

        public bool IsRegenerateEnabled
        {
            get => _isRegenerateEnabled;
            set { _isRegenerateEnabled = value; OnPropertyChanged(nameof(IsRegenerateEnabled)); }
        }

        public ICommand NextImageCommand { get; }
        public ICommand RegenerateCommand { get; }

        private List<Illustration>? _illustrations;
        private int _illustrationIndex;

        private IQueryDispatcher _queryDispatcher;
        private ICommandDispatcher _commandDispatcher;

        public ImageViewerPageViewModel(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;

            NextImageCommand = new AsyncRelayCommand(NextImage);
            RegenerateCommand = new AsyncRelayCommand(RegenerateImage, () => IsRegenerateEnabled);

            //InitializeIllustrations();

            // Инициализация данных
            //if (_illustrations is not null)
            //{
            //    _illustrationIndex = -1;

            //    Title = _illustrations[_illustrationIndex].Title;
            //    Prompt = _illustrations[_illustrationIndex].Prompt;
            //    ImageSource = _illustrations[_illustrationIndex].IllustrationPath;
            //}
            
        }

        // TODO: Реализовать метод получения данных об иллюстрации
        private async Task InitializeIllustrations()
        {
            var command = new GetUnreviewedIllustrationsQuery();
            var response = await _queryDispatcher.Send<GetUnreviewedIllustrationsQuery, GetUnreviewedIllustrationsQueryResponse>(command);
            _illustrations = response.Illustrations;
        }

        private async Task NextImage()
        {
            if (_illustrations is null)
            {
                await InitializeIllustrations();
                _illustrationIndex = 0;
            }
            else
            {
                _illustrationIndex++;
            }

            Title = _illustrations[_illustrationIndex].Title;
            Prompt = _illustrations[_illustrationIndex].Prompt;
            ImageSource = _illustrations[_illustrationIndex].IllustrationPath;
        }

        private async Task RegenerateImage()
        {
            var createCommand = new CreateIllustrationQuery
            {
                Prompt = Prompt,
                FolderPath = ImageSource
            };
            var response = await _queryDispatcher.Send<CreateIllustrationQuery, CreateIllustrationQueryResponse>(createCommand);
            ImageSource = response.IllustrationPath;

            var addCommand = new AddIllustrationCommand
            {
                Title = this.Title,
                Prompt = this.Prompt,
                IllustrationPath = ImageSource
            };
            await _commandDispatcher.Send<AddIllustrationCommand>(addCommand);
            IsRegenerateEnabled = false;
        }
    }
}
