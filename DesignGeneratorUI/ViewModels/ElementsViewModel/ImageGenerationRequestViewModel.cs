using CommunityToolkit.Mvvm.Messaging;
using DesignGenerator.Application.Messages;
using DesignGenerator.Domain.Interfaces.ImageGeneration;
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
        private string? _title;
        private ObservableCollection<ParameterViewModel> _params = new();

        /// <summary>
        /// Gets or sets the title of the request. 
        /// Typically used for UI identification or display purposes (e.g., in a list).
        /// </summary>
        public string Title
        {
            get { return _title ??= ""; }
            set { _title = value; OnPropertyChanged(nameof(Title)); }
        }

        /// <summary>
        /// Gets or sets the collection of parameter view models that define the request's settings.
        /// These parameters are typically bound to the UI and editable by the user.
        /// </summary>
        public ObservableCollection<ParameterViewModel> Params
        {
            get { return _params; }
            set
            {
                _params = value ?? new();
                OnPropertyChanged(nameof(Params)); 
            }
        }

        /// <summary>
        /// Reloads the parameter view models from the specified image generation client.
        /// This is useful when changing the underlying model or switching between different clients.
        /// </summary>
        /// <param name="generationClient">The client that provides default generation parameters.</param>
        public void ReloadParameters(IImageGenerationClient generationClient)
        {
            var generationParams = generationClient.DefaultParams;
            Params.Clear();

            foreach (var parameter in generationParams.Parameters)
            {
                Params.Add(new ParameterViewModel(parameter));
            }
        }

        public string GetParameterValueByDisplayName(string displayName)
        {
            var parameter = Params.FirstOrDefault(p => p.DisplayName == displayName);
            if (parameter == null || parameter.Value == null)
                throw new Exception($"Parameter with display name '{displayName}' was not found.");

            return parameter.Value.ToString() ?? "";
        }
    }
}
