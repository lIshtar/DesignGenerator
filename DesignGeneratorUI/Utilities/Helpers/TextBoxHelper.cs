using DesignGeneratorUI.ViewModels;
using DesignGeneratorUI.ViewModels.PagesViewModels;
using DesignGeneratorUI.Views;
using DesignGeneratorUI.Views.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TextBox = System.Windows.Controls.TextBox;

namespace DesignGeneratorUI.Helpers
{
    // взято из интернета, решение выглядит ОООООчень плохо, но других нет
    public static class TextBoxHelper
    {
        public static readonly DependencyProperty BindToSelectedTextProperty =
            DependencyProperty.RegisterAttached(
                "BindToSelectedText",
                typeof(bool),
                typeof(TextBoxHelper),
                new PropertyMetadata(false, OnBindToSelectedTextChanged));

        public static bool GetBindToSelectedText(DependencyObject obj)
        {
            return (bool)obj.GetValue(BindToSelectedTextProperty);
        }

        public static void SetBindToSelectedText(DependencyObject obj, bool value)
        {
            obj.SetValue(BindToSelectedTextProperty, value);
        }

        private static void OnBindToSelectedTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBox textBox && (bool)e.NewValue)
            {
                textBox.SelectionChanged -= TextBox_SelectionChanged;
                textBox.SelectionChanged += TextBox_SelectionChanged;
            }
        }

        // О господи, не смотри на это. Жесточайший гавнокод
        private static void TextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                // Получаем главное окно
                var mainWindow = System.Windows.Application.Current.MainWindow;
                if (mainWindow == null) return;

                // Получаем ViewModel из DataContext окна
                if (mainWindow is MainWindow window)
                {
                    if (window.MainFrame.Content is MainInteractionPage mainInteractionPage)
                    {
                        if (mainInteractionPage.DataContext is MainInteractionPageViewModel pageViewModel)
                        {
                            pageViewModel.SelectedText = textBox.SelectedText;
                        }
                    }
                }
            }
        }
    }
}
