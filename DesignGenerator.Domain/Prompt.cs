using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Domain
{
    public class Prompt
    {
        public string Name { get; set; }
        public string Text { get; set; }

        public Prompt(string name, string text)
        {
            Name = name;
            Text = text;
        }

        public Prompt() { }
    }
}
