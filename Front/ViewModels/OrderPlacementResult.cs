namespace Front.ViewModels
{
    /// <summary>
    /// نتيجة محاولة تأكيد الطلب. ممكن يفشل (مثلاً منتج نفذ من المخزون
    /// بين ما ضافه المستخدم للسلة ولحظة الـ Checkout)، فبنرجع سبب واضح.
    /// </summary>
    public class OrderPlacementResult
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
        public int? OrderId { get; set; }

        public static OrderPlacementResult Fail(string message) => new() { Success = false, ErrorMessage = message };
        public static OrderPlacementResult Ok(int orderId) => new() { Success = true, OrderId = orderId };
    }
}
