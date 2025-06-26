using CommunityToolkit.Mvvm.Messaging;
using DesignGenerator.Application.Messages;
using DesignGenerator.Domain.Interfaces.ImageGeneration;
using DesignGeneratorUI.Messages;
using DesignGeneratorUI.ViewModels.ElementsViewModel;
using DesignGeneratorUI.ViewModels.Navigation;
using DesignGeneratorUI.Views.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DesignGeneratorUI.ViewModels.PagesViewModels
{
    // TODO: поздно инициализируется, не ловит из-за этого ивент изменения модели
    public class BatchGenerationViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IMessenger _messenger;

        public ObservableCollection<ImageGenerationRequestViewModel> Requests { get; } = new();
        public ObservableCollection<ParameterViewModel> SharedParameters { get; } = new();

        public ICommand NextCommand { get; private set; }
        public ICommand BackCommand { get; private set; }

        public BatchGenerationViewModel(INavigationService navigationService, IMessenger messenger)
        {
            _navigationService = navigationService;
            _messenger = messenger;

            _messenger.Register<ModelSelectionChangedMessage>(this, (r, m) => ReloadSharedParameters(m.Value));

            NextCommand = new RelayCommand(Next);
            BackCommand = new RelayCommand(Back);
        }

        private void ReloadSharedParameters(IImageGenerationClient imageGenerationClient)
        {
            SharedParameters.Clear();
            var paramsContainer = imageGenerationClient.DefaultParams;

            foreach (var param in paramsContainer.Parameters)
            {
                // Exclude Prompt from shared editing
                if (param.Name.Equals("Prompt", StringComparison.OrdinalIgnoreCase))
                    continue;

                SharedParameters.Add(new ParameterViewModel(param)
                {
                    Value = param.Value
                });
            }
        }

        private void Next()
        {
            ApplySharedParametersToAll();
            _messenger.Send(new RequestBatchCreatedMessage(Requests));
            _navigationService.NavigateTo<DescriptionsViewerPage>();
        }

        private void Back()
        {
            _navigationService.NavigateBack();
        }

        public void ApplySharedParametersToAll()
        {
            foreach (var request in Requests)
            {
                foreach (var shared in SharedParameters)
                {
                    var target = request.Parameters.FirstOrDefault(p => p.Name == shared.Name);
                    if (target != null)
                        target.Value = shared.Value;
                }
            }
        }
    }
}
