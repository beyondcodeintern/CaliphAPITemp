namespace Caliph.Library.Models
{
    public class BudgetMonthlyEnt : BaseEnt
    {
        public long BudgetMonthlyId { get; set; }
        public long UserId { get; set; }
        public string Username { get; set; }
        public long BudgetYear { get; set; }
        public long BudgetMonth { get; set; }
        public long MonthlyBudgetTypeId { get; set; }
        public string MonthlyBudgetTypeDesc { get; set; }
        public decimal MonthlyBudgetPercentage { get; set; }
        public long NoOfCases { get; set; }
        public long ClientId { get; set; }
        public string ClientName { get; set; }
        public string PersonName { get; set; }
        public decimal BudgetValue { get; set; }
        public decimal AchieveValue { get; set; }
        public string Remarks { get; set; }
        public long StatusId { get; set; }
    }

    public class BudgetMonthlyFilter
    {
        public long? BudgetMonthlyId { get; set; }
        public long? BudgetYear { get; set; }
        public long? BudgetMonth { get; set; }
        public long? UserId { get; set; }
        public long? MonthlyBudgetTypeId { get; set; }
        public long? StatusId { get; set; }
        public long PageSize { get; set; }
        public long PageNumber { get; set; }
    }
}
