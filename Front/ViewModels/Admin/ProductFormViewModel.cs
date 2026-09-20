using System.ComponentModel.DataAnnotations;
using Front.Models;

namespace Front.ViewModels.Admin
{
    public class ProductFormViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Product name is required")]
        [StringLength(100)]
        [Display(Name = "Product Name")]
        public string Name { get; set; } = string.Empty;

        [Range(0.01, 10000, ErrorMessage = "Price must be greater than 0")]
        [Display(Name = "Price (JD)")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Please choose an image from ProjectFile/img")]
        [Display(Name = "Product Image")]
        public string ImageFileName { get; set; } = string.Empty;

        [Display(Name = "Category")]
        public ProductCategory Category { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Stock quantity can't be negative")]
        [Display(Name = "Stock Quantity")]
        public int StockQuantity { get; set; }

        public List<string> AvailableImageFileNames { get; set; } = new();
    }
}
