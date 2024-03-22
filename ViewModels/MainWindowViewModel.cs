using Prism.Mvvm;

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
    }
}