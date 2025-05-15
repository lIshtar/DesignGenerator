using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DesignGenerator.Application.Interfaces;
using DesignGenerator.Application.Queries.GetAllIllustrations;
using DesignGenerator.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICommand = System.Windows.Input.ICommand;

namespace DesignGeneratorUI.ViewModels.PagesViewModels
{
    public class ImageHistoryPageViewModel : ObservableObject
    {
        private IQueryDispatcher _queryDispatcher;
        private const int PageSize = 6;

        public ObservableCollection<Illustration> AllImages { get; set; } = new();
        public ObservableCollection<Illustration> PagedImages { get; set; } = new();

        public ObservableCollection<int> PageNumbers { get; set; } = new();

        private int _currentPage = 1;
        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                UpdatePagedImages();
            }
        }

        public int TotalPages => (int)Math.Ceiling((double)AllImages.Count / PageSize);

        public ICommand LoadedCommand { get; }
        public ICommand NextPageCommand { get; }
        public ICommand PreviousPageCommand { get; }
        public ICommand GoToPageCommand { get; }

        public ImageHistoryPageViewModel(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;

            LoadedCommand = new AsyncRelayCommand(Loaded);
            NextPageCommand = new RelayCommand(NextPage);
            PreviousPageCommand = new RelayCommand(PreviousPage);
            GoToPageCommand = new RelayCommand<int>(GoToPage);

            UpdatePagedImages();
        }

        private async Task Loaded()
        {
            await LoadImages();
        }

        private void NextPage()
        {
            if (CurrentPage < TotalPages)
                CurrentPage++;
        }

        private void PreviousPage()
        {
            if (CurrentPage > 1)
                CurrentPage--;
        }

        private void GoToPage(int page)
        {
            if (page >= 1 && page <= TotalPages)
                CurrentPage = page;
        }

        private void UpdatePagedImages()
        {
            PagedImages.Clear();
            var pageItems = AllImages.Skip((CurrentPage - 1) * PageSize).Take(PageSize);
            foreach (var item in pageItems)
                PagedImages.Add(item);

            UpdatePageNumbers();
        }

        private void UpdatePageNumbers()
        {
            PageNumbers.Clear();
            for (int i = 1; i <= TotalPages; i++)
                PageNumbers.Add(i);
        }

        private async Task LoadImages()
        {
            var query = new GetAllIllustrationQuery();
            var response = await _queryDispatcher.Send<GetAllIllustrationQuery, GetAllIllustrationQueryResponse>(query);
            AllImages = new ObservableCollection<Illustration>(response.Illustrations);
            CurrentPage = 1;
            UpdatePagedImages();
        }
    }
}
