using DesignGenerator.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application.Queries.GetUnreviewedIllustrations
{
    public class GetUnreviewedIllustrationsQueryHandler : IQueryHandler<GetUnreviewedIllustrationsQuery, GetUnreviewedIllustrationsQueryResponse>
    {
        IIllustartionRepository _illustrationRepository;
        public GetUnreviewedIllustrationsQueryHandler(IIllustartionRepository illustartionRepository)
        {
            _illustrationRepository = illustartionRepository;
        }

        public async Task<GetUnreviewedIllustrationsQueryResponse> Handle(GetUnreviewedIllustrationsQuery query)
        {
            throw new NotImplementedException();
        }
    }
}
