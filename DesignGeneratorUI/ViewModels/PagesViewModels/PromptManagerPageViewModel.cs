using CommunityToolkit.Mvvm.Input;
using DesignGenerator.Application.Commands.AddPrompt;
using DesignGenerator.Application.Commands.DeletePrompt;
using DesignGenerator.Application.Commands.UpdatePrompt;
using DesignGenerator.Application.Interfaces;
using DesignGenerator.Application.Queries.GetAllPrompts;
using DesignGenerator.Domain;
using System.Collections.ObjectModel;
using ICommand = System.Windows.Input.ICommand;

namespace DesignGeneratorUI.ViewModels.PagesViewModels
{
    public class PromptManagerPageViewModel : BaseViewModel
    {
        private bool _isEditMode;
        public bool IsEditMode
        {
            get => _isEditMode;
            set 
            {
                if (_isEditMode != value)
                {
                    _isEditMode = value;
                    OnPropertyChanged(nameof(ConfirmButtonText));
                    OnPropertyChanged(nameof(IsEditMode));
                }
            }
        }
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
                    IsEditMode = true;
                }
                OnPropertyChanged(nameof(SelectedPrompt));
            }
        }

        public string EditingPromptName { get; set; }
        public string EditingPromptText { get; set; }
        public string ConfirmButtonText => IsEditMode ? "Изменить" : "Создать";

        public ICommand LoadedCommand { get; }
        public ICommand ConfirmEditCommand { get; }
        public ICommand CancelEditCommand { get; }
        public ICommand DeletePromptCommand { get; }
        public ICommand NewPromptCommand { get; }

        private IQueryDispatcher _queryDispatcher;
        private ICommandDispatcher _commandDispatcher;

        public PromptManagerPageViewModel(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;

            LoadedCommand = new AsyncRelayCommand(Loaded);
            ConfirmEditCommand = new RelayCommand(ConfirmEdit);
            CancelEditCommand = new RelayCommand(CancelEdit);
            DeletePromptCommand = new RelayCommand(DeletePrompt);
            NewPromptCommand = new RelayCommand(NewPrompt);
            _commandDispatcher = commandDispatcher;
        }

        private void NewPrompt()
        {
            SelectedPrompt = new Prompt();
            IsEditMode = false;
        }

        private async Task Loaded()
        {
            SavedPrompts = await LoadPrompts();
            OnPropertyChanged(nameof(SavedPrompts));
        }

        private async Task<ObservableCollection<Prompt>> LoadPrompts()
        {
            var query = new GetAllPromptsQuery();
            var response = await _queryDispatcher.Send<GetAllPromptsQuery, GetAllPromptsResponse>(query);
            return [.. response.Prompts];
        }

        private void ConfirmEdit()
        {
            if (IsEditMode)
            {
                var command = new UpdatePromptCommand
                {
                    Id = SelectedPrompt.Id,
                    Name = SelectedPrompt.Name,
                    Text = SelectedPrompt.Text,
                };

                int index = SavedPrompts.IndexOf(SelectedPrompt);
                if (index >= 0)
                {
                    SavedPrompts[index] = new Prompt
                    {
                        Id = SelectedPrompt.Id,
                        Name = SelectedPrompt.Name,
                        Text = SelectedPrompt.Text
                    };
                }

                _commandDispatcher.Send(command);
            }
            else
            {
                SavedPrompts.Add(new Prompt { Name = SelectedPrompt.Name, Text = SelectedPrompt.Text });
                var command = new AddPromptCommand
                {
                    Name = SelectedPrompt.Name,
                    Text = SelectedPrompt.Text,
                };
                _commandDispatcher.Send(command);
            }
            OnPropertyChanged(nameof(SavedPrompts));
            CancelEdit();
        }

        private void CancelEdit()
        {
            SelectedPrompt = new Prompt { Name = "", Text = ""};
            IsEditMode = false;
        }

        private void DeletePrompt()
        {
            if (SelectedPrompt != null)
            {
                SavedPrompts.Remove(SelectedPrompt);
                var command = new DeletePromptCommand
                {
                    Id = SelectedPrompt.Id,
                    Name = SelectedPrompt.Name,
                    Text = SelectedPrompt.Text,
                }; 
                _commandDispatcher.Send(command);
                CancelEdit();
            }
        }
    }
}
