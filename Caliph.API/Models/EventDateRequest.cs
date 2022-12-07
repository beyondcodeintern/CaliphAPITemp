using Caliph.Library.Models;
using System;

namespace Caliph.API.Models
{
    public class EventDateRequest : BaseEnt
    {
        public long EventDateId { get; set; }
        public long EventId { get; set; }
        public DateTime EventDateFrom { get; set; }
        public DateTime EventDateTo { get; set; }
        public DateTime RegClosingDate { get; set; }
        //public long EventDateStatusId { get; set; }
        public long StatusId { get; set; }
    }
}