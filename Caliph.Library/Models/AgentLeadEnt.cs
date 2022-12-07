using System;

namespace Caliph.Library.Models
{
    public class AgentLeadEnt : BaseEnt
    {
        public long AgentActivityId { get; set; }
        public long AgentLeadId { get; set; }
        public long AgentId { get; set; }
        public string Name { get; set; }
        public string AgentRecruitName { get; set; }
        public string HP { get; set; }
        public long StatusId { get; set; }
        public string StatusDesc { get; set; }
        public string Remarks { get; set; }
    }

    public class AgentLeadFilter
    {
        public long? AgentActivityId { get; set; }
        public long? AgentLeadId { get; set; }
        public long? AgentId { get; set; }
        public long? StatusId { get; set; }
        public string Name { get; set; }
        public string AgentRecruitName { get; set; }
        public string CreatedBy { get; set; }
        public long PageSize { get; set; }
        public long PageNumber { get; set; }
    }

    public class AgentLeadReport
    {
        public long AgentId { get; set; }
        public string AgentName { get; set; }
        public string ReferralName { get; set; }
        public string HP { get; set; }
        public string Remarks { get; set; }
        public long StatusId { get; set; }
        public string StatusDesc { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }

    public class AgentLeadReportFilter
    {
        public long? AgentId { get; set; }
        public long? StatusId { get; set; }
        public string AgentName { get; set; }
        public string ReferralName { get; set; }
        public string CreatedBy { get; set; }
        public long PageSize { get; set; }
        public long PageNumber { get; set; }
    }
}
