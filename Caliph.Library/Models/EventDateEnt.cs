using System;

namespace Caliph.Library.Models
{
    public class EventDateEnt : BaseEnt
    {
        public long EventDateId { get; set; }
        public long EventId { get; set; }
        public DateTime EventDateFrom { get; set; }
        public DateTime EventDateTo { get; set; }
        public DateTime RegClosingDate { get; set; }
        public long EventDateStatusId { get; set; }
        public long StatusId { get; set; }
        public string StatusDesc { get; set; }
        public long TotalAttendance { get; set; }
    }

    public class EventRoleEnt : BaseEnt
    {
        public long EventRoleId { get; set; }
        public long EventId { get; set; }
        public long RoleId { get; set; }
        public string EventRoleDesc { get; set; }
        public long StatusId { get; set; }
        public string StatusDesc { get; set; }
    }

    public class EventDateFilter
    {
        public long? EventDateId { get; set; }
        public long? EventId { get; set; }
        public long PageSize { get; set; }
        public long PageNumber { get; set; }
    }
}
