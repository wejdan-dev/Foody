using Front.Models;
using Microsoft.EntityFrameworkCore;

namespace Front.Data
{
    public static class BlogSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext db)
        {
            if (await db.BlogPosts.AnyAsync())
            {
                return;
            }

            var posts = new List<BlogPost>
            {
                new()
                {
                    Title = "Organic Farming: Nourishing the Earth, Nourishing Ourselves",
                    Summary = "Discover how organic farming supports healthier food, richer soil, and a more sustainable future.",
                    Content = "Organic farming is more than just a trend; it is a return to healthier soil, better biodiversity, and food grown with care for people and the planet. By avoiding synthetic pesticides and fertilizers, organic farmers protect ecosystems while producing nutrient-dense crops.\n\nWhy does it matter? Organic methods improve soil health, reduce pollution, support animal welfare, and often create tastier produce. Farmers rely on crop rotation, composting, cover crops, and natural pest control to keep land productive over time.\n\nChoosing organic food does not only benefit our plates. It also encourages more sustainable agriculture and stronger local food systems.",
                    ImageFileName = "blog1.jpg",
                    AuthorName = "Admin",
                    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new()
                {
                    Title = "Organic Fertilizers: Nourishing the Earth, Naturally",
                    Summary = "A quick guide to compost, manure, seaweed, and other natural fertilizers that enrich the soil.",
                    Content = "Organic fertilizers are derived from natural materials such as compost, manure, seaweed, and plant residues. Instead of forcing rapid growth with harsh chemicals, they feed the soil gradually and improve its structure over time.\n\nSome of the most common options include compost, green manure, bone meal, blood meal, and vermicompost. These materials help soil retain water, support beneficial microorganisms, and provide balanced nutrition for crops.\n\nUsing organic fertilizers reduces environmental impact and supports a farming model that is both productive and sustainable.",
                    ImageFileName = "blog2.jpg",
                    AuthorName = "Admin",
                    CreatedAt = new DateTime(2025, 1, 5, 0, 0, 0, DateTimeKind.Utc)
                },
                new()
                {
                    Title = "Organic Harvesting: Reaping the Rewards of Sustainable Farming",
                    Summary = "See why careful timing, selective picking, and crop diversity make organic harvesting special.",
                    Content = "Harvesting is one of the most rewarding moments in farming, and in organic agriculture it reflects months of careful planning and natural growing methods. Organic farmers often watch their crops closely to harvest them at peak freshness and flavor.\n\nOrganic harvesting frequently involves more manual care, more selective picking, and more respect for plant cycles. Because organic farms usually grow a greater diversity of produce, harvesting seasons can be more dynamic and resilient.\n\nThe result is fresh food, grown responsibly, with strong flavor and a smaller environmental footprint.",
                    ImageFileName = "blog3.jpg",
                    AuthorName = "Admin",
                    CreatedAt = new DateTime(2025, 1, 11, 0, 0, 0, DateTimeKind.Utc)
                }
            };

            db.BlogPosts.AddRange(posts);
            await db.SaveChangesAsync();
        }
    }
}
