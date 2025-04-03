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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesignGenerator.Application.Mappers;

namespace DesignGenerator.Application
{
    public static class ServiceInitializer
    {
        public static IServiceCollection InitializeServices(this IServiceCollection services)
        {
            services.AddTransient<ICommandDispatcher, CommandDispatcher>();
            services.AddTransient<ICommandHandler<AddIllustrationCommand>, AddNewIllustrationCommandHandler>();
            services.AddTransient<ICommandHandler<UpdateIllustrationCommand>, UpdateIllustrationCommandHandler>();

            services.AddTransient<IQueryDispatcher, QueryDispatcher>();
            services.AddTransient<IQueryHandler<CommunicateQuery, CommunicateQueryResponse>, CommunicateQueryHandler>();
            services.AddTransient<IQueryHandler<GetUnreviewedIllustrationsQuery, GetUnreviewedIllustrationsQueryResponse>, GetUnreviewedIllustrationsQueryHandler>();
            services.AddTransient<IQueryHandler<CreateIllustrationQuery, CreateIllustrationQueryResponse>, CreateIllustrationQueryHandler>();

            services.AddTransient<ITemplateParser, IllustrationTemplateJsonParser>();

            services.AddAutoMapper(typeof(AddIllustrationCommandProfile));
            services.AddAutoMapper(typeof(GetUnreviewedIllustrationsQueryResponseProfile));
            services.AddAutoMapper(typeof(UpdateIllustrationCommandProfile));

            return services;
        }
    }
}
