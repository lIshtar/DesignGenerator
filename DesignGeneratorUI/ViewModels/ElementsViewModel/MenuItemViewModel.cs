using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DesignGeneratorUI.ViewModels
{
    public class MenuItemViewModel
    {
        public string? Title { get; }
        public Page? Page { get; }
        public ImageSource? Icon { get; }

        public MenuItemViewModel(string title, string imageUri, Page page)
        {
            Title = title;

            if (!string.IsNullOrEmpty(imageUri))
            {
                var yourImage = new BitmapImage(new Uri(imageUri, UriKind.Relative));
                yourImage.Freeze(); // -> to prevent error: "Must create DependencySource on same Thread as the DependencyObject"
                Icon = yourImage;
            }
            else
            {
                Icon = null;
            }
            Page = page;
        }
    }
}
