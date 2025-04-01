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

namespace DesignGeneratorUI.ViewModels.PagesViewModels
{
    public class DataPageViewModel : BaseViewModel
    {
        private string? _savePath;
        private ObservableCollection<PrintedData> _dataList;

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

        public ObservableCollection<PrintedData> DataList
        {
            get => _dataList;
            set 
            { 
                _dataList = value; 
                OnPropertyChanged(nameof(DataList));
            }
        }

        private readonly IOpenDialogService _dialogService;
        private readonly IFileService _fileService;
        private readonly IConfiguration _config;
        //private readonly ApplicationDbContext _dbContext;

        public ICommand BrowseCommand { get; }
        public ICommand ExportToExcelCommand { get; }

        public DataPageViewModel(IFileService fileService, IOpenDialogService dialogService, IConfiguration config)
        {
            _fileService = fileService;
            _dialogService = dialogService;
            //_dbContext = dbContext;
            _config = config;

            SavePath = _config.GetRequiredSection("Folders")
                .GetRequiredSection("DefaultExcelFolder").Value
                ?? throw new Exception("Unable to find DefaultExcelFolder in appsettings.json");

            ExportToExcelCommand = new RelayCommand(ExportToExcel);
            BrowseCommand = new RelayCommand(Browse);

            LoadData();
        }

        private void ExportToExcel(object argument)
        {
            try
            {
                LoadData();
                _fileService.SaveToFile(SavePath + "\\ExportedData.xlsx", DataList);
                //_dialogService.ShowMessage("Файл сохранен");
            }
            catch (Exception ex)
            {
                _dialogService.ShowMessage(ex.Message);
            }
        }


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

        private void LoadData()
        {
            //var data = from items in _dbContext.Items
            //           join il in _dbContext.Illustrations on items.Id equals il.Item
            //           join c in _dbContext.Categories on items.Category equals c.Id
            //           join p in _dbContext.Patterns on items.Pattern equals p.Id
            //           select new PrintedData
            //           (
            //               items.Name,
            //               c.Name,
            //               p.Name,
            //               il.Prompt,
            //               il.Path,
            //               il.IllustrationText
            //           );

            //DataList = new ObservableCollection<PrintedData>(data.Take(10));
        }

        public record PrintedData(string? Name, string? Category, string? Pattern, string? Prompt, string? IllustrationPath, string? IllustrationText);
    }
}
