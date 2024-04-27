namespace CityOrganisations.Models
{
    public class Organization : IEntity
    {
        public int Id { get; set; }
        public string OrgName { get; set; }
        public string LegalAddress { get; set; }
        public string TaxId { get; set; }
    }
}