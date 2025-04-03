using AutoMapper;
using DesignGenerator.Application.Interfaces;
using DesignGenerator.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application.Commands.UpdateIllustration
{
    class UpdateIllustrationCommandHandler : ICommandHandler<UpdateIllustrationCommand>
    {
        private IRepositoryService<Illustration> _repositoryService;
        private IMapper _mapper;

        public UpdateIllustrationCommandHandler(IRepositoryService<Illustration> illustartionRepository, IMapper mapper)
        {
            _repositoryService = illustartionRepository;
            _mapper = mapper;
        }

        public async Task Handle(UpdateIllustrationCommand command)
        {
            var illustration = _mapper.Map<Illustration>(command);
            await _repositoryService.UpdateAsync(illustration);
        }
    }
}
