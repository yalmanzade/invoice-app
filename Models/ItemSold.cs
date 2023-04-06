using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace invoice.Models
{
    public class ItemSold
    {
        [Key]
        public ulong Id { get; set; }
        [Required]
        [ForeignKey("InvoiceId")]
        public ulong InvoiceId { get; set; }
        public string Name { get; set; }
        [Required]
        [Precision(18, 2)]
        [Range(0.00, 100000000, ErrorMessage = "Amount must be between {1} and {2}. Remember to use valid Dollar values such as $1.99")]
        public decimal Amount { get; set; }
        [Required]
        [Range(1, 10000000, ErrorMessage = "Quantity must be between {1} and {2}.")]
        public int Quantity { get; set; }
        [NotMapped]
        public string FormatedPrice
        {
            get
            {
                var total = Amount * Quantity;
                return string.Format("{0:C}", total);
            }
        }
    }
}
