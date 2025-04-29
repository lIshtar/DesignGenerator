using AutoMapper;
using DesignGenerator.Application.Commands.AddIllustration;
using DesignGenerator.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application.Commands.UpdateIllustration
{
    public class UpdateIllustrationCommandProfile : Profile
    {
        public UpdateIllustrationCommandProfile()
        {
            CreateMap<UpdateIllustrationCommand, Illustration>().ReverseMap();
        }
    }
}
