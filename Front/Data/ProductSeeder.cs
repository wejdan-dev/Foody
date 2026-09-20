using Front.Data;
using Front.Models;
using Microsoft.EntityFrameworkCore;

namespace Front.Data
{
    /// <summary>
    /// بيعبّي جدول Products بالبيانات الأصلية من تصميم Foody، بس أول مرة
    /// (لو الجدول فاضي). بينادى عليه مرة وحدة وقت تشغيل التطبيق، بنفس
    /// أسلوب IdentitySeeder.
    /// </summary>
    public static class ProductSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext db)
        {
            if (await db.Products.AnyAsync())
            {
                return; // فيه منتجات أصلاً، ما في داعي نزرع من جديد.
            }

            var products = new List<Product>
            {
                // Vegetables
                new() { Name = "Fresh Tomato", Price = 3.00m, ImageFileName = "tomato.jpg", Category = ProductCategory.Vegetable, StockQuantity = 40 },
                new() { Name = "Romano Pepper", Price = 2.50m, ImageFileName = "Romanopepper.jpg", Category = ProductCategory.Vegetable, StockQuantity = 35 },
                new() { Name = "Potato", Price = 3.50m, ImageFileName = "potato.png", Category = ProductCategory.Vegetable, StockQuantity = 60 },
                new() { Name = "Onion", Price = 2.00m, ImageFileName = "Onion.jpg", Category = ProductCategory.Vegetable, StockQuantity = 50 },
                new() { Name = "Garlic", Price = 5.00m, ImageFileName = "Garlic.jpg", Category = ProductCategory.Vegetable, StockQuantity = 25 },
                new() { Name = "Carrot", Price = 3.50m, ImageFileName = "carrot.jpg", Category = ProductCategory.Vegetable, StockQuantity = 45 },
                new() { Name = "Courgettes", Price = 4.00m, ImageFileName = "courgettes.jpg", Category = ProductCategory.Vegetable, StockQuantity = 20 },
                new() { Name = "Cauliflower", Price = 3.00m, ImageFileName = "cauliflower.png", Category = ProductCategory.Vegetable, StockQuantity = 0 },

                // Fruits
                new() { Name = "Apple", Price = 3.00m, ImageFileName = "Apple.jpg", Category = ProductCategory.Fruit, StockQuantity = 55 },
                new() { Name = "Avocado", Price = 6.00m, ImageFileName = "Avocado.jpg", Category = ProductCategory.Fruit, StockQuantity = 15 },
                new() { Name = "Bananas", Price = 3.50m, ImageFileName = "Bananas.jpg", Category = ProductCategory.Fruit, StockQuantity = 60 },
                new() { Name = "Black Grapes", Price = 8.00m, ImageFileName = "BlackGrapes.jpg", Category = ProductCategory.Fruit, StockQuantity = 18 },
                new() { Name = "Orange", Price = 2.50m, ImageFileName = "Orange.jpg", Category = ProductCategory.Fruit, StockQuantity = 40 },
                new() { Name = "Kiwi", Price = 7.00m, ImageFileName = "Kiwi.jpg", Category = ProductCategory.Fruit, StockQuantity = 22 },
                new() { Name = "Pears", Price = 6.50m, ImageFileName = "Pears.jpg", Category = ProductCategory.Fruit, StockQuantity = 0 },
                new() { Name = "Clementines", Price = 4.00m, ImageFileName = "Clementines.jpg", Category = ProductCategory.Fruit, StockQuantity = 30 },

                // Herbs
                new() { Name = "Bay Leaves", Price = 8.50m, ImageFileName = "BayLeaves.jpg", Category = ProductCategory.Herb, StockQuantity = 12 },
                new() { Name = "Coriander", Price = 1.00m, ImageFileName = "Coriander.jpg", Category = ProductCategory.Herb, StockQuantity = 70 },
                new() { Name = "Flat Parsley", Price = 1.00m, ImageFileName = "FlatParsley.jpg", Category = ProductCategory.Herb, StockQuantity = 65 },
                new() { Name = "Garlic Chives", Price = 3.00m, ImageFileName = "GarlicChives.jpg", Category = ProductCategory.Herb, StockQuantity = 20 },
                new() { Name = "Mint", Price = 1.50m, ImageFileName = "Mint.jpg", Category = ProductCategory.Herb, StockQuantity = 80 },
                new() { Name = "Rosemary", Price = 5.00m, ImageFileName = "Rosemary.jpg", Category = ProductCategory.Herb, StockQuantity = 0 },
                new() { Name = "Sage", Price = 4.50m, ImageFileName = "Sage.jpg", Category = ProductCategory.Herb, StockQuantity = 14 },
                new() { Name = "Thyme", Price = 9.50m, ImageFileName = "Thyme.jpg", Category = ProductCategory.Herb, StockQuantity = 10 },
            };

            db.Products.AddRange(products);
            await db.SaveChangesAsync();
        }
    }
}
