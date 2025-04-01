using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DesignGeneratorUI.Fabrics
{
    public class PageFactory : IPageFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public PageFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public T CreatePage<T>() where T : Page
        {
            return _serviceProvider.GetRequiredService<T>();
        }

        public object CreatePage(Type type)
        {
            return _serviceProvider.GetRequiredService(type);
        }
    }
}
