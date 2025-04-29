using AutoMapper;
using DesignGenerator.Application.Commands.AddIllustration;
using DesignGenerator.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application.Commands.AddPrompt
{
    public class AddPromptCommandProfile : Profile
    {
        public AddPromptCommandProfile()
        {
            CreateMap<AddPromptCommand, Prompt>().ReverseMap();
        }
    }
}
