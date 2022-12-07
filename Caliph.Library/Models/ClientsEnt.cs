using System;
using System.Collections.Generic;

namespace Caliph.Library.Models
{
    public class ClientsEnt : BaseEnt
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
        public string CurrentAddress { get; set; }
        public DateTime? FilingDate { get; set; }
        public DateTime? DOB { get; set; }
    }

    public class ClientDetailsEnt : BaseEnt
    {
        public long TotalDeals { get; set; }
        public long ClientId { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public string ContactNo { get; set; }
        public string ICNo { get; set; }
        public string EmailAdd { get; set; }
        public string CurrentAddress { get; set; }
        public string EducationDesc { get; set; }
        public string IncomeYearDesc { get; set; }
        public string OtherSourceofIncomeDesc { get; set; }
        public DateTime? FilingDate { get; set; }
        public DateTime? KIVDate { get; set; }
        public DateTime? DOB { get; set; }
        public long SourceId { get; set; }
        public string SourceDesc { get; set; }
        public long StatusId { get; set; }
        public string StatusDesc { get; set; }
        public long AnnualIncomeId { get; set; }
        public string AnnualIncomeDesc { get; set; }
        public long AgeId { get; set; }
        public string AgeDesc { get; set; }
        public long OccupationId { get; set; }
        public string OccupationDesc { get; set; }
        public long MaritalId { get; set; }
        public string MaritalDesc { get; set; }
        public long LengthOfTimeKnownId { get; set; }
        public string LengthOfTimeKnownDesc { get; set; }
        public long HowOftenSeenInAYearId { get; set; }
        public string HowOftenSeenInAYearDesc { get; set; }
        public long HowWellKnownId { get; set; }
        public string HowWellKnownDesc { get; set; }
        public long CouldApproachOnBusinessId { get; set; }
        public string CouldApproachOnBusinessDesc { get; set; }
        public long AbilityToProvideRefId { get; set; }
        public string AbilityToProvideRefDesc { get; set; }
        public long GenderId { get; set; }
        public string GenderDesc { get; set; }
        public string CareerDesc { get; set; }
    }

    public class ClientHPDetailsEnt : BaseEnt
    {
        public long ClientHPId { get; set; }
        public long ClientId { get; set; }
        public long StatusId { get; set; }
        public string StatusDesc { get; set; }
        public long HPId { get; set; }
        public string HPDesc { get; set; }
        public string ContactNo { get; set; }
    }

    public class ClientKIVRevertHistory : BaseEnt
    {
        public long ClientKIVHistoryId { get; set; }
        public long ClientId { get; set; }
        public string Name { get; set; }
        public string ContactNo { get; set; }
        public DateTime? FilingDate { get; set; }
        public DateTime? KIVDate { get; set; }
        public DateTime? RevertDate { get; set; }
    }

    public class ClientSummaryEnt
    {
        public string MasterName { get; set; }
        public string MasterDataName { get; set; }
        public int Total { get; set; }
        public decimal Percentage { get; set; }
    }
}
