using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Domain.Interfaces.ImageGeneration
{
    public class ParameterDescriptor
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public ParameterType Type { get; set; }
        public IEnumerable<string>? Options { get; set; } // Для Dropdown
        public object? DefaultValue { get; set; }
        public bool Required { get; set; }
        public string? Tooltip { get; set; } // 💡 Новый параметр
    }

    public enum ParameterType
    {
        Text,
        Number,
        Dropdown,
        Bool
    }
}
