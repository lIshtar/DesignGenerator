using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGeneratorUI.ViewModels.ElementsViewModel
{
    public class ChatMessageViewModel : BaseViewModel
    {
        private bool _isSelected;
        private bool _isSelectable;
        private bool _isBotMessage;

        public string? Text { get; set; }
        public bool IsBotMessage
        {
            get { return _isBotMessage; }
            set
            {
                _isBotMessage = value;
                OnPropertyChanged(nameof(IsBotMessage));
            }
        }
        public bool IsSelectable 
        { 
            get { return _isSelectable; }
            set
            {
                _isSelectable = value;
                OnPropertyChanged(nameof(IsSelectable));
            }
        } // Для отображения чекбоксов
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }
    }
}
