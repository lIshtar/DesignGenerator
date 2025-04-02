using DesignGenerator.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application.Queries.CreateIllustration
{
    public class CreateIllustrationQueryResponse : IResult
    {
        public string IllustrationPath {  get; set; }
    }
}
