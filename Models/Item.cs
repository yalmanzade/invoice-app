using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace invoice.Models
{
    public class Item
    {
        [Key]
        public ulong Id { get; set; }
        [Required]
        [Precision(18, 2)]
        [Range(0.00, 100000000, ErrorMessage = "Amount must be between {1} and {2}.")]
        public decimal Price { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        [Display(Name = "Description (Optional)")]
        public string? Description { get; set; }
        [NotMapped]
        public string FormatedPrice { get
            {
                return Price.ToString("0.00");
            }  
        }
    }
}
