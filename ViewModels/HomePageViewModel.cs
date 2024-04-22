using CityOrganisations.Models;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace CityOrganisations.ViewModels
{
    public class HomePageViewModel : BindableBase
    {
        private FilialBorderViewModel _filialBorderViewModel;

        public HomePageViewModel()
        {
            _filialBorderViewModel = new FilialBorderViewModel();
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