using System.ComponentModel.DataAnnotations;

namespace Front.Models
{
    public class BlogPost
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Blog title is required")]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Summary is required")]
        [StringLength(500)]
        public string Summary { get; set; } = string.Empty;

        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; } = string.Empty;

        [Required(ErrorMessage = "Image file name is required")]
        [StringLength(255)]
        public string ImageFileName { get; set; } = string.Empty;

        [StringLength(100)]
        public string AuthorName { get; set; } = "Foody Admin";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
    }
}
