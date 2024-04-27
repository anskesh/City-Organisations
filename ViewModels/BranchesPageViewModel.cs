using CityOrganisations.Models;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using CityOrganisations.DataBase.Services;

namespace CityOrganisations.ViewModels
{
    public class BranchesPageViewModel : BindableBase
    {
        public ObservableCollection<BranchModel> Items => _dbService.Branches;
        public BranchModel SelectedBranch => Items[0];
        
        private readonly DbService _dbService;

        public BranchesPageViewModel(DbService dbService)
        {
            _dbService = dbService;
        }
    }
}