using System;

namespace Caliph.API.Models
{
    public class UserActivityRequest
    {
        public long UserActivityId { get; set; }
        public long UserId { get; set; }
        public long ActivityPointId { get; set; }
        public long StatusId { get; set; }
        public DateTime? ActivityStartDate { get; set; }
        public DateTime? ActivityEndDate { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }

}