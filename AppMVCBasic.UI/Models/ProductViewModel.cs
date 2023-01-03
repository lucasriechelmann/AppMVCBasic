using AppMVCBasic.UI.Extensions;
using System.ComponentModel.DataAnnotations;

namespace AppMVCBasic.UI.Models
{
    public class ProductViewModel
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "The field {0} is required.")]
        [Display(Name = "Supplier")]
        public Guid SupplierId { get; set; }
        public SupplierViewModel Supplier { get; set; }
        [Required(ErrorMessage = "The field {0} is required.")]
        [StringLength(200, ErrorMessage = "The field {0} need to have between {2} and {1} characters", MinimumLength = 2)]
        public string Name { get; set; }
        [Required(ErrorMessage = "The field {0} is required.")]
        [StringLength(1000, ErrorMessage = "The field {0} need to have between {2} and {1} characters", MinimumLength = 2)]
        [Display(Name = "Description")]
        public string Description { get; set; }        
        [Display(Name = "Image")]
        public IFormFile ImageUpload { get; set; }
        
        public string Image { get; set; }
        [Currency]
        [Required(ErrorMessage = "The field {0} is required.")]
        public decimal Value { get; set; }
        [ScaffoldColumn(false)]
        public DateTime RegisteredDate { get; set; }
        [Display(Name = "Active?")]
        public bool Active { get; set; }
        public IEnumerable<SupplierViewModel> Suppliers { get; set; }
    }
}
