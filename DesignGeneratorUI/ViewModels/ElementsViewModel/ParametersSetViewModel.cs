using CommunityToolkit.Mvvm.Messaging;
using DesignGenerator.Application.Messages;
using DesignGenerator.Domain.Interfaces.ImageGeneration;
using System.Collections.ObjectModel;

namespace DesignGeneratorUI.ViewModels.ElementsViewModel
{
    /// <summary>
    /// ViewModel responsible for holding and updating the set of parameters used for image generation.
    /// </summary>
    public class ParametersSetViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParametersSetViewModel"/> class and subscribes to model selection changes.
        /// </summary>
        /// <param name="messenger">The messaging service used for inter-component communication.</param>
        public ParametersSetViewModel(IMessenger messenger)
        {
            // Register to receive a message when the user selects a new image generation model.
            // When received, it triggers reloading of parameters specific to the selected model.
            messenger.Register<ModelSelectionChangedMessage>(this, (r, m) => ReloadParameters(m.Value));
        }

        /// <summary>
        /// Collection of parameter view models currently active for the selected image generation model.
        /// This collection is data-bound to the UI to dynamically render parameter input fields.
        /// </summary>
        public ObservableCollection<ParameterViewModel> Parameters { get; } = new();

        /// <summary>
        /// Clears the current parameters and loads a new set from the selected image generation client.
        /// </summary>
        /// <param name="generationClient">The client representing the selected image generation model.</param>
        private void ReloadParameters(IImageGenerationClient generationClient)
        {
            var parameters = generationClient.DefaultParams;
            Parameters.Clear();

            foreach (var parameter in parameters)
            {
                Parameters.Add(new ParameterViewModel(parameter));
            }
        }
    }
}
