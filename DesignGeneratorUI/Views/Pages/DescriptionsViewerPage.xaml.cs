using DesignGeneratorUI.ViewModels.PagesViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesignGeneratorUI.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для DescriptionsViewerPage.xaml
    /// </summary>
    public partial class DescriptionsViewerPage : Page
    {
        public DescriptionsViewerPage(DescriptionsViewerPageViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
