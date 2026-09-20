using Front.Data;
using Front.Helpers;
using Front.Models;
using Front.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Front.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _db;

        public OrderService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<OrderSummaryViewModel>> GetOrdersForAdminAsync(OrderStatus? status = null)
        {
            var query = _db.Orders.AsNoTracking();

            if (status.HasValue)
            {
                query = query.Where(o => o.Status == status.Value);
            }

            var orders = await query
                .OrderByDescending(o => o.OrderDate)
                .Select(o => new
                {
                    o.Id,
                    o.OrderDate,
                    o.TotalAmount,
                    o.Status,
                    CustomerName = _db.Users.Where(u => u.Id == o.ApplicationUserId).Select(u => u.FullName).FirstOrDefault(),
                    CustomerEmail = _db.Users.Where(u => u.Id == o.ApplicationUserId).Select(u => u.Email).FirstOrDefault()
                })
                .ToListAsync();

            return orders.Select(o => new OrderSummaryViewModel
            {
                OrderId = o.Id,
                OrderDate = o.OrderDate,
                TotalAmount = o.TotalAmount,
                Status = o.Status,
                StatusText = o.Status.ToDisplayText(),
                StatusMessage = o.Status.ToCustomerMessage(),
                CustomerName = string.IsNullOrWhiteSpace(o.CustomerName) ? "Customer" : o.CustomerName!,
                CustomerEmail = o.CustomerEmail ?? string.Empty
            }).ToList();
        }

        public async Task<OrderDetailsViewModel?> GetOrderDetailsForAdminAsync(int orderId)
        {
            var order = await _db.Orders
                .AsNoTracking()
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order is null)
            {
                return null;
            }

            var customer = await _db.Users
                .AsNoTracking()
                .Where(u => u.Id == order.ApplicationUserId)
                .Select(u => new { u.FullName, u.Email })
                .FirstOrDefaultAsync();

            return new OrderDetailsViewModel
            {
                OrderId = order.Id,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                StatusText = order.Status.ToDisplayText(),
                StatusMessage = order.Status.ToCustomerMessage(),
                CustomerName = string.IsNullOrWhiteSpace(customer?.FullName) ? "Customer" : customer!.FullName,
                CustomerEmail = customer?.Email ?? string.Empty,
                Items = order.Items.Select(i => new OrderItemViewModel
                {
                    ProductName = i.ProductName,
                    UnitPrice = i.UnitPrice,
                    Quantity = i.Quantity
                }).ToList()
            };
        }

        public async Task<List<OrderSummaryViewModel>> GetOrdersForCustomerAsync(string userId)
        {
            var customer = await _db.Users
                .AsNoTracking()
                .Where(u => u.Id == userId)
                .Select(u => new { u.FullName, u.Email })
                .FirstOrDefaultAsync();

            var orders = await _db.Orders
                .AsNoTracking()
                .Where(o => o.ApplicationUserId == userId)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return orders.Select(o => new OrderSummaryViewModel
            {
                OrderId = o.Id,
                OrderDate = o.OrderDate,
                TotalAmount = o.TotalAmount,
                Status = o.Status,
                StatusText = o.Status.ToDisplayText(),
                StatusMessage = o.Status.ToCustomerMessage(),
                CustomerName = string.IsNullOrWhiteSpace(customer?.FullName) ? "Customer" : customer!.FullName,
                CustomerEmail = customer?.Email ?? string.Empty
            }).ToList();
        }

        public async Task<OrderDetailsViewModel?> GetOrderDetailsForCustomerAsync(string userId, int orderId)
        {
            var order = await _db.Orders
                .AsNoTracking()
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == orderId && o.ApplicationUserId == userId);

            if (order is null)
            {
                return null;
            }

            var customer = await _db.Users
                .AsNoTracking()
                .Where(u => u.Id == userId)
                .Select(u => new { u.FullName, u.Email })
                .FirstOrDefaultAsync();

            return new OrderDetailsViewModel
            {
                OrderId = order.Id,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                StatusText = order.Status.ToDisplayText(),
                StatusMessage = order.Status.ToCustomerMessage(),
                CustomerName = string.IsNullOrWhiteSpace(customer?.FullName) ? "Customer" : customer!.FullName,
                CustomerEmail = customer?.Email ?? string.Empty,
                Items = order.Items.Select(i => new OrderItemViewModel
                {
                    ProductName = i.ProductName,
                    UnitPrice = i.UnitPrice,
                    Quantity = i.Quantity
                }).ToList()
            };
        }

        public async Task<bool> UpdateStatusAsync(int orderId, OrderStatus status)
        {
            var order = await _db.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
            if (order is null)
            {
                return false;
            }

            order.Status = status;
            await _db.SaveChangesAsync();
            return true;
        }

        public Task<int> GetTotalOrdersCountAsync() => _db.Orders.CountAsync();
        public Task<int> GetPendingOrdersCountAsync() => _db.Orders.CountAsync(o => o.Status == OrderStatus.Pending);
        public Task<int> GetProcessingOrdersCountAsync() => _db.Orders.CountAsync(o => o.Status == OrderStatus.Processing);
        public Task<int> GetApprovedOrdersCountAsync() => _db.Orders.CountAsync(o => o.Status == OrderStatus.Approved);
        public Task<int> GetRejectedOrdersCountAsync() => _db.Orders.CountAsync(o => o.Status == OrderStatus.Rejected);

        public async Task<List<OrderSummaryViewModel>> GetRecentOrdersAsync(int count = 5)
        {
            return await GetOrdersForAdminAsync(null)
                .ContinueWith(task => task.Result.Take(count).ToList());
        }
    }
}
