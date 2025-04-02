using DesignGenerator.Application.Interfaces;
using DesignGenerator.Infrastructure.AICommunicators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application.Queries.Communicate
{
    public class CommunicateQueryHandler : IQueryHandler<CommunicateQuery, CommunicateQueryResponse>
    {
        private ITextAICommunicator _textAICommunicator;
        public CommunicateQueryHandler(ITextAICommunicator textAICommunicator)
        {
            _textAICommunicator = textAICommunicator;
        }

        public async Task<CommunicateQueryResponse> Handle(CommunicateQuery query)
        {
            string response = await _textAICommunicator.GetTextAnswerAsync(query.Query);
            return new CommunicateQueryResponse { Response = response };
        }
    }
}
