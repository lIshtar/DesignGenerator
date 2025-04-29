using AutoMapper;
using DesignGenerator.Application.Commands.UpdateIllustration;
using DesignGenerator.Application.Interfaces;
using DesignGenerator.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application.Commands.DeletePrompt
{
    public class DeletePromptCommandHandler : ICommandHandler<DeletePromptCommand>
    {
        private IRepositoryService<Prompt> _repositoryService;
        private IMapper _mapper;

        public DeletePromptCommandHandler(IRepositoryService<Prompt> repositoryService, IMapper mapper)
        {
            _repositoryService = repositoryService;
            _mapper = mapper;
        }

        public async Task Handle(DeletePromptCommand command)
        {
            var prompt = _mapper.Map<Prompt>(command);
            await _repositoryService.DeleteAsync(prompt);
        }
    }
}
