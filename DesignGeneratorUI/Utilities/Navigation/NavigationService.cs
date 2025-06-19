using DesignGeneratorUI.Fabrics;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Page = System.Windows.Controls.Page;
using Frame = System.Windows.Controls.Frame;


namespace DesignGeneratorUI.ViewModels.Navigation
{
    public class NavigationService : INavigationService
    {
        private Frame? _frame;
        private readonly IPageFactory _pageFactory;

        public NavigationService(IPageFactory pageFactory)
        {
            _pageFactory = pageFactory;
        }

        public void SelectFrame(Frame frame)
        {
            _frame = frame ?? throw new Exception("Переданный Frame я");
        }

        public void NavigateTo<T>() where T : Page
        {
            if (_frame == null)
                throw new InvalidOperationException("NavigationService не инициализирован. Вызовите SelectFrame(Frame).");

            var page = _pageFactory.CreatePage<T>();
            _frame.Navigate(page);
        }

        public void NavigateTo(Page page)
        {
            if (_frame == null)
                throw new InvalidOperationException("NavigationService не инициализирован. Вызовите SelectFrame(Frame).");

            _frame.Navigate(page);
        }

        public void NavigateBack()
        {
            if (_frame == null)
                throw new InvalidOperationException("NavigationService не инициализирован. Вызовите SelectFrame(Frame).");

            if (_frame.CanGoBack)
            {
                _frame.GoBack();
            }
        }
    }
}
