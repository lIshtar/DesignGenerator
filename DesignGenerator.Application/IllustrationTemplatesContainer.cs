using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application
{
    public class IllustrationTemplatesContainer
    {
        public static IllustrationTemplatesContainer GetInstance()
        {
            return Nested.instance;
        }

        public ObservableCollection<IllustrationTemplate> IllustrastionsTemplates { get; set; } = new();

        private class Nested
        {
            internal static readonly IllustrationTemplatesContainer instance = new IllustrationTemplatesContainer();
        }
    }
}
