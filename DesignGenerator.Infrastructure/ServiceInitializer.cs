using DesignGenerator.Application.Interfaces;
using DesignGenerator.Domain.Interfaces.ImageGeneration;
using DesignGenerator.Infrastructure.AICommunicators;
using DesignGenerator.Infrastructure.Database;
using DesignGenerator.Infrastructure.Database.DBEntities;
using DesignGenerator.Infrastructure.DBEntities;
using DesignGenerator.Infrastructure.ImageGeneration.StableDiffusion;
using DesignGenerator.Infrastructure.IO;
using DesignGenerator.Infrastructure.Mappers;
using DesignGenerator.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
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
            services.AddTransient<ITextAICommunicator, GptConnector>();

            services.AddTransient<IRepository<Illustration>, IllustrationRepository>();
            services.AddTransient<IRepositoryService<Domain.Illustration>, IllustrationRepositoryService>();
            services.AddTransient<IRepository<Prompt>, PromptsRepository>();
            services.AddTransient<IRepositoryService<Domain.Prompt>, PromptRepositoryService>();
            services.AddTransient<IRepository<Message>, MessageRepository>();
            services.AddTransient<IRepositoryService<Domain.Message>, MessageRepositoryService>();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(),
                ServiceLifetime.Scoped);

            services.AddAutoMapper(typeof(IllustrationProfile));
            services.AddAutoMapper(typeof(PromptProfile));
            services.AddAutoMapper(typeof(MessageProfile));

            services.AddTransient<IImageGenerationClient, StableDiffusionClient>();
            services.AddTransient<StableDiffusionApiClient>();
            services.AddTransient<StableDiffusionConfig>();
            services.AddTransient<StableDiffusionRequestBuilder>();

            services.AddTransient<IImageSaver, LocalImageSaver>();

            return services;
        }
    }
}
