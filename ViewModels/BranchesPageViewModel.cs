using CityOrganisations.Models;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using CityOrganisations.DataBase.Services;

namespace CityOrganisations.ViewModels
{
    public class BranchesPageViewModel : BaseViewModel
    {
        public ObservableCollection<BranchModel> Items => _dbService.Branches;
        public BranchModel SelectedItem => Items[0];
        
        private readonly DbService _dbService;

        public BranchesPageViewModel(DbService dbService)
        {
            _dbService = dbService;
        }
    }
}