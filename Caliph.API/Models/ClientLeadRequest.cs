using Caliph.Library.Models;
using System;

namespace Caliph.API.Models
{
    public class ClientLeadRequest : BaseEnt
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

    public class ClientLeadFilterRequest
    {
        public long? StatusId { get; set; }
        public long? ClientLeadId { get; set; }
        public long? clientDealActivityId { get; set; }
        public long PageSize { get; set; }
        public long PageNumber { get; set; }
    }
}