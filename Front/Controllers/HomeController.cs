using System.Diagnostics;
using Front.Models;
using Front.Services;
using Front.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Front.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IContactMessageService _contactMessages;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IBlogService _blogService;

        public HomeController(
            ILogger<HomeController> logger,
            IContactMessageService contactMessages,
            UserManager<ApplicationUser> userManager,
            IBlogService blogService)
        {
            _logger = logger;
            _contactMessages = contactMessages;
            _userManager = userManager;
            _blogService = blogService;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Homepage()
        {
            var model = new HomePageViewModel
            {
                LatestBlogPosts = await _blogService.GetLatestAsync(3)
            };

            return View(model);
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public async Task<IActionResult> Blog1()
        {
            var post = await _blogService.GetByImageFileNameAsync("blog1.jpg");
            return post is null
                ? RedirectToAction("Index", "Blog")
                : RedirectToAction("Details", "Blog", new { id = post.Id });
        }

        public async Task<IActionResult> Blog2()
        {
            var post = await _blogService.GetByImageFileNameAsync("blog2.jpg");
            return post is null
                ? RedirectToAction("Index", "Blog")
                : RedirectToAction("Details", "Blog", new { id = post.Id });
        }

        public async Task<IActionResult> Blog3()
        {
            var post = await _blogService.GetByImageFileNameAsync("blog3.jpg");
            return post is null
                ? RedirectToAction("Index", "Blog")
                : RedirectToAction("Details", "Blog", new { id = post.Id });
        }

        public IActionResult Services()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ContactUs()
        {
            var model = new ContactMessageInputViewModel();

            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user is not null)
                {
                    model.Name = string.IsNullOrWhiteSpace(user.FullName) ? user.UserName ?? string.Empty : user.FullName;
                    model.Email = user.Email ?? string.Empty;
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ContactUs(ContactMessageInputViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _contactMessages.AddAsync(new ContactMessage
            {
                Name = model.Name,
                Email = model.Email,
                Subject = model.Subject,
                Message = model.Message,
                CreatedAt = DateTime.UtcNow,
                IsRead = false
            });

            _logger.LogInformation("A new contact message was submitted by {Email}.", model.Email);
            TempData["ContactSuccess"] = "Your message was sent successfully. We will get back to you soon.";
            return RedirectToAction(nameof(ContactUs));
        }

        public IActionResult BlogGrid()
        {
            return RedirectToAction("Index", "Blog");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
