using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGeneratorUI.FileServices
{
    public interface IOpenDialogService
    {
        void ShowMessage(string message);   // показ сообщения
        string FilePath { get; set; }   // путь к выбранному файлу
        bool OpenDialog();  // открытие файла
    }
}
