using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGeneratorUI.ViewModels.ElementsViewModel
{
    public class FieldVisibilitySelector : BaseViewModel
    {
        public string Name { get; }
        public string DisplayName => Name;

        private bool _isVisible;
        public bool IsVisible
        {
            get => _isVisible;
            set => _isVisible = value;
        }

        public FieldVisibilitySelector(string name, bool isVisible = false)
        {
            Name = name;
            IsVisible = isVisible;
        }

        public FieldVisibilitySelector(ParameterViewModel parameterViewModel, bool isVisible = false)
        {
            Name = parameterViewModel.Name;
            IsVisible = isVisible; 
        }
    }
}
