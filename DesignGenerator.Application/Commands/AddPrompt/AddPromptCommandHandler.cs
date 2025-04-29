using AutoMapper;
using DesignGenerator.Application.Commands.AddIllustration;
using DesignGenerator.Application.Interfaces;
using DesignGenerator.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application.Commands.AddPrompt
{
    public class AddPromptCommandHandler : ICommandHandler<AddPromptCommand>
    {
        private readonly IRepositoryService<Prompt> _repositoryService;
        private readonly IMapper _mapper;

        public AddPromptCommandHandler(IRepositoryService<Prompt> repository, IMapper mapper)
        {
            _repositoryService = repository;
            _mapper = mapper;
        }

        public async Task Handle(AddPromptCommand command)
        {
            var prompt = _mapper.Map<Prompt>(command);
            await _repositoryService.AddAsync(prompt);
        }
    }
}
