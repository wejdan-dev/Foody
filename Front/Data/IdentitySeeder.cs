using Front.Models;
using Microsoft.AspNetCore.Identity;

namespace Front.Data
{
    /// <summary>
    /// بيتأكد إن أدوار "Admin" و"Customer" موجودة بقاعدة البيانات، وبينشئ
    /// حساب أدمن افتراضي أول مرة بس لو ما كان موجود مسبقاً.
    /// بينادى عليه مرة وحدة عند تشغيل التطبيق (بـ Program.cs).
    /// </summary>
    public static class IdentitySeeder
    {
        private const string AdminEmail = "admin@foody.local";
        private const string AdminPassword = "Admin@12345"; // غيّرها بأول تسجيل دخول!

        public static async Task SeedAsync(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

            foreach (var roleName in new[] { "Admin", "Customer" })
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            var existingAdmin = await userManager.FindByEmailAsync(AdminEmail);
            if (existingAdmin is null)
            {
                var admin = new ApplicationUser
                {
                    UserName = AdminEmail,
                    Email = AdminEmail,
                    FullName = "Foody Admin",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(admin, AdminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }
    }
}
