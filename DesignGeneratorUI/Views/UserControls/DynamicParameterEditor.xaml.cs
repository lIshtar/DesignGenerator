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

        public static readonly DependencyProperty ParametersProperty =
        DependencyProperty.Register("Parameters",
            typeof(IEnumerable<ParameterViewModel>),
            typeof(DynamicParameterEditor),
            new PropertyMetadata(null));

        public IEnumerable<ParameterViewModel> Parameters
        {
            get => (IEnumerable<ParameterViewModel>)GetValue(ParametersProperty);
            set => SetValue(ParametersProperty, value);
        }
    }
}
