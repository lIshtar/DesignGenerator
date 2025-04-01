using DesignGenerator.Application.Interfaces;
using DesignGenerator.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application.Queries.GetUnreviewedIllustrations
{
    public class GetUnreviewedIllustrationsQueryResponse : IResult
    {
        List<Illustartion> illustrations { get; set; }
    }
}
