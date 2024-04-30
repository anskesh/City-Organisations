namespace CityOrganisations.Models
{
    public class OrganizationModel : Model
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LegalAddress { get; set; }
        public string TaxId { get; set; }

        public OrganizationModel(){}
        public OrganizationModel(OrganizationModel model) : 
            this(model.Id, model.Name, model.LegalAddress, model.TaxId) {}
        
        public OrganizationModel(int id, string name, string legalAddress, string taxId)
        {
            Id = id;
            Name = name;
            LegalAddress = legalAddress;
            TaxId = taxId;
        }

        public override string ToString()
        {
            return Name;
        }

        public override bool HasEmptyValue()
        {
            return string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(LegalAddress) || string.IsNullOrEmpty(TaxId);
        }
    }
}