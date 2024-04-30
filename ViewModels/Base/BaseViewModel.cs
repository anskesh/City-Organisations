using Prism.Commands;
using Prism.Mvvm;

namespace CityOrganisations.ViewModels
{
    public abstract class BaseViewModel : BindableBase
    {
        public bool Selectable
        {
            get => _selectable;
            set => SetProperty(ref _selectable, value);
        }
        
        private bool _selectable = true;
        
        public int SelectedIndex
        {
            get => _selectedIndex;
            set => SetProperty(ref _selectedIndex, value);
        }
        
        private int _selectedIndex;

        public DelegateCommand<int?> SelectedItemCommand { get; set; }

        protected BaseViewModel()
        {
            SelectedItemCommand = new DelegateCommand<int?>(ChangeSelectCommand);
        }
        
        protected virtual void ChangeSelectCommand(int? selectedIndex) {}
    }
}