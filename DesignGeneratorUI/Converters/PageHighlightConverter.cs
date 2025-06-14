﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace DesignGeneratorUI.Converters
{
    public class PageHighlightConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 2 || values[0] == null || values[1] == null)
                return new SolidColorBrush(Colors.Transparent);

            int currentPage = System.Convert.ToInt32(values[0]);
            int buttonPage = System.Convert.ToInt32(values[1]);

            return currentPage == buttonPage ? new SolidColorBrush(Colors.LightBlue) : new SolidColorBrush(Colors.Transparent);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
