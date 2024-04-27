using System;
using CityOrganisations.Models;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;
using CityOrganisations.DataBase.Services;

namespace CityOrganisations.ViewModels
{
    public class HomePageViewModel : BindableBase
    {
        public IEnumerable<BranchModel> Items => _dbService.Branches;
        
        public bool IsFilterPopupOpen
        {
            get => _isFilterPopupOpen;
            set => SetProperty(ref _isFilterPopupOpen, value);
        }
        
        private bool _isFilterPopupOpen;
        
        private readonly DbService _dbService;

        public HomePageViewModel(DbService dbService)
        {
            _dbService = dbService;

            OpenFilterCommand = new DelegateCommand(ExecuteOpenFilterCommand);
            ApplyFilterCommand = new DelegateCommand(ExecuteApplyFilterCommand);
        }

        public DelegateCommand OpenFilterCommand { get; set; }
        public DelegateCommand ApplyFilterCommand { get; set; }

        private void ExecuteOpenFilterCommand()
        {
            IsFilterPopupOpen = true;
        }

        private void ExecuteApplyFilterCommand()
        {
            IsFilterPopupOpen = false;
        }
    }
}