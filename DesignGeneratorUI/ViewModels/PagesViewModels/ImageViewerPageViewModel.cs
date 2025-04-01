using DesignGenerator.Application.Commands.AddNewIllustration;
using DesignGenerator.Application.Commands.CreateIllustration;
using DesignGenerator.Application.Interfaces;
using DesignGenerator.Application.Parsers;
using DesignGeneratorUI.ViewModels.ElementsViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICommand = System.Windows.Input.ICommand;

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

        private List<CreatedIllustrationViewModel>? _illustrations;
        private int _illustrationIndex;

        private IQueryDispatcher _queryDispatcher;
        private ICommandDispatcher _commandDispatcher;

        public ImageViewerPageViewModel(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;

            NextImageCommand = new RelayCommand(NextImage);
            RegenerateCommand = new RelayCommand(RegenerateImage, (arg) => IsRegenerateEnabled);

            _illustrations = InitializeIllustrations();

            // Инициализация данных
            if (_illustrations is not null)
            {
                _illustrationIndex = 0;

                Title = _illustrations[_illustrationIndex].Title;
                Prompt = _illustrations[_illustrationIndex].Prompt;
                ImageSource = _illustrations[_illustrationIndex].IllustrationPath;
            }
            
        }

        // TODO: Реализовать метод получения данных об иллюстрации
        private List<CreatedIllustrationViewModel> InitializeIllustrations()
        {
            throw new NotImplementedException();
        }

        private void NextImage(object argument)
        {
            _illustrationIndex++;

            Title = _illustrations[_illustrationIndex].Title;
            Prompt = _illustrations[_illustrationIndex].Prompt;
            ImageSource = _illustrations[_illustrationIndex].IllustrationPath;
        }

        // TODO: Пересмотреть IllustrationFolder
        private void RegenerateImage(object argument)
        {
            var createCommand = new CreateIllustrationCommand
            {
                Prompt = Prompt,
                FolderPath = ImageSource
            };
            _commandDispatcher.Send<CreateIllustrationCommand>(createCommand);

            var addCommand = new AddNewIllustrationCommand
            {
                Title = this.Title,
                Description = this.Prompt,
                IllustrationFolder = ImageSource
            };
            _commandDispatcher.Send<AddNewIllustrationCommand>(addCommand);
            IsRegenerateEnabled = false;
        }
    }
}
