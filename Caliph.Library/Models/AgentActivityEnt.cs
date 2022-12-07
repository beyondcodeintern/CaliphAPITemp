using System;

namespace Caliph.Library.Models
{
    public class AgentActivityEnt : BaseEnt
    {
        public long AgentActivityId { get; set; }
        public long AgentId { get; set; }
        public long AgentStatusId { get; set; }
        public string AgentStatusDesc { get; set; }
        public string AgentName { get; set; }
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

    public class AgentActivityFilter
    {
        public long? StatusId { get; set; }
        public long? AgentActivityId { get; set; }
        public long? AgentId { get; set; }
        public DateTime? ActivityStartDate { get; set; }
        public DateTime? ActivityEndDate { get; set; }
        public string CreatedBy { get; set; }
        public long PageSize { get; set; }
        public long PageNumber { get; set; }
    }
}
