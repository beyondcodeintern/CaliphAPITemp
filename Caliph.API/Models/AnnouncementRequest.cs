using System;
using System.Collections.Generic;

namespace Caliph.API.Models
{
    public class AnnouncementRequest
    {
        public long AnnouncementId { get; set; }
        public string Title { get; set; }
        public string Remarks { get; set; }
        public long StatusId { get; set; }
        public long AnnouncementTypeId { get; set; }
        public List<long> UserIdList { get; set; }
        public DateTime PublishStartDate { get; set; }
        public DateTime PublishEndDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}