using Core.Models;

namespace Core.DataBase.Models
{
    public class Organization : IEntity
    {
        public int Id { get; set; }
        public string OrgName { get; set; }
        public string LegalAddress { get; set; }
        public string TaxId { get; set; }
        
        public Organization(){}
        
        public Organization(OrganizationModel model)
        {
            Copy(model);
        }

        public void Copy(OrganizationModel model)
        {
            OrgName = model.Name;
            LegalAddress = model.LegalAddress;
            TaxId = model.TaxId;
        }
    }
}