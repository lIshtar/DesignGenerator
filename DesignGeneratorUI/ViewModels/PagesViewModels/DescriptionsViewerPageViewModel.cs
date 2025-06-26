using DesignGenerator.Application.Interfaces;
using ICommand = System.Windows.Input.ICommand;
using DesignGeneratorUI.ViewModels.ElementsViewModel;
using DesignGeneratorUI.ViewModels.Navigation;
using DesignGeneratorUI.Views.Pages;
using System.Collections.ObjectModel;
using DesignGenerator.Application;
using CommunityToolkit.Mvvm.Messaging;
using DesignGeneratorUI.Messages;

namespace DesignGeneratorUI.ViewModels.PagesViewModels
{

    // TODO: Пока что не тестировал переделай класс, нужно проверить, что все работает корректно
    public class DescriptionsViewerPageViewModel : BaseViewModel
    {
        private ObservableCollection<ImageGenerationRequestViewModel> _dataItems;
        public ObservableCollection<ImageGenerationRequestViewModel> DataItems
        {
            get => _dataItems;
            set
            {
                _dataItems = value;
                _messenger.Send(new TemplatesModifiedMessage(value));
                OnPropertyChanged(nameof(DataItems));
            }
        }
        

        public ICommand ToggleVisibilityPopupCommand { get; }

        private bool _isPopupOpen;

        public bool IsPopupOpen
        {
            get => _isPopupOpen;
            set => _isPopupOpen = value;
        }
        public ICommand StartCreationCommand { get; }
        public ICommand ReturnBackCommand { get; }
        public ICommand AddDataCommand { get; }

        private readonly INavigationService _navigationService;
        private readonly IMessenger _messenger;

        public DescriptionsViewerPageViewModel(INavigationService navigationService, IMessenger messenger)
        {
            _navigationService = navigationService;
            _messenger = messenger;

            DataItems = new();
            messenger.Register<RequestBatchCreatedMessage>(this, (e, m) => ReloadData(m.Value));

            ToggleVisibilityPopupCommand = new RelayCommand(ToggleVisibilityPopup);
            StartCreationCommand = new RelayCommand(StartCreation);
            ReturnBackCommand = new RelayCommand(ReturnBack);
            AddDataCommand = new RelayCommand(AddData);
        }

        private void ToggleVisibilityPopup()
        {
            IsPopupOpen = !IsPopupOpen;
        }

        private void ReloadData(IEnumerable<ImageGenerationRequestViewModel> templates)
        {
            DataItems = new(templates);

            foreach (var item in DataItems)
            {
                foreach (var param in item.Parameters)
                {
                    param.IsVisible = param.DisplayName == "Prompt";
                }
            }
        }

        private void AddData()
        {
            //DataItems.Add(new ImageGenerationRequestViewModel());
        }

        private void StartCreation()
        {
            _navigationService.NavigateTo<GenerationProgressPage>();
        }

        private void ReturnBack()
        {
            _navigationService.NavigateBack();
        }
    }
}
