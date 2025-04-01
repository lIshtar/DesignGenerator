using DesignGenerator.Application.Commands;
using DesignGenerator.Application.Commands.AddNewIllustration;
using DesignGenerator.Application.Commands.CreateIllustration;
using DesignGenerator.Application.Commands.UpdateIllustrationPrompt;
using DesignGenerator.Application.Interfaces;
using DesignGenerator.Application.Queries;
using DesignGenerator.Application.Queries.Communicate;
using DesignGenerator.Application.Queries.GetUnreviewedIllustrations;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application
{
    public static class ServiceInitializer
    {
        public static IServiceCollection InitializeServices(this IServiceCollection services)
        {
            services.AddTransient<ICommandDispatcher, CommandDispatcher>();
            services.AddTransient<ICommandHandler<AddNewIllustrationCommand>, AddNewIllustrationCommandHandler>();
            services.AddTransient<ICommandHandler<CreateIllustrationCommand>, CreateIllustrationCommandHandler>();
            services.AddTransient<ICommandHandler<UpdateIllustrationPromptCommand>, UpdateIllustrationPromptCommandHandler>();

            services.AddTransient<IQueryDispatcher, QueryDispatcher>();
            services.AddTransient<IQueryHandler<CommunicateQuery, CommunicateQueryResponse>, CommunicateQueryHandler>();
            services.AddTransient<IQueryHandler<GetUnreviewedIllustrationsQuery, GetUnreviewedIllustrationsQueryResponse>, GetUnreviewedIllustrationsQueryHandler>();

            return services;
        }
    }
}
