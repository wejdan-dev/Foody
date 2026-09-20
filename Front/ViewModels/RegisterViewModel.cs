using System.ComponentModel.DataAnnotations;

namespace Front.ViewModels
{
    /// <summary>
    /// الفرق بين هاد وبين ApplicationUser: هاد بس شكل نموذج التسجيل اللي
    /// بيملاه المستخدم بالواجهة (فيه تأكيد كلمة السر مثلاً، وهاد مش موجود
    /// بجدول المستخدمين أصلاً). فصل الـ ViewModel عن الـ Model الحقيقي
    /// ممارسة أساسية بالـ Clean Code.
    /// </summary>
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Full name is required")]
        [StringLength(100)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please confirm your password")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
