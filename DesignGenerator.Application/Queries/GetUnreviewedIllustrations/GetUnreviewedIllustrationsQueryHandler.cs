using AutoMapper;
using DesignGenerator.Application.Interfaces;
using DesignGenerator.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application.Queries.GetUnreviewedIllustrations
{
    public class GetUnreviewedIllustrationsQueryHandler : IQueryHandler<GetUnreviewedIllustrationsQuery, GetUnreviewedIllustrationsQueryResponse>
    {
        IRepositoryService<Illustration> _repositoryService;
        IMapper _mapper;
        public GetUnreviewedIllustrationsQueryHandler(IRepositoryService<Illustration> illustartionRepository, IMapper mapper)
        {
            _repositoryService = illustartionRepository;
            _mapper = mapper;
        }

        // TODO: maybe implement (manually in mapper) mappering to response
        public async Task<GetUnreviewedIllustrationsQueryResponse> Handle(GetUnreviewedIllustrationsQuery query)
        {
            var illustrations = await _repositoryService.GetAllAsync();
            var unreviewedIllustrations = illustrations.Where(i => i.IsReviewed == false).ToList();
            var response = new GetUnreviewedIllustrationsQueryResponse
            {
                Illustrations = unreviewedIllustrations
            };
            return response;
        }
    }
}
