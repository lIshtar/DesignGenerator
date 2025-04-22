using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Application
{
    public class IllustrationTemplate : INotifyPropertyChanged
    {
        public string _title;
        public string _prompt;
        public string Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(nameof(Title)); }
        }

        public string Prompt
        {
            get { return _prompt; }
            set { _prompt = value; OnPropertyChanged(nameof(Title)); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
