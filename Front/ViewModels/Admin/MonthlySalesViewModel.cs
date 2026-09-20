namespace Front.ViewModels.Admin
{
    public class MonthlySalesViewModel
    {
        public string MonthLabel { get; set; } = string.Empty;
        public int OrderCount { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
