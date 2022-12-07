using System;

namespace Caliph.Library.Models
{
    public class ActivityReviewEnt : BaseEnt
    {
        public long ActivityReviewId { get; set; }
        public long ActivityReviewTypeId { get; set; }
        public string ActivityReviewTypeDesc { get; set; }
        public long UserId { get; set; }
        public string Username { get; set; }
        public DateTime? DateInWeek { get; set; }
        public string ReviewText1 { get; set; }
        public string ReviewText2 { get; set; }
        public string ReviewText3 { get; set; }
        public string ReviewText4 { get; set; }
        public string ReviewText5 { get; set; }
        public string ReviewText6 { get; set; }
        public string ReviewText7 { get; set; }
        public string ReviewText8 { get; set; }
        public string ReviewText9 { get; set; }
        public string ReviewText10 { get; set; }
        public string ReviewText11 { get; set; }
        public string Remarks { get; set; }
        public long StatusId { get; set; }
        public string StatusDesc { get; set; }
    }

    public class ActivityReviewFilter
    {
        public long? ActivityReviewId { get; set; }
        public long? ActivityReviewTypeId { get; set; }
        public long? UserId { get; set; }
        public DateTime? DateInWeekFrom { get; set; }
        public DateTime? DateInWeekTo { get; set; }
        public long? StatusId { get; set; }
        public long PageSize { get; set; }
        public long PageNumber { get; set; }
    }
}
