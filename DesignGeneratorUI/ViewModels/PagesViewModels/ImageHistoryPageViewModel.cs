using CommunityToolkit.Mvvm.ComponentModel;
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
    // TODO: implement db data
    public class ImageHistoryPageViewModel : ObservableObject
    {
        private const int PageSize = 6;

        public ObservableCollection<Illustration> AllImages { get; set; }
        public ObservableCollection<Illustration> PagedImages { get; set; }
        public ObservableCollection<int> PageNumbers { get; set; }

        private int _currentPage = 1;
        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                SetProperty(ref _currentPage, value);
                UpdatePagedImages();
            }
        }

        public int TotalPages => (int)Math.Ceiling((double)AllImages.Count / PageSize);

        public ICommand NextPageCommand { get; }
        public ICommand PreviousPageCommand { get; }
        public ICommand GoToPageCommand { get; }

        public ImageHistoryPageViewModel()
        {
            AllImages = new ObservableCollection<Illustration>();
            PagedImages = new ObservableCollection<Illustration>();
            PageNumbers = new ObservableCollection<int>();

            AllImages = InitializeImages();

            NextPageCommand = new RelayCommand(NextPage);
            PreviousPageCommand = new RelayCommand(PreviousPage);
            GoToPageCommand = new RelayCommand<int>(GoToPage);

            UpdatePagedImages();
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

        private ObservableCollection<Illustration> InitializeImages()
        {
            return null;
        }
    }
}
