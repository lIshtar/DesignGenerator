using DesignGenerator.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGeneratorUI.ViewModels.ElementsViewModel
{
    public class ParameterViewModel : BaseViewModel
    {
        public ParameterDescriptor Descriptor { get; }

        private object? _value;
        public object? Value
        {
            get => _value;
            set
            {
                _value = value;
                OnPropertyChanged(nameof(Value));
            }
        }

        public ParameterViewModel(ParameterDescriptor descriptor)
        {
            Descriptor = descriptor;
            Value = descriptor.DefaultValue;
        }

        public string Name => Descriptor.Name;
        public string DisplayName => Descriptor.DisplayName;
        public string? Tooltip => Descriptor.Tooltip;
        public ParameterType Type => Descriptor.Type;
        public IEnumerable<string>? Options => Descriptor.Options;
        public object? DefaultValue => Descriptor.DefaultValue;

        public ParameterDescriptor CreateParameterDescriptor()
        {
            return new ParameterDescriptor
            {
                Name = Name,
                DisplayName = DisplayName,
                Tooltip = Tooltip,
                Type = Type,
                Options = Options,
                Value = Value ?? DefaultValue
            };
        }
    }
}
