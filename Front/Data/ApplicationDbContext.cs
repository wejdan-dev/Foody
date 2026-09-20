using Front.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Front.Data
{
    /// <summary>
    /// IdentityDbContext جاهز فيه كل جداول Identity (Users, Roles, Claims...).
    /// لما نضيف جداول المنتجات/الطلبات الحقيقية بمرحلة جاية، بنضيفها هون كـ
    /// DbSet جديد بدل ما نبني DbContext منفصل.
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<BlogPost> BlogPosts => Set<BlogPost>();
        public DbSet<CartItem> CartItems => Set<CartItem>();
        public DbSet<ContactMessage> ContactMessages => Set<ContactMessage>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        public DbSet<Product> Products => Set<Product>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Order>()
                .HasMany(o => o.Items)
                .WithOne()
                .HasForeignKey(oi => oi.OrderId);

            // بنحدد دقة الأرقام العشرية بوضوح لحقول الفلوس (18 رقم إجمالي،
            // 2 بعد الفاصلة) حتى ما يفرض SQL Server دقة افتراضية ممكن تقص أرقام.
            builder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasPrecision(18, 2);

            builder.Entity<OrderItem>()
                .Property(oi => oi.UnitPrice)
                .HasPrecision(18, 2);

            builder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);
        }
    }
}
