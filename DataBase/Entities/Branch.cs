namespace CityOrganisations.Models
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
    }
}