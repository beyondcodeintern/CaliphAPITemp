using System;

namespace Caliph.Library.Models
{
    public class ClientLeadEnt : BaseEnt
    {
        public long ClientLeadId { get; set; }
        public long ClientDealActivityId { get; set; }
        public long ClientId { get; set; }
        public string Name { get; set; }
        public string HP { get; set; }
        public long StatusId { get; set; }
        public string StatusDesc { get; set; }
        public string Remarks { get; set; }
    }

    public class ClientLeadReport
    {
        public long ClientId { get; set; }
        public string ClientName { get; set; }
        public string ReferralName { get; set; }
        public string HP { get; set; }
        public string Remarks { get; set; }
        public long StatusId { get; set; }
        public string StatusDesc { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }

    public class ClientLeadReportFilter
    {
        public long? ClientId { get; set; }
        public long? StatusId { get; set; }
        public string ClientName { get; set; }
        public string ReferralName { get; set; }
        public string CreatedBy { get; set; }
        public long PageSize { get; set; }
        public long PageNumber { get; set; }
    }
}
