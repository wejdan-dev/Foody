using System.ComponentModel.DataAnnotations;

namespace Front.Models
{
    /// <summary>
    /// يمثل منتج واحد بالمتجر (خضار / فواكة / أعشاب).
    /// </summary>
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "اسم المنتج مطلوب")]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Range(0.01, 10000, ErrorMessage = "السعر لازم يكون أكبر من صفر")]
        public decimal Price { get; set; }

        /// <summary>
        /// اسم ملف الصورة فقط (مثلاً "tomato.jpg")، مش المسار الكامل.
        /// المسار الكامل بينبنى بالـ View حسب مكان تخزين الصور.
        /// </summary>
        [Required]
        public string ImageFileName { get; set; } = string.Empty;

        public ProductCategory Category { get; set; }

        /// <summary>
        /// الكمية المتوفرة بالمخزون. لو وصلت صفر، المنتج بيصير "غير متوفر" بالواجهة.
        /// </summary>
        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }

        public bool IsInStock => StockQuantity > 0;

        /// <summary>
        /// الأعشاب بتنباع بكميات صغيرة أصلاً، فحدّ "المخزون المنخفض" إلها أعلى
        /// (10) من الخضار والفواكة (5) اللي بتنباع بكميات/أوزان أكبر عادة.
        /// </summary>
        public int LowStockThreshold => Category == ProductCategory.Herb ? 10 : 5;

        public bool IsLowStock => IsInStock && StockQuantity < LowStockThreshold;
    }
}
