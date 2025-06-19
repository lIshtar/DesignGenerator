using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application.Interfaces    
{
    public interface IQueryDispatcher
    {
        Task<TResponse> Send<TQuery, TResponse>(TQuery query) 
            where TQuery : IQuery<TResponse> 
            where TResponse : IResult;
    }
}
