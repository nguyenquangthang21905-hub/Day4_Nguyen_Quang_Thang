using System.ComponentModel.DataAnnotations;

namespace Day4.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên danh mục.")]
        [StringLength(100)]
        [Display(Name = "Tên danh mục")]
        public string? CategoryName { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}
