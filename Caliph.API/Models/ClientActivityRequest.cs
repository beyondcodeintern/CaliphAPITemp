using Caliph.Library.Models;
using System;

namespace Caliph.API.Models
{
    public class ClientActivityRequest : BaseEnt
    {
        public long ClientActivityId { get; set; }
        public long ClientDealActivityId { get; set; }
        public long ClientDealId { get; set; }
        public long ActivityPointId { get; set; }
        public int Points { get; set; }
        public long StatusId { get; set; }
        public DateTime? ActivityStartDate { get; set; }
        public DateTime? ActivityEndDate { get; set; }
        public string Remarks { get; set; }
    }

    public class ClientActivityFilterRequest
    {
        public long? StatusId { get; set; }
        public long? ClientDealStatusId { get; set; }
        public long? ClientId { get; set; }
        public long? ClientDealActivityId { get; set; }
        public long? DealTitleId { get; set; }
        public long? ClientDealId { get; set; }
        public DateTime? ActivityStartDate { get; set; }
        public DateTime? ActivityEndDate { get; set; }
        public string CreatedBy { get; set; }
        public long PageSize { get; set; }
        public long PageNumber { get; set; }
    }
}