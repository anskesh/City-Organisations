using System.Collections.ObjectModel;
using CityOrganisations.Services.DataBase;
using CityOrganisations.Dialogs;
using CityOrganisations.Events;
using CityOrganisations.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Services.Dialogs;

namespace CityOrganisations.ViewModels
{
    public abstract class BaseEditableViewModel<T> : BaseViewModel where T: Model, new()
    {
        public ObservableCollection<T> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        public T SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        private T _selectedItem;
        
        public DelegateCommand AddClickCommand { get; set; }
        public DelegateCommand EditClickCommand { get; set; }
        public DelegateCommand SaveClickCommand { get; set; }
        public DelegateCommand CancelClickCommand { get; set; }
        public DelegateCommand RemoveClickCommand { get; set; }

        protected readonly IEventAggregator EventAggregator;
        protected readonly IDatabaseService<T> DatabaseService;
        protected readonly IDialogService DialogService;
        
        private bool _isAdding;
        private ObservableCollection<T> _items;

        protected BaseEditableViewModel(IDatabaseService<T> databaseService, IDialogService dialogService, IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
            DatabaseService = databaseService;
            DialogService = dialogService;

            Items = DatabaseService.Items;
            
            if (Items.Count > 0) // если будет 0 элементов, то будет пустой элемент
                ChangeSelectCommand(SelectedIndex);

            EventAggregator.GetEvent<UpdateSelectedItemEvent>().Subscribe(UpdateItemSelect);
            
            AddClickCommand = new DelegateCommand(AddCommand);
            EditClickCommand = new DelegateCommand(EditCommand, CanExecuteEdit);
            SaveClickCommand = new DelegateCommand(SaveCommand, CanExecuteSave);
            CancelClickCommand = new DelegateCommand(CancelCommand);
            RemoveClickCommand = new DelegateCommand(RemoveCommand);
        }

        private void AddCommand()
        {
            Selectable = false;
            _isAdding = true;

            SelectedItem = new T();
        }

        private bool CanExecuteEdit()
        {
            return SelectedItem != null;
        }

        private void EditCommand()
        {
            Selectable = false;
            _isAdding = false;
            
            if (_selectedItem != null)
                SelectedItem = CreateItemCopy(GetSelectedItem());
        }

        protected abstract T CreateItemCopy(T item);

        private bool CanExecuteSave()
        {
            if (SelectedItem.HasEmptyValue()) // проверка на пустые поля
            {
                DialogService.ShowDialog(nameof(InformationDialog), new DialogParameters("Message=Чтобы продолжить, заполните все поля"), _ => {});
                return false;
            }

            if (!OnSaveAdditionalCheck()) // дополнительная проверка, если нужно
            {
                return false;
            }
            
            return true;
        }

        protected abstract bool OnSaveAdditionalCheck();
        
        private void SaveCommand()
        {
            if (_isAdding)
            {
                DatabaseService.Add(SelectedItem);
                ChangeSelectedIndex(Items.IndexOf(SelectedItem));
            }
            else
            {
                DatabaseService.Update(GetSelectedItem(), SelectedItem);
            }
            
            Selectable = true;
        }

        private void CancelCommand()
        {
            ChangeSelectCommand(SelectedIndex);
            Selectable = true;
        }
        
        private void RemoveCommand()
        {
            ButtonResult newResult = ButtonResult.None;
            
            DialogService.ShowDialog(nameof(ConfirmationDialog), new DialogParameters("Message=Вы уверены, что хотите удалить элемент?"),
                result => { newResult = result.Result; });
            
            if (newResult != ButtonResult.Yes)
                return;
            
            DatabaseService.Remove(SelectedItem);
        }
        
        protected override void ChangeSelectCommand(int? selectedIndex)
        {
            SelectedItem = GetSelectedItem();
        }

        private T GetSelectedItem() // TODO: проверить работу при сортировках и при пустых списках
        {
            if (SelectedIndex == -1) // элемент не выбран
            {
                if (Items.Count > 0)
                    ChangeSelectedIndex(0);
                else
                    return SelectedItem;
            }
            
            return Items[SelectedIndex];
        }

        private void ChangeSelectedIndex(int index)
        {
            SelectedIndex = index;
        }

        private void UpdateItemSelect()
        {
            SelectedItem = null;
            ChangeSelectCommand(SelectedIndex);
        }
    }
}