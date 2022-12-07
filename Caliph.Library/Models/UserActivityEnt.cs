using System;

namespace Caliph.Library.Models
{
    public class UserActivityEnt : BaseEnt
    {
        public long UserActivityId { get; set; }
        public long UserId { get; set; }
        public string Username { get; set; }
        public long ActivityPointId { get; set; }
        public string ActivityPointsDesc { get; set; }
        public int PointSetting { get; set; }
        public string ColorCode { get; set; }
        public long StatusId { get; set; }
        public string StatusDesc { get; set; }
        public DateTime? ActivityStartDate { get; set; }
        public DateTime? ActivityEndDate { get; set; }
        public string Remarks { get; set; }
    }

    public class UserActivityFilter
    {
        public long? UserActivityId { get; set; }
        public long? UserId { get; set; }
        public long? StatusId { get; set; }
        public DateTime? ActivityStartDate { get; set; }
        public DateTime? ActivityEndDate { get; set; }
        public long PageSize { get; set; }
        public long PageNumber { get; set; }
    }
}
