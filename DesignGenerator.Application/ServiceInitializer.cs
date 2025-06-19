using DesignGenerator.Application.Commands;
using DesignGenerator.Application.Commands.AddIllustration;
using DesignGenerator.Application.Queries.CreateIllustration;
using DesignGenerator.Application.Commands.UpdateIllustration;
using DesignGenerator.Application.Interfaces;
using DesignGenerator.Application.Parsers;
using DesignGenerator.Application.Queries;
using DesignGenerator.Application.Queries.Communicate;
using DesignGenerator.Application.Queries.GetUnreviewedIllustrations;
using Microsoft.Extensions.DependencyInjection;
using DesignGenerator.Application.Mappers;
using DesignGenerator.Application.Queries.GetAllIllustrations;
using DesignGenerator.Application.Commands.AddMessage;
using DesignGenerator.Application.Commands.AddPrompt;
using DesignGenerator.Application.Commands.DeletePrompt;
using DesignGenerator.Application.Commands.UpdatePrompt;
using DesignGenerator.Application.Queries.GetAllPrompts;
using DesignGenerator.Application.Messages;
using DesignGenerator.Application.Settings;

namespace DesignGenerator.Application
{
    public static class ServiceInitializer
    {
        public static IServiceCollection InitializeServices(this IServiceCollection services)
        {
            services.AddTransient<ICommandDispatcher, CommandDispatcher>();
            services.AddTransient<ICommandHandler<AddIllustrationCommand>, AddNewIllustrationCommandHandler>();
            services.AddTransient<ICommandHandler<UpdateIllustrationCommand>, UpdateIllustrationCommandHandler>();
            services.AddTransient<ICommandHandler<AddMessageCommand>, AddMessageCommandHandler>();
            services.AddTransient<ICommandHandler<AddPromptCommand>, AddPromptCommandHandler>();
            services.AddTransient<ICommandHandler<DeletePromptCommand>, DeletePromptCommandHandler>();
            services.AddTransient<ICommandHandler<UpdatePromptCommand>, UpdatePromptCommandHandler>();

            services.AddTransient<IQueryDispatcher, QueryDispatcher>();
            services.AddTransient<IQueryHandler<CommunicateQuery, CommunicateQueryResponse>, CommunicateQueryHandler>();
            services.AddTransient<IQueryHandler<GetAllIllustrationQuery, GetAllIllustrationQueryResponse>, GetAllIllustrationQueryHandler>();
            services.AddTransient<IQueryHandler<GetUnreviewedIllustrationsQuery, GetUnreviewedIllustrationsQueryResponse>, GetUnreviewedIllustrationsQueryHandler>();
            services.AddTransient<IQueryHandler<CreateIllustrationQuery, CreateIllustrationQueryResponse>, CreateIllustrationQueryHandler>();
            services.AddTransient<IQueryHandler<GetAllPromptsQuery, GetAllPromptsResponse>, GetAllPromptsQueryHandler>();

            services.AddTransient<ITemplateParser, IllustrationTemplateJsonParser>();

            // Регистрируем сервисы настроек
            services.AddSingleton<ApiKeysService>();
            services.AddSingleton<DirectoriesService>();
            services.AddSingleton<ModelSelectionService>();
            services.AddSingleton<SettingsService>();

            services.AddAutoMapper(typeof(AddIllustrationCommandProfile));
            services.AddAutoMapper(typeof(GetUnreviewedIllustrationsQueryResponseProfile));
            services.AddAutoMapper(typeof(UpdateIllustrationCommandProfile));
            services.AddAutoMapper(typeof(AddMessageCommandProfile));
            services.AddAutoMapper(typeof(AddPromptCommandProfile));
            services.AddAutoMapper(typeof(DeletePromptCommandProfile));
            services.AddAutoMapper(typeof(UpdatePromptCommandProfile));

            return services;
        }
    }
}
