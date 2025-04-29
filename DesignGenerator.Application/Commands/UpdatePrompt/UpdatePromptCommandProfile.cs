using AutoMapper;
using DesignGenerator.Application.Commands.AddIllustration;
using DesignGenerator.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application.Commands.UpdatePrompt
{
    public class UpdatePromptCommandProfile : Profile
    {
        public UpdatePromptCommandProfile()
        {
            CreateMap<UpdatePromptCommand, Prompt>().ReverseMap();
        }
    }
}
