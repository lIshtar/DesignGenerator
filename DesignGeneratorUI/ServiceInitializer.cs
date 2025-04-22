using DesignGenerator.Application.Interfaces;
using DesignGenerator.Infrastructure.AICommunicators;
using DesignGenerator.Infrastructure.Database;
using DesignGeneratorUI.Fabrics;
using DesignGeneratorUI.FileServices;
using DesignGeneratorUI.ViewModels.Navigation;
using DesignGeneratorUI.ViewModels.PagesViewModels;
using DesignGeneratorUI.ViewModels;
using DesignGeneratorUI.Views.Pages;
using DesignGeneratorUI.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGeneratorUI
{
    public static class ServiceInitializer
    {
        public static IServiceCollection InitializeServices(this IServiceCollection services)
        {
            // Добавляем данные основной страницы
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<MainWindow>();

            // Добавляем все страницы
            services.AddSingleton<HomePage>();
            services.AddSingleton<DataPage>();
            services.AddSingleton<MainInteractionPage>();
            services.AddSingleton<GenerationProgressPage>();
            services.AddSingleton<DescriptionsViewerPage>();
            services.AddSingleton<ImageViewerPage>();
            services.AddSingleton<PromptManagerPage>();

            // Добавляем все ViewModel страниц
            services.AddSingleton<DataPageViewModel>();
            services.AddSingleton<HomePageViewModel>();
            services.AddSingleton<MainInteractionPageViewModel>();
            services.AddSingleton<GenerationProgressPageViewModel>();
            services.AddSingleton<DescriptionsViewerPageViewModel>();
            services.AddSingleton<ImageViewerPageViewModel>();
            services.AddSingleton<PromptManagerPageViewModel>();

            // Добавляем все фабрики
            services.AddTransient<IPageFactory, PageFactory>();

            // Добавляем все сервисы
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddTransient<IFileService, NpoiExcelFileService>();
            services.AddTransient<IOpenDialogService, FolderDialogService>();

            return services;
        }
    }
}
