using Front.Data;
using Front.Models;
using Front.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Front.Services
{
    public class CartService : ICartService
    {
        private readonly ApplicationDbContext _db;
        private readonly IProductCatalogService _catalog;

        public CartService(ApplicationDbContext db, IProductCatalogService catalog)
        {
            _db = db;
            _catalog = catalog;
        }

        public async Task<List<CartItemViewModel>> GetCartItemsAsync(string userId)
        {
            var items = await _db.CartItems
                .Where(c => c.ApplicationUserId == userId)
                .ToListAsync();

            var result = new List<CartItemViewModel>();

            foreach (var item in items)
            {
                var product = await _catalog.GetByIdAsync(item.ProductId);
                if (product is null)
                {
                    continue;
                }

                result.Add(new CartItemViewModel
                {
                    CartItemId = item.Id,
                    ProductId = product.Id,
                    ProductName = product.Name,
                    ImageFileName = product.ImageFileName,
                    UnitPrice = product.Price,
                    Quantity = item.Quantity,
                    AvailableStock = product.StockQuantity
                });
            }

            return result;
        }

        public async Task<int> GetCartItemCountAsync(string userId)
        {
            return await _db.CartItems
                .Where(c => c.ApplicationUserId == userId)
                .SumAsync(c => (int?)c.Quantity) ?? 0;
        }

        public async Task<decimal> GetCartTotalAsync(string userId)
        {
            var items = await GetCartItemsAsync(userId);
            return items.Sum(i => i.LineTotal);
        }

        public async Task AddToCartAsync(string userId, int productId, int quantity)
        {
            if (quantity < 1)
            {
                quantity = 1;
            }

            var existing = await _db.CartItems
                .FirstOrDefaultAsync(c => c.ApplicationUserId == userId && c.ProductId == productId);

            if (existing is not null)
            {
                existing.Quantity += quantity;
            }
            else
            {
                _db.CartItems.Add(new CartItem
                {
                    ApplicationUserId = userId,
                    ProductId = productId,
                    Quantity = quantity
                });
            }

            await _db.SaveChangesAsync();
        }

        public async Task UpdateQuantityAsync(string userId, int cartItemId, int quantity)
        {
            var item = await _db.CartItems
                .FirstOrDefaultAsync(c => c.Id == cartItemId && c.ApplicationUserId == userId);

            if (item is null)
            {
                return;
            }

            if (quantity < 1)
            {
                _db.CartItems.Remove(item);
            }
            else
            {
                item.Quantity = quantity;
            }

            await _db.SaveChangesAsync();
        }

        public async Task RemoveItemAsync(string userId, int cartItemId)
        {
            var item = await _db.CartItems
                .FirstOrDefaultAsync(c => c.Id == cartItemId && c.ApplicationUserId == userId);

            if (item is not null)
            {
                _db.CartItems.Remove(item);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<OrderPlacementResult> PlaceOrderAsync(string userId)
        {
            var cartItems = await _db.CartItems
                .Where(c => c.ApplicationUserId == userId)
                .ToListAsync();

            if (cartItems.Count == 0)
            {
                return OrderPlacementResult.Fail("Your cart is empty.");
            }

            var order = new Order
            {
                ApplicationUserId = userId,
                OrderDate = DateTime.UtcNow,
                Status = OrderStatus.Pending
            };

            var productsSnapshot = new List<(Product Product, int Quantity)>();

            foreach (var cartItem in cartItems)
            {
                var product = await _catalog.GetByIdAsync(cartItem.ProductId);
                if (product is null)
                {
                    return OrderPlacementResult.Fail("A product in your cart is no longer available.");
                }

                if (product.StockQuantity < cartItem.Quantity)
                {
                    return OrderPlacementResult.Fail($"Sorry, '{product.Name}' only has {product.StockQuantity} left in stock.");
                }

                productsSnapshot.Add((product, cartItem.Quantity));
            }

            decimal total = 0;

            foreach (var (product, quantity) in productsSnapshot)
            {
                await _catalog.TryDecrementStockAsync(product.Id, quantity);

                var lineTotal = product.Price * quantity;
                total += lineTotal;

                order.Items.Add(new OrderItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    UnitPrice = product.Price,
                    Quantity = quantity
                });
            }

            order.TotalAmount = total;

            _db.Orders.Add(order);
            _db.CartItems.RemoveRange(cartItems);
            await _db.SaveChangesAsync();

            return OrderPlacementResult.Ok(order.Id);
        }
    }
}
