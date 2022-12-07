using System;
using System.Collections.Generic;

namespace Caliph.Library.Models
{
    public class AnnouncementEnt : BaseEnt
    {
        public long AnnouncementId { get; set; }
        public string Title { get; set; }
        public string Remarks { get; set; }
        public long StatusId { get; set; }
        public string StatusDesc { get; set; }
        public long AnnouncementTypeId { get; set; }
        public string AnnouncementTypeDesc { get; set; }
        public DateTime PublishStartDate { get; set; }
        public DateTime PublishEndDate { get; set; }
        public List<UserAnnouncementEnt> UserList { get; set; }
    }

    public class AnnouncementFilter
    {
        public long? AnnouncementId { get; set; }
        public long? UserId { get; set; }
        public DateTime? PublishStartDate { get; set; }
        public DateTime? PublishEndDate { get; set; }
        public long? StatusId { get; set; }
        public long PageSize { get; set; }
        public long PageNumber { get; set; }
    }

    public class UserAnnouncementEnt
    {
        public long AnnouncementId { get; set; }
        public long UserId { get; set; }
        public string Username { get; set; }
    }

}
