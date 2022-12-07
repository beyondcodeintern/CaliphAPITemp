using System;

namespace Caliph.Library.Models
{
    public class UserEventPaymentEnt : BaseEnt
    {
        public long UserEventPaymentId { get; set; }
        public long UserEventId { get; set; }
        public string UserEventPaymentRefNo { get; set; }
        public long PaymentChannelId { get; set; }
        public string PaymentChannelDesc { get; set; }
        public string PaymentRefId { get; set; }
        public string PaymentResponse { get; set; }
        public long PayementStatusId { get; set; }
        public string PayementStatusDesc { get; set; }
        public DateTime PayementCreatedDate { get; set; }
        public string Remarks { get; set; }
        public long StatusId { get; set; }
        public string StatusDesc { get; set; }
    }

    public class UserEventPaymentFilter
    {
        public long? UserEventPaymentId { get; set; }
        public long? UserEventId { get; set; }
        public string UserEventPaymentRefNo { get; set; }
        public long? PayementStatusId { get; set; }
        public long? StatusId { get; set; }
        public string CreatedBy { get; set; }
        public long PageSize { get; set; }
        public long PageNumber { get; set; }
    }
}
