using Front.Models;
using Front.ViewModels;

namespace Front.Services
{
    public interface IOrderService
    {
        Task<List<OrderSummaryViewModel>> GetOrdersForAdminAsync(OrderStatus? status = null);
        Task<OrderDetailsViewModel?> GetOrderDetailsForAdminAsync(int orderId);
        Task<List<OrderSummaryViewModel>> GetOrdersForCustomerAsync(string userId);
        Task<OrderDetailsViewModel?> GetOrderDetailsForCustomerAsync(string userId, int orderId);
        Task<bool> UpdateStatusAsync(int orderId, OrderStatus status);
        Task<int> GetTotalOrdersCountAsync();
        Task<int> GetPendingOrdersCountAsync();
        Task<int> GetProcessingOrdersCountAsync();
        Task<int> GetApprovedOrdersCountAsync();
        Task<int> GetRejectedOrdersCountAsync();
        Task<List<OrderSummaryViewModel>> GetRecentOrdersAsync(int count = 5);
    }
}
