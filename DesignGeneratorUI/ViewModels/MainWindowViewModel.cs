﻿using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using DesignGeneratorUI.Fabrics;
using DesignGeneratorUI.ViewModels.Navigation;
using DesignGeneratorUI.Views;
using DesignGeneratorUI.Views.Pages;

namespace DesignGeneratorUI.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private double _sideMenuWidth = 60;
        private bool _isMenuExpanded;

        public ObservableCollection<MenuItemViewModel> MenuItems { get; set; }
        public double SideMenuWidth
        {
            get => _sideMenuWidth;
            set { _sideMenuWidth = value; OnPropertyChanged(nameof(SideMenuWidth)); }
        }

        public bool IsMenuExpanded
        {
            get => _isMenuExpanded;
            set
            {
                if (_isMenuExpanded != value)
                {
                    _isMenuExpanded = value;
                    OnPropertyChanged(nameof(IsMenuExpanded));
                    // Обновляем ширину меню при изменении состояния
                    SideMenuWidth = _isMenuExpanded ? 200 : 50;
                }
            }
        }

        public ICommand NavigateCommand { get; }


        private readonly INavigationService _navigationService;

        public MainWindowViewModel(INavigationService navigationService, IPageFactory pageFactory)
        {
            _navigationService = navigationService;

            MenuItems = new ObservableCollection<MenuItemViewModel>
            {
                //new MenuItemViewModel("Home", "Images/homeicon.png", pageFactory.CreatePage<HomePage>()),
                new MenuItemViewModel("Главная", "Images/languagemodelicon.png", () => pageFactory.CreatePage<MainInteractionPage>()),
                new MenuItemViewModel("Экспорт", "Images/exporticon.png", () => pageFactory.CreatePage<DataPage>()),
                new MenuItemViewModel("Ревью", "Images/reviewicon.png", () => pageFactory.CreatePage<ImageViewerPage>()),
                new MenuItemViewModel("Промпты", "Images/promptsicon.png", () => pageFactory.CreatePage<PromptManagerPage>()),
                new MenuItemViewModel("История", "Images/historyicon.png", () => pageFactory.CreatePage<ImageHistoryPage>()),
                new MenuItemViewModel("Настройки", "Images/settingsicon.png", () => pageFactory.CreatePage<SettingsPage>()),
            };

            NavigateCommand = new RelayCommand<Page>(Navigate);
        }
       
        private void Navigate(object argument)
        {
            if (argument is Page page)
            {
                _navigationService.NavigateTo(page);
            }
            else
            {
                throw new Exception("Не получилось перейти на страницу");
            }
        }
    }
}
