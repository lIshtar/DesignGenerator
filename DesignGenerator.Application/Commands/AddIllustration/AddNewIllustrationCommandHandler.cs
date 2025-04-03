using AutoMapper;
using DesignGenerator.Application.Interfaces;
using DesignGenerator.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application.Commands.AddIllustration
{
    public class AddNewIllustrationCommandHandler : ICommandHandler<AddIllustrationCommand>
    {
        private readonly IRepositoryService<Illustration> _repositoryService;
        private readonly IMapper _mapper;

        public AddNewIllustrationCommandHandler(IRepositoryService<Illustration> repository, IMapper mapper)
        {
            _repositoryService = repository;
            _mapper = mapper;
        }

        public async Task Handle(AddIllustrationCommand command)
        {
            var illustration = _mapper.Map<Illustration>(command);
            await _repositoryService.AddAsync(illustration);
        }
    }
}
