using Microsoft.AspNetCore.Identity;

namespace Front.Models
{
    /// <summary>
    /// يوسّع المستخدم الجاهز من ASP.NET Core Identity (اللي بيتكفل بكلمة السر،
    /// التشفير، والتحقق من البريد تلقائياً) بخصائص إضافية خاصة بموقعنا.
    /// ما لازم نبني نظام مصادقة من الصفر - Identity مبني ومختبر وآمن أصلاً.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
    }
}
