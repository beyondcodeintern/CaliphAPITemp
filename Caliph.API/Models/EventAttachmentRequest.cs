using Caliph.Library.Models;

namespace Caliph.API.Models
{
    public class EventAttachmentRequest : BaseEnt
    {
        public long EventAttachmentId { get; set; }
        public long EventId { get; set; }
        public string EventAttachmentName { get; set; }
        public string EventAttachmentPath { get; set; }
        public string Remarks { get; set; }
        public long StatusId { get; set; }
    }
}