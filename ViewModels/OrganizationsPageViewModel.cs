using System.Collections.ObjectModel;
using CityOrganisations.DataBase.Services;
using CityOrganisations.Models;

namespace CityOrganisations.ViewModels
{
    public class OrganizationsPageViewModel : BaseViewModel
    {
        public ObservableCollection<OrganizationModel> Items => _dbService.Organizations;
        public OrganizationModel SelectedItem => Items[0];
        
        private readonly DbService _dbService;

        public OrganizationsPageViewModel(DbService dbService)
        {
            _dbService = dbService;
        }
    }
}