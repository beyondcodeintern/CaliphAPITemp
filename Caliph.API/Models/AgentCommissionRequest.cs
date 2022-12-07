using Caliph.Library.Models;
using System;

namespace Caliph.API.Models
{
    public class AgentCommissionRequest : BaseEnt
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
}