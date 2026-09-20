using Front.Data;
using Front.Models;
using Microsoft.EntityFrameworkCore;

namespace Front.Services
{
    public class ContactMessageService : IContactMessageService
    {
        private readonly ApplicationDbContext _db;

        public ContactMessageService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<ContactMessage>> GetAllAsync()
        {
            return await _db.ContactMessages
                .OrderByDescending(m => m.CreatedAt)
                .ToListAsync();
        }

        public async Task<ContactMessage?> GetByIdAsync(int id)
        {
            return await _db.ContactMessages.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<ContactMessage> AddAsync(ContactMessage message)
        {
            _db.ContactMessages.Add(message);
            await _db.SaveChangesAsync();
            return message;
        }

        public async Task<bool> MarkAsReadAsync(int id)
        {
            var message = await _db.ContactMessages.FirstOrDefaultAsync(m => m.Id == id);
            if (message is null)
            {
                return false;
            }

            if (!message.IsRead)
            {
                message.IsRead = true;
                await _db.SaveChangesAsync();
            }

            return true;
        }
    }
}
