using AutoMapper;
using DesignGenerator.Application.Commands.AddIllustration;
using DesignGenerator.Application.Queries.GetUnreviewedIllustrations;
using DesignGenerator.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application.Mappers
{
    internal class GetUnreviewedIllustrationsQueryResponseProfile : Profile
    {
        public GetUnreviewedIllustrationsQueryResponseProfile()
        {
            CreateMap<GetUnreviewedIllustrationsQueryResponse, Illustration>().ReverseMap();
        }
    }
}
