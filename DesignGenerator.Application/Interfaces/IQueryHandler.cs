using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application.Interfaces
{
    public interface IQueryHandler
    {
    }
    public interface IQueryHandler<TQuery, TResult> : IQueryHandler 
        where TQuery : IQuery<TResult> 
        where TResult : IResult
    {
        Task<TResult> Handle(TQuery query);
    }
}
