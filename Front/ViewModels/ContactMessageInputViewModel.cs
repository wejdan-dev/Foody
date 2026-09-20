using System.ComponentModel.DataAnnotations;

namespace Front.ViewModels
{
    public class ContactMessageInputViewModel
    {
        [Required(ErrorMessage = "Your name is required")]
        [StringLength(100)]
        [Display(Name = "Your Name")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Your email is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [StringLength(200)]
        [Display(Name = "Your Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Subject is required")]
        [StringLength(150)]
        [Display(Name = "Subject")]
        public string Subject { get; set; } = string.Empty;

        [Required(ErrorMessage = "Message is required")]
        [StringLength(4000)]
        [Display(Name = "Message")]
        public string Message { get; set; } = string.Empty;
    }
}
