using Caliph.Library.Models;
using System;

namespace Caliph.API.Models
{
    public class AgentActivityRequest : BaseEnt
    {
        public long AgentActivityId { get; set; }
        public long AgentId { get; set; }
        public long ActivityPointId { get; set; }
        public int Points { get; set; }
        public long StatusId { get; set; }
        public DateTime? ActivityStartDate { get; set; }
        public DateTime? ActivityEndDate { get; set; }
        public string Remarks { get; set; }
    }   
}