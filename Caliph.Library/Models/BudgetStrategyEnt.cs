namespace Caliph.Library.Models
{
    public class BudgetStrategyEnt : BaseEnt
    {
        public long BudgetStrategyId { get; set; }
        public long UserId { get; set; }
        public string Username { get; set; }
        public long BudgetStrategyYear { get; set; }
        public long NoOfCasesForTheYear { get; set; }
        public decimal GoalACEValue { get; set; }
        public decimal HighEndPercentage { get; set; }
        public decimal LowEndPercentage { get; set; }
        public decimal HighEndACEValue { get; set; }
        public decimal LowEndACEValue { get; set; }
        public decimal HighEndAveragePremium { get; set; }
        public decimal LowEndAveragePremium { get; set; }
        public long HighEndNoOfCases { get; set; }
        public long LowEndNoOfCases { get; set; }
        public string Remarks { get; set; }
        public long StatusId { get; set; }
    }

    public class BudgetStrategyFilter
    {
        public long? BudgetStrategyId { get; set; }
        public long? BudgetStrategyYear { get; set; }
        public long? UserId { get; set; }
        public long? StatusId { get; set; }
        public long PageSize { get; set; }
        public long PageNumber { get; set; }
    }
}
