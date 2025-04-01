using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesignGenerator.Application.Interfaces;

namespace DesignGenerator.Application.Commands.AddNewIllustration
{
    public class AddNewIllustrationCommand : ICommand
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string IllustrationFolder {  get; set; }
    }
}
