using System;

namespace Caliph.Library.Models
{
    public class AgentCommissionEnt : BaseEnt
    {
        public long AgentCommissionId { get; set; }
        public string Username { get; set; }
        public string AgentName { get; set; }
        public DateTime PayoutDate { get; set; }
        public DateTime CycleStartDate { get; set; }
        public DateTime CycleEndDate { get; set; }
        public long StatusId { get; set; }
        public decimal CommAmt { get; set; }
    }

    public class AgentCommissionFilter
    {
        public long? AgentCommissionId { get; set; }
        public string Username { get; set; }
        public DateTime? PayoutDateFrom { get; set; }
        public DateTime? PayoutDateTo { get; set; }
        public long PageSize { get; set; }
        public long PageNumber { get; set; }
    }
}
