using Front.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Front.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ContactMessagesController : Controller
    {
        private readonly IContactMessageService _contactMessages;

        public ContactMessagesController(IContactMessageService contactMessages)
        {
            _contactMessages = contactMessages;
        }

        public async Task<IActionResult> Index()
        {
            var messages = await _contactMessages.GetAllAsync();
            return View(messages);
        }

        public async Task<IActionResult> Details(int id)
        {
            var message = await _contactMessages.GetByIdAsync(id);
            if (message is null)
            {
                return NotFound();
            }

            return View(message);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var updated = await _contactMessages.MarkAsReadAsync(id);
            if (!updated)
            {
                return NotFound();
            }

            TempData["AdminMessage"] = "Message marked as read.";
            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
