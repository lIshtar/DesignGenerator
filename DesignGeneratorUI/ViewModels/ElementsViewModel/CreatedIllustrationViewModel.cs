using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGeneratorUI.ViewModels.ElementsViewModel
{
    public class CreatedIllustrationViewModel : BaseViewModel
    {
        private string _title;
        private string _prompt;
        private string _ilustrationPath;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public string Prompt
        {
            get => _prompt;
            set
            {
                _prompt = value;
                OnPropertyChanged(nameof(Prompt));
            }
        }

        public string IllustrationPath
        {
            get => _ilustrationPath;
            set
            {
                _ilustrationPath = value;
                OnPropertyChanged(nameof(IllustrationPath));
            }
        }
    }
}
