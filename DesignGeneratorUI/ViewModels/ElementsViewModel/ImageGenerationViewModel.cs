using CommunityToolkit.Mvvm.Messaging;
using DesignGenerator.Domain.Interfaces.ImageGeneration;
using System.Collections.ObjectModel;

namespace DesignGeneratorUI.ViewModels.ElementsViewModel
{
    // TODO: Догружать и обновлять параметры при получении ивента от мессенджера

    public class ImageGenerationViewModel
    {
        IMessenger _messenger;
        public ImageGenerationViewModel(IMessenger messenger)
        {
            _messenger = messenger;
            LoadParameters();
        }
        public ObservableCollection<ParameterViewModel> Parameters { get; } = new();

        public void LoadParameters(IEnumerable<ParameterDescriptor> descriptors)
        {
            Parameters.Clear();
            foreach (var descriptor in descriptors)
            {
                Parameters.Add(new ParameterViewModel(descriptor));
            }
        }
    }
}
