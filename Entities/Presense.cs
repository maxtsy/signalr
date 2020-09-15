namespace Entities
{
    public class Presense
    {
        public Presense()
        {

        }
        public Presense(User user) : this()
        {
            RegionId = user.Region.Id;
            CompanyId = user.Company.Id;
            AgencyId = user.DeliveryCenter.Id;
        }
        public long Id { get; set; }
        public long RegionId { get; set; }
        public string RegionName { get; set; }
        public long CompanyId { get; set; }
        public long AgencyId { get; set; }
        public bool IsCurrent { get; set; }
    }
}