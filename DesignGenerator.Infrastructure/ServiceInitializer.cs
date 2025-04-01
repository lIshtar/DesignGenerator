using DesignGenerator.Application.Interfaces;
using DesignGenerator.Infrastructure.AICommunicators;
using DesignGenerator.Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Infrastructure
{
    public static class ServiceInitializer
    {
        public static IServiceCollection InitializeServices(this IServiceCollection services)
        {
            services.AddTransient<IImageAICommunicator, ImageAIDefaultCommunicator>();
            services.AddTransient<ITextAICommunicator, TLLCommunicator>();
            services.AddSingleton<IImageDownloader, ImageDownloader>();
            services.AddTransient<IIllustartionRepository, IllustrationRepository>();
            services.AddSingleton<ApplicationDbContext>();

            return services;
        }
    }
}
