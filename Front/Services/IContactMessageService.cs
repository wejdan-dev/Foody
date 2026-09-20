using Front.Models;

namespace Front.Services
{
    public interface IContactMessageService
    {
        Task<List<ContactMessage>> GetAllAsync();
        Task<ContactMessage?> GetByIdAsync(int id);
        Task<ContactMessage> AddAsync(ContactMessage message);
        Task<bool> MarkAsReadAsync(int id);
    }
}
