namespace Front.Models
{
    /// <summary>
    /// طلب حقيقي بينحفظ بقاعدة البيانات وقت ما المستخدم يأكد الشراء (Checkout).
    /// </summary>
    public class Order
    {
        public int Id { get; set; }

        public string ApplicationUserId { get; set; } = string.Empty;

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public decimal TotalAmount { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public List<OrderItem> Items { get; set; } = new();
    }
}
