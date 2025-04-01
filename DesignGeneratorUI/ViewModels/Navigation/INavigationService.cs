using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DesignGeneratorUI.ViewModels.Navigation
{
    public interface INavigationService
    {
        void NavigateTo<T>() where T : Page;
        void NavigateTo(Page page);
        void NavigateBack();
        public void SelectFrame(Frame frame);
    }
}
