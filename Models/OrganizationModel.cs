using System.Collections.ObjectModel;

namespace CityOrganisations.Models
{
    public class OrganizationModel
    {
        public string OrganizationName { get; set; }
        public ObservableCollection<BranchModel> Branches { get; set; }

        public OrganizationModel(string organizationName)
        {
            OrganizationName = organizationName;
            Branches = new ObservableCollection<BranchModel>();
        }
    }
}