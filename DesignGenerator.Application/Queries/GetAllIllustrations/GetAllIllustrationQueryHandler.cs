using AutoMapper;
using DesignGenerator.Application.Interfaces;
using DesignGenerator.Application.Queries.CreateIllustration;
using DesignGenerator.Application.Queries.GetUnreviewedIllustrations;
using DesignGenerator.Domain;
using DesignGenerator.Infrastructure.AICommunicators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application.Queries.GetAllIllustrations
{
    public class GetAllIllustrationQueryHandler : IQueryHandler<GetAllIllustrationQuery, GetAllIllustrationQueryResponse>
    {
        IRepositoryService<Illustration> _repositoryService;
        IMapper _mapper;
        public GetAllIllustrationQueryHandler(IRepositoryService<Illustration> illustartionRepository, IMapper mapper)
        {
            _repositoryService = illustartionRepository;
            _mapper = mapper;
        }
        public async Task<GetAllIllustrationQueryResponse> Handle(GetAllIllustrationQuery command)
        {
            var illustrations = await _repositoryService.GetAllAsync();
            var unreviewedIllustrations = illustrations.ToList();
            var response = new GetAllIllustrationQueryResponse
            {
                Illustrations = unreviewedIllustrations
            };
            return response;
        }
    }
}
