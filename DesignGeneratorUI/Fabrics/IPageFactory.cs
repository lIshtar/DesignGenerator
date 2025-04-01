using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DesignGeneratorUI.Fabrics
{
    public interface IPageFactory
    {
        T CreatePage<T>() where T : Page;
        object CreatePage(Type pageType);
    }
}
