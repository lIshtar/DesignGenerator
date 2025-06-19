using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGeneratorUI.FileServices
{
    public interface IFileService
    {
        void OpenFile(string filename);
        void SaveToFile(string filename, IEnumerable<object> data);
    }
}
