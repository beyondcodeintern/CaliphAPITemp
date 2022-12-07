using System;

namespace Caliph.Library.Models
{
    public class ClientActivityEnt : BaseEnt
    {
        public long ClientDealActivityId { get; set; }
        public long ClientDealId { get; set; }
        public long DealTitleId { get; set; }
        public string DealTitleDesc { get; set; }
        public long ClientDealStatusId { get; set; }
        public string ClientDealStatusDesc { get; set; }
        public long ClientId { get; set; }
        public long ClientStatusId { get; set; }
        public string ClientStatusDesc { get; set; }
        public string ClientName { get; set; }
        public string ContactNo { get; set; }
        public long ActivityPointId { get; set; }
        public string ActivityPointsDesc { get; set; }
        public int Points { get; set; }
        public int PointSetting { get; set; }
        public string ColorCode { get; set; }
        public long StatusId { get; set; }
        public string StatusDesc { get; set; }
        public DateTime? ActivityStartDate { get; set; }
        public DateTime? ActivityEndDate { get; set; }
        public string Remarks { get; set; }
    }
}
