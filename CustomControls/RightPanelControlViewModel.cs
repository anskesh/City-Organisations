using CityOrganisations.Services.DataBase;
using Prism.Commands;
using Prism.Mvvm;

namespace CityOrganisations.CustomControls
{
    public class RightPanelControlViewModel : BindableBase
    {
        public bool IsFilterPopupOpen
        {
            get => _isFilterPopupOpen;
            set => SetProperty(ref _isFilterPopupOpen, value);
        }
        
        private bool _isFilterPopupOpen;
        
        public DelegateCommand OpenFilterCommand { get; set; }
        public DelegateCommand ApplyFilterCommand { get; set; }
        
        public RightPanelControlViewModel(EventService eventService)
        {
            OpenFilterCommand = new DelegateCommand(ExecuteOpenFilterCommand);
            ApplyFilterCommand = new DelegateCommand(ExecuteApplyFilterCommand);
        }
        
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