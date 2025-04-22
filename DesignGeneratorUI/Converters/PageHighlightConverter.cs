using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DesignGeneratorUI.Converters
{
    public class PageHighlightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return Brushes.Transparent;

            int currentPage = (int)value;
            int buttonPage = System.Convert.ToInt32(parameter);

            return currentPage == buttonPage ? Brushes.LightBlue : Brushes.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
