namespace Caliph.Library.Models
{
    public class ClientDealEnt : BaseEnt
    {
        public long ClientDealId { get; set; }
        public long ClientId { get; set; }
        public string ClientName { get; set; }
        public long StatusId { get; set; }
        public string StatusDesc { get; set; }
        public string DealTitleDesc { get; set; }
        public long DealTitleId { get; set; }
        public string Name { get; set; }
        public string Remarks { get; set; }
        public string ContactNo { get; set; }
        public string ClientCreatedBy { get; set; }
    }
}
