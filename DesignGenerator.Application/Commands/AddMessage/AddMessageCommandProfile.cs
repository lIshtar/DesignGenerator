using AutoMapper;
using DesignGenerator.Domain;


namespace DesignGenerator.Application.Commands.AddMessage
{
    public class AddMessageCommandProfile : Profile
    {
        public AddMessageCommandProfile()
        {
            CreateMap<AddMessageCommand, Message>().ReverseMap();
        }
    }
}
