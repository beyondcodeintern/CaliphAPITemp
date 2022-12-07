using Caliph.Library.Models;
using System;

namespace Caliph.API.Models
{
    public class ClientPolicyRequest : BaseEnt
    {
        public long ClientPolicyId { get; set; }
        public long ClientId { get; set; }
        public long StatusId { get; set; }
        public string CompanyDesc { get; set; }
        public string PolicyNo { get; set; }
        public string PolicyTypeDesc { get; set; }
        public decimal SumAssured { get; set; }
        public decimal Premium { get; set; }
        public decimal CriticaIIllnessVal { get; set; }
        public decimal PersonalAccidentVal { get; set; }
        public decimal MedicalCardVal { get; set; }
        public string CoverageTerms { get; set; }
        public DateTime? DateInForced { get; set; }
    }

    public class ClientPolicyFilterRequest
    {
        public long? StatusId { get; set; }
        public long? ClientId { get; set; }
        public long PageSize { get; set; }
        public long PageNumber { get; set; }
    }
}