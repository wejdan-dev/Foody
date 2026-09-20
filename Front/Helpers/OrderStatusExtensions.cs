using Front.Models;

namespace Front.Helpers
{
    public static class OrderStatusExtensions
    {
        public static string ToDisplayText(this OrderStatus status) => status switch
        {
            OrderStatus.Pending => "Pending Review / بانتظار المراجعة",
            OrderStatus.Processing => "Processing / قيد التجهيز",
            OrderStatus.Approved => "Approved / تم الشحن",
            OrderStatus.Rejected => "Rejected / مرفوض",
            _ => status.ToString()
        };

        public static string ToCustomerMessage(this OrderStatus status) => status switch
        {
            OrderStatus.Pending => "طلبك وصلنا وبانتظار مراجعة الأدمن.",
            OrderStatus.Processing => "طلبك قيد التجهيز حالياً.",
            OrderStatus.Approved => "تم شحن طلبك وهو في الطريق إليك.",
            OrderStatus.Rejected => "تم رفض الطلب. تواصل مع الدعم إذا كنت تحتاج تفاصيل إضافية.",
            _ => string.Empty
        };

        public static string ToBadgeClass(this OrderStatus status) => status switch
        {
            OrderStatus.Pending => "bg-secondary",
            OrderStatus.Processing => "bg-warning text-dark",
            OrderStatus.Approved => "bg-success",
            OrderStatus.Rejected => "bg-danger",
            _ => "bg-secondary"
        };
    }
}
