using System.Collections.ObjectModel;

namespace CityOrganisations.Models
{
    public class OrganizationModel
    {
        public string Name { get; set; }
        public ObservableCollection<BranchModel> Branches { get; set; }

        public OrganizationModel(string organizationName)
        {
            Name = organizationName;
            Branches = new ObservableCollection<BranchModel>();
        }
    }
}