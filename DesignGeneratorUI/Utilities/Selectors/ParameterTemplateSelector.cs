using DesignGeneratorUI.ViewModels.ElementsViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using DesignGenerator.Domain.Interfaces.ImageGeneration;


namespace DesignGeneratorUI.Utilities.Selectors
{
    /// <summary>
    /// Selects the appropriate DataTemplate for a parameter based on its type.
    /// Used by ItemsControl to render UI elements dynamically for different parameter types.
    /// </summary>
    public class ParameterTemplateSelector : DataTemplateSelector
    {
        /// <summary>
        /// Template for text input parameters.
        /// </summary>
        public DataTemplate TextTemplate { get; set; }

        /// <summary>
        /// Template for parameters with a predefined set of selectable values (e.g. dropdowns).
        /// </summary>
        public DataTemplate DropdownTemplate { get; set; }

        /// <summary>
        /// Template for numeric input parameters.
        /// </summary>
        public DataTemplate NumberTemplate { get; set; }

        /// <summary>
        /// Template for boolean parameters (e.g. checkbox or toggle).
        /// </summary>
        public DataTemplate BoolTemplate { get; set; }

        /// <summary>
        /// Template for parameters that are best represented as a slider input,
        /// typically used for selecting numeric values within a defined range (e.g., 0 to 100).
        /// </summary>
        public DataTemplate SliderTemplate { get; set; }

        /// <summary>
        /// Chooses the appropriate DataTemplate based on the ParameterType defined in the ParameterViewModel.
        /// </summary>
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is not ParameterViewModel vm) return null;

            return vm.Type switch
            {
                ParameterType.Text => TextTemplate,
                ParameterType.Dropdown => DropdownTemplate,
                ParameterType.Number => NumberTemplate,
                ParameterType.Bool => BoolTemplate,
                ParameterType.Slider => SliderTemplate,
                _ => base.SelectTemplate(item, container)
            };
        }
    }
}
