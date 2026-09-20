using Front.Models;
using Front.Services;
using Front.ViewModels.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Front.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BlogPostsController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly IProjectImageService _projectImages;

        public BlogPostsController(IBlogService blogService, IProjectImageService projectImages)
        {
            _blogService = blogService;
            _projectImages = projectImages;
        }

        public async Task<IActionResult> Index()
        {
            var posts = await _blogService.GetAllAsync();
            return View(posts);
        }

        public IActionResult Create()
        {
            var model = new BlogPostFormViewModel();
            PopulateImageOptions(model);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogPostFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                PopulateImageOptions(model);
                return View(model);
            }

            await _blogService.AddAsync(new BlogPost
            {
                Title = model.Title,
                Summary = model.Summary,
                Content = model.Content,
                ImageFileName = model.ImageFileName,
                AuthorName = model.AuthorName
            });

            TempData["AdminMessage"] = $"Blog '{model.Title}' was added successfully.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var post = await _blogService.GetByIdAsync(id);
            if (post is null)
            {
                return NotFound();
            }

            var model = new BlogPostFormViewModel
            {
                Id = post.Id,
                Title = post.Title,
                Summary = post.Summary,
                Content = post.Content,
                ImageFileName = post.ImageFileName,
                AuthorName = post.AuthorName
            };

            PopulateImageOptions(model);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BlogPostFormViewModel model)
        {
            if (!ModelState.IsValid || model.Id is null)
            {
                PopulateImageOptions(model);
                return View(model);
            }

            var updated = await _blogService.UpdateAsync(new BlogPost
            {
                Id = model.Id.Value,
                Title = model.Title,
                Summary = model.Summary,
                Content = model.Content,
                ImageFileName = model.ImageFileName,
                AuthorName = model.AuthorName
            });

            if (!updated)
            {
                return NotFound();
            }

            TempData["AdminMessage"] = $"Blog '{model.Title}' was updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _blogService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            TempData["AdminMessage"] = "Blog was deleted.";
            return RedirectToAction(nameof(Index));
        }

        private void PopulateImageOptions(BlogPostFormViewModel model)
        {
            model.AvailableImageFileNames = _projectImages.GetAvailableImageFileNames(model.ImageFileName).ToList();
        }
    }
}
