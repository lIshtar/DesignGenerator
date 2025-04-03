using DesignGenerator.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application.Queries
{
    class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceProvider _service;

        public QueryDispatcher(IServiceProvider service)
        {
            _service = service;
        }

        public async Task<TResponse> Send<TQuery, TResponse>(TQuery query) where TQuery : IQuery<TResponse> where TResponse : IResult
        {
            var handler = _service.GetService(typeof(IQueryHandler<TQuery, TResponse>));
            if (handler != null)
                return await ((IQueryHandler<TQuery, TResponse>)handler).Handle(query);
            else
                throw new Exception($"Command doesn't have any handler {query.GetType().Name}");
        }
    }
}
