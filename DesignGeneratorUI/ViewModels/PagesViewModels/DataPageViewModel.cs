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

namespace DesignGeneratorUI.ViewModels.PagesViewModels
{
    public class DataPageViewModel : BaseViewModel
    {
        private string? _savePath;
        private ObservableCollection<Illustration> _dataList;

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

        public ObservableCollection<Illustration> DataList
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
        private readonly IQueryDispatcher _queryDispatcher;

        public ICommand BrowseCommand { get; }
        public ICommand ExportToExcelCommand { get; }

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

        //TODO: implement data loading with RequiredDate
        private void LoadData()
        {
            //DataList = 
        }
    }
}
