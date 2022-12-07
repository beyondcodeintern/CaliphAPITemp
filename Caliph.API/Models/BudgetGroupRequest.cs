namespace Caliph.API.Models
{
    public class BudgetGroupRequest
    {
        public long BudgetGroupId { get; set; }
        public long BudgetId { get; set; }
        public string BudgetTitle { get; set; }
        public long UserId { get; set; }
        public long TargetCount { get; set; }
        public decimal TargetComm { get; set; }
        public long TotalCase { get; set; }
        public string Remarks { get; set; }
        public long StatusId { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}