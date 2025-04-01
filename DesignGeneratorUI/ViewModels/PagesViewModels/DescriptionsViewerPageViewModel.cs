using DesignGenerator.Application.Interfaces;
using ICommand = System.Windows.Input.ICommand;
using DesignGeneratorUI.ViewModels.ElementsViewModel;
using DesignGeneratorUI.ViewModels.Navigation;
using DesignGeneratorUI.Views.Pages;
using System.Collections.ObjectModel;
using DesignGenerator.Application;

namespace DesignGeneratorUI.ViewModels.PagesViewModels
{
    public class DescriptionsViewerPageViewModel : BaseViewModel
    {
        public ObservableCollection<IllustrationTemplate> DataItems { get; set; }
        public ICommand StartCreationCommand { get; }
        public ICommand ReturnBackCommad { get; }

        private readonly INavigationService _navigationService;
        private readonly IQueryDispatcher _queryDispatcher;

        public DescriptionsViewerPageViewModel(INavigationService navigationService, IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            _navigationService = navigationService;

            DataItems = IllustrationTemplatesContainer.GetInstance().IllustrastionsTemplates;

            StartCreationCommand = new RelayCommand(StartCreation);
            ReturnBackCommad = new RelayCommand(ReturnBack);
        }

        private void StartCreation(object argument)
        {
            _navigationService.NavigateTo<GenerationProgressPage>();
        }

        private void ReturnBack(object argument)
        {
            _navigationService.NavigateBack();
        }
    }
}
