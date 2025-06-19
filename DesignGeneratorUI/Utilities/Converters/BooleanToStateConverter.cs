using System;
using System.Globalization;
using System.Windows.Data;

namespace DesignGeneratorUI.Converters
{
    public class BooleanToStateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isExpanded)
            {
                return isExpanded ? "Expanded" : "Collapsed";
            }
            return "Collapsed";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString() == "Expanded";
        }
    }
}
