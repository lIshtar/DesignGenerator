using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DesignGeneratorUI.Converters
{
    public class BoolToHorizontalAlignmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Если IsBotMessage == true, выравниваем по левому краю
            // Если IsBotMessage == false (т.е. сообщение пользователя), выравниваем по правому краю
            var ans = (value is bool isBotMessage && isBotMessage) ? HorizontalAlignment.Left : HorizontalAlignment.Right;
            return ans;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
