using Front.ViewModels;

namespace Front.Services
{
    public interface ICartService
    {
        Task<List<CartItemViewModel>> GetCartItemsAsync(string userId);
        Task<int> GetCartItemCountAsync(string userId);
        Task<decimal> GetCartTotalAsync(string userId);
        Task AddToCartAsync(string userId, int productId, int quantity);
        Task UpdateQuantityAsync(string userId, int cartItemId, int quantity);
        Task RemoveItemAsync(string userId, int cartItemId);
        Task<OrderPlacementResult> PlaceOrderAsync(string userId);
    }
}
