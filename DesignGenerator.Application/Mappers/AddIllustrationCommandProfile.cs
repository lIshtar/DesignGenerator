using AutoMapper;
using DesignGenerator.Application.Commands.AddIllustration;
using DesignGenerator.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application.Mappers
{
    public class AddIllustrationCommandProfile : Profile
    {
        public AddIllustrationCommandProfile()
        {
            CreateMap<AddIllustrationCommand, Illustration>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Prompt, opt => opt.MapFrom(src => src.Prompt))
            .ForMember(dest => dest.IllustrationPath, opt => opt.MapFrom(src => src.IllustrationPath))
            .ForMember(dest => dest.IsReviewed, opt => opt.MapFrom(src => src.IsReviewed))
            .ReverseMap();
            //.ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
