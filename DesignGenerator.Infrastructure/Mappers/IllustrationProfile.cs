using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Infrastructure.Mappers
{
    public class IllustrationProfile : Profile
    {
        public IllustrationProfile()
        {
            CreateMap<Domain.Illustration, DBEntities.Illustration>().ReverseMap();
        }
    }
}
