using DesignGenerator.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DesignGeneratorUI.ViewModels.PagesViewModels
{
    // TODO: implement prompt storage
    public class PromptManagerPageViewModel : BaseViewModel
    {
        private bool IsEditMode;
        private Prompt _selectedPrompt;
        public ObservableCollection<Prompt> SavedPrompts { get; set; }

        public Prompt SelectedPrompt
        {
            get => _selectedPrompt;
            set
            {
                _selectedPrompt = value;
                if (value != null)
                {
                    EditingPromptName = value.Name;
                    EditingPromptText = value.Text;
                    IsEditMode = true;
                }
                OnPropertyChanged(nameof(SelectedPrompt));
            }
        }

        public string EditingPromptName { get; set; }
        public string EditingPromptText { get; set; }
        public string ConfirmButtonText => IsEditMode ? "Изменить" : "Создать";

        public ICommand ConfirmEditCommand { get; }
        public ICommand CancelEditCommand { get; }
        public ICommand DeletePromptCommand { get; }

        public PromptManagerPageViewModel()
        {
            //SavedPrompts = PromptStorage.LoadPrompts();

            ConfirmEditCommand = new RelayCommand(ConfirmEdit);
            CancelEditCommand = new RelayCommand(CancelEdit);
            DeletePromptCommand = new RelayCommand(DeletePrompt);
        }

        private void ConfirmEdit()
        {
            if (IsEditMode)
            {
                SelectedPrompt.Name = EditingPromptName;
                SelectedPrompt.Text = EditingPromptText;
            }
            else
            {
                SavedPrompts.Add(new Prompt { Name = EditingPromptName, Text = EditingPromptText });
            }

            //PromptStorage.SavePrompts(SavedPrompts);
            CancelEdit();
        }

        private void CancelEdit()
        {
            SelectedPrompt = null;
            EditingPromptName = "";
            EditingPromptText = "";
            IsEditMode = false;
            OnPropertyChanged(nameof(EditingPromptName));
            OnPropertyChanged(nameof(EditingPromptText));
            OnPropertyChanged(nameof(ConfirmButtonText));
        }

        private void DeletePrompt()
        {
            if (SelectedPrompt != null)
            {
                SavedPrompts.Remove(SelectedPrompt);
                //PromptStorage.SavePrompts(SavedPrompts);
                CancelEdit();
            }
        }
    }
}
