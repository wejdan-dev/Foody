namespace Front.ViewModels
{
    /// <summary>
    /// شكل مدموج لعرض السلة بالـ View: بيانات CartItem (الكمية) + بيانات
    /// Product (الاسم، السعر، الصورة) سوا. مافي داعي الـ View يعرف مصدرين
    /// مختلفين للبيانات.
    /// </summary>
    public class CartItemViewModel
    {
        public int CartItemId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ImageFileName { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int AvailableStock { get; set; }

        public decimal LineTotal => UnitPrice * Quantity;
    }
}
