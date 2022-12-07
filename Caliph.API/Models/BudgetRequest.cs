namespace Caliph.API.Models
{
    public class BudgetRequest
    {
        public long BudgetId { get; set; }
        public long UserId { get; set; }
        public long BudgetYear { get; set; }
        public long BudgetMonth { get; set; }
        public decimal TargetApptClosingRatio { get; set; }
        public decimal TargetApptCallRatio { get; set; }
        public decimal ProductPricePlan { get; set; }
        public long ProductStartMonth { get; set; }
        public long ProductQtyMonth1 { get; set; }
        public long ProductQtyMonth2 { get; set; }
        public long ProductQtyMonth3 { get; set; }
        public long ProductQtyMonth4 { get; set; }
        public long ProductQtyMonth5 { get; set; }
        public long ProductQtyMonth6 { get; set; }
        public long ProductQtyMonth7 { get; set; }
        public long ProductQtyMonth8 { get; set; }
        public long ProductQtyMonth9 { get; set; }
        public long ProductQtyMonth10 { get; set; }
        public long ProductQtyMonth11 { get; set; }
        public long ProductQtyMonth12 { get; set; }
        public long RecruitmentCount1 { get; set; }
        public long RecruitmentCount2 { get; set; }
        public long RecruitmentCount3 { get; set; }
        public long RecruitmentCount4 { get; set; }
        public long RecruitmentCount5 { get; set; }
        public long RecruitmentCount6 { get; set; }
        public long RecruitmentCount7 { get; set; }
        public long RecruitmentCount8 { get; set; }
        public long RecruitmentCount9 { get; set; }
        public long RecruitmentCount10 { get; set; }
        public long RecruitmentCount11 { get; set; }
        public long RecruitmentCount12 { get; set; }        
        public string Remarks { get; set; }
        public long StatusId { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class BudgetPropotionRequest
    {
        public long BudgetId { get; set; }
        public long BudgetProportionStartMonth { get; set; }
        public decimal BudgetProportionPercentage1 { get; set; }
        public decimal BudgetProportionPercentage2 { get; set; }
        public decimal BudgetProportionPercentage3 { get; set; }
        public decimal BudgetProportionPercentage4 { get; set; }
        public decimal BudgetProportionPercentage5 { get; set; }
        public decimal BudgetProportionPercentage6 { get; set; }
        public decimal BudgetProportionPercentage7 { get; set; }
        public decimal BudgetProportionPercentage8 { get; set; }
        public decimal BudgetProportionPercentage9 { get; set; }
        public decimal BudgetProportionPercentage10 { get; set; }
        public decimal BudgetProportionPercentage11 { get; set; }
        public decimal BudgetProportionPercentage12 { get; set; }
        public decimal BudgetProportionAmt1 { get; set; }
        public decimal BudgetProportionAmt2 { get; set; }
        public decimal BudgetProportionAmt3 { get; set; }
        public decimal BudgetProportionAmt4 { get; set; }
        public decimal BudgetProportionAmt5 { get; set; }
        public decimal BudgetProportionAmt6 { get; set; }
        public decimal BudgetProportionAmt7 { get; set; }
        public decimal BudgetProportionAmt8 { get; set; }
        public decimal BudgetProportionAmt9 { get; set; }
        public decimal BudgetProportionAmt10 { get; set; }
        public decimal BudgetProportionAmt11 { get; set; }
        public decimal BudgetProportionAmt12 { get; set; }
        public string UpdatedBy { get; set; }
    }
}