using System.Collections.ObjectModel;

namespace CityOrganisations.Models
{
    public class OrganizationModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LegalAddress { get; set; }
        public string TaxId { get; set; }
        public ObservableCollection<BranchModel> Branches { get; set; }

        public OrganizationModel(int id, string name, string legalAddress, string taxId)
        {
            Id = id;
            Name = name;
            LegalAddress = legalAddress;
            TaxId = taxId;
            
            Branches = new ObservableCollection<BranchModel>();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}