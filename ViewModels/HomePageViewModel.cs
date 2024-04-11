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
        private IEnumerable<string> _allBranches;
        private bool _isFilterPopupOpen;

        public ObservableCollection<OrganizationModel> Organizations { get; set; }

        public HomePageViewModel()
        {
            Organizations = new ObservableCollection<OrganizationModel>();

            var organization1 = new OrganizationModel("ЧГУ");
            var organization2 = new OrganizationModel("Магнит");

            for (int i = 0; i < 20; i++)
            {
                organization1.Branches.Add(new BranchModel($"Филиал {i + 1}"));
                organization2.Branches.Add(new BranchModel($"Филиал {i + 21}"));
            }

            Organizations.Add(organization1);
            Organizations.Add(organization2);
        }

        // получение списка из всех организаций и всех филиалов для вывода в интерфейс
        public IEnumerable<string> GetAllBranches()
        {
            foreach (var organization in Organizations)
            {
                foreach (var branch in organization.Branches)
                {
                    yield return $"{organization.Name} {branch.Name}";
                }
            }
        }

        public IEnumerable<string> AllBranches
        {
            get { return _allBranches ??= GetAllBranches(); }
        }

        // обработка нажатия на "фильтр" для всплывающего окна
        public bool IsFilterPopupOpen
        {
            get => _isFilterPopupOpen;
            set => SetProperty(ref _isFilterPopupOpen, value);
        }

        private DelegateCommand _openFilterCommand;
        public DelegateCommand OpenFilterCommand =>
            _openFilterCommand ??= new DelegateCommand(ExecuteOpenFilterCommand);

        private void ExecuteOpenFilterCommand()
        {
            IsFilterPopupOpen = true;
        }

        private DelegateCommand _applyFilterCommand;
        public DelegateCommand ApplyFilterCommand =>
            _applyFilterCommand ??= new DelegateCommand(ExecuteApplyFilterCommand);

        private void ExecuteApplyFilterCommand()
        {
            IsFilterPopupOpen = false;
        }
    }
}