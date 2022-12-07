namespace Caliph.Library.Models
{
    public class EventAttachmentEnt : BaseEnt
    {
        public long EventAttachmentId { get; set; }
        public long EventId { get; set; }
        public string EventAttachmentName { get; set; }
        public string EventAttachmentPath { get; set; }
        public string Remarks { get; set; }
        public long StatusId { get; set; }
        public string StatusDesc { get; set; }
    }

    public class EventAttachmentFilter
    {
        public long? EventAttachmentId { get; set; }
        public long? EventId { get; set; }
        public long PageSize { get; set; }
        public long PageNumber { get; set; }
    }
}
