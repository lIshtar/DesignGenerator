using DesignGenerator.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application.Commands.CreateIllustration
{
    public class CreateIllustrationCommand : ICommand
    {
        public string Prompt { get; set; }
        public string FolderPath { get; set; }
    }
}
