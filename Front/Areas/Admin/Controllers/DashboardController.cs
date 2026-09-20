using Front.Services;
using Front.ViewModels.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Front.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly IAdminReportService _reports;
        private readonly IProductCatalogService _catalog;
        private readonly IOrderService _orders;

        public DashboardController(
            IAdminReportService reports,
            IProductCatalogService catalog,
            IOrderService orders)
        {
            _reports = reports;
            _catalog = catalog;
            _orders = orders;
        }

        public async Task<IActionResult> Index()
        {
            var allProducts = await _catalog.GetAllAsync();

            var model = new DashboardViewModel
            {
                TotalRevenue = await _reports.GetTotalRevenueAsync(),
                TotalOrders = await _orders.GetTotalOrdersCountAsync(),
                PendingOrders = await _orders.GetPendingOrdersCountAsync(),
                ProcessingOrders = await _orders.GetProcessingOrdersCountAsync(),
                ApprovedOrders = await _orders.GetApprovedOrdersCountAsync(),
                RejectedOrders = await _orders.GetRejectedOrdersCountAsync(),
                MonthlySales = await _reports.GetMonthlySalesAsync(),
                TopProducts = await _reports.GetTopProductsAsync(),
                LowStockProducts = allProducts.Where(p => p.IsLowStock || !p.IsInStock).ToList(),
                RecentOrders = await _orders.GetRecentOrdersAsync(6)
            };

            return View(model);
        }
    }
}
