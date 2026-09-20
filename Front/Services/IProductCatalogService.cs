using Front.Models;

namespace Front.Services
{
    /// <summary>
    /// كل ما يخص قراءة/إدارة المنتجات بيمر من هالواجهة.
    /// الـ Controllers بتتعامل مع هاي الواجهة بس، مش مع طريقة التخزين نفسها.
    /// هيك لما نربط قاعدة بيانات حقيقية (EF Core) بمرحلة جاية، بنبدل التطبيق (Implementation)
    /// بس، وما لازم نلمس ولا Controller.
    /// </summary>
    public interface IProductCatalogService
    {
        Task<List<Product>> GetAllAsync();

        Task<List<Product>> GetByCategoryAsync(ProductCategory category);

        Task<Product?> GetByIdAsync(int id);

        Task<Product> AddAsync(Product product);

        Task<bool> UpdateAsync(Product product);

        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// بينقص الكمية المطلوبة من المخزون وقت تأكيد الطلب.
        /// بيرجع false لو الكمية المتوفرة أقل من المطلوب (حتى ما ننقص لتحت الصفر).
        /// </summary>
        Task<bool> TryDecrementStockAsync(int productId, int quantity);

        /// <summary>
        /// بيزيد الكمية بالمخزون - يستخدمها الأدمن لما توصل بضاعة جديدة.
        /// </summary>
        Task<bool> IncreaseStockAsync(int productId, int quantity);
    }
}
