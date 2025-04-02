using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using OfficeOpenXml;
using DesignGeneratorUI.Views;
using Microsoft.Extensions.DependencyInjection;
using DesignGeneratorUI.ViewModels;
using DesignGeneratorUI.Views.Pages;
using DesignGeneratorUI.ViewModels.Navigation;
using DesignGeneratorUI.Fabrics;
using DesignGeneratorUI.FileServices;
using Microsoft.EntityFrameworkCore;
using DesignGeneratorUI.ViewModels.PagesViewModels;
using DesignGenerator.Infrastructure.AICommunicators;

namespace DesignGeneratorUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        public static IServiceProvider? ServiceProvider { get; private set; }
        private IConfiguration? _config;

        protected override void OnStartup(StartupEventArgs e)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var services = new ServiceCollection();
            ConfigureServices(services);

            ServiceProvider = services.BuildServiceProvider();

            MainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.InitializeServices();
            services = DesignGenerator.Application.ServiceInitializer.InitializeServices(services);
            services = DesignGenerator.Infrastructure.ServiceInitializer.InitializeServices(services);

            // Добавляем конфигурацию
            if (_config is not  null)
                services.AddSingleton(_config);
        }
    }
}
