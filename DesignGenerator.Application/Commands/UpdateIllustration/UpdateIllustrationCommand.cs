using DesignGenerator.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application.Commands.UpdateIllustration
{
    public class UpdateIllustrationCommand : ICommand
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Prompt { get; set; }
        public string IllustrationPath { get; set; }
        public bool IsReviewed { get; set; }
    }
}
