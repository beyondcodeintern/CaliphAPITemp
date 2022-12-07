using Caliph.Library.Models;
using System;

namespace Caliph.API.Models
{
    public class AgentLeadRequest : BaseEnt
    {
        public long AgentActivityId { get; set; }
        public long AgentLeadId { get; set; }
        public long AgentId { get; set; }
        public string Name { get; set; }
        public string HP { get; set; }
        public long StatusId { get; set; }
        public string StatusDesc { get; set; }
        public string Remarks { get; set; }
    }

    public class AgentLeadFilterRequest
    {
        public long? AgentActivityId { get; set; }
        public long? AgentLeadId { get; set; }
        public long? AgentId { get; set; }
        public long? StatusId { get; set; }
        public long PageSize { get; set; }
        public long PageNumber { get; set; }
    }
}