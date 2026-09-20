using Front.Data;
using Front.Models;
using Microsoft.EntityFrameworkCore;

namespace Front.Services
{
    public class BlogService : IBlogService
    {
        private readonly ApplicationDbContext _db;

        public BlogService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<BlogPost>> GetAllAsync()
        {
            return await _db.BlogPosts
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<BlogPost>> GetLatestFirstAsync()
        {
            return await GetAllAsync();
        }

        public async Task<List<BlogPost>> GetLatestAsync(int count)
        {
            return await _db.BlogPosts
                .OrderByDescending(p => p.CreatedAt)
                .Take(count)
                .ToListAsync();
        }

        public async Task<BlogPost?> GetByIdAsync(int id)
        {
            return await _db.BlogPosts.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<BlogPost?> GetByImageFileNameAsync(string imageFileName)
        {
            return await _db.BlogPosts.FirstOrDefaultAsync(p => p.ImageFileName == imageFileName);
        }

        public async Task<BlogPost> AddAsync(BlogPost post)
        {
            post.CreatedAt = DateTime.UtcNow;
            post.UpdatedAt = null;
            _db.BlogPosts.Add(post);
            await _db.SaveChangesAsync();
            return post;
        }

        public async Task<bool> UpdateAsync(BlogPost post)
        {
            var existing = await _db.BlogPosts.FirstOrDefaultAsync(p => p.Id == post.Id);
            if (existing is null)
            {
                return false;
            }

            existing.Title = post.Title;
            existing.Summary = post.Summary;
            existing.Content = post.Content;
            existing.ImageFileName = post.ImageFileName;
            existing.AuthorName = post.AuthorName;
            existing.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _db.BlogPosts.FirstOrDefaultAsync(p => p.Id == id);
            if (existing is null)
            {
                return false;
            }

            _db.BlogPosts.Remove(existing);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
