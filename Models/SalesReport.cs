using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace invoice.Models
{
    public class SalesReport
    {
        [Key]
        public ulong Id { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [Required]
        public int ItemsSold { get; set; } = 0;
        [Required]
        public string TotalSales { get; set; } = string.Empty;
        [Required]
        public string BestSellingItem { get; set; } = string.Empty;
        [Required]
        public string BestCompanyJson { get; set; } = string.Empty;
        [Required]
        public int InvoicesGenerated { get; set; } = 0;
        [Required]
        public string LastInvoiceJson { get; set; } = string.Empty;
        [NotMapped]
        public Company BestCompany { get; set; } = new Company();
        [NotMapped]
        public List<Company> Companies { get; set; } = new List<Company>();
        [NotMapped]
        public Invoice LastInvoice { get; set; } = new Invoice();
    }
}
