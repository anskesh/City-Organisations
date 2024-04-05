using CityOrganisations.Views;
using Prism.Mvvm;
using Prism.Regions;

namespace CityOrganisations.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;

        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            _regionManager.RequestNavigate("HomeRegion", nameof(HomePage));
        }
        
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
        
        private string _title = "City organizations";
    }
}