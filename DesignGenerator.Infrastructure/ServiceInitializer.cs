using DesignGenerator.Application.Interfaces;
using DesignGenerator.Infrastructure.AICommunicators;
using DesignGenerator.Infrastructure.Database;
using DesignGenerator.Infrastructure.DBEntities;
using DesignGenerator.Infrastructure.Mappers;
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
            services.AddTransient<IRepository<Illustration>, IllustrationRepository>();
            services.AddTransient<IRepositoryService<Domain.Illustration>, IllustrationRepositoryService>();
            services.AddSingleton<ApplicationDbContext>();
            services.AddAutoMapper(typeof(IllustrationProfile));

            return services;
        }
    }
}
