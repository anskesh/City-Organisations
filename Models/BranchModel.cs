namespace CityOrganisations.Models
{
    public class BranchModel
    {
        public int Id { get; set; }
        public string OrgName { get; set; }
        public string Director { get; set; }
        public string PhysicalAddress { get; set; }
        public string Region { get; set; }
        public float Xpos { get; set; }
        public float Ypos { get; set; }

        public BranchModel(int id, string orgName, string director, string physicalAddress, string region, float xpos, float ypos)
        {
            Id = id;
            OrgName = orgName;
            Director = director;
            PhysicalAddress = physicalAddress;
            Region = region;
            Xpos = xpos;
            Ypos = ypos;
        }

        public override string ToString()
        {
            return $"{OrgName}, {PhysicalAddress}";
        }
    }
}