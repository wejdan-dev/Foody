using Front.Models;
using Front.Services;
using Microsoft.AspNetCore.Mvc;

namespace Front.Controllers
{
    /// <summary>
    /// مسؤول بس عن عرض المنتجات - مو عن الصفحات الثانية (About, Blog, Contact...).
    /// هاد الفصل هو تطبيق لمبدأ Single Responsibility Principle.
    /// </summary>
    public class ProductsController : Controller
    {
        private readonly IProductCatalogService _catalog;

        public ProductsController(IProductCatalogService catalog)
        {
            _catalog = catalog;
        }

        // GET: /Products
        public async Task<IActionResult> Index()
        {
            var allProducts = await _catalog.GetAllAsync();
            return View(allProducts);
        }
    }
}
