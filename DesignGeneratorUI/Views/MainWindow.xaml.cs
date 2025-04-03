using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Navigation = DesignGeneratorUI.ViewModels.Navigation;
using System.Windows.Shapes;
using DesignGeneratorUI.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata;
using DesignGeneratorUI.ViewModels.Navigation;
using DesignGeneratorUI.Fabrics;
using DesignGeneratorUI.Views.Pages;

namespace DesignGeneratorUI.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(MainWindowViewModel mainWindowViewModel, INavigationService navigationService)
        {
            InitializeComponent();
            
            DataContext = mainWindowViewModel;

            navigationService.SelectFrame(MainFrame);
            navigationService.NavigateTo<MainInteractionPage>();
        }
    }
}