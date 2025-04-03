using DesignGenerator.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application.Queries.Communicate
{
    public class CommunicateQueryResponse : IResult
    {
        public string Message { get; set; }
    }
}
