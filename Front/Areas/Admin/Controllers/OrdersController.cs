using Front.Models;
using Front.Services;
using Front.ViewModels.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Front.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orders;

        public OrdersController(IOrderService orders)
        {
            _orders = orders;
        }

        public async Task<IActionResult> Index(string filter = "all")
        {
            OrderStatus? status = filter.ToLowerInvariant() switch
            {
                "pending" => OrderStatus.Pending,
                "processing" => OrderStatus.Processing,
                "approved" => OrderStatus.Approved,
                "rejected" => OrderStatus.Rejected,
                _ => null
            };

            var model = new AdminOrdersIndexViewModel
            {
                CurrentFilter = filter.ToLowerInvariant(),
                Orders = await _orders.GetOrdersForAdminAsync(status),
                AllCount = await _orders.GetTotalOrdersCountAsync(),
                PendingCount = await _orders.GetPendingOrdersCountAsync(),
                ProcessingCount = await _orders.GetProcessingOrdersCountAsync(),
                ApprovedCount = await _orders.GetApprovedOrdersCountAsync(),
                RejectedCount = await _orders.GetRejectedOrdersCountAsync()
            };

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var order = await _orders.GetOrderDetailsForAdminAsync(id);
            if (order is null)
            {
                return NotFound();
            }

            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkProcessing(int id)
        {
            var updated = await _orders.UpdateStatusAsync(id, OrderStatus.Processing);
            if (!updated)
            {
                return NotFound();
            }

            TempData["AdminMessage"] = "Order moved to Processing / قيد التجهيز.";
            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(int id)
        {
            var updated = await _orders.UpdateStatusAsync(id, OrderStatus.Approved);
            if (!updated)
            {
                return NotFound();
            }

            TempData["AdminMessage"] = "Order marked as Approved / تم الشحن.";
            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reject(int id)
        {
            var updated = await _orders.UpdateStatusAsync(id, OrderStatus.Rejected);
            if (!updated)
            {
                return NotFound();
            }

            TempData["AdminMessage"] = "Order marked as Rejected / مرفوض.";
            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
