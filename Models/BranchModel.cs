namespace CityOrganisations.Models
{
    public class BranchModel : Model
    {
        public int Id { get; set; }
        public int OrgId { get; set; }
        public string OrgName { get; set; }
        public string Director { get; set; }
        public string PhysicalAddress { get; set; }
        public string Region { get; set; }
        public float Xpos { get; set; }
        public float Ypos { get; set; }

        public BranchModel(){}
        
        public BranchModel(BranchModel model) : 
            this(model.Id, model.OrgId, model.OrgName, model.Director, model.PhysicalAddress, model.Region, model.Xpos, model.Ypos){}
        
        public BranchModel(int id, int orgId, string orgName, string director, string physicalAddress, string region, float xpos, float ypos)
        {
            Id = id;
            OrgId = orgId;
            OrgName = orgName;
            Director = director;
            PhysicalAddress = physicalAddress;
            Region = region;
            Xpos = xpos;
            Ypos = ypos;
        }

        public override bool HasEmptyValue()
        {
            return string.IsNullOrEmpty(OrgName) || string.IsNullOrEmpty(Director) || string.IsNullOrEmpty(PhysicalAddress);
        }

        public override string ToString()
        {
            return $"{OrgName}, {PhysicalAddress}";
        }
    }
}