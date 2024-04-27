using System.Collections.ObjectModel;
using CityOrganisations.DataBase.Services;
using CityOrganisations.Models;
using Prism.Mvvm;

namespace CityOrganisations.ViewModels
{
    public class OrganizationsPageViewModel : BindableBase
    {
        public ObservableCollection<OrganizationModel> Items => _dbService.Organizations;
        public OrganizationModel SelectedOrganization => Items[0];
        
        private readonly DbService _dbService;

        public OrganizationsPageViewModel(DbService dbService)
        {
            _dbService = dbService;
        }
    }
}