using AutoMapper;
using DesignGenerator.Application.Interfaces;
using DesignGenerator.Application.Queries.GetAllIllustrations;
using DesignGenerator.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application.Queries.GetAllPrompts
{
    public class GetAllPromptsQueryHandler : IQueryHandler<GetAllPromptsQuery, GetAllPromptsResponse>
    {
        IRepositoryService<Prompt> _repositoryService;
        IMapper _mapper;
        public GetAllPromptsQueryHandler(IRepositoryService<Prompt> repositoryService, IMapper mapper)
        {
            _repositoryService = repositoryService;
            _mapper = mapper;
        }
        public async Task<GetAllPromptsResponse> Handle(GetAllPromptsQuery command)
        {
            var prompts = await _repositoryService.GetAllAsync();
            var response = new GetAllPromptsResponse
            {
                Prompts = prompts.ToList()
            };
            return response;
        }
    }
}
