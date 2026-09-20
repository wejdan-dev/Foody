using System.ComponentModel.DataAnnotations;

namespace Front.ViewModels.Admin
{
    public class BlogPostFormViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Blog title is required")]
        [StringLength(200)]
        [Display(Name = "Title")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Summary is required")]
        [StringLength(500)]
        [Display(Name = "Summary")]
        public string Summary { get; set; } = string.Empty;

        [Required(ErrorMessage = "Content is required")]
        [Display(Name = "Content")]
        public string Content { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please choose an image from ProjectFile/img")]
        [StringLength(255)]
        [Display(Name = "Image")]
        public string ImageFileName { get; set; } = string.Empty;

        [StringLength(100)]
        [Display(Name = "Author Name")]
        public string AuthorName { get; set; } = "Foody Admin";

        public List<string> AvailableImageFileNames { get; set; } = new();
    }
}
