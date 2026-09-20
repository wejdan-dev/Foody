using Front.Models;

namespace Front.Services
{
    public interface IBlogService
    {
        Task<List<BlogPost>> GetAllAsync();
        Task<List<BlogPost>> GetLatestFirstAsync();
        Task<List<BlogPost>> GetLatestAsync(int count);
        Task<BlogPost?> GetByIdAsync(int id);
        Task<BlogPost?> GetByImageFileNameAsync(string imageFileName);
        Task<BlogPost> AddAsync(BlogPost post);
        Task<bool> UpdateAsync(BlogPost post);
        Task<bool> DeleteAsync(int id);
    }
}
