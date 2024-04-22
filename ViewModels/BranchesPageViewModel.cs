using CityOrganisations.Models;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;

namespace CityOrganisations.ViewModels
{
    public class BranchesPageViewModel : BindableBase
    {
        private FilialBorderViewModel _filialBorderViewModel;
        public BranchModel Branch { get; set; }

        public BranchesPageViewModel()
        {
            _filialBorderViewModel = new FilialBorderViewModel();
            Branch = new BranchModel("Первая компания");
        }

        public IEnumerable<string> AllBranches => _filialBorderViewModel.AllBranches;

        public bool IsFilterPopupOpen
        {
            get => _filialBorderViewModel.IsFilterPopupOpen;
            set => _filialBorderViewModel.IsFilterPopupOpen = value;
        }

        public DelegateCommand OpenFilterCommand => _filialBorderViewModel.OpenFilterCommand;

        public DelegateCommand ApplyFilterCommand => _filialBorderViewModel.ApplyFilterCommand;

    }
}