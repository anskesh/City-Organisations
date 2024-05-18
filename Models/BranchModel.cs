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
        public float XPos { get; set; }
        public float YPos { get; set; }

        public BranchModel(){}
        
        public BranchModel(BranchModel model) : 
            this(model.Id, model.OrgId, model.OrgName, model.Director, model.PhysicalAddress, model.Region, model.XPos, model.YPos){}
        
        public BranchModel(int id, int orgId, string orgName, string director, string physicalAddress, string region, float xPos, float yPos)
        {
            Id = id;
            OrgId = orgId;
            OrgName = orgName;
            Director = director;
            PhysicalAddress = physicalAddress;
            Region = region;
            XPos = xPos;
            YPos = yPos;
        }

        public void Copy(BranchModel branchModel)
        {
            Id = branchModel.Id;
            OrgId = branchModel.OrgId;
            OrgName = branchModel.OrgName;
            Director = branchModel.Director;
            PhysicalAddress = branchModel.PhysicalAddress;
            Region = branchModel.Region;
            XPos = branchModel.XPos;
            YPos = branchModel.YPos;
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