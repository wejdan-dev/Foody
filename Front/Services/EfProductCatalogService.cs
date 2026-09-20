using Front.Data;
using Front.Models;
using Microsoft.EntityFrameworkCore;

namespace Front.Services
{
    /// <summary>
    /// النسخة الحقيقية من كتالوج المنتجات - كل شي هون بينحفظ فعلياً بقاعدة
    /// البيانات (جدول Products)، فلو الأدمن ضاف أو حذف منتج، التغيير بيضل
    /// موجود حتى لو انطفى السيرفر. هاي حلت مكان InMemoryProductCatalogService
    /// بدون ما نغيّر أي Controller أو View - لأن الكل بيتعامل مع
    /// IProductCatalogService بس (Dependency Inversion Principle).
    /// </summary>
    public class EfProductCatalogService : IProductCatalogService
    {
        private readonly ApplicationDbContext _db;

        public EfProductCatalogService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _db.Products.OrderBy(p => p.Category).ThenBy(p => p.Name).ToListAsync();
        }

        public async Task<List<Product>> GetByCategoryAsync(ProductCategory category)
        {
            return await _db.Products.Where(p => p.Category == category).OrderBy(p => p.Name).ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _db.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> AddAsync(Product product)
        {
            _db.Products.Add(product);
            await _db.SaveChangesAsync();
            return product;
        }

        public async Task<bool> UpdateAsync(Product product)
        {
            var existing = await _db.Products.FirstOrDefaultAsync(p => p.Id == product.Id);
            if (existing is null)
            {
                return false;
            }

            existing.Name = product.Name;
            existing.Price = product.Price;
            existing.ImageFileName = product.ImageFileName;
            existing.Category = product.Category;
            existing.StockQuantity = product.StockQuantity;

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _db.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (existing is null)
            {
                return false;
            }

            _db.Products.Remove(existing);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> TryDecrementStockAsync(int productId, int quantity)
        {
            var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == productId);
            if (product is null || product.StockQuantity < quantity)
            {
                return false;
            }

            product.StockQuantity -= quantity;
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> IncreaseStockAsync(int productId, int quantity)
        {
            var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == productId);
            if (product is null || quantity <= 0)
            {
                return false;
            }

            product.StockQuantity += quantity;
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
