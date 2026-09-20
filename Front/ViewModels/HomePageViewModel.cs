using Front.Models;

namespace Front.ViewModels
{
    public class HomePageViewModel
    {
        public List<BlogPost> LatestBlogPosts { get; set; } = new();
    }
}
