using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Infrastructure.Mappers
{
    public class PromptProfile : Profile
    {
        public PromptProfile()
        {
            CreateMap<Domain.Prompt, Database.DBEntities.Prompt>().ReverseMap();
        }
    }
}
