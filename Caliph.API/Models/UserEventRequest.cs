using Caliph.Library.Models;

namespace Caliph.API.Models
{
    public class UserEventRequest : BaseEnt
    {
        public long UserEventId { get; set; }
        public long UserId { get; set; }
        public long EventDateId { get; set; }
        public long AttendanceId { get; set; }
        public long QuizScoreId { get; set; }
        public string Remarks { get; set; }
        public long CPDPoint { get; set; }
        public bool IsEmailSent { get; set; }
        public long StatusId { get; set; }
    }
}