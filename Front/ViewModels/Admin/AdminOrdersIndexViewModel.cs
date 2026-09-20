using Front.ViewModels;

namespace Front.ViewModels.Admin
{
    public class AdminOrdersIndexViewModel
    {
        public string CurrentFilter { get; set; } = "all";
        public int AllCount { get; set; }
        public int PendingCount { get; set; }
        public int ProcessingCount { get; set; }
        public int ApprovedCount { get; set; }
        public int RejectedCount { get; set; }
        public List<OrderSummaryViewModel> Orders { get; set; } = new();
    }
}
