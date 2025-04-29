using DesignGenerator.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application.Commands.AddPrompt
{
    public class AddPromptCommand : ICommand
    {
        public string Name { get; set; }
        public string Text { get; set; }
    }
}
