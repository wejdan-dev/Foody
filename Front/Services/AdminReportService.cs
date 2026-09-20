using Front.Data;
using Front.Models;
using Front.ViewModels.Admin;
using Microsoft.EntityFrameworkCore;

namespace Front.Services
{
    public class AdminReportService : IAdminReportService
    {
        private readonly ApplicationDbContext _db;

        public AdminReportService(ApplicationDbContext db)
        {
            _db = db;
        }

        private IQueryable<Order> ApprovedOrders => _db.Orders.Where(o => o.Status == OrderStatus.Approved);

        public async Task<decimal> GetTotalRevenueAsync()
        {
            return await ApprovedOrders.SumAsync(o => (decimal?)o.TotalAmount) ?? 0;
        }

        public async Task<int> GetTotalOrdersAsync()
        {
            return await _db.Orders.CountAsync();
        }

        public async Task<List<MonthlySalesViewModel>> GetMonthlySalesAsync(int monthsBack = 6)
        {
            var cutoff = DateTime.UtcNow.AddMonths(-monthsBack);

            var raw = await ApprovedOrders
                .Where(o => o.OrderDate >= cutoff)
                .GroupBy(o => new { o.OrderDate.Year, o.OrderDate.Month })
                .Select(g => new
                {
                    g.Key.Year,
                    g.Key.Month,
                    OrderCount = g.Count(),
                    TotalRevenue = g.Sum(o => o.TotalAmount)
                })
                .OrderBy(g => g.Year)
                .ThenBy(g => g.Month)
                .ToListAsync();

            return raw.Select(r => new MonthlySalesViewModel
            {
                MonthLabel = new DateTime(r.Year, r.Month, 1).ToString("MMM yyyy"),
                OrderCount = r.OrderCount,
                TotalRevenue = r.TotalRevenue
            }).ToList();
        }

        public async Task<List<TopProductViewModel>> GetTopProductsAsync(int count = 5)
        {
            var approvedOrderIds = ApprovedOrders.Select(o => o.Id);

            var raw = await _db.OrderItems
                .Where(oi => approvedOrderIds.Contains(oi.OrderId))
                .GroupBy(oi => oi.ProductName)
                .Select(g => new
                {
                    ProductName = g.Key,
                    TotalQuantitySold = g.Sum(oi => oi.Quantity),
                    TotalRevenue = g.Sum(oi => oi.UnitPrice * oi.Quantity)
                })
                .OrderByDescending(g => g.TotalQuantitySold)
                .Take(count)
                .ToListAsync();

            return raw.Select(r => new TopProductViewModel
            {
                ProductName = r.ProductName,
                TotalQuantitySold = r.TotalQuantitySold,
                TotalRevenue = r.TotalRevenue
            }).ToList();
        }
    }
}
