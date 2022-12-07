using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Caliph.API.Models
{
    public class ClientRequest
    {
        public long ClientId { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public string ICNo { get; set; }
        public string ContactNo { get; set; }
        public string EmailAdd { get; set; }
        public long SourceId { get; set; }
        public long StatusId { get; set; }
        public long AnnualIncomeId { get; set; }
        public long AgeId { get; set; }
        public long OccupationId { get; set; }
        public long MaritalId { get; set; }
        public long LengthOfTimeKnownId { get; set; }
        public long HowWellKnownId { get; set; }
        public long HowOftenSeenInAYearId { get; set; }
        public long CouldApproachOnBusinessId { get; set; }
        public long AbilityToProvideRefId { get; set; }
        public long GenderId { get; set; }
        public string EducationDesc { get; set; }
        public string IncomeYearDesc { get; set; }
        public string OtherSourceofIncomeDesc { get; set; }
        public string CareerDesc { get; set; }
        public DateTime? FilingDate { get; set; }
        public string UpdatedBy { get; set; }
        public string CurrentAddress { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? DOB { get; set; }
    }

    public class ClientContact
    {
        public long HPId { get; set; }
        public string ContactNo { get; set; }
    }

    public class ClientFilterRequest
    {
        public long? StatusId { get; set; }
        public string StatusIdList { get; set; }
        public long? ClientId { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? KIVDateFrom { get; set; }
        public DateTime? KIVDateTo { get; set; }
        public DateTime? CreatedDateFrom { get; set; }
        public DateTime? CreatedDateTo { get; set; }
        public long PageSize { get; set; }
        public long PageNumber { get; set; }
    }

    public class ClientKIVRevertHistoryFilterRequest
    {        
        public long? ClientId { get; set; }
        public string Name { get; set; }
        public DateTime? KIVDateFrom { get; set; }
        public DateTime? KIVDateTo { get; set; }
        public string CreatedBy { get; set; }
        public long PageSize { get; set; }
        public long PageNumber { get; set; }
    }

    public class ClientSummaryFilterRequest
    {
        public DateTime? CreatedDateFrom { get; set; }
        public DateTime? CreatedDateTo { get; set; }
        public string CreatedBy { get; set; }
    }
}