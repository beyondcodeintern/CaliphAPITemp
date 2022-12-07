using System;
using System.Collections.Generic;

namespace Caliph.Library.Models
{
    public class EventEnt : BaseEnt
    {
        public long EventId { get; set; }
        public string EventName { get; set; }
        public long EventTypeId { get; set; }
        public string EventTypeDesc { get; set; }
        public long EventHostId { get; set; }
        public string EventHostDesc { get; set; }
        public long EventChannelId { get; set; }
        public string EventChannelDesc { get; set; }
        public string EventChannelLocation { get; set; }
        public decimal EventFees { get; set; }
        public string Remarks { get; set; }
        public int PaxLimit { get; set; }
        public int CPDPoint { get; set; }
        public long AttendantTypeId { get; set; }
        public string AttendantTypeDesc { get; set; }
        public long StatusId { get; set; }
        public string StatusDesc { get; set; }
        public List<EventDateEnt> EventDateList { get; set; }
        public List<EventRoleEnt> EventRoleList { get; set; } = new List<EventRoleEnt>();
    }

    public class EventFilter
    {
        public long? EventId { get; set; }
        public string EventName { get; set; }
        public long? EventTypeId { get; set; }
        public long? RoleId { get; set; }
        public long? EventHostId { get; set; }
        public long? UserEventUserId { get; set; }
        public long? StatusId { get; set; }
        public long? UserEventStatusId { get; set; }
        public long? AttendanceId { get; set; }
        public DateTime? EventDateFrom { get; set; }
        public DateTime? EventDateTo { get; set; }
        public string CreatedBy { get; set; }
        public long PageSize { get; set; }
        public long PageNumber { get; set; }
    }

    public class UpcomingEventEnt : BaseEnt
    {
        public long EventId { get; set; }
        public string EventName { get; set; }
        public long EventTypeId { get; set; }
        public string EventTypeDesc { get; set; }
        public long EventHostId { get; set; }
        public string EventHostDesc { get; set; }
        public long EventChannelId { get; set; }
        public string EventChannelDesc { get; set; }
        public string EventChannelLocation { get; set; }
        public decimal EventFees { get; set; }
        public string Remarks { get; set; }
        public int PaxLimit { get; set; }
        public int CPDPoint { get; set; }
        public long AttendantTypeId { get; set; }
        public string AttendantTypeDesc { get; set; }
        public long StatusId { get; set; }
        public string StatusDesc { get; set; }
        public string UserEventStatusId { get; set; }
        public string UserEventStatusDesc { get; set; }
        public long EventDateId { get; set; }
        public DateTime EventDateFrom { get; set; }
        public DateTime EventDateTo { get; set; }
        public DateTime RegClosingDate { get; set; }
        public long? UserEventId { get; set; }
    }
}
