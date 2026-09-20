using Front.Services;
using Microsoft.AspNetCore.Mvc;

namespace Front.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<IActionResult> Index()
        {
            var posts = await _blogService.GetLatestFirstAsync();
            return View(posts);
        }

        public async Task<IActionResult> Details(int id)
        {
            var post = await _blogService.GetByIdAsync(id);
            if (post is null)
            {
                return NotFound();
            }

            return View(post);
        }
    }
}
