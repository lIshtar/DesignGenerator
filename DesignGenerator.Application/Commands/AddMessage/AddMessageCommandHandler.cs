using AutoMapper;
using DesignGenerator.Application.Commands.AddPrompt;
using DesignGenerator.Application.Interfaces;
using DesignGenerator.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application.Commands.AddMessage
{
    public class AddMessageCommandHandler : ICommandHandler<AddMessageCommand>
    {
        private readonly IRepositoryService<Message> _repositoryService;
        private readonly IMapper _mapper;

        public AddMessageCommandHandler(IRepositoryService<Message> repository, IMapper mapper)
        {
            _repositoryService = repository;
            _mapper = mapper;
        }

        public async Task Handle(AddMessageCommand command)
        {
            var message = _mapper.Map<Message>(command);
            await _repositoryService.AddAsync(message);
        }
    }
}
