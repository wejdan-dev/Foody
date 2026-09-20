using Front.Models;
using Front.Services;
using Front.ViewModels.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Front.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private readonly IProductCatalogService _catalog;
        private readonly IProjectImageService _projectImages;

        public ProductsController(IProductCatalogService catalog, IProjectImageService projectImages)
        {
            _catalog = catalog;
            _projectImages = projectImages;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _catalog.GetAllAsync();
            return View(products);
        }

        public IActionResult Create()
        {
            var model = new ProductFormViewModel();
            PopulateImageOptions(model);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                PopulateImageOptions(model);
                return View(model);
            }

            await _catalog.AddAsync(new Product
            {
                Name = model.Name,
                Price = model.Price,
                ImageFileName = model.ImageFileName,
                Category = model.Category,
                StockQuantity = model.StockQuantity
            });

            TempData["AdminMessage"] = $"Product '{model.Name}' was added successfully.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var product = await _catalog.GetByIdAsync(id);
            if (product is null)
            {
                return NotFound();
            }

            var model = new ProductFormViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                ImageFileName = product.ImageFileName,
                Category = product.Category,
                StockQuantity = product.StockQuantity
            };

            PopulateImageOptions(model);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductFormViewModel model)
        {
            if (!ModelState.IsValid || model.Id is null)
            {
                PopulateImageOptions(model);
                return View(model);
            }

            var updated = await _catalog.UpdateAsync(new Product
            {
                Id = model.Id.Value,
                Name = model.Name,
                Price = model.Price,
                ImageFileName = model.ImageFileName,
                Category = model.Category,
                StockQuantity = model.StockQuantity
            });

            if (!updated)
            {
                return NotFound();
            }

            TempData["AdminMessage"] = $"Product '{model.Name}' was updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _catalog.DeleteAsync(id);
            TempData["AdminMessage"] = "Product was deleted.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Restock(int id, int quantity)
        {
            if (quantity > 0)
            {
                await _catalog.IncreaseStockAsync(id, quantity);
                TempData["AdminMessage"] = $"Added {quantity} units to stock.";
            }

            return RedirectToAction(nameof(Index));
        }

        private void PopulateImageOptions(ProductFormViewModel model)
        {
            model.AvailableImageFileNames = _projectImages.GetAvailableImageFileNames(model.ImageFileName).ToList();
        }
    }
}
