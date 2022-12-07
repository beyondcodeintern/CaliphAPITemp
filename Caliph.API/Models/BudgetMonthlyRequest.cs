namespace Caliph.API.Models
{
    public class BudgetMonthlyRequest
    {
        public long BudgetMonthlyId { get; set; }
        public long UserId { get; set; }
        public long BudgetYear { get; set; }
        public long BudgetMonth { get; set; }
        public long MonthlyBudgetTypeId { get; set; }
        public decimal MonthlyBudgetPercentage { get; set; }
        public long NoOfCases { get; set; }
        public string PersonName { get; set; }
        public long ClientId { get; set; }
        public decimal BudgetValue { get; set; }
        public decimal AchieveValue { get; set; }
        public string Remarks { get; set; }
        public long StatusId { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}