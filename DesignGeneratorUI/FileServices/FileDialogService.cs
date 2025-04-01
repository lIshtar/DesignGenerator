using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;
using MessageBox = System.Windows.MessageBox;
using System.Windows;

namespace DesignGeneratorUI.FileServices
{
    public class FileDialogService : IOpenDialogService
    {
        public string FilePath { get; set; } = "";

        public bool OpenDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileName;
                return true;
            }
            return false;
        }

        //public bool SaveDialog()
        //{
        //    SaveFileDialog saveFileDialog = new SaveFileDialog();
        //    if (saveFileDialog.ShowDialog() == true)
        //    {
        //        FilePath = saveFileDialog.FileName;
        //        return true;
        //    }
        //    return false;
        //}

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}
