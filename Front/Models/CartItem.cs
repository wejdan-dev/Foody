using System.ComponentModel.DataAnnotations;

namespace Front.Models
{
    /// <summary>
    /// عنصر واحد بسلة مستخدم معين. بنخزن بس ProductId والكمية - بيانات المنتج
    /// (الاسم، السعر، الصورة) بنجيبها وقت العرض من IProductCatalogService،
    /// حتى ما نكرر البيانات بمكانين.
    /// </summary>
    public class CartItem
    {
        public int Id { get; set; }

        [Required]
        public string ApplicationUserId { get; set; } = string.Empty;

        public int ProductId { get; set; }

        [Range(1, 999)]
        public int Quantity { get; set; }
    }
}
