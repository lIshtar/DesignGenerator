using DesignGenerator.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application.Queries.Communicate
{
    public class CommunicateQuery: IQuery<CommunicateQueryResponse>
    {
        public string Query {  get; set; }
    }
}
