using DesignGenerator.Application.Interfaces;
using ICommand = System.Windows.Input.ICommand;
using DesignGeneratorUI.ViewModels.ElementsViewModel;
using DesignGeneratorUI.ViewModels.Navigation;
using DesignGeneratorUI.Views.Pages;
using System.Collections.ObjectModel;
using DesignGenerator.Application;

namespace DesignGeneratorUI.ViewModels.PagesViewModels
{
    // TODO: добавить возможность внесения изменений в данные
    public class DescriptionsViewerPageViewModel : BaseViewModel
    {
        public ObservableCollection<IllustrationTemplate> DataItems { get; set; }
        public ICommand StartCreationCommand { get; }
        public ICommand ReturnBackCommand { get; }
        public ICommand AddDataCommand { get; }

        private readonly INavigationService _navigationService;
        private readonly IQueryDispatcher _queryDispatcher;

        public DescriptionsViewerPageViewModel(INavigationService navigationService, IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            _navigationService = navigationService;

            DataItems = IllustrationTemplatesContainer.GetInstance().IllustrastionsTemplates;

            StartCreationCommand = new RelayCommand(StartCreation);
            ReturnBackCommand = new RelayCommand(ReturnBack);
            AddDataCommand = new RelayCommand(AddData);
        }

        private void AddData()
        {
            DataItems.Add(new IllustrationTemplate());
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
