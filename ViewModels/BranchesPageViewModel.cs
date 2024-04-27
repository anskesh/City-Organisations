using CityOrganisations.Models;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using CityOrganisations.DataBase.Services;

namespace CityOrganisations.ViewModels
{
    public class BranchesPageViewModel : BindableBase
    {
        //private FilialBorderViewModel _filialBorderViewModel;
        public ObservableCollection<BranchModel> Items => _dbService.Branches;
        public BranchModel SelectedBranch => Items[0];
        
        private readonly DbService _dbService;

        public BranchesPageViewModel(DbService dbService)
        {
            _dbService = dbService;
        }


        /*public bool IsFilterPopupOpen
        {
            get => _filialBorderViewModel.IsFilterPopupOpen;
            set => _filialBorderViewModel.IsFilterPopupOpen = value;
        }

        public DelegateCommand OpenFilterCommand => _filialBorderViewModel.OpenFilterCommand;

        public DelegateCommand ApplyFilterCommand => _filialBorderViewModel.ApplyFilterCommand;*/

    }
}