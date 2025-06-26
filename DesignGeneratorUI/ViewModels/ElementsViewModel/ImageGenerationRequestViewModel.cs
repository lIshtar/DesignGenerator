using CommunityToolkit.Mvvm.Messaging;
using DesignGenerator.Application.Messages;
using DesignGenerator.Domain.Interfaces.ImageGeneration;
using DesignGenerator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGeneratorUI.ViewModels.ElementsViewModel
{
    /// <summary>
    /// Represents a view model for a single image generation request.
    /// Holds metadata like title and the set of parameter view models used for the request.
    /// </summary>
    public class ImageGenerationRequestViewModel : BaseViewModel
    {
        public ObservableCollection<ParameterViewModel> Parameters { get; } = new();

        private string? _title;
        public string? Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public string? Prompt
        {
            get => Parameters.FirstOrDefault(p => p.Name == "prompt")?.Value?.ToString();
            set
            {
                var promptParam = Parameters.FirstOrDefault(p => p.Name == "prompt");
                if (promptParam != null)
                    promptParam.Value = value;
            }
        }


        public ImageGenerationRequestViewModel() { }

        public ImageGenerationRequestViewModel(IEnumerable<ParameterDescriptor> baseDescriptors)
        {
            foreach (var descriptor in baseDescriptors)
                Parameters.Add(new ParameterViewModel(descriptor));
        }

        /// <summary>
        /// Reloads the parameter view models from the specified image generation client.
        /// This is useful when changing the underlying model or switching between different clients.
        /// </summary>
        /// <param name="generationClient">The client that provides default generation parameters.</param>
        public void ReloadParameters(IImageGenerationClient generationClient)
        {
            var generationParams = generationClient.DefaultParams;
            Parameters.Clear();

            foreach (var parameter in generationParams.Parameters)
            {
                Parameters.Add(new ParameterViewModel(parameter));
            }
        }

        public IEnumerable<ParameterDescriptor> ToParameterDescriptors()
        {
            return Parameters.Select(p => p.ToParameterDescriptor());
        }
    }
}
