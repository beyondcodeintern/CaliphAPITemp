using System;

namespace Caliph.Library.Models
{
    public class AgentRecruitEnt : BaseEnt
    {
        public long AgentRecruitId { get; set; }
        public string Name { get; set; }
        public string ContactNo { get; set; }
        public long EducationBgId { get; set; }
        public string EducationBgDesc { get; set; }
        public long AgeId { get; set; }
        public string AgeDesc { get; set; }
        public string EmailAdd { get; set; }
        public long AnnualIncomeId { get; set; }
        public string AnnualIncomeDesc { get; set; }
        public long OccupationId { get; set; }
        public string OccupationDesc { get; set; }
        public long MaritalId { get; set; }
        public string MaritalDesc { get; set; }
        public long TypeId { get; set; }
        public string TypeDesc { get; set; }
        public long StatusId { get; set; }
        public string StatusDesc { get; set; }
        public string Remarks { get; set; }
    }

    public class AgentRecruitFilter
    {        
        public long? AgentRecruitId { get; set; }
        public long? StatusId { get; set; }
        public string CreatedBy { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedDateFrom { get; set; }
        public DateTime? CreatedDateTo { get; set; }
        public long PageSize { get; set; }
        public long PageNumber { get; set; }
    }
}
