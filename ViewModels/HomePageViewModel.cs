using CityOrganisations.Models;
using System.Collections.ObjectModel;
using CityOrganisations.DataBase.Services;

namespace CityOrganisations.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        public ObservableCollection<BranchModel> Items => _dbService.Branches;
        
        private readonly DbService _dbService;

        public HomePageViewModel(DbService dbService)
        {
            _dbService = dbService;
        }
    }
}