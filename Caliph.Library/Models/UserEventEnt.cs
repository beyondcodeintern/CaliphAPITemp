using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caliph.Library.Models
{
    public class UserEventEnt : BaseEnt
    {
        public long UserEventId { get; set; }
        public long UserId { get; set; }
        public string Username { get; set; }
        public string EventId { get; set; }
        public string EventName { get; set; }
        public DateTime EventDateFrom  { get; set; }
        public DateTime EventDateTo { get; set; }
        public DateTime RegClosingDate { get; set; }
        public DateTime JoinDate { get; set; }

        public string DisplayName { get; set; }
        public long EventDateId { get; set; }
        public long UserEventStatusId { get; set; }
        public string UserEventStatusDesc { get; set; }
        public long AttendanceId { get; set; }
        public string AttendanceDesc { get; set; }
        public long QuizScoreId { get; set; }
        public string QuizScoreDesc { get; set; }

        public long PaymentChannelId { get; set; }
        public string PaymentChannelDesc { get; set; }
        public string Remarks { get; set; }
        public long CPDPoint { get; set; }
        public bool IsEmailSent { get; set; }
        public long StatusId { get; set; }
        public string StatusDesc { get; set; }
    }

    public class UserEventFilter
    {
        public long? UserEventId { get; set; }
        public long? UserId { get; set; }
        public long? EventId { get; set; }
        public long? EventDateId { get; set; }
        public string EventName { get; set; }
        public long? EventTypeId { get; set; }
        public long? EventHostId { get; set; }
        public long? StatusId { get; set; }
        public long? AttendanceId { get; set; }
        public long? PaymentChannelId { get; set; }
        public string CreatedBy { get; set; }
        public long PageSize { get; set; }
        public long PageNumber { get; set; }
        public DateTime? EventDateFrom { get;  set; }
        public DateTime? EventDateTo { get;  set; }
    }
}
