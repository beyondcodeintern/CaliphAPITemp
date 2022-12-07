using System;

namespace Caliph.API.Models
{
    public class UserEventPaymentRequest
    {
        public long UserEventPaymentId { get; set; }
        public long UserEventId { get; set; }
        public long PaymentChannelId { get; set; }
        public string PaymentRefId { get; set; }
        public string PaymentResponse { get; set; }
        public long PayementStatusId { get; set; }
        public DateTime PayementCreatedDate { get; set; }
        public string Remarks { get; set; }
        public long StatusId { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class UserEventPaymentResponse
    {
        public long UserEventPaymentId { get; set; }
        public string UserEventPaymentRefNo { get; set; }
    }
}