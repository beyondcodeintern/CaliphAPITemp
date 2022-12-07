namespace Caliph.Library.Models
{
    public class AgentRecruitTrackEnt : BaseEnt
    {
        public long AgentRecruitTrackId { get; set; }
        public long AgentRecruitId { get; set; }
        public string TrackRemarks { get; set; }
    }

    public class AgentRecruitTrackFilter
    {
        public long? AgentRecruitTrackId { get; set; }
        public long? AgentRecruitId { get; set; }
        public string CreatedBy { get; set; }
        public long PageSize { get; set; }
        public long PageNumber { get; set; }
    }
}
