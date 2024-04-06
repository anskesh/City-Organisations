using CityOrganisations.Views;
using Prism.Mvvm;
using Prism.Regions;

namespace CityOrganisations.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
        
        private string _title = "City organizations";
        private readonly IRegionManager _regionManager;

        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            
            _regionManager.RegisterViewWithRegion(RegionNames.BranchesRegion, typeof(BranchesPage));
            _regionManager.RegisterViewWithRegion(RegionNames.HomeRegion, typeof(HomePage));
            _regionManager.RegisterViewWithRegion(RegionNames.OrganizationsRegion, typeof(OrganizationsPage));
            _regionManager.RegisterViewWithRegion(RegionNames.RegistersRegion, typeof(RegistersPage));
        }
    }
}