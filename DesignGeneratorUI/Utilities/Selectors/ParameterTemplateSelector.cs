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
    public class ParameterTemplateSelector : DataTemplateSelector
    {
        public DataTemplate TextTemplate { get; set; }
        public DataTemplate DropdownTemplate { get; set; }
        public DataTemplate NumberTemplate { get; set; }
        public DataTemplate BoolTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is not ParameterViewModel vm) return null;

            return vm.Type switch
            {
                ParameterType.Text => TextTemplate,
                ParameterType.Dropdown => DropdownTemplate,
                ParameterType.Number => NumberTemplate,
                ParameterType.Bool => BoolTemplate,
                _ => base.SelectTemplate(item, container)
            };
        }
    }

}
