using DesignGenerator.Application.Interfaces;
using DesignGenerator.Application.Queries.CreateIllustration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application.Queries.CreateIllustration
{
    public class CreateIllustrationQuery : IQuery<CreateIllustrationQueryResponse>
    {
        public string Prompt { get; set; }
        public string FolderPath { get; set; }
    }
}
