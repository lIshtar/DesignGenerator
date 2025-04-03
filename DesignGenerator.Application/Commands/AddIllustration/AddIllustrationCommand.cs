using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesignGenerator.Application.Interfaces;

namespace DesignGenerator.Application.Commands.AddIllustration
{
    public class AddIllustrationCommand : ICommand
    {
        public string Title { get; set; }
        public string Prompt { get; set; }
        public string IllustrationPath { get; set; }
        public bool IsReviewed { get; set; }
    }
}
