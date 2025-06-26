using DesignGeneratorUI.ViewModels.ElementsViewModel;
using System.Windows;
using UserControl = System.Windows.Controls.UserControl;

namespace DesignGeneratorUI.Views.UserControls
{
    /// <summary>
    /// Interaction logic for DynamicParameterEditor.xaml
    /// </summary>
    public partial class DynamicParameterEditor : UserControl
    {
        public DynamicParameterEditor()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty DataProperty =
        DependencyProperty.Register("Data",
            typeof(ImageGenerationRequestViewModel),
            typeof(DynamicParameterEditor),
            new PropertyMetadata(null));

        public ImageGenerationRequestViewModel Data
        {
            get => (ImageGenerationRequestViewModel)GetValue(DataProperty);
            set => SetValue(DataProperty, value);
        }
    }
}
