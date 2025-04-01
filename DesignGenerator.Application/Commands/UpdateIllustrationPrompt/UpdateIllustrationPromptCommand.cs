using DesignGenerator.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application.Commands.UpdateIllustrationPrompt
{
    public class UpdateIllustrationPromptCommand : ICommand
    {
        public string Prompt { get; set; }
        public string IllustrationPath { get; set; }
    }
}
