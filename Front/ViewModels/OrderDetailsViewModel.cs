using Front.Models;

namespace Front.ViewModels
{
    public class OrderDetailsViewModel
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public string StatusText { get; set; } = string.Empty;
        public string StatusMessage { get; set; } = string.Empty;
        public List<OrderItemViewModel> Items { get; set; } = new();
    }
}
