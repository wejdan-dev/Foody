using System.Security.Claims;
using Front.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Front.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;

        public CartController(ICartService cartService, IOrderService orderService)
        {
            _cartService = cartService;
            _orderService = orderService;
        }

        private string CurrentUserId => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        public async Task<IActionResult> Index()
        {
            var items = await _cartService.GetCartItemsAsync(CurrentUserId);
            return View(items);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart(int productId, int quantity = 1, string? returnUrl = null)
        {
            await _cartService.AddToCartAsync(CurrentUserId, productId, quantity);
            TempData["CartMessage"] = "Item added to your cart.";

            if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return LocalRedirect(returnUrl);
            }

            var referer = Request.Headers.Referer.ToString();
            if (!string.IsNullOrWhiteSpace(referer) && Uri.TryCreate(referer, UriKind.Absolute, out var refererUri))
            {
                var localReferer = refererUri.PathAndQuery + refererUri.Fragment;
                if (Url.IsLocalUrl(localReferer))
                {
                    return LocalRedirect(localReferer);
                }
            }

            return RedirectToAction("Index", "Products");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateQuantity(int cartItemId, int quantity)
        {
            await _cartService.UpdateQuantityAsync(CurrentUserId, cartItemId, quantity);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveItem(int cartItemId)
        {
            await _cartService.RemoveItemAsync(CurrentUserId, cartItemId);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Checkout()
        {
            var items = await _cartService.GetCartItemsAsync(CurrentUserId);
            if (items.Count == 0)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(items);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PlaceOrder()
        {
            var result = await _cartService.PlaceOrderAsync(CurrentUserId);

            if (!result.Success)
            {
                TempData["CartError"] = result.ErrorMessage;
                return RedirectToAction(nameof(Checkout));
            }

            return RedirectToAction(nameof(OrderConfirmation), new { orderId = result.OrderId });
        }

        public IActionResult OrderConfirmation(int orderId)
        {
            ViewBag.OrderId = orderId;
            return View();
        }

        public async Task<IActionResult> MyOrders()
        {
            var orders = await _orderService.GetOrdersForCustomerAsync(CurrentUserId);
            return View(orders);
        }

        public async Task<IActionResult> OrderDetails(int id)
        {
            var order = await _orderService.GetOrderDetailsForCustomerAsync(CurrentUserId, id);
            if (order is null)
            {
                return NotFound();
            }

            return View(order);
        }
    }
}
