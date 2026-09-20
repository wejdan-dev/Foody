using System.ComponentModel.DataAnnotations;

namespace Front.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }

        /// <summary>
        /// وين نرجّع المستخدم بعد ما يسجل دخول (مثلاً لو كان جاي من صفحة محمية).
        /// </summary>
        public string? ReturnUrl { get; set; }
    }
}
