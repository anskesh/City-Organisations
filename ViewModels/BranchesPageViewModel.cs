using CityOrganisations.Models;
using Prism.Mvvm;

namespace CityOrganisations.ViewModels
{
    public class BranchesPageViewModel : BindableBase
    {
        public BranchModel Branch { get; set; }

        public BranchesPageViewModel()
        {
            Branch = new BranchModel("Первая компания");
        }
    }
}