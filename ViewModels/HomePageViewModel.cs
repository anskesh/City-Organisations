using CityOrganisations.Models;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CityOrganisations.ViewModels
{
    public class HomePageViewModel : BindableBase
    {
        private IEnumerable<string> _allBranches;

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
    }
}