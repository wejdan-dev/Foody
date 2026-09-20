namespace Front.Models
{
    /// <summary>
    /// عنصر واحد جوا طلب. بنحفظ اسم المنتج وسعره "وقت الشراء" (Snapshot) بدل ما
    /// نعتمد بس على ProductId - هيك لو تغيّر سعر المنتج أو حتى انحذف من الكتالوج
    /// بعدين، الفاتورة القديمة تضل صحيحة ومطابقة لللي دفعه الزبون فعلياً.
    /// هاي ممارسة أساسية بأي نظام تجارة إلكترونية حقيقي.
    /// </summary>
    public class OrderItem
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        public decimal LineTotal => UnitPrice * Quantity;
    }
}
