using Caliph.Library.Models;
using System.Collections.Generic;

namespace Caliph.API.Models
{
    public class EventRequest : BaseEnt
    {
        public long EventId { get; set; }
        public string EventName { get; set; }
        public long EventTypeId { get; set; }
        public long EventHostId { get; set; }
        public long EventChannelId { get; set; }
        public string EventChannelLocation { get; set; }
        public decimal EventFees { get; set; }
        public string Remarks { get; set; }
        public int PaxLimit { get; set; }
        public int CPDPoint { get; set; }
        public long AttendantTypeId { get; set; }
        public long StatusId { get; set; }
        public List<int> EventRoleIds { get; set; }
    }
}