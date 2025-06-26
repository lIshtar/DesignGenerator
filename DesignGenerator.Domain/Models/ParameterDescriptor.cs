using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Domain.Models
{
    // TODO: Добавить Сохранение параметров по умолчанию для каждой модели в БД
    /// <summary>
    /// Describes a parameter that can be dynamically rendered and configured in the UI,
    /// including metadata for its type, display, default value, and constraints.
    /// </summary>
    public class ParameterDescriptor
    {
        /// <summary>
        /// The internal name (key) of the parameter, used for binding and identification.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The display name of the parameter shown to the user.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// The type of the parameter, which determines how it is rendered in the UI.
        /// </summary>
        public ParameterType Type { get; set; }

        /// <summary>
        /// Available options for Dropdown parameters.
        /// </summary>
        public IEnumerable<string>? Options { get; set; }

        /// <summary>
        /// The default value to use when the parameter is not specified by the user.
        /// </summary>
        public object? DefaultValue { get; set; }

        /// <summary>
        /// The value processed to api
        /// </summary>
        public object? Value { get; set; } = null;

        /// <summary>
        /// Indicates whether this parameter is required for the generation API.
        /// </summary>
        public bool Required { get; set; }

        /// <summary>
        /// An optional tooltip to help the user understand the parameter's purpose.
        /// </summary>
        public string? Tooltip { get; set; }

        /// <summary>
        /// Minimum value allowed for number or slider parameters. Null if not applicable.
        /// </summary>
        public double? Min { get; set; }

        /// <summary>
        /// Maximum value allowed for number or slider parameters. Null if not applicable.
        /// </summary>
        public double? Max { get; set; }

        /// <summary>
        /// Optional increment step used in sliders or numeric inputs.
        /// </summary>
        public double? Step { get; set; }

        /// <summary>
        /// Indicates whether this parameter should be shown to the user in the UI.
        /// </summary>
        public bool IsVisible { get; set; } = true;
    }

    /// <summary>
    /// Represents the type of UI element to use for parameter input.
    /// </summary>
    public enum ParameterType
    {
        Text,     // Freeform string input
        Number,   // Numeric input (may use TextBox or Spinner)
        Dropdown, // Selection from predefined values
        Bool,     // Toggle (checkbox or switch)
        Slider    // Numeric input with visual slider between Min and Max
    }
}
