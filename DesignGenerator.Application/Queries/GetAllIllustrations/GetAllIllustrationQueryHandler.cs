using AutoMapper;
using DesignGenerator.Application.Interfaces;
using DesignGenerator.Domain;

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
            var response = new GetAllIllustrationQueryResponse
            {
                Illustrations = illustrations.ToList()
            };
            return response;
        }
    }
}
