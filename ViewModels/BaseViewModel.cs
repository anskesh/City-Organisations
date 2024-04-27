using Prism.Commands;
using Prism.Mvvm;

namespace CityOrganisations.ViewModels
{
    public class BaseViewModel : BindableBase
    {
        public bool IsFilterPopupOpen
        {
            get => _isFilterPopupOpen;
            set => SetProperty(ref _isFilterPopupOpen, value);
        }
        
        private bool _isFilterPopupOpen;

        public BaseViewModel()
        {
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