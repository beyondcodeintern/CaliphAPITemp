namespace Caliph.API.Models
{
    public class AgentRecruitRequest
    {
        public long AgentRecruitId { get; set; }
        public string Name { get; set; }
        public string ContactNo { get; set; }
        public long StatusId { get; set; }
        public string StatusDesc { get; set; }
        public long EducationBgId { get; set; }
        public long AgeId { get; set; }
        public string EmailAdd { get; set; }
        public long AnnualIncomeId { get; set; }
        public long OccupationId { get; set; }
        public long MaritalId { get; set; }
        public long TypeId { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}