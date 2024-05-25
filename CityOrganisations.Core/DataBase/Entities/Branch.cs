using Core.Models;

namespace Core.DataBase.Models
{
    public class Branch : IEntity
    {
        public int Id { get; set; }
        public int OrgId { get; set; }
        public string Director { get; set; }
        public string PhysicalAddress { get; set; }
        public string Region { get; set; }
        public float XPos { get; set; }
        public float YPos { get; set; }
        
        public Branch(){}

        public Branch(BranchModel model)
        {
            Copy(model);
        }

        public void Copy(BranchModel model)
        {
            Id = model.Id;
            OrgId = model.OrgId;
            Director = model.Director;
            PhysicalAddress = model.PhysicalAddress;
            Region = model.Region;
            XPos = model.XPos;
            YPos = model.YPos;
        }
    }
}