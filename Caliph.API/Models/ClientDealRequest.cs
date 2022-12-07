using Caliph.Library.Models;
using System;

namespace Caliph.API.Models
{
    public class ClientDealRequest : BaseEnt
    {
        public long ClientDealId { get; set; }
        public long ClientId { get; set; }
        public string Name { get; set; }
        public long StatusId { get; set; }
        public string StatusDesc { get; set; }
        public long DealTitleId { get; set; }
        public string Remarks { get; set; }
    }

    public class ClientDealFilterRequest
    {
        public long? StatusId { get; set; }
        public long? ClientId { get; set; }
        public string Name { get; set; }
        public string ClientName { get; set; }
        public long? DealTitleId { get; set; }
        public string CreatedBy { get; set; }
        public string ClientCreatedBy { get; set; }
        public DateTime? CreatedDateFrom { get; set; }
        public DateTime? CreatedDateTo { get; set; }
        public long PageSize { get; set; }
        public long PageNumber { get; set; }
    }
}