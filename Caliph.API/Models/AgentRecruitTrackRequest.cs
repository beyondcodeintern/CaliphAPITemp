namespace Caliph.API.Models
{
    public class AgentRecruitTrackRequest
    {
        public long AgentRecruitTrackId { get; set; }
        public long AgentRecruitId { get; set; }
        public string TrackRemarks { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}