using Front.Models;
using Front.ViewModels;

namespace Front.ViewModels.Admin
{
    public class DashboardViewModel
    {
        public decimal TotalRevenue { get; set; }
        public int TotalOrders { get; set; }
        public int PendingOrders { get; set; }
        public int ProcessingOrders { get; set; }
        public int ApprovedOrders { get; set; }
        public int RejectedOrders { get; set; }
        public List<MonthlySalesViewModel> MonthlySales { get; set; } = new();
        public List<TopProductViewModel> TopProducts { get; set; } = new();
        public List<Product> LowStockProducts { get; set; } = new();
        public List<OrderSummaryViewModel> RecentOrders { get; set; } = new();
    }
}
