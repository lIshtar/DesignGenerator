using Microsoft.Win32;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DesignGeneratorUI.FileServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using DesignGenerator.Application.Interfaces;
using ICommand = System.Windows.Input.ICommand;
using DesignGenerator.Domain;
using DesignGenerator.Application.Queries.GetAllIllustrations;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Dynamic;

namespace DesignGeneratorUI.ViewModels.PagesViewModels
{
    public class DataPageViewModel : BaseViewModel
    {
        private string? _savePath;
        public bool _includeReviewing;
        public bool _includeGenerationDate;
        private ObservableCollection<Illustration> _filteredIllustrations;
        //public ObservableCollection<ExportColumn> AvailableColumns { get; set; } = new();

        public string? SavePath
        {
            get => _savePath;
            set 
            { 
                _savePath = value;
                _config.GetRequiredSection("Folders").GetRequiredSection("DefaultExcelFolder").Value = value;
                OnPropertyChanged(nameof(SavePath)); 
            }
        }

        public ObservableCollection<Illustration> FilteredIllustrations
        {
            get => _filteredIllustrations;
            set 
            { 
                _filteredIllustrations = value; 
                OnPropertyChanged(nameof(FilteredIllustrations));
            }
        }

        public bool IncludeGenerationDate
        {
            get => _includeGenerationDate;
            set
            {
                _includeGenerationDate = value;
                OnPropertyChanged(nameof(IncludeGenerationDate));
            }
        }

        public bool IncludeReviewing
        {
            get => _includeReviewing;
            set
            {
                _includeReviewing = value;
                OnPropertyChanged(nameof(IncludeReviewing));
            }
        }

        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }


        private readonly IOpenDialogService _dialogService;
        private readonly IFileService _fileService;
        private readonly IConfiguration _config;
        private readonly IQueryDispatcher _queryDispatcher;

        public ICommand BrowseCommand { get; }
        public ICommand ExportToExcelCommand { get; }
        public ICommand FilterCommand { get; }


        public DataPageViewModel(IFileService fileService, IOpenDialogService dialogService, IQueryDispatcher queryDispatcher, IConfiguration config)
        {
            _fileService = fileService;
            _dialogService = dialogService;
            _queryDispatcher = queryDispatcher;
            _config = config;

            SavePath = _config.GetRequiredSection("Folders")
                .GetRequiredSection("DefaultExcelFolder").Value
                ?? throw new Exception("Unable to find DefaultExcelFolder in appsettings.json");

            ExportToExcelCommand = new RelayCommand(ExportToExcel);
            BrowseCommand = new RelayCommand(Browse);
            FilterCommand = new RelayCommand(ApplyFilter);


            //AvailableColumns = new ObservableCollection<ExportColumn>
            //{
            //    new ExportColumn("Номер", "Id"),
            //    new ExportColumn("Название", "Title"),
            //    new ExportColumn("Промпт", "Prompt"),
            //    new ExportColumn("Путь", "IllustrationPath"),
            //    new ExportColumn("Проверено", "IsReviewed"),
            //    new ExportColumn("Дата", "GenerationDate"),
            //};

            LoadData();
        }

        // TODO: export to excel does not work (maybe normal office required)
        private void ExportToExcel(object argument)
        {
            try
            {
                LoadData();
                var exportedData = FilteredIllustrations.Select(i => 
                { 
                    dynamic obj = new ExpandoObject();
                    obj.Title = i.Title;
                    obj.Prompt = i.Prompt;
                    obj.Path = i.IllustrationPath;
                    if (IncludeGenerationDate)
                        obj.CreatedAt = i.GenerationDate;
                    if (IncludeReviewing)
                        obj.IsReviewed = i.IsReviewed;
                    return obj;
                });
                _fileService.SaveToFile(SavePath + "\\ExportedData.xlsx", exportedData);
                _dialogService.ShowMessage("Успешно экспортировано");
            }
            catch (Exception ex)
            {
                _dialogService.ShowMessage(ex.Message);
            }
        }

        private async void ApplyFilter(object argument)
        {
            var query = new GetAllIllustrationQuery();
            var response = await _queryDispatcher.Send<GetAllIllustrationQuery, GetAllIllustrationQueryResponse>(query);
            var all = response.Illustrations;

            var filtered = all.Where(i =>
            (!DateFrom.HasValue || i.GenerationDate >= DateFrom) &&
            (!DateTo.HasValue || i.GenerationDate <= DateTo)).ToList();

            FilteredIllustrations = new ObservableCollection<Illustration>(filtered);
        }


        //TODO: implement default path change
        private void Browse(object argument)
        {
            try
            {
                if (_dialogService.OpenDialog() == true)
                {
                    SavePath = _dialogService.FilePath;
                    LoadData();
                    //_fileService.OpenFile(_dialogService.FilePath);
                    //DataList.Clear();
                    //_dialogService.ShowMessage("Файл открыт");
                }
            }
            catch (Exception ex)
            {
                _dialogService.ShowMessage(ex.Message);
            }
        }

        private async void LoadData()
        {
            var query = new GetAllIllustrationQuery();
            var response = await _queryDispatcher.Send<GetAllIllustrationQuery, GetAllIllustrationQueryResponse>(query);
            var all = response.Illustrations;

            var filtered = all.Where(i =>
            (!DateFrom.HasValue || i.GenerationDate >= DateFrom) &&
            (!DateTo.HasValue || i.GenerationDate <= DateTo)).ToList();

            FilteredIllustrations = new ObservableCollection<Illustration>(filtered);
        }
    }
}

//public record ExportColumn(string Header, string BindingPath, bool IsSelected = true);
